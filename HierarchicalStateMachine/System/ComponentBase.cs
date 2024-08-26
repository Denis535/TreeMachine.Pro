namespace System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public abstract class ComponentBase {
    public enum State_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    // State
    public State_ State { get; internal set; } = State_.Inactive;
    // Owner
    internal object? Owner { get; set; }

    // System
    public ComponentSystemBase? System => Owner as ComponentSystemBase;

    // Parent
    public ComponentBase? Parent => Owner as ComponentBase;
    // Ancestors
    public IEnumerable<ComponentBase> Ancestors {
        get {
            if (Parent != null) {
                yield return Parent;
                foreach (var i in Parent.Ancestors) yield return i;
            }
        }
    }
    public IEnumerable<ComponentBase> AncestorsAndSelf => Ancestors.Prepend( this );
    // Root
    [MemberNotNullWhen( false, "Parent" )] public bool IsRoot => Parent == null;
    public ComponentBase Root => IsRoot ? this : Parent.Root;

    // Children
    private List<ComponentBase> Children_ { get; } = new List<ComponentBase>( 0 );
    public IReadOnlyList<ComponentBase> Children => Children_;
    // Descendants
    public IEnumerable<ComponentBase> Descendants {
        get {
            foreach (var child in Children) {
                yield return child;
                foreach (var i in child.Descendants) yield return i;
            }
        }
    }
    public IEnumerable<ComponentBase> DescendantsAndSelf => Descendants.Prepend( this );

    // OnActivate
    public Action<object?>? OnBeforeActivateEvent;
    public Action<object?>? OnAfterActivateEvent;
    public Action<object?>? OnBeforeDeactivateEvent;
    public Action<object?>? OnAfterDeactivateEvent;
    // OnDescendantActivate
    public event Action<ComponentBase, object?>? OnBeforeDescendantActivateEvent;
    public event Action<ComponentBase, object?>? OnAfterDescendantActivateEvent;
    public event Action<ComponentBase, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<ComponentBase, object?>? OnAfterDescendantDeactivateEvent;

    // Constructor
    public ComponentBase() {
    }

    // Activate
    internal void Activate(object? argument) {
        Assert.Operation.Message( $"Component {this} must be inactive" ).Valid( State is State_.Inactive );
        foreach (var ancestor in Ancestors.Reverse()) {
            ancestor.OnBeforeDescendantActivateEvent?.Invoke( this, argument );
            ancestor.OnBeforeDescendantActivate( this, argument );
        }
        OnBeforeActivateEvent?.Invoke( argument );
        OnBeforeActivate( argument );
        {
            State = State_.Activating;
            OnActivate( argument );
            foreach (var child in Children) {
                child.Activate( argument );
            }
            State = State_.Active;
        }
        OnAfterActivate( argument );
        OnAfterActivateEvent?.Invoke( argument );
        foreach (var ancestor in Ancestors) {
            ancestor.OnAfterDescendantActivate( this, argument );
            ancestor.OnAfterDescendantActivateEvent?.Invoke( this, argument );
        }
    }
    internal void Deactivate(object? argument) {
        Assert.Operation.Message( $"Component {this} must be active" ).Valid( State is State_.Active );
        foreach (var ancestor in Ancestors.Reverse()) {
            ancestor.OnBeforeDescendantDeactivateEvent?.Invoke( this, argument );
            ancestor.OnBeforeDescendantDeactivate( this, argument );
        }
        OnBeforeDeactivateEvent?.Invoke( argument );
        OnBeforeDeactivate( argument );
        {
            State = State_.Deactivating;
            foreach (var child in Children.Reverse()) {
                child.Deactivate( argument );
            }
            OnDeactivate( argument );
            State = State_.Inactive;
        }
        OnAfterDeactivate( argument );
        OnAfterDeactivateEvent?.Invoke( argument );
        foreach (var ancestor in Ancestors) {
            ancestor.OnAfterDescendantDeactivate( this, argument );
            ancestor.OnAfterDescendantDeactivateEvent?.Invoke( this, argument );
        }
    }

    // OnActivate
    protected virtual void OnBeforeActivate(object? argument) {
    }
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument) {
    }
    protected virtual void OnBeforeDeactivate(object? argument) {
    }
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument) {
    }

    // OnDescendantActivate
    protected abstract void OnBeforeDescendantActivate(ComponentBase descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(ComponentBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(ComponentBase descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(ComponentBase descendant, object? argument);

    // AddChild
    protected virtual void AddChild(ComponentBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.State is State_.Inactive );
        Assert.Operation.Message( $"Component {this} must have no child {child} component" ).Valid( !Children.Contains( child ) );
        {
            Children_.Add( child );
            child.Owner = this;
        }
        if (State is State_.Active) {
            child.Activate( argument );
        } else {
            Assert.Argument.Message( $"Argument 'argument' ({argument}) must be null" ).Valid( argument == null );
        }
    }
    protected virtual void RemoveChild(ComponentBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be active" ).Valid( child.State is State_.Active );
        Assert.Operation.Message( $"Component {this} must have child {child} component" ).Valid( Children.Contains( child ) );
        if (State is State_.Active) {
            child.Deactivate( argument );
        } else {
            Assert.Argument.Message( $"Argument 'argument' ({argument}) must be null" ).Valid( argument == null );
        }
        {
            child.Owner = null;
            Children_.Remove( child );
        }
    }
    protected bool RemoveChild(Func<ComponentBase, bool> predicate, object? argument = null) {
        var child = Children.LastOrDefault( predicate );
        if (child != null) {
            RemoveChild( child, argument );
            return true;
        }
        return false;
    }
    protected void RemoveChildren(IEnumerable<ComponentBase> children, object? argument = null) {
        foreach (var child in children) {
            RemoveChild( child, argument );
        }
    }
    protected int RemoveChildren(Func<ComponentBase, bool> predicate, object? argument = null) {
        var children = Children.Where( predicate ).Reverse().ToList();
        if (children.Any()) {
            RemoveChildren( children, argument );
            return children.Count;
        }
        return 0;
    }
    protected void RemoveSelf(object? argument = null) {
        Assert.Operation.Message( $"Component {this} must have owner" ).Valid( Owner != null );
        if (Owner is ComponentBase parent) {
            parent.RemoveChild( this, argument );
        } else {
            ((ComponentSystemBase) Owner).RemoveRootComponent( this, argument );
        }
    }

}

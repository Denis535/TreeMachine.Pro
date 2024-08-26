namespace System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public abstract class HierarchicalStateBase : IDisposable {
    public enum State_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    // System
    public bool IsDisposed { get; private set; }
    protected virtual bool DisposeWhenDeactivate => true;
    // Owner
    internal object? Owner { get; set; }
    // State
    public State_ State { get; private set; } = State_.Inactive;
    // StateMachine
    protected HierarchicalStateMachineBase? StateMachine => Owner as HierarchicalStateMachineBase;
    // Root
    [MemberNotNullWhen( false, "Parent" )] public bool IsRoot => Parent == null;
    public HierarchicalStateBase Root => IsRoot ? this : Parent.Root;
    // Parent
    public HierarchicalStateBase? Parent => Owner as HierarchicalStateBase;
    // Ancestors
    public IEnumerable<HierarchicalStateBase> Ancestors {
        get {
            if (Parent != null) {
                yield return Parent;
                foreach (var i in Parent.Ancestors) yield return i;
            }
        }
    }
    public IEnumerable<HierarchicalStateBase> AncestorsAndSelf => Ancestors.Prepend( this );
    // Children
    public IReadOnlyList<HierarchicalStateBase> Children => Children_;
    private List<HierarchicalStateBase> Children_ { get; } = new List<HierarchicalStateBase>( 0 );
    // Descendants
    public IEnumerable<HierarchicalStateBase> Descendants {
        get {
            foreach (var child in Children) {
                yield return child;
                foreach (var i in child.Descendants) yield return i;
            }
        }
    }
    public IEnumerable<HierarchicalStateBase> DescendantsAndSelf => Descendants.Prepend( this );
    // OnActivate
    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;
    // OnDescendantActivate
    public event Action<HierarchicalStateBase, object?>? OnBeforeDescendantActivateEvent;
    public event Action<HierarchicalStateBase, object?>? OnAfterDescendantActivateEvent;
    public event Action<HierarchicalStateBase, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<HierarchicalStateBase, object?>? OnAfterDescendantDeactivateEvent;

    // Constructor
    public HierarchicalStateBase() {
    }
    public virtual void Dispose() {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be inactive" ).Valid( State is State_.Inactive );
        foreach (var child in Children) {
            Assert.Operation.Message( $"Child {child} must be disposed" ).Valid( child.IsDisposed );
        }
        IsDisposed = true;
    }

    // Activate
    internal void Activate(object? argument) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be inactive" ).Valid( State is State_.Inactive );
        foreach (var ancestor in Ancestors.Reverse()) {
            ancestor.OnBeforeDescendantActivateEvent?.Invoke( this, argument );
            ancestor.OnBeforeDescendantActivate( this, argument );
        }
        {
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
        }
        foreach (var ancestor in Ancestors) {
            ancestor.OnAfterDescendantActivate( this, argument );
            ancestor.OnAfterDescendantActivateEvent?.Invoke( this, argument );
        }
    }
    internal void Deactivate(object? argument) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be active" ).Valid( State is State_.Active );
        foreach (var ancestor in Ancestors.Reverse()) {
            ancestor.OnBeforeDescendantDeactivateEvent?.Invoke( this, argument );
            ancestor.OnBeforeDescendantDeactivate( this, argument );
        }
        {
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
        }
        foreach (var ancestor in Ancestors) {
            ancestor.OnAfterDescendantDeactivate( this, argument );
            ancestor.OnAfterDescendantDeactivateEvent?.Invoke( this, argument );
        }
        if (DisposeWhenDeactivate) {
            Dispose();
        }
    }

    // OnActivate
    protected virtual void OnBeforeActivate(object? argument) {
    }
    protected abstract void OnActivate(object? argument); // override to init and show self
    protected virtual void OnAfterActivate(object? argument) {
    }
    protected virtual void OnBeforeDeactivate(object? argument) {
    }
    protected abstract void OnDeactivate(object? argument); // override to hide self and deinit
    protected virtual void OnAfterDeactivate(object? argument) {
    }

    // OnDescendantActivate
    protected abstract void OnBeforeDescendantActivate(HierarchicalStateBase descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(HierarchicalStateBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(HierarchicalStateBase descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(HierarchicalStateBase descendant, object? argument);

    // AddChild
    protected virtual void AddChild(HierarchicalStateBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be non-disposed" ).Valid( !child.IsDisposed );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.State is State_.Inactive );
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must have no child {child} state" ).Valid( !Children.Contains( child ) );
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
    protected virtual void RemoveChild(HierarchicalStateBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be non-disposed" ).Valid( !child.IsDisposed );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be active" ).Valid( child.State is State_.Active );
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must have child {child} state" ).Valid( Children.Contains( child ) );
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
    protected bool RemoveChild(Func<HierarchicalStateBase, bool> predicate, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        var child = Children.LastOrDefault( predicate );
        if (child != null) {
            RemoveChild( child, argument );
            return true;
        }
        return false;
    }
    protected void RemoveChildren(IEnumerable<HierarchicalStateBase> children, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        foreach (var child in children) {
            RemoveChild( child, argument );
        }
    }
    protected int RemoveChildren(Func<HierarchicalStateBase, bool> predicate, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        var children = Children.Where( predicate ).Reverse().ToList();
        if (children.Any()) {
            RemoveChildren( children, argument );
            return children.Count;
        }
        return 0;
    }
    protected void RemoveSelf(object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must have parent or state-machine" ).Valid( Parent != null || StateMachine != null );
        if (Parent != null) {
            Parent.RemoveChild( this, argument );
        } else {
            StateMachine!.RemoveState( this, argument );
        }
    }

}

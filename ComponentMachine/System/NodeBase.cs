namespace System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public abstract class NodeBase {
    public enum State_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    // State
    public State_ State { get; private set; } = State_.Inactive;

    // Owner
    private object? Owner { get; set; }
    // Hierarchy
    public HierarchyBase? Hierarchy => Owner as HierarchyBase;
    // Parent
    public NodeBase? Parent => Owner as NodeBase;
    // Root
    [MemberNotNullWhen( false, "Parent" )] public bool IsRoot => Parent == null;
    public NodeBase Root => IsRoot ? this : Parent.Root;
    // Ancestors
    public IEnumerable<NodeBase> Ancestors {
        get {
            if (Parent != null) {
                yield return Parent;
                foreach (var i in Parent.Ancestors) yield return i;
            }
        }
    }
    public IEnumerable<NodeBase> AncestorsAndSelf => Ancestors.Prepend( this );

    // Children
    private List<NodeBase> Children_ { get; } = new List<NodeBase>( 0 );
    public IReadOnlyList<NodeBase> Children => Children_;
    // Descendants
    public IEnumerable<NodeBase> Descendants {
        get {
            foreach (var child in Children) {
                yield return child;
                foreach (var i in child.Descendants) yield return i;
            }
        }
    }
    public IEnumerable<NodeBase> DescendantsAndSelf => Descendants.Prepend( this );

    // OnActivate
    public Action<object?>? OnBeforeActivateEvent;
    public Action<object?>? OnAfterActivateEvent;
    public Action<object?>? OnBeforeDeactivateEvent;
    public Action<object?>? OnAfterDeactivateEvent;
    // OnDescendantActivate
    public event Action<NodeBase, object?>? OnBeforeDescendantActivateEvent;
    public event Action<NodeBase, object?>? OnAfterDescendantActivateEvent;
    public event Action<NodeBase, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<NodeBase, object?>? OnAfterDescendantDeactivateEvent;

    // Constructor
    public NodeBase() {
    }

    // Activate
    internal void Activate(HierarchyBase owner, object? argument) {
        Owner = owner;
        Activate( argument );
    }
    internal void Deactivate(HierarchyBase owner, object? argument) {
        Assert.Argument.Message( $"Argument 'owner' ({owner}) must be valid" ).Valid( owner == Owner );
        Deactivate( argument );
        Owner = null;
    }

    // Activate
    internal void Activate(NodeBase owner, object? argument) {
        if (owner.State is State_.Active) {
            Owner = owner;
            Activate( argument );
        } else {
            Assert.Argument.Message( $"Argument 'argument' ({argument}) must be null" ).Valid( argument == null );
            Owner = owner;
        }
    }
    internal void Deactivate(NodeBase owner, object? argument) {
        Assert.Argument.Message( $"Argument 'owner' ({owner}) must be valid" ).Valid( owner == Owner );
        if (owner.State is State_.Active) {
            Deactivate( argument );
            Owner = null;
        } else {
            Assert.Argument.Message( $"Argument 'argument' ({argument}) must be null" ).Valid( argument == null );
            Owner = null;
        }
    }

    // Activate
    private void Activate(object? argument) {
        Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( State is State_.Inactive );
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
    private void Deactivate(object? argument) {
        Assert.Operation.Message( $"Node {this} must be active" ).Valid( State is State_.Active );
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
    protected abstract void OnBeforeDescendantActivate(NodeBase descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(NodeBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(NodeBase descendant, object? argument);

    // AddChild
    protected virtual void AddChild(NodeBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Operation.Message( $"Node {this} must have no child {child} node" ).Valid( !Children.Contains( child ) );
        Children_.Add( child );
        child.Activate( this, argument );
    }
    protected virtual void RemoveChild(NodeBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Operation.Message( $"Node {this} must have child {child} node" ).Valid( Children.Contains( child ) );
        child.Deactivate( this, argument );
        Children_.Remove( child );
    }
    protected bool RemoveChild(Func<NodeBase, bool> predicate, object? argument = null) {
        var child = Children.LastOrDefault( predicate );
        if (child != null) {
            RemoveChild( child, argument );
            return true;
        }
        return false;
    }
    protected void RemoveChildren(IEnumerable<NodeBase> children, object? argument = null) {
        foreach (var child in children) {
            RemoveChild( child, argument );
        }
    }
    protected int RemoveChildren(Func<NodeBase, bool> predicate, object? argument = null) {
        var children = Children.Where( predicate ).Reverse().ToList();
        if (children.Any()) {
            RemoveChildren( children, argument );
            return children.Count;
        }
        return 0;
    }
    protected void RemoveSelf(object? argument = null) {
        Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
        if (Owner is NodeBase parent) {
            parent.RemoveChild( this, argument );
        } else {
            ((HierarchyBase) Owner).RemoveRoot( this, argument );
        }
    }

}

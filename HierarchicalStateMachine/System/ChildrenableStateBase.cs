namespace System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public abstract class ChildrenableStateBase : StateBase {

    // Children
    private List<StateBase> Children_ { get; } = new List<StateBase>( 0 );
    public IReadOnlyList<StateBase> Children => Children_;
    // Descendants
    public IEnumerable<StateBase> Descendants {
        get {
            foreach (var child in Children) {
                yield return child;
                if (child is ChildrenableStateBase child_) {
                    foreach (var i in child_.Descendants) yield return i;
                }
            }
        }
    }
    public IEnumerable<StateBase> DescendantsAndSelf => Descendants.Prepend( this );
    // OnDescendantActivate
    public event Action<StateBase, object?>? OnBeforeDescendantActivateEvent;
    public event Action<StateBase, object?>? OnAfterDescendantActivateEvent;
    public event Action<StateBase, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<StateBase, object?>? OnAfterDescendantDeactivateEvent;

    // Constructor
    public ChildrenableStateBase() {
    }
    public override void Dispose() {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be inactive" ).Valid( State is State_.Inactive );
        foreach (var child in Children) {
            Assert.Operation.Message( $"Child {child} must be disposed" ).Valid( child.IsDisposed );
        }
        IsDisposed = true;
    }

    // Activate
    internal override void Activate(object? argument) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be inactive" ).Valid( State is State_.Inactive );
        foreach (var ancestor in Ancestors.OfType<ChildrenableStateBase>().Reverse()) {
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
        foreach (var ancestor in Ancestors.OfType<ChildrenableStateBase>()) {
            ancestor.OnAfterDescendantActivate( this, argument );
            ancestor.OnAfterDescendantActivateEvent?.Invoke( this, argument );
        }
    }
    internal override void Deactivate(object? argument) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be active" ).Valid( State is State_.Active );
        foreach (var ancestor in Ancestors.OfType<ChildrenableStateBase>().Reverse()) {
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
        foreach (var ancestor in Ancestors.OfType<ChildrenableStateBase>()) {
            ancestor.OnAfterDescendantDeactivate( this, argument );
            ancestor.OnAfterDescendantDeactivateEvent?.Invoke( this, argument );
        }
    }

    // OnDescendantActivate
    protected abstract void OnBeforeDescendantActivate(StateBase descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(StateBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(StateBase descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(StateBase descendant, object? argument);

    // AddChild
    protected internal virtual void AddChild(StateBase child, object? argument = null) {
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
    protected internal virtual void RemoveChild(StateBase child, object? argument = null) {
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
    protected internal bool RemoveChild(Func<StateBase, bool> predicate, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        var child = Children.LastOrDefault( predicate );
        if (child != null) {
            RemoveChild( child, argument );
            return true;
        }
        return false;
    }
    protected internal void RemoveChildren(IEnumerable<StateBase> children, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        foreach (var child in children) {
            RemoveChild( child, argument );
        }
    }
    protected internal int RemoveChildren(Func<StateBase, bool> predicate, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        var children = Children.Where( predicate ).Reverse().ToList();
        if (children.Any()) {
            RemoveChildren( children, argument );
            return children.Count;
        }
        return 0;
    }

}

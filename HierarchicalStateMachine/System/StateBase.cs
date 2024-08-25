namespace System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public abstract class StateBase : IDisposable {
    public enum State_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    // System
    public bool IsDisposed { get; private set; }
    protected virtual bool DisposeWhenDeactivate => true;
    // State
    public State_ State { get; private set; } = State_.Inactive;
    protected StateMachineBase? StateMachine { get; private set; }
    // Root
    [MemberNotNullWhen( false, "Parent" )] public bool IsRoot => Parent == null;
    public StateBase Root => IsRoot ? this : Parent.Root;
    // Parent
    public StateBase? Parent { get; private set; }
    // Ancestors
    public IEnumerable<StateBase> Ancestors {
        get {
            if (Parent != null) {
                yield return Parent;
                foreach (var i in Parent.Ancestors) yield return i;
            }
        }
    }
    public IEnumerable<StateBase> AncestorsAndSelf => Ancestors.Prepend( this );
    // Children
    public IReadOnlyList<StateBase> Children => Children_;
    private List<StateBase> Children_ { get; } = new List<StateBase>();
    // Descendants
    public IEnumerable<StateBase> Descendants {
        get {
            foreach (var child in Children) {
                yield return child;
                foreach (var i in child.Descendants) yield return i;
            }
        }
    }
    public IEnumerable<StateBase> DescendantsAndSelf => Descendants.Prepend( this );
    // OnActivate
    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;
    // OnDescendantActivate
    public event Action<StateBase, object?>? OnBeforeDescendantActivateEvent;
    public event Action<StateBase, object?>? OnAfterDescendantActivateEvent;
    public event Action<StateBase, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<StateBase, object?>? OnAfterDescendantDeactivateEvent;

    // Constructor
    public StateBase() {
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
    internal void Activate(StateMachineBase stateMachine, object? argument) {
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
                StateMachine = stateMachine;
                OnActivate( argument );
                foreach (var child in Children) {
                    child.Activate( stateMachine, argument );
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
    internal void Deactivate(StateMachineBase stateMachine, object? argument) {
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
                    child.Deactivate( stateMachine, argument );
                }
                OnDeactivate( argument );
                StateMachine = null;
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
    protected abstract void OnBeforeDescendantActivate(StateBase descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(StateBase descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(StateBase descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(StateBase descendant, object? argument);

    // AddChild
    protected virtual void AddChild(StateBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be non-disposed" ).Valid( !child.IsDisposed );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.State is State_.Inactive );
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must have no child {child} state" ).Valid( !Children.Contains( child ) );
        if (State is State_.Active) {
            Children_.Add( child );
            child.Parent = this;
            child.Activate( StateMachine!, argument );
        } else {
            Assert.Argument.Message( $"Argument 'argument' ({argument}) must be null" ).Valid( argument == null );
            Children_.Add( child );
            child.Parent = this;
        }
    }
    protected virtual void RemoveChild(StateBase child, object? argument = null) {
        Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be non-disposed" ).Valid( !child.IsDisposed );
        Assert.Argument.Message( $"Argument 'child' ({child}) must be active" ).Valid( child.State is State_.Active );
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must have child {child} state" ).Valid( Children.Contains( child ) );
        if (State is State_.Active) {
            child.Deactivate( StateMachine!, argument );
            child.Parent = null;
            Children_.Remove( child );
        } else {
            Assert.Argument.Message( $"Argument 'argument' ({argument}) must be null" ).Valid( argument == null );
            child.Parent = null;
            Children_.Remove( child );
        }
    }
    protected bool RemoveChild(Func<StateBase, bool> predicate, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        var child = Children.LastOrDefault( predicate );
        if (child != null) {
            RemoveChild( child, argument );
            return true;
        }
        return false;
    }
    protected void RemoveChildren(IEnumerable<StateBase> children, object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        foreach (var child in children) {
            RemoveChild( child, argument );
        }
    }
    protected int RemoveChildren(Func<StateBase, bool> predicate, object? argument = null) {
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

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
    public bool IsDisposed { get; internal set; }
    // State
    public State_ State { get; internal set; } = State_.Inactive;
    // Owner
    internal object? Owner { get; set; }
    // StateMachine
    public StateMachineBase? StateMachine => Owner as StateMachineBase;
    // Root
    [MemberNotNullWhen( false, "Parent" )] public bool IsRoot => Parent == null;
    public StateBase Root => IsRoot ? this : Parent.Root;
    // Parent
    public StateBase? Parent => Owner as StateBase;
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
    // OnActivate
    public Action<object?>? OnBeforeActivateEvent;
    public Action<object?>? OnAfterActivateEvent;
    public Action<object?>? OnBeforeDeactivateEvent;
    public Action<object?>? OnAfterDeactivateEvent;

    // Constructor
    public StateBase() {
    }
    public virtual void Dispose() {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be inactive" ).Valid( State is State_.Inactive );
        IsDisposed = true;
    }

    // Activate
    internal virtual void Activate(object? argument) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be inactive" ).Valid( State is State_.Inactive );
        OnBeforeActivateEvent?.Invoke( argument );
        OnBeforeActivate( argument );
        {
            State = State_.Activating;
            OnActivate( argument );
            State = State_.Active;
        }
        OnAfterActivate( argument );
        OnAfterActivateEvent?.Invoke( argument );
    }
    internal virtual void Deactivate(object? argument) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must be active" ).Valid( State is State_.Active );
        OnBeforeDeactivateEvent?.Invoke( argument );
        OnBeforeDeactivate( argument );
        {
            State = State_.Deactivating;
            OnDeactivate( argument );
            State = State_.Inactive;
        }
        OnAfterDeactivate( argument );
        OnAfterDeactivateEvent?.Invoke( argument );
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

    // RemoveSelf
    protected void RemoveSelf(object? argument = null) {
        Assert.Operation.Message( $"State {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"State {this} must have owner" ).Valid( Owner != null );
        if (Owner is ChildrenableStateBase parent) {
            parent.RemoveChild( this, argument );
        } else {
            ((StateMachineBase) Owner).RemoveState( this, argument );
        }
    }

}

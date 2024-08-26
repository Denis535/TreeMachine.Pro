namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class HierarchicalStateMachineBase : IDisposable {

    // System
    public bool IsDisposed { get; private set; }
    // State
    protected internal HierarchicalStateBase? State { get; private set; }

    // Constructor
    public HierarchicalStateMachineBase() {
    }
    public virtual void Dispose() {
        Assert.Operation.Message( $"StateMachine {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"StateMachine {this} must have no state" ).Valid( State == null );
        IsDisposed = true;
    }

    // SetState
    protected internal virtual void SetState(HierarchicalStateBase state, object? argument = null) {
        Assert.Argument.Message( $"Argument 'state' must be non-null" ).NotNull( state != null );
        Assert.Argument.Message( $"Argument 'state' ({state}) must be non-disposed" ).Valid( !state.IsDisposed );
        Assert.Argument.Message( $"Argument 'state' ({state}) must be inactive" ).Valid( state.State is HierarchicalStateBase.State_.Inactive );
        Assert.Operation.Message( $"StateMachine {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"StateMachine {this} must have no state" ).Valid( State == null );
        {
            State = state;
            State.Owner = this;
        }
        State.Activate( argument );
    }
    protected internal virtual void RemoveState(HierarchicalStateBase state, object? argument = null) {
        Assert.Argument.Message( $"Argument 'state' must be non-null" ).NotNull( state != null );
        Assert.Argument.Message( $"Argument 'state' ({state}) must be non-disposed" ).Valid( !state.IsDisposed );
        Assert.Argument.Message( $"Argument 'state' ({state}) must be active" ).Valid( state.State is HierarchicalStateBase.State_.Active );
        Assert.Operation.Message( $"StateMachine {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"StateMachine {this} must have {state} state" ).Valid( State == state );
        RemoveState( argument );
    }
    protected internal virtual void RemoveState(object? argument = null) {
        Assert.Operation.Message( $"StateMachine {this} must be non-disposed" ).NotDisposed( !IsDisposed );
        Assert.Operation.Message( $"StateMachine {this} must have state" ).Valid( State != null );
        State.Deactivate( argument );
        {
            State.Owner = null;
            State = null;
        }
    }

}

namespace System.TreeMachine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public abstract class NodeBase3<TThis> : NodeBase2<TThis> where TThis : NodeBase3<TThis> {

    // OnDescendantActivate
    public event Action<TThis, object?>? OnBeforeDescendantActivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantActivateEvent;
    public event Action<TThis, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantDeactivateEvent;

    // Constructor
    public NodeBase3() {
    }

    // OnActivate
    protected override void OnBeforeActivate(object? argument) {
        foreach (var ancestor in Ancestors.Reverse()) {
            ancestor.OnBeforeDescendantActivateEvent?.Invoke( (TThis) this, argument );
            ancestor.OnBeforeDescendantActivate( (TThis) this, argument );
        }
        base.OnBeforeActivate( argument );
    }
    protected override void OnAfterActivate(object? argument) {
        base.OnAfterActivate( argument );
        foreach (var ancestor in Ancestors) {
            ancestor.OnAfterDescendantActivate( (TThis) this, argument );
            ancestor.OnAfterDescendantActivateEvent?.Invoke( (TThis) this, argument );
        }
    }
    protected override void OnBeforeDeactivate(object? argument) {
        foreach (var ancestor in Ancestors.Reverse()) {
            ancestor.OnBeforeDescendantDeactivateEvent?.Invoke( (TThis) this, argument );
            ancestor.OnBeforeDescendantDeactivate( (TThis) this, argument );
        }
        base.OnBeforeDeactivate( argument );
    }
    protected override void OnAfterDeactivate(object? argument) {
        base.OnAfterDeactivate( argument );
        foreach (var ancestor in Ancestors) {
            ancestor.OnAfterDescendantDeactivate( (TThis) this, argument );
            ancestor.OnAfterDescendantDeactivateEvent?.Invoke( (TThis) this, argument );
        }
    }

    // OnDescendantActivate
    protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

}

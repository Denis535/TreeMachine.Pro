namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

        // Owner
        internal object? Owner { get; private set; }
        // Tree
        public ITree<TThis>? Tree => (Owner as ITree<TThis>) ?? (Owner as NodeBase<TThis>)?.Tree;

        // OnAttach
        public event Action<object?>? OnBeforeAttachEvent;
        public event Action<object?>? OnAfterAttachEvent;
        public event Action<object?>? OnBeforeDetachEvent;
        public event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public NodeBase() {
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
            OnBeforeAttachEvent?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            OnAfterAttachEvent?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            OnBeforeDetachEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            OnAfterDetachEvent?.Invoke( argument );
        }

    }
}

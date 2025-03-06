namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class NodeBase2<TThis> : NodeBase<TThis> where TThis : NodeBase2<TThis> {

        // Owner
        private protected override object? Owner { get; set; } = null;
        // Tree
        public override ITree<TThis>? Tree => (Owner as ITree<TThis>) ?? (Owner as NodeBase<TThis>)?.Tree;

        // OnAttach
        public override event Action<object?>? OnBeforeAttachEvent;
        public override event Action<object?>? OnAfterAttachEvent;
        public override event Action<object?>? OnBeforeDetachEvent;
        public override event Action<object?>? OnAfterDetachEvent;

        // Constructor
        public NodeBase2() {
        }

        // Attach
        internal override void Attach(ITree<TThis> owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must have no owner" ).Valid( Owner == null );
            Owner = owner;
            OnBeforeAttach( argument );
            OnAttach( argument );
            OnAfterAttach( argument );
        }
        internal override void Detach(ITree<TThis> owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must have {owner} owner" ).Valid( Owner == owner );
            OnBeforeDetach( argument );
            OnDetach( argument );
            OnAfterDetach( argument );
            Owner = null;
        }

        // Attach
        internal override void Attach(TThis owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must have no owner" ).Valid( Owner == null );
            Owner = owner;
            OnBeforeAttach( argument );
            OnAttach( argument );
            OnAfterAttach( argument );
        }
        internal override void Detach(TThis owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must have {owner} owner" ).Valid( Owner == owner );
            OnBeforeDetach( argument );
            OnDetach( argument );
            OnAfterDetach( argument );
            Owner = null;
        }

        // OnAttach
        //protected override void OnAttach(object? argument) {
        //}
        protected override void OnBeforeAttach(object? argument) {
            OnBeforeAttachEvent?.Invoke( argument );
        }
        protected override void OnAfterAttach(object? argument) {
            OnAfterAttachEvent?.Invoke( argument );
        }

        // OnDetach
        //protected override void OnDetach(object? argument) {
        //}
        protected override void OnBeforeDetach(object? argument) {
            OnBeforeDetachEvent?.Invoke( argument );
        }
        protected override void OnAfterDetach(object? argument) {
            OnAfterDetachEvent?.Invoke( argument );
        }

    }
}

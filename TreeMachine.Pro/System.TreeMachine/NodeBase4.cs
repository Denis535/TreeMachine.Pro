namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class NodeBase4<TThis> : NodeBase3<TThis> where TThis : NodeBase4<TThis> {

        // Activity
        public override Activity_ Activity { get; private protected set; } = Activity_.Inactive;

        // OnActivate
        public override event Action<object?>? OnBeforeActivateEvent;
        public override event Action<object?>? OnAfterActivateEvent;
        public override event Action<object?>? OnBeforeDeactivateEvent;
        public override event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        public NodeBase4() {
        }

        // Attach
        internal override void Attach(ITree<TThis> owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            base.Attach( owner, argument );
            Activate( argument );
        }
        internal override void Detach(ITree<TThis> owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must be active" ).Valid( Activity is Activity_.Active );
            Deactivate( argument );
            base.Detach( owner, argument );
        }

        // Attach
        internal override void Attach(TThis owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            if (owner.Activity is Activity_.Active) {
                base.Attach( owner, argument );
                Activate( argument );
            } else {
                base.Attach( owner, argument );
            }
        }
        internal override void Detach(TThis owner, object? argument) {
            if (owner.Activity is Activity_.Active) {
                Assert.Operation.Message( $"Node {this} must be active" ).Valid( Activity is Activity_.Active );
                Deactivate( argument );
                base.Detach( owner, argument );
            } else {
                Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
                base.Detach( owner, argument );
            }
        }

        // Activate
        private protected override void Activate(object? argument) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"Node {this} must have owner with valid activity" ).Valid( (Owner is ITree<TThis>) || ((NodeBase2<TThis>) Owner).Activity is Activity_.Active or Activity_.Activating );
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            OnBeforeActivate( argument );
            Activity = Activity_.Activating;
            {
                OnActivate( argument );
                foreach (var child in Children) {
                    child.Activate( argument );
                }
            }
            Activity = Activity_.Active;
            OnAfterActivate( argument );
        }
        private protected override void Deactivate(object? argument) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"Node {this} must have owner with valid activity" ).Valid( (Owner is ITree<TThis>) || ((NodeBase2<TThis>) Owner).Activity is Activity_.Active or Activity_.Deactivating );
            Assert.Operation.Message( $"Node {this} must be active" ).Valid( Activity is Activity_.Active );
            OnBeforeDeactivate( argument );
            Activity = Activity_.Deactivating;
            {
                foreach (var child in Children.Reverse()) {
                    child.Deactivate( argument );
                }
                OnDeactivate( argument );
            }
            Activity = Activity_.Inactive;
            OnAfterDeactivate( argument );
        }

        // OnActivate
        //protected override void OnActivate(object? argument) {
        //}
        protected override void OnBeforeActivate(object? argument) {
            OnBeforeActivateEvent?.Invoke( argument );
        }
        protected override void OnAfterActivate(object? argument) {
            OnAfterActivateEvent?.Invoke( argument );
        }

        // OnDeactivate
        //protected override void OnDeactivate(object? argument) {
        //}
        protected override void OnBeforeDeactivate(object? argument) {
            OnBeforeDeactivateEvent?.Invoke( argument );
        }
        protected override void OnAfterDeactivate(object? argument) {
            OnAfterDeactivateEvent?.Invoke( argument );
        }

    }
}

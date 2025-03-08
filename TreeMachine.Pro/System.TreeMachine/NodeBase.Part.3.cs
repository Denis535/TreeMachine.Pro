namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        // Activity
        public Activity_ Activity { get; private set; } = Activity_.Inactive;

        // OnActivate
        public event Action<object?>? OnBeforeActivateEvent;
        public event Action<object?>? OnAfterActivateEvent;
        public event Action<object?>? OnBeforeDeactivateEvent;
        public event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        //public NodeBase() {
        //}

        // Attach
        internal void Attach(ITree<TThis> owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            AttachBase( owner, argument );
            Activate( argument );
        }
        internal void Detach(ITree<TThis> owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must be active" ).Valid( Activity is Activity_.Active );
            Deactivate( argument );
            DetachBase( owner, argument );
        }

        // Attach
        internal void Attach(TThis owner, object? argument) {
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
            if (owner.Activity is Activity_.Active) {
                AttachBase( owner, argument );
                Activate( argument );
            } else {
                AttachBase( owner, argument );
            }
        }
        internal void Detach(TThis owner, object? argument) {
            if (owner.Activity is Activity_.Active) {
                Assert.Operation.Message( $"Node {this} must be active" ).Valid( Activity is Activity_.Active );
                Deactivate( argument );
                DetachBase( owner, argument );
            } else {
                Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity is Activity_.Inactive );
                DetachBase( owner, argument );
            }
        }

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"Node {this} must have owner with valid activity" ).Valid( (Owner is ITree<TThis>) || ((NodeBase<TThis>) Owner).Activity is Activity_.Active or Activity_.Activating );
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
        private void Deactivate(object? argument) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
            Assert.Operation.Message( $"Node {this} must have owner with valid activity" ).Valid( (Owner is ITree<TThis>) || ((NodeBase<TThis>) Owner).Activity is Activity_.Active or Activity_.Deactivating );
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
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            OnBeforeActivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            OnAfterActivateEvent?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            OnBeforeDeactivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            OnAfterDeactivateEvent?.Invoke( argument );
        }

    }
}

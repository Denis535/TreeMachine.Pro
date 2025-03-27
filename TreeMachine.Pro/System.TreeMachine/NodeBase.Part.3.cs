namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

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
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have no owner", Owner == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", Activity == Activity_.Inactive );
            {
                Owner = owner;
                OnBeforeAttach( argument );
                OnAttach( argument );
                OnAfterAttach( argument );
            }
            Activate( argument );
        }
        internal void Detach(ITree<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have owner", Owner == owner );
            Assert.Operation.Valid( $"Node {this} must be active", Activity == Activity_.Active );
            Deactivate( argument );
            {
                OnBeforeDetach( argument );
                OnDetach( argument );
                OnAfterDetach( argument );
                Owner = null;
            }
        }

        // Attach
        internal void Attach(TThis owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have no owner", Owner == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", Activity == Activity_.Inactive );
            if (owner.Activity == Activity_.Active) {
                {
                    Owner = owner;
                    OnBeforeAttach( argument );
                    OnAttach( argument );
                    OnAfterAttach( argument );
                }
                Activate( argument );
            } else {
                {
                    Owner = owner;
                    OnBeforeAttach( argument );
                    OnAttach( argument );
                    OnAfterAttach( argument );
                }
            }
        }
        internal void Detach(TThis owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have owner", Owner == owner );
            if (owner.Activity == Activity_.Active) {
                Assert.Operation.Valid( $"Node {this} must be active", Activity == Activity_.Active );
                Deactivate( argument );
                {
                    OnBeforeDetach( argument );
                    OnDetach( argument );
                    OnAfterDetach( argument );
                    Owner = null;
                }
            } else {
                Assert.Operation.Valid( $"Node {this} must be inactive", Activity == Activity_.Inactive );
                {
                    OnBeforeDetach( argument );
                    OnDetach( argument );
                    OnAfterDetach( argument );
                    Owner = null;
                }
            }
        }

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", Owner != null );
            Assert.Operation.Valid( $"Node {this} must have owner with valid activity", (Owner is ITree<TThis>) || ((NodeBase<TThis>) Owner).Activity is Activity_.Active or Activity_.Activating );
            Assert.Operation.Valid( $"Node {this} must be inactive", Activity == Activity_.Inactive );
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
            Assert.Operation.Valid( $"Node {this} must have owner", Owner != null );
            Assert.Operation.Valid( $"Node {this} must have owner with valid activity", (Owner is ITree<TThis>) || ((NodeBase<TThis>) Owner).Activity is Activity_.Active or Activity_.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", Activity == Activity_.Active );
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

#nullable enable
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

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Owner != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", (this.Owner is ITree<TThis>) || ((NodeBase<TThis>) this.Owner).Activity is Activity_.Active or Activity_.Activating );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity_.Inactive );
            this.OnBeforeActivate( argument );
            this.Activity = Activity_.Activating;
            {
                this.OnActivate( argument );
                foreach (var child in this.Children) {
                    child.Activate( argument );
                }
            }
            this.Activity = Activity_.Active;
            this.OnAfterActivate( argument );
        }
        private void Deactivate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Owner != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", (this.Owner is ITree<TThis>) || ((NodeBase<TThis>) this.Owner).Activity is Activity_.Active or Activity_.Deactivating );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity_.Active );
            this.OnBeforeDeactivate( argument );
            this.Activity = Activity_.Deactivating;
            {
                foreach (var child in this.Children.Reverse()) {
                    child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
            }
            this.Activity = Activity_.Inactive;
            this.OnAfterDeactivate( argument );
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
            this.OnBeforeActivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateEvent?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateEvent?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateEvent?.Invoke( argument );
        }

    }
}

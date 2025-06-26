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
        public event Action<object?>? OnBeforeActivateCallback;
        public event Action<object?>? OnAfterActivateCallback;
        public event Action<object?>? OnBeforeDeactivateCallback;
        public event Action<object?>? OnAfterDeactivateCallback;

        // Constructor
        //public NodeBase() {
        //}

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Valid( $"Node {this} must have owner", this.Tree_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Tree_NoRecursive != null || this.Parent!.Activity is Activity_.Active or Activity_.Activating );
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
            Assert.Operation.Valid( $"Node {this} must have owner", this.Tree_NoRecursive != null || this.Parent != null );
            Assert.Operation.Valid( $"Node {this} must have valid owner", this.Tree_NoRecursive != null || this.Parent!.Activity is Activity_.Active or Activity_.Deactivating );
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
            this.OnBeforeActivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterActivate(object? argument) {
            this.OnAfterActivateCallback?.Invoke( argument );
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivateCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivateCallback?.Invoke( argument );
        }

    }
}

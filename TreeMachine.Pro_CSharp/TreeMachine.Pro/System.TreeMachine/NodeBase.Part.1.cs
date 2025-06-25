#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

        // Owner
        private object? Owner { get; set; }
        // Tree
        public ITree<TThis>? Tree => this.Owner as ITree<TThis>;
        public ITree<TThis>? TreeRecursive => (this.Owner as ITree<TThis>) ?? (this.Owner as NodeBase<TThis>)?.TreeRecursive;

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Constructor
        public NodeBase() {
        }

        // Attach
        internal void Attach(ITree<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Tree} tree", this.Tree == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity_.Inactive );
            {
                this.Owner = owner;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            {
                this.Activate( argument );
            }
        }
        internal void Attach(TThis owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Tree} tree", this.Tree == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity_.Inactive );
            {
                this.Owner = owner;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (owner.Activity == Activity_.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal void Detach(ITree<TThis> owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have {owner} tree", this.Tree == owner );
            Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity_.Active );
            {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }
        internal void Detach(TThis owner, object? argument) {
            Assert.Argument.NotNull( $"Argument 'owner' must be non-null", owner != null );
            Assert.Operation.Valid( $"Node {this} must have {owner} parent", this.Parent == owner );
            if (owner.Activity == Activity_.Active) {
                Assert.Operation.Valid( $"Node {this} must be active", this.Activity == Activity_.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity_.Inactive );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
            this.OnBeforeAttachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterAttach(object? argument) {
            this.OnAfterAttachCallback?.Invoke( argument );
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
            this.OnBeforeDetachCallback?.Invoke( argument );
        }
        protected virtual void OnAfterDetach(object? argument) {
            this.OnAfterDetachCallback?.Invoke( argument );
        }

    }
}

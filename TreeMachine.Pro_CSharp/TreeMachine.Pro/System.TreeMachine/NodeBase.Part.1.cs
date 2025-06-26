#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

        // Owner
        private object? Owner { get; set; }
        // Tree
        public ITree<TThis>? Tree => (this.Owner as ITree<TThis>) ?? (this.Owner as NodeBase<TThis>)?.Tree;
        internal ITree<TThis>? Tree_NoRecursive => this.Owner as ITree<TThis>;

        // OnAttach
        public event Action<object?>? OnBeforeAttachCallback;
        public event Action<object?>? OnAfterAttachCallback;
        public event Action<object?>? OnBeforeDetachCallback;
        public event Action<object?>? OnAfterDetachCallback;

        // Constructor
        public NodeBase() {
        }

        // Attach
        internal void Attach(ITree<TThis> tree, object? argument) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Tree_NoRecursive} tree", this.Tree_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity_.Inactive );
            {
                this.Owner = tree;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            {
                this.Activate( argument );
            }
        }
        internal void Attach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Tree_NoRecursive} tree", this.Tree_NoRecursive == null );
            Assert.Operation.Valid( $"Node {this} must have no {this.Parent} parent", this.Parent == null );
            Assert.Operation.Valid( $"Node {this} must be inactive", this.Activity == Activity_.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (parent.Activity == Activity_.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        internal void Detach(ITree<TThis> tree, object? argument) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Operation.Valid( $"Node {this} must have {tree} tree", this.Tree_NoRecursive == tree );
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
        internal void Detach(TThis parent, object? argument) {
            Assert.Argument.NotNull( $"Argument 'parent' must be non-null", parent != null );
            Assert.Operation.Valid( $"Node {this} must have {parent} parent", this.Parent == parent );
            if (parent.Activity == Activity_.Active) {
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

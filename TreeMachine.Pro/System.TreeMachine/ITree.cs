namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITree<T> where T : notnull, NodeBase<T> {

        // Root
        protected T? Root { get; set; }

        // SetRoot
        protected void SetRoot(T? root, object? argument, Action<T>? callback);
        protected void AddRoot(T root, object? argument);
        protected internal void RemoveRoot(T root, object? argument, Action<T>? callback);

        // Helpers
        protected static void SetRoot(ITree<T> tree, T? root, object? argument, Action<T>? callback) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            if (tree.Root != null) {
                tree.RemoveRoot( tree.Root, argument, callback );
            }
            if (root != null) {
                tree.AddRoot( root, argument );
            }
        }
        protected static void AddRoot(ITree<T> tree, T root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Operation.Valid( $"Tree {tree} must have no root node", tree.Root == null );
            Assert.Operation.Valid( $"Root {root} must have no owner", root.Owner == null );
            Assert.Operation.Valid( $"Root {root} must be inactive", root.Activity == NodeBase<T>.Activity_.Inactive );
            tree.Root = root;
            tree.Root.Attach( tree, argument );
        }
        protected static void RemoveRoot(ITree<T> tree, T root, object? argument, Action<T>? callback) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Operation.Valid( $"Tree {tree} must have root {root} node", tree.Root == root );
            Assert.Operation.Valid( $"Root {root} must have owner", root.Owner == tree );
            Assert.Operation.Valid( $"Root {root} must be active", root.Activity == NodeBase<T>.Activity_.Active );
            tree.Root.Detach( tree, argument );
            tree.Root = null;
            callback?.Invoke( root );
        }

    }
}

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
            if (tree.Root != null) {
                tree.RemoveRoot( tree.Root, argument, callback );
            }
            if (root != null) {
                tree.AddRoot( root, argument );
            }
        }
        protected static void AddRoot(ITree<T> tree, T root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Operation.Valid( $"Tree {tree} must have no root node", tree.Root == null );
            tree.Root = root;
            tree.Root.Attach( tree, argument );
        }
        protected static void RemoveRoot(ITree<T> tree, T root, object? argument, Action<T>? callback) {
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Operation.Valid( $"Tree {tree} must have root {root} node", tree.Root == root );
            tree.Root.Detach( tree, argument );
            tree.Root = null;
            callback?.Invoke( root );
        }

    }
}

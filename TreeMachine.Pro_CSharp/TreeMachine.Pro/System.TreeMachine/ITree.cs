﻿#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITree<T> where T : notnull, NodeBase<T> {

        // Root
        protected T? Root { get; set; }

        // AddRoot
        protected void AddRoot(T root, object? argument);
        protected internal void RemoveRoot(T root, object? argument, Action<T, object?>? callback);
        protected void RemoveRoot(object? argument, Action<T, object?>? callback);

        // Helpers
        protected static void AddRoot(ITree<T> tree, T root, object? argument) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Argument.Valid( $"Argument 'tree' ({tree}) must have no {tree.Root} root", tree.Root == null );
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no tree", root.Tree_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be inactive", root.Activity == NodeBase<T>.Activity_.Inactive );
            tree.Root = root;
            tree.Root.Attach( tree, argument );
        }
        protected static void RemoveRoot(ITree<T> tree, T root, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Argument.Valid( $"Argument 'tree' ({tree}) must have {root} root", tree.Root == root );
            Assert.Argument.NotNull( $"Argument 'root' must be non-null", root != null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have {tree} tree", root.Tree_NoRecursive == tree );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must have no parent", root.Parent == null );
            Assert.Argument.Valid( $"Argument 'root' ({root}) must be active", root.Activity == NodeBase<T>.Activity_.Active );
            tree.Root.Detach( tree, argument );
            tree.Root = null;
            callback?.Invoke( root, argument );
        }
        protected static void RemoveRoot(ITree<T> tree, object? argument, Action<T, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'tree' must be non-null", tree != null );
            Assert.Argument.Valid( $"Argument 'tree' ({tree}) must have root", tree.Root != null );
            tree.RemoveRoot( tree.Root, argument, callback );
        }

    }
}

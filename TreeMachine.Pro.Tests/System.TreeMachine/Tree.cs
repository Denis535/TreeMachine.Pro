namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

    internal class Tree<T> : ITree<Node> {

        // Root
        Node? ITree<Node>.Root { get => Root; set => Root = value; }
        public Node? Root { get; private set; }

        // Constructor
        public Tree() {
        }

        // SetRoot
        public void SetRoot(Node? root, object? argument, Action<Node>? onRemoved) {
            ITree<Node>.SetRoot( this, root, argument, onRemoved );
        }
        public void AddRoot(Node root, object? argument) {
            ITree<Node>.AddRoot( this, root, argument );
        }
        public void RemoveRoot(Node root, object? argument, Action<Node>? onRemoved) {
            ITree<Node>.RemoveRoot( this, root, argument, onRemoved );
        }

    }
    internal abstract class Node : NodeBase3<Node> {

        //public bool IsDisposed { get; private set; }

        public Node() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"Node {this} must have no tree" ).Valid( Tree == null );
        //    foreach (var child in Children) {
        //        child.Dispose();
        //    }
        //    IsDisposed = true;
        //}

        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        protected override void OnBeforeDescendantAttach(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantAttach(Node descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDetach(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantDetach(Node descendant, object? argument) {
        }

        protected override void OnActivate(object? argument) {
            TestContext.Out.WriteLine( "OnActivate: " + GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            TestContext.Out.WriteLine( "OnDeactivate: " + GetType().Name );
        }

        protected override void OnBeforeDescendantActivate(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(Node descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(Node descendant, object? argument) {
        }

        protected override void AddChild(Node child, object? argument) {
            base.AddChild( child, argument );
        }
        protected override void RemoveChild(Node child, object? argument, Action<Node>? onRemoved) {
            base.RemoveChild( child, argument, onRemoved );
        }

        protected void AddChild(Node child) {
            AddChild( child, null );
        }
        protected void RemoveChild(Node child) {
            RemoveChild( child, null, null );
        }
        protected bool RemoveChild(Func<Node, bool> predicate) {
            return RemoveChild( predicate, null, null );
        }
        protected int RemoveChildren(Func<Node, bool> predicate) {
            return RemoveChildren( predicate, null, null );
        }
        protected void RemoveSelf() {
            RemoveSelf( null, null );
        }

        protected override void Sort(List<Node> children) {
            base.Sort( children );
        }

    }
}

﻿namespace System.TreeMachine {
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
        public void SetRoot(Node? root, object? argument, Action<Node>? callback) {
            ITree<Node>.SetRoot( this, root, argument, callback );
        }
        public void AddRoot(Node root, object? argument) {
            ITree<Node>.AddRoot( this, root, argument );
        }
        public void RemoveRoot(Node root, object? argument, Action<Node>? callback) {
            ITree<Node>.RemoveRoot( this, root, argument, callback );
        }

    }
    internal abstract class Node : NodeBase4<Node> {

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

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnDescendantAttach
        protected override void OnBeforeDescendantAttach(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantAttach(Node descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDetach(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantDetach(Node descendant, object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            TestContext.Out.WriteLine( "OnActivate: " + GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            TestContext.Out.WriteLine( "OnDeactivate: " + GetType().Name );
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(Node descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(Node descendant, object? argument) {
        }

        // AddChild
        protected override void AddChild(Node child, object? argument) {
            base.AddChild( child, argument );
        }
        protected override void RemoveChild(Node child, object? argument, Action<Node>? callback) {
            base.RemoveChild( child, argument, callback );
        }

        // Sort
        protected override void Sort(List<Node> children) {
            base.Sort( children );
        }

    }
}

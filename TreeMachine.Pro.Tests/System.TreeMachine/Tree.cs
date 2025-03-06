namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

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
}

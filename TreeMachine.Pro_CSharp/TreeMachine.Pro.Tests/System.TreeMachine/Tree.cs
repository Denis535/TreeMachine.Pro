namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree : ITree<Node> {

        // Root
        Node? ITree<Node>.Root { get => this.Root; set => this.Root = value; }
        public Node? Root { get; private set; }

        // Constructor
        public Tree() {
        }

        // AddRoot
        public void AddRoot(Node root, object? argument) {
            ITree<Node>.AddRoot( this, root, argument );
        }
        public void RemoveRoot(Node root, object? argument, Action<Node>? callback) {
            ITree<Node>.RemoveRoot( this, root, argument, callback );
        }
        public void RemoveRoot(object? argument, Action<Node>? callback) {
            ITree<Node>.RemoveRoot( this, argument, callback );
        }

    }
}

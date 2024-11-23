namespace System.TreeMachine;
using System;
using System.Collections.Generic;
using System.Text;

public class Tree<T> : ITree<T> where T : NodeBase<T> {

    // Root
    public T? Root { get; private set; }

    // Constructor
    public Tree() {
    }

    // SetRoot
    public virtual void SetRoot(T? root, object? argument = null) {
        ITree<T>.SetRoot( this, (tree, root) => ((Tree<T>) tree).Root = root, root, argument );
    }

}

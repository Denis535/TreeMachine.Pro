namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Tree<T> : ITree<T> where T : NodeBase<T> {

    // Root
    public T? Root { get; private set; }
    // Root
    T? ITree<T>.Root { get => Root; set => Root = value; }

    // Constructor
    public Tree() {
    }

    // SetRoot
    public virtual void SetRoot(T? root, object? argument = null) {
        ITree<T>.SetRoot( this, root, argument );
    }

}

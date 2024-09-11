namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class TreeBase<T> : ITree<T> where T : NodeBase<T> {

    // Root
    protected T? Root { get; set; }
    // Root
    T? ITree<T>.Root { get => Root; set => Root = value; }

    // Constructor
    public TreeBase() {
    }

    // SetRoot
    protected virtual void SetRoot(T? root, object? argument = null) {
        ITree<T>.SetRoot( this, root, argument );
    }
    // SetRoot
    void ITree<T>.SetRoot(T? root, object? argument) => SetRoot( root, argument );

}

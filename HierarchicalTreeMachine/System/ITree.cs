namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public interface ITree {
}
public interface ITree<T> : ITree where T : NodeBase<T> {

    // Root
    protected T? Root { get; }

    // SetRoot
    protected internal void SetRoot(T? root, object? argument = null);

}

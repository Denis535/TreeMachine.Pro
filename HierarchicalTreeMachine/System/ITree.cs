namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public interface ITree {
}
public interface ITree<T> : ITree where T : NodeBase<T> {

    protected T? Root { get; }

    protected internal void AddRoot(T root, object? argument = null);
    protected internal void RemoveRoot(T root, object? argument = null);
    protected internal void RemoveRoot(object? argument = null);

}

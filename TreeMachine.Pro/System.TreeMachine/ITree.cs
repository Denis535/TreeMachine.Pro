namespace System.TreeMachine;
using System;
using System.Collections.Generic;
using System.Text;

public interface ITree<T> where T : NodeBase<T> {

    // Root
    protected T? Root { get; }

    // SetRoot
    protected internal void SetRoot(T? root, object? argument = null);

    // Helpers
    protected static void SetRoot(ITree<T> tree, Action<ITree<T>, T?> rootSetter, T? root, object? argument) {
        if (tree.Root != null) {
            tree.Root.Detach( tree, argument );
            rootSetter( tree, null );
        }
        if (root != null) {
            rootSetter( tree, root );
            tree.Root!.Attach( tree, argument );
        }
    }

}

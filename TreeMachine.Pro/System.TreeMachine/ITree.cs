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
            var prevRoot = tree.Root;
            prevRoot.Detach( tree, argument );
            rootSetter( tree, null );
            prevRoot.DisposeWhenRemove( argument );
        }
        if (root != null) {
            rootSetter( tree, root );
            root.Attach( tree, argument );
        }
    }

}

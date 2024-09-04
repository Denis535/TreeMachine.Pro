namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public interface ITree {
}
public interface ITree<T> : ITree where T : NodeBase<T> {

    // Root
    public T? Root { get; protected set; }

    // AddRoot
    public void AddRoot(T root, object? argument = null);
    public void RemoveRoot(T root, object? argument = null);
    public void RemoveRoot(object? argument = null);

    // Helpers
    protected static void AddRoot(ITree<T> tree, T root, object? argument) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Tree {tree} must have no root" ).Valid( tree.Root == null );
        tree.Root = root;
        tree.Root.Activate( tree, argument );
    }
    protected static void RemoveRoot(ITree<T> tree, T root, object? argument) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Tree {tree} must have {root} root" ).Valid( tree.Root == root );
        RemoveRoot( tree, argument );
    }
    protected static void RemoveRoot(ITree<T> tree, object? argument) {
        Assert.Operation.Message( $"Tree {tree} must have root" ).Valid( tree.Root != null );
        tree.Root.Deactivate( tree, argument );
        tree.Root = null;
    }

}

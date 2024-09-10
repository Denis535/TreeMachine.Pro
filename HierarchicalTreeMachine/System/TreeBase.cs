namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class TreeBase<T> : ITree<T> where T : NodeBase<T> {

    // Root
    protected T? Root { get; private set; }
    // Root
    T? ITree<T>.Root => Root;

    // Constructor
    public TreeBase() {
    }

    // AddRoot
    protected virtual void AddRoot(T root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Tree {this} must have no root" ).Valid( Root == null );
        Root = root;
        Root.Activate( this, argument );
    }
    protected virtual void RemoveRoot(T root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Tree {this} must have {root} root" ).Valid( Root == root );
        RemoveRoot( argument );
    }
    protected virtual void RemoveRoot(object? argument = null) {
        Assert.Operation.Message( $"Tree {this} must have root" ).Valid( Root != null );
        Root.Deactivate( this, argument );
        Root = null;
    }
    // AddRoot
    void ITree<T>.AddRoot(T root, object? argument) => AddRoot( root, argument );
    void ITree<T>.RemoveRoot(T root, object? argument) => RemoveRoot( root, argument );
    void ITree<T>.RemoveRoot(object? argument) => RemoveRoot( argument );

}
public class Tree<T> : TreeBase<T> where T : NodeBase<T> {

    // Root
    public new T? Root => base.Root;

    // Constructor
    public Tree() {
    }

    // AddRoot
    public virtual new void AddRoot(T root, object? argument = null) {
        base.AddRoot( root, argument );
    }
    public virtual new void RemoveRoot(T root, object? argument = null) {
        base.RemoveRoot( root, argument );
    }
    public virtual new void RemoveRoot(object? argument = null) {
        base.RemoveRoot( argument );
    }

}

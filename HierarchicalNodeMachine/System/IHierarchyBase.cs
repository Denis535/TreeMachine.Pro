namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public interface IHierarchyBase {
}
public interface IHierarchyBase<T> : IHierarchyBase where T : NodeBase<T> {

    // Root
    protected T? Root { get; set; }

    // AddRoot
    protected void AddRoot(T root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Hierarchy {this} must have no root" ).Valid( Root == null );
        Root = root;
        Root.Activate( this, argument );
    }
    protected internal void RemoveRoot(T root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Hierarchy {this} must have {root} root" ).Valid( Root == root );
        RemoveRoot( argument );
    }
    protected void RemoveRoot(object? argument = null) {
        Assert.Operation.Message( $"Hierarchy {this} must have root" ).Valid( Root != null );
        Root.Deactivate( this, argument );
        Root = null;
    }

}

namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public interface IHierarchy {
}
public interface IHierarchy<T> : IHierarchy where T : NodeBase<T> {

    // Root
    public T? Root { get; protected set; }

    // AddRoot
    public sealed void AddRoot(T root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Hierarchy {this} must have no root" ).Valid( Root == null );
        Root = root;
        Root.Activate( this, argument );
    }
    public sealed void RemoveRoot(T root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Hierarchy {this} must have {root} root" ).Valid( Root == root );
        RemoveRoot( argument );
    }
    public sealed void RemoveRoot(object? argument = null) {
        Assert.Operation.Message( $"Hierarchy {this} must have root" ).Valid( Root != null );
        Root.Deactivate( this, argument );
        Root = null;
    }

}

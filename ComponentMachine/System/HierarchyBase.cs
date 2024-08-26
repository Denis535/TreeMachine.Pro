namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class HierarchyBase {

    // Root
    protected NodeBase? Root { get; private set; }

    // Constructor
    public HierarchyBase() {
    }

    // SetRoot
    protected virtual void SetRoot(NodeBase root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Hierarchy {this} must have no root" ).Valid( Root == null );
        Root = root;
        Root.Activate( this, argument );
    }
    protected internal virtual void RemoveRoot(NodeBase root, object? argument = null) {
        Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
        Assert.Operation.Message( $"Hierarchy {this} must have {root} root" ).Valid( Root == root );
        RemoveRoot( argument );
    }
    protected virtual void RemoveRoot(object? argument = null) {
        Assert.Operation.Message( $"Hierarchy {this} must have root" ).Valid( Root != null );
        Root.Deactivate( this, argument );
        Root = null;
    }

}

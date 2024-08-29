namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Tests {

    [Test]
    public void Test_00() {
        using var hierarchy = new Hierarchy();
    }

}
internal class Hierarchy : IHierarchy<Node>, IDisposable {

    // Root
    Node? IHierarchy<Node>.Root { get; set; }
    protected Node? Root => ((IHierarchy<Node>) this).Root;

    // Constructor
    public Hierarchy() {
        AddRoot( new RootNode() );
    }
    public void Dispose() {
        RemoveRoot();
    }

    // AddRoot
    protected void AddRoot(Node root, object? argument = null) {
        ((IHierarchy<Node>) this).AddRoot( root, argument );
    }
    protected void RemoveRoot(Node root, object? argument = null) {
        ((IHierarchy<Node>) this).RemoveRoot( root, argument );
    }
    protected void RemoveRoot(object? argument = null) {
        ((IHierarchy<Node>) this).RemoveRoot( argument );
    }

}
internal abstract class Node : NodeBase<Node> {

    protected override void OnActivate(object? argument) {
        TestContext.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
    }

    protected override void OnBeforeDescendantActivate(Node descendant, object? argument) {
    }
    protected override void OnAfterDescendantActivate(Node descendant, object? argument) {
    }
    protected override void OnBeforeDescendantDeactivate(Node descendant, object? argument) {
    }
    protected override void OnAfterDescendantDeactivate(Node descendant, object? argument) {
    }

}
// Root
internal class RootNode : Node {

    public RootNode() {
        AddChild( new A_Node() );
        AddChild( new B_Node() );
    }

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
// Level-1
internal class A_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
        AddChild( new A1_Node() );
        AddChild( new A2_Node() );
    }
    protected override void OnDeactivate(object? argument) {
        RemoveChildren( i => true );
        base.OnDeactivate( argument );
    }

}
internal class B_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
        AddChild( new B1_Node() );
        AddChild( new B2_Node() );
    }
    protected override void OnDeactivate(object? argument) {
        //RemoveChildren( i => true );
        base.OnDeactivate( argument );
    }

}
// Level-2
internal class A1_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class A2_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B1_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B2_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}

namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Tests {

    [Test]
    public void Test_00() {
        using var tree = new Tree();
    }

}
// Tree
internal class Tree : TreeBase<NodeBase2>, IDisposable {

    public Tree() {
        SetRoot( new RootNode() );
    }
    public void Dispose() {
        SetRoot( null );
    }

}
// NodeBase2
internal abstract class NodeBase2 : NodeBase<NodeBase2> {

    protected override void OnActivate(object? argument) {
        TestContext.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
    }

    protected override void OnBeforeDescendantActivate(NodeBase2 descendant, object? argument) {
    }
    protected override void OnAfterDescendantActivate(NodeBase2 descendant, object? argument) {
    }
    protected override void OnBeforeDescendantDeactivate(NodeBase2 descendant, object? argument) {
    }
    protected override void OnAfterDescendantDeactivate(NodeBase2 descendant, object? argument) {
    }

}
// RootNode
internal class RootNode : NodeBase2 {

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
internal class A_Node : NodeBase2 {

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
internal class B_Node : NodeBase2 {

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
internal class A1_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class A2_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
// Level-2
internal class B1_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B2_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}

namespace System.TreeMachine;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

internal class Tree<T> : ITree<Node> {

    // Root
    Node? ITree<Node>.Root { get => Root; set => Root = value; }
    public Node? Root { get; private set; }

    // Constructor
    public Tree() {
    }

    // SetRoot
    public void SetRoot(Node? root, object? argument = null) {
        ITree<Node>.SetRoot( this, root, argument );
    }

}
internal abstract class Node : NodeBase3<Node> {

    //public bool IsDisposed { get; private set; }

    public Node() {
    }
    //public virtual void Dispose() {
    //    System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
    //    System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
    //    System.Assert.Operation.Message( $"Node {this} must have no tree" ).Valid( Tree == null );
    //    foreach (var child in Children) {
    //        child.Dispose();
    //    }
    //    IsDisposed = true;
    //}

    protected sealed override void OnAttach(object? argument) {
    }
    protected sealed override void OnDetach(object? argument) {
    }

    protected override void OnActivate(object? argument) {
        TestContext.Out.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.Out.WriteLine( "OnDeactivate: " + GetType().Name );
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

#pragma warning disable CA2000 // Dispose objects before losing scope
namespace System.TreeMachine;
using System;
using System.Collections.Generic;
using System.Text;

public class Tests_00 {

    [Test]
    public void Test_00() {
        var tree = new Tree<Node>();
        tree.SetRoot( null );
        tree.SetRoot( new Root() );
        tree.SetRoot( null );
    }

    // Node
    internal abstract class Node : NodeBase2<Node>, IDisposable {

        public Node() {
            //TestContext.Out.WriteLine( "Constructor: " + GetType().Name );
        }
        public virtual void Dispose() {
            //TestContext.Out.WriteLine( "Dispose: " + GetType().Name );
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
    // Root
    internal class Root : Node {

        public Root() {
            AddChild( new A() );
            AddChild( new B() );
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    // A
    internal class A : Node {

        public A() {
            AddChild( new A1() );
            AddChild( new A2() );
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    internal class A1 : Node {

        public A1() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    internal class A2 : Node {

        public A2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    // B
    internal class B : Node {

        public B() {
            AddChild( new B1() );
            AddChild( new B2() );
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    internal class B1 : Node {

        public B1() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    internal class B2 : Node {

        public B2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
}

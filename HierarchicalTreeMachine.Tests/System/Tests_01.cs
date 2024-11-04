#pragma warning disable CA2000 // Dispose objects before losing scope
namespace System;
using System;
using System.Collections.Generic;
using System.Text;

public class Tests_01 {

    [Test]
    public void Test_00() {
        var tree = new Tree<NodeBase2>();
        tree.SetRoot( new Root() );
        tree.SetRoot( null );
    }

    // NodeBase
    internal abstract class NodeBase2 : NodeBase<NodeBase2>, IDisposable {

        public NodeBase2() {
            TestContext.WriteLine( "Constructor: " + GetType().Name );
        }
        public virtual void Dispose() {
            //TestContext.WriteLine( "Dispose: " + GetType().Name );
        }

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
    // Root
    internal class Root : NodeBase2 {

        public Root() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
            AddChild( new A() );
            AddChild( new B() );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    // A
    internal class A : NodeBase2 {

        public A() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
            AddChild( new A1() );
            AddChild( new A2() );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    internal class A1 : NodeBase2 {

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
    internal class A2 : NodeBase2 {

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
    internal class B : NodeBase2 {

        public B() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            base.OnActivate( argument );
            AddChild( new B1() );
            AddChild( new B2() );
        }
        protected override void OnDeactivate(object? argument) {
            base.OnDeactivate( argument );
        }

    }
    internal class B1 : NodeBase2 {

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
    internal class B2 : NodeBase2 {

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

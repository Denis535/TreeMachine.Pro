#pragma warning disable CA2000 // Dispose objects before losing scope
namespace System.TreeMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

public class Tests_00 {

    [Test]
    public void Test_00() {
        var tree = new Tree<Node>();
        var root = new Root();

        NUnit.Framework.Assert.That( tree.Root, Is.Null );
        NUnit.Framework.Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
        foreach (var node in root.DescendantsAndSelf) {
            NUnit.Framework.Assert.That( node.IsDisposed, Is.False );
            NUnit.Framework.Assert.That( node.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( node.Tree, Is.Null );
        }

        tree.SetRoot( root );
        NUnit.Framework.Assert.That( tree.Root, Is.EqualTo( root ) );
        NUnit.Framework.Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
        foreach (var node in root.DescendantsAndSelf) {
            NUnit.Framework.Assert.That( node.IsDisposed, Is.False );
            NUnit.Framework.Assert.That( node.Activity, Is.EqualTo( Node.Activity_.Active ) );
            NUnit.Framework.Assert.That( node.Tree, Is.EqualTo( tree ) );
        }

        tree.SetRoot( null );
        NUnit.Framework.Assert.That( tree.Root, Is.Null );
        NUnit.Framework.Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
        foreach (var node in root.DescendantsAndSelf) {
            NUnit.Framework.Assert.That( node.IsDisposed, Is.True );
            NUnit.Framework.Assert.That( node.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            NUnit.Framework.Assert.That( node.Tree, Is.Null );
        }
    }

    // Node
    internal abstract class Node : NodeBase2<Node>, IDisposable {

        public bool IsDisposed { get; private set; }

        public Node() {
        }
        public virtual void Dispose() {
            System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
            System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
            System.Assert.Operation.Message( $"Node {this} must have no tree" ).Valid( Tree == null );
            foreach (var child in Children) {
                child.Dispose();
            }
            IsDisposed = true;
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
    // Misc
    internal class A1 : Node {
    }
    internal class A2 : Node {
    }
    internal class B1 : Node {
    }
    internal class B2 : Node {
    }
}

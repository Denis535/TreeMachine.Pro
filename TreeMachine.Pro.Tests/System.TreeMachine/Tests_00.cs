namespace System.TreeMachine {
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

            tree.SetRoot( null );
            NUnit.Framework.Assert.That( tree.Root, Is.Null );
            NUnit.Framework.Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
            foreach (var node in root.DescendantsAndSelf) {
                NUnit.Framework.Assert.That( node.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                NUnit.Framework.Assert.That( node.Tree, Is.Null );
            }

            tree.SetRoot( root );
            NUnit.Framework.Assert.That( tree.Root, Is.EqualTo( root ) );
            NUnit.Framework.Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
            foreach (var node in root.DescendantsAndSelf) {
                NUnit.Framework.Assert.That( node.Activity, Is.EqualTo( Node.Activity_.Active ) );
                NUnit.Framework.Assert.That( node.Tree, Is.EqualTo( tree ) );
            }

            tree.SetRoot( null );
            NUnit.Framework.Assert.That( tree.Root, Is.Null );
            NUnit.Framework.Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
            foreach (var node in root.DescendantsAndSelf) {
                NUnit.Framework.Assert.That( node.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                NUnit.Framework.Assert.That( node.Tree, Is.Null );
            }
        }

        // Root
        internal class Root : Node {

            public Root() {
                AddChild( new A() );
                AddChild( new B() );
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
}

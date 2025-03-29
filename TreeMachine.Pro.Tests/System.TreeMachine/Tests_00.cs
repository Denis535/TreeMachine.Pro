namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using NUnit.Framework;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var tree = new Tree<Node>();
            Assert.That( tree.Root, Is.Null );

            var root = new Root();
            Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
            foreach (var descendant in root.DescendantsAndSelf) {
                Assert.That( descendant.Tree, Is.Null );
                Assert.That( descendant.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }

            {
                // SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );
                Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
                foreach (var descendant in root.DescendantsAndSelf) {
                    Assert.That( descendant.Tree, Is.Null );
                    Assert.That( descendant.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                }
            }
            {
                // SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
                foreach (var descendant in root.DescendantsAndSelf) {
                    Assert.That( descendant.Tree, Is.EqualTo( tree ) );
                    Assert.That( descendant.Activity, Is.EqualTo( Node.Activity_.Active ) );
                }
            }
            {
                // SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );
                Assert.That( root.DescendantsAndSelf.Count(), Is.EqualTo( 7 ) );
                foreach (var descendant in root.DescendantsAndSelf) {
                    Assert.That( descendant.Tree, Is.Null );
                    Assert.That( descendant.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                }
            }
        }

        // Root
        internal class Root : Node {

            public Root() {
                this.AddChild( new A(), null );
                this.AddChild( new B(), null );
                this.RemoveChildren( i => true, null, null );
                this.AddChild( new A(), null );
                this.AddChild( new B(), null );
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
                this.AddChild( new A1(), null );
                this.AddChild( new A2(), null );
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
                this.AddChild( new B1(), null );
                this.AddChild( new B2(), null );
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

namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_01 {

        [Test]
        public void Test_00() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();

            root.OnBeforeAttachCallback += arg => {
                root.AddChildren( [ a, b ], null );
            };
            root.OnAfterDetachCallback += arg => {
                root.RemoveChildren( null, null );
            };

            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.TreeRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 2 ) );
                foreach (var child in tree.Root.Children) {
                    Assert.That( child.Tree, Is.Null );
                    Assert.That( child.TreeRecursive, Is.EqualTo( tree ) );
                    Assert.That( child.Parent, Is.EqualTo( tree.Root ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                }
            }
            {
                // tree.RemoveRoot
                tree.RemoveRoot( null, null );
                Assert.That( tree.Root, Is.Null );
            }
        }

        [Test]
        public void Test_01() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();

            root.OnAfterAttachCallback += arg => {
                root.AddChildren( [ a, b ], null );
            };
            root.OnBeforeDetachCallback += arg => {
                root.RemoveChildren( null, null );
            };

            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.TreeRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 2 ) );
                foreach (var child in tree.Root.Children) {
                    Assert.That( child.Tree, Is.Null );
                    Assert.That( child.TreeRecursive, Is.EqualTo( tree ) );
                    Assert.That( child.Parent, Is.EqualTo( tree.Root ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                }
            }
            {
                // tree.RemoveRoot
                tree.RemoveRoot( null, null );
                Assert.That( tree.Root, Is.Null );
            }
        }

    }
}

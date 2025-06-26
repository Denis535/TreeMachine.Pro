namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.TreeRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 0 ) );
            }
            {
                // tree.Root.AddChildren a, b
                tree.Root.AddChildren( [ a, b ], null );
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
                // tree.Root.RemoveChildren a, b
                tree.Root.RemoveChildren( null, null );
                Assert.That( tree.Root.Children.Count, Is.Zero );
                Assert.That( a.Tree, Is.Null );
                Assert.That( a.TreeRecursive, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                Assert.That( a.Children.Count, Is.EqualTo( 0 ) );
                Assert.That( b.Tree, Is.Null );
                Assert.That( b.TreeRecursive, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                Assert.That( b.Children.Count, Is.EqualTo( 0 ) );
            }
            {
                // tree.RemoveRoot
                tree.RemoveRoot( null, null );
                Assert.That( tree.Root, Is.Null );
                Assert.That( root.Tree, Is.Null );
                Assert.That( root.TreeRecursive, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                Assert.That( root.Children.Count, Is.EqualTo( 0 ) );
            }
        }

        [Test]
        public void Test_01() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.TreeRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 0 ) );
            }
            {
                // tree.Root.AddChildren a, b
                tree.Root.AddChildren( [ a, b ], null );
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
                Assert.That( root.Tree, Is.Null );
                Assert.That( root.TreeRecursive, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                Assert.That( root.Children.Count, Is.EqualTo( 2 ) );
                foreach (var child in root.Children) {
                    Assert.That( child.Tree, Is.Null );
                    Assert.That( child.TreeRecursive, Is.Null );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                }
            }
        }

        [Test]
        public void Test_02() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            {
                // root.AddChildren a, b
                root.AddChildren( [ a, b ], null );
                Assert.That( root.Children.Count, Is.EqualTo( 2 ) );
                foreach (var child in root.Children) {
                    Assert.That( child.Tree, Is.Null );
                    Assert.That( child.TreeRecursive, Is.Null );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                }
            }
            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.TreeRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 2 ) );
            }
            {
                // tree.RemoveRoot
                tree.RemoveRoot( null, null );
                Assert.That( tree.Root, Is.Null );
                Assert.That( root.Tree, Is.Null );
                Assert.That( root.TreeRecursive, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                Assert.That( root.Children.Count, Is.EqualTo( 2 ) );
                foreach (var child in root.Children) {
                    Assert.That( child.Tree, Is.Null );
                    Assert.That( child.TreeRecursive, Is.Null );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                }
            }
        }

    }
}

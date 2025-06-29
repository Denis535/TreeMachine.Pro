namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_02 {

        [Test]
        public void Test_00() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();

            root.OnBeforeActivateCallback += arg => {
                root.AddChildren( [ a, b ], null );
            };
            root.OnAfterDeactivateCallback += arg => {
                root.RemoveChildren( null, null );
            };

            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Tree_NoRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.IsRoot, Is.True );
                Assert.That( tree.Root.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( tree.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( tree.Root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( tree.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in tree.Root.Children) {
                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
                    Assert.That( child.Tree, Is.EqualTo( tree ) );
                    Assert.That( child.Tree_NoRecursive, Is.Null );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( root ) );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
                }
            }
            {
                // tree.RemoveRoot
                tree.RemoveRoot( null, null );
                Assert.That( tree.Root, Is.Null );
            }
        }

        [Test]
        public void Test_02() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();

            root.OnAfterActivateCallback += arg => {
                root.AddChildren( [ a, b ], null );
            };
            root.OnBeforeDeactivateCallback += arg => {
                root.RemoveChildren( null, null );
            };

            {
                // tree.AddRoot root
                tree.AddRoot( root, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Tree, Is.EqualTo( tree ) );
                Assert.That( tree.Root.Tree_NoRecursive, Is.EqualTo( tree ) );
                Assert.That( tree.Root.IsRoot, Is.True );
                Assert.That( tree.Root.Root, Is.EqualTo( root ) );
                Assert.That( tree.Root.Parent, Is.Null );
                Assert.That( tree.Root.Ancestors.Count(), Is.EqualTo( 0 ) );
                Assert.That( tree.Root.AncestorsAndSelf.Count(), Is.EqualTo( 1 ) );
                Assert.That( tree.Root.Activity, Is.EqualTo( Node.Activity_.Active ) );
                Assert.That( tree.Root.Children.Count, Is.EqualTo( 2 ) );
                Assert.That( tree.Root.Descendants.Count, Is.EqualTo( 2 ) );
                Assert.That( tree.Root.DescendantsAndSelf.Count, Is.EqualTo( 3 ) );
                foreach (var child in tree.Root.Children) {
                    Assert.That( child, Is.EqualTo( a ).Or.EqualTo( b ) );
                    Assert.That( child.Tree, Is.EqualTo( tree ) );
                    Assert.That( child.Tree_NoRecursive, Is.Null );
                    Assert.That( child.IsRoot, Is.False );
                    Assert.That( child.Root, Is.EqualTo( root ) );
                    Assert.That( child.Parent, Is.EqualTo( root ) );
                    Assert.That( child.Ancestors.Count(), Is.EqualTo( 1 ) );
                    Assert.That( child.AncestorsAndSelf.Count(), Is.EqualTo( 2 ) );
                    Assert.That( child.Activity, Is.EqualTo( Node.Activity_.Active ) );
                    Assert.That( child.Children.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.Descendants.Count, Is.EqualTo( 0 ) );
                    Assert.That( child.DescendantsAndSelf.Count, Is.EqualTo( 1 ) );
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

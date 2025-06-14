namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;

    public class Tests_00 {

        [Test]
        public void Test_00() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();

            {
                // tree.SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );

                Assert.That( root.Tree, Is.EqualTo( tree ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // root.AddChild A, B
                root.AddChild( a, null );
                root.AddChild( b, null );
                Assert.That( root.Children.Count, Is.EqualTo( 2 ) );

                Assert.That( a.Tree, Is.EqualTo( tree ) );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( b.Tree, Is.EqualTo( tree ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // root.RemoveChildren A, B
                root.RemoveChildren( i => true, null, null );
                Assert.That( root.Children.Count, Is.Zero );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
            {
                // tree.SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );

                Assert.That( root.Tree, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
        }

        [Test]
        public void Test_01() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();

            {
                // root.AddChild A, B
                root.AddChild( a, null );
                root.AddChild( b, null );
                Assert.That( root.Children.Count, Is.EqualTo( 2 ) );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.EqualTo( null ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
            {
                // tree.SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );

                Assert.That( root.Tree, Is.EqualTo( tree ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( a.Tree, Is.EqualTo( tree ) );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( b.Tree, Is.EqualTo( tree ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // tree.SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );

                Assert.That( root.Tree, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
            {
                // root.RemoveChildren A, B
                root.RemoveChildren( i => true, null, null );
                Assert.That( root.Children.Count, Is.Zero );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
        }

        [Test]
        public void Test_10() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            root.OnBeforeAttachEvent += arg => {
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
            };
            root.OnAfterDetachEvent += arg => {
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
            };

            {
                // tree.SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );

                Assert.That( root.Tree, Is.EqualTo( tree ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( a.Tree, Is.EqualTo( tree ) );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( b.Tree, Is.EqualTo( tree ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // tree.SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );

                Assert.That( root.Tree, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
        }

        [Test]
        public void Test_11() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            root.OnAfterAttachEvent += arg => {
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
            };
            root.OnBeforeDetachEvent += arg => {
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
            };

            {
                // tree.SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );

                Assert.That( root.Tree, Is.EqualTo( tree ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( a.Tree, Is.EqualTo( tree ) );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( b.Tree, Is.EqualTo( tree ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // tree.SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );

                Assert.That( root.Tree, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
        }

        [Test]
        public void Test_20() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            root.OnBeforeActivateEvent += arg => {
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
            };
            root.OnAfterDeactivateEvent += arg => {
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
            };

            {
                // tree.SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );

                Assert.That( root.Tree, Is.EqualTo( tree ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( a.Tree, Is.EqualTo( tree ) );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( b.Tree, Is.EqualTo( tree ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // tree.SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );

                Assert.That( root.Tree, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
        }

        [Test]
        public void Test_21() {
            var tree = new Tree();
            var root = new Root();
            var a = new A();
            var b = new B();
            root.OnAfterActivateEvent += arg => {
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
            };
            root.OnBeforeDeactivateEvent += arg => {
                root.RemoveChildren( i => true, null, null );
                root.AddChild( a, null );
                root.AddChild( b, null );
                root.RemoveChildren( i => true, null, null );
            };

            {
                // tree.SetRoot root
                tree.SetRoot( root, null, null );
                Assert.That( tree.Root, Is.EqualTo( root ) );

                Assert.That( root.Tree, Is.EqualTo( tree ) );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( a.Tree, Is.EqualTo( tree ) );
                Assert.That( a.Parent, Is.EqualTo( root ) );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Active ) );

                Assert.That( b.Tree, Is.EqualTo( tree ) );
                Assert.That( b.Parent, Is.EqualTo( root ) );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Active ) );
            }
            {
                // tree.SetRoot null
                tree.SetRoot( null, null, null );
                Assert.That( tree.Root, Is.Null );

                Assert.That( root.Tree, Is.Null );
                Assert.That( root.Parent, Is.Null );
                Assert.That( root.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( a.Tree, Is.Null );
                Assert.That( a.Parent, Is.Null );
                Assert.That( a.Activity, Is.EqualTo( Node.Activity_.Inactive ) );

                Assert.That( b.Tree, Is.Null );
                Assert.That( b.Parent, Is.Null );
                Assert.That( b.Activity, Is.EqualTo( Node.Activity_.Inactive ) );
            }
        }

    }
}

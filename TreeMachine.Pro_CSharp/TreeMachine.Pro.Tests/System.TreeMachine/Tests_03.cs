namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using Assert = NUnit.Framework.Assert;

    public class Tests_03 {

        [Test]
        public void Test_00() {
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
                // root.RemoveChildren a, b
                root.RemoveChildren( null, null );
                Assert.That( root.Children.Count, Is.Zero );
            }
        }

    }
}

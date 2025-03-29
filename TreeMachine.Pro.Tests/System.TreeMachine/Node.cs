﻿namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public abstract class Node : NodeBase2<Node> {

        //public bool IsDisposed { get; private set; }

        public Node() {
        }
        //public virtual void Dispose() {
        //    System.Assert.Operation.Message( $"Node {this} must be non-disposed" ).Valid( !IsDisposed );
        //    System.Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( Activity == Activity_.Inactive );
        //    System.Assert.Operation.Message( $"Node {this} must have no tree" ).Valid( Tree == null );
        //    foreach (var child in Children) {
        //        child.Dispose();
        //    }
        //    IsDisposed = true;
        //}

        // OnAttach
        protected override void OnAttach(object? argument) {
        }
        protected override void OnDetach(object? argument) {
        }

        // OnDescendantAttach
        protected override void OnBeforeDescendantAttach(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantAttach(Node descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDetach(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantDetach(Node descendant, object? argument) {
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            Trace.WriteLine( "OnActivate: " + this.GetType().Name );
        }
        protected override void OnDeactivate(object? argument) {
            Trace.WriteLine( "OnDeactivate: " + this.GetType().Name );
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(Node descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(Node descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(Node descendant, object? argument) {
        }

        // AddChild
        public new void AddChild(Node child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void RemoveChild(Node child, object? argument, Action<Node>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<Node, bool> predicate, object? argument, Action<Node>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<Node, bool> predicate, object? argument, Action<Node>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new void RemoveSelf(object? argument, Action<Node>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // Sort
        protected override void Sort(List<Node> children) {
            base.Sort( children );
        }

    }
    public class Root : Node {
    }
    public class A : Node {
    }
    //public class A1 : Node {
    //}
    //public class A2 : Node {
    //}
    public class B : Node {
    }
    //public class B1 : Node {
    //}
    //public class B2 : Node {
    //}
}

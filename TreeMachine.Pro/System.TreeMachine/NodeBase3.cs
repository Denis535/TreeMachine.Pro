namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public abstract class NodeBase3<TThis> : NodeBase2<TThis> where TThis : NodeBase3<TThis> {

        private readonly List<TThis> children = new List<TThis>( 0 );

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public override bool IsRoot => Parent == null;
        public override TThis Root => Parent?.Root ?? (TThis) this;

        // Parent
        public override TThis? Parent => Owner as TThis;
        public override IEnumerable<TThis> Ancestors {
            get {
                if (Parent != null) {
                    yield return Parent;
                    foreach (var i in Parent.Ancestors) yield return i;
                }
            }
        }
        public override IEnumerable<TThis> AncestorsAndSelf => Ancestors.Prepend( (TThis) this );

        // Children
        public override IReadOnlyList<TThis> Children => children;
        public override IEnumerable<TThis> Descendants {
            get {
                foreach (var child in Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public override IEnumerable<TThis> DescendantsAndSelf => Descendants.Prepend( (TThis) this );

        // Constructor
        public NodeBase3() {
        }

        // AddChild
        protected override void AddChild(TThis child, object? argument) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"Node {this} must have no child {child} node" ).Valid( !Children.Contains( child ) );
            children.Add( child );
            Sort( children );
            child.Attach( (TThis) this, argument );
        }
        protected override void RemoveChild(TThis child, object? argument, Action<TThis>? callback) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"Node {this} must have child {child} node" ).Valid( Children.Contains( child ) );
            child.Detach( (TThis) this, argument );
            children.Remove( child );
            callback?.Invoke( child );
        }
        protected override bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback) {
            var child = Children.LastOrDefault( predicate );
            if (child != null) {
                RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected override int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback) {
            var children = Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected override void RemoveSelf(object? argument, Action<TThis>? callback) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
            if (Parent != null) {
                Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Tree!.RemoveRoot( (TThis) this, argument, callback );
            }
        }

        // Sort
        protected override void Sort(List<TThis> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}

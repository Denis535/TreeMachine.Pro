namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Linq;

    public abstract partial class NodeBase<TThis> {

        private readonly List<TThis> children = new List<TThis>( 0 );

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => Parent == null;
        public TThis Root => Parent?.Root ?? (TThis) this;

        // Parent
        public TThis? Parent => Owner as TThis;
        public IEnumerable<TThis> Ancestors {
            get {
                if (Parent != null) {
                    yield return Parent;
                    foreach (var i in Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<TThis> AncestorsAndSelf => Ancestors.Prepend( (TThis) this );

        // Children
        public IReadOnlyList<TThis> Children => children;
        public IEnumerable<TThis> Descendants {
            get {
                foreach (var child in Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<TThis> DescendantsAndSelf => Descendants.Prepend( (TThis) this );

        // Constructor
        //public NodeBase() {
        //}

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Operation.Valid( $"Node {this} must have no child {child} node", !Children.Contains( child ) );
            Assert.Operation.Valid( $"Child {child} must have no owner", child.Owner == null );
            Assert.Operation.Valid( $"Child {child} must be inactive", child.Activity == Activity_.Inactive );
            children.Add( child );
            Sort( children );
            child.Attach( (TThis) this, argument );
        }
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Operation.Valid( $"Node {this} must have child {child} node", Children.Contains( child ) );
            Assert.Operation.Valid( $"Child {child} must have owner", child.Owner == this );
            child.Detach( (TThis) this, argument );
            children.Remove( child );
            callback?.Invoke( child );
        }
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback) {
            var child = Children.LastOrDefault( predicate );
            if (child != null) {
                RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback) {
            var children = Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected void RemoveSelf(object? argument, Action<TThis>? callback) {
            Assert.Operation.Valid( $"Node {this} must have owner", Owner != null );
            if (Parent != null) {
                Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Tree!.RemoveRoot( (TThis) this, argument, callback );
            }
        }

        // Sort
        protected virtual void Sort(List<TThis> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}

namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public abstract class NodeBase2<TThis> : NodeBase<TThis> where TThis : NodeBase2<TThis> {

        // Tree
        public ITree<TThis>? Tree => (ITree<TThis>?) Root?.Owner;

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
        private List<TThis> Children_ { get; } = new List<TThis>( 0 );
        public IReadOnlyList<TThis> Children => Children_;
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
        public NodeBase2() {
        }

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"Node {this} must have no child {child} node" ).Valid( !Children.Contains( child ) );
            Children_.Add( child );
            Sort( Children_ );
            child.Attach( (TThis) this, argument );
        }
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis>? callback) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"Node {this} must have child {child} node" ).Valid( Children.Contains( child ) );
            child.Detach( (TThis) this, argument );
            Children_.Remove( child );
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
            var children = Children.Where( predicate ).Reverse().ToList();
            foreach (var child in children) {
                RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected void RemoveSelf(object? argument, Action<TThis>? callback) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( Owner != null );
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

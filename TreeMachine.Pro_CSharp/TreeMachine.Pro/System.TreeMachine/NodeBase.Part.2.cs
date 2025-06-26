#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> {

        private readonly List<TThis> children = new List<TThis>( 0 );

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => this.Parent == null;
        public TThis Root => this.Parent?.Root ?? (TThis) this;

        // Parent
        public TThis? Parent => this.Owner as TThis;
        public IEnumerable<TThis> Ancestors {
            get {
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<TThis> AncestorsAndSelf => this.Ancestors.Prepend( (TThis) this );

        // Children
        public IReadOnlyList<TThis> Children => this.children;
        public IEnumerable<TThis> Descendants {
            get {
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<TThis> DescendantsAndSelf => this.Descendants.Prepend( (TThis) this );

        // Constructor
        //public NodeBase() {
        //}

        // AddChild
        protected virtual void AddChild(TThis child, object? argument) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Tree_NoRecursive} tree", child.Tree_NoRecursive == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have no {child.Parent} parent", child.Parent == null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            Assert.Operation.Valid( $"Node {this} must have no {child} child", !this.Children.Contains( child ) );
            this.children.Add( child );
            this.Sort( this.children );
            child.Attach( (TThis) this, argument );
        }
        protected virtual void AddChildren(TThis[] children, object? argument) {
            Assert.Argument.NotNull( $"Argument 'children' must be non-null", children != null );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }
        protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback) {
            Assert.Argument.NotNull( $"Argument 'child' must be non-null", child != null );
            Assert.Argument.Valid( $"Argument 'child' ({child}) must have {this} parent", child.Parent == this );
            if (this.Activity == Activity_.Active) {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be active", child.Activity == Activity_.Active );
            } else {
                Assert.Argument.Valid( $"Argument 'child' ({child}) must be inactive", child.Activity == Activity_.Inactive );
            }
            Assert.Operation.Valid( $"Node {this} must have {child} child", this.Children.Contains( child ) );
            child.Detach( (TThis) this, argument );
            this.children.Remove( child );
            callback?.Invoke( child, argument );
        }
        protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback) {
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected int RemoveChildren(object? argument, Action<TThis, object?>? callback) {
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected void RemoveSelf(object? argument, Action<TThis, object?>? callback) {
            if (this.Parent != null) {
                this.Parent.RemoveChild( (TThis) this, argument, callback );
            } else {
                Assert.Operation.Valid( $"Node {this} must have tree", this.Tree_NoRecursive != null );
                this.Tree_NoRecursive.RemoveRoot( (TThis) this, argument, callback );
            }
        }

        // Sort
        protected virtual void Sort(List<TThis> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}

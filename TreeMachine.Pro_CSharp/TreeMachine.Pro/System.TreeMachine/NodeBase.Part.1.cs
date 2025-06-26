#nullable enable
namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        private readonly List<TThis> children = new List<TThis>( 0 );

        // Owner
        private object? Owner { get; set; }
        // Tree
        public ITree<TThis>? Tree => (this.Owner as ITree<TThis>) ?? (this.Owner as NodeBase<TThis>)?.Tree;
        internal ITree<TThis>? Tree_NoRecursive => this.Owner as ITree<TThis>;

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

        // Activity
        public Activity_ Activity { get; private set; } = Activity_.Inactive;

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
        public NodeBase() {
        }

    }
}

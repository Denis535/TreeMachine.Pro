namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public abstract partial class NodeBase<TThis> where TThis : NodeBase<TThis> {

        // Owner
        private protected abstract object? Owner { get; set; }
        // Tree
        public abstract ITree<TThis>? Tree { get; }

        // OnAttach
        public abstract event Action<object?>? OnBeforeAttachEvent;
        public abstract event Action<object?>? OnAfterAttachEvent;
        public abstract event Action<object?>? OnBeforeDetachEvent;
        public abstract event Action<object?>? OnAfterDetachEvent;

        // Constructor
        private protected NodeBase() {
        }

        // Attach
        internal abstract void Attach(ITree<TThis> owner, object? argument);
        internal abstract void Detach(ITree<TThis> owner, object? argument);

        // Attach
        internal abstract void Attach(TThis owner, object? argument);
        internal abstract void Detach(TThis owner, object? argument);

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected abstract void OnBeforeAttach(object? argument);
        protected abstract void OnAfterAttach(object? argument);

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected abstract void OnBeforeDetach(object? argument);
        protected abstract void OnAfterDetach(object? argument);

    }
    public abstract partial class NodeBase<TThis> {

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public abstract bool IsRoot { get; }
        public abstract TThis Root { get; }

        // Parent
        public abstract TThis? Parent { get; }
        public abstract IEnumerable<TThis> Ancestors { get; }
        public abstract IEnumerable<TThis> AncestorsAndSelf { get; }

        // Children
        public abstract IReadOnlyList<TThis> Children { get; }
        public abstract IEnumerable<TThis> Descendants { get; }
        public abstract IEnumerable<TThis> DescendantsAndSelf { get; }

        // Constructor
        //public NodeBase() {
        //}

        // AddChild
        protected abstract void AddChild(TThis child, object? argument);
        protected abstract void RemoveChild(TThis child, object? argument, Action<TThis>? callback);
        protected abstract bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback);
        protected abstract int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback);
        protected abstract void RemoveSelf(object? argument, Action<TThis>? callback);

        // Sort
        protected abstract void Sort(List<TThis> children);

    }
    public abstract partial class NodeBase<TThis> {
        public enum Activity_ {
            Inactive,
            Activating,
            Active,
            Deactivating,
        }

        // Activity
        public abstract Activity_ Activity { get; private protected set; }

        // OnActivate
        public abstract event Action<object?>? OnBeforeActivateEvent;
        public abstract event Action<object?>? OnAfterActivateEvent;
        public abstract event Action<object?>? OnBeforeDeactivateEvent;
        public abstract event Action<object?>? OnAfterDeactivateEvent;

        // Constructor
        //public NodeBase() {
        //}

        // Activate
        private protected abstract void Activate(object? argument);
        private protected abstract void Deactivate(object? argument);

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected abstract void OnBeforeActivate(object? argument);
        protected abstract void OnAfterActivate(object? argument);

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected abstract void OnBeforeDeactivate(object? argument);
        protected abstract void OnAfterDeactivate(object? argument);

    }
}

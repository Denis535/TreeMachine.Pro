# Overview
The library that allows you to easily implement a tree structure.

# Reference
```
namespace System.TreeMachine;
public interface ITree<T> where T : NodeBase<T> {

    protected T? Root { get; set; }

    protected void SetRoot(T? root, object? argument, Action<T>? callback);
    protected void AddRoot(T root, object? argument);
    protected internal void RemoveRoot(T root, object? argument, Action<T>? callback);

}
public abstract partial class NodeBase<TThis> where TThis : NodeBase<TThis> {

    private protected abstract object? Owner { get; set; }
    public abstract ITree<TThis>? Tree { get; }

    public abstract event Action<object?>? OnBeforeAttachEvent;
    public abstract event Action<object?>? OnAfterAttachEvent;
    public abstract event Action<object?>? OnBeforeDetachEvent;
    public abstract event Action<object?>? OnAfterDetachEvent;

    public NodeBase();

    internal abstract void Attach(ITree<TThis> owner, object? argument);
    internal abstract void Detach(ITree<TThis> owner, object? argument);

    internal abstract void Attach(TThis owner, object? argument);
    internal abstract void Detach(TThis owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected abstract void OnBeforeAttach(object? argument);
    protected abstract void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected abstract void OnBeforeDetach(object? argument);
    protected abstract void OnAfterDetach(object? argument);

}
public abstract partial class NodeBase<TThis> {

    [MemberNotNullWhen( false, nameof( Parent ) )] public abstract bool IsRoot { get; }
    public abstract TThis Root { get; }

    public abstract TThis? Parent { get; }
    public abstract IEnumerable<TThis> Ancestors { get; }
    public abstract IEnumerable<TThis> AncestorsAndSelf { get; }

    public abstract IReadOnlyList<TThis> Children { get; }
    public abstract IEnumerable<TThis> Descendants { get; }
    public abstract IEnumerable<TThis> DescendantsAndSelf { get; }

    protected abstract void AddChild(TThis child, object? argument);
    protected abstract void RemoveChild(TThis child, object? argument, Action<TThis>? callback);
    protected abstract bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback);
    protected abstract int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis>? callback);
    protected abstract void RemoveSelf(object? argument, Action<TThis>? callback);

    protected abstract void Sort(List<TThis> children);

}
public abstract partial class NodeBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public abstract Activity_ Activity { get; private protected set; }

    public abstract event Action<object?>? OnBeforeActivateEvent;
    public abstract event Action<object?>? OnAfterActivateEvent;
    public abstract event Action<object?>? OnBeforeDeactivateEvent;
    public abstract event Action<object?>? OnAfterDeactivateEvent;

    private protected abstract void Activate(object? argument);
    private protected abstract void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected abstract void OnBeforeActivate(object? argument);
    protected abstract void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected abstract void OnBeforeDeactivate(object? argument);
    protected abstract void OnAfterDeactivate(object? argument);

}
```

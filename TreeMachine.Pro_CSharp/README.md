# Overview
The library that allows you to easily implement a tree structure.

# Reference
```
namespace System.TreeMachine;
public interface ITree<T> where T : notnull, NodeBase<T> {

    protected T? Root { get; set; }

    protected void AddRoot(T root, object? argument);
    protected internal void RemoveRoot(T root, object? argument, Action<T, object?>? callback);
    protected void RemoveRoot(object? argument, Action<T, object?>? callback);

}
public abstract partial class NodeBase<TThis> where TThis : notnull, NodeBase<TThis> {

    internal object? Owner { get; private set; }
    public ITree<TThis>? Tree { get; }
    public ITree<TThis>? TreeRecursive { get; }

    public event Action<object?>? OnBeforeAttachEvent;
    public event Action<object?>? OnAfterAttachEvent;
    public event Action<object?>? OnBeforeDetachEvent;
    public event Action<object?>? OnAfterDetachEvent;

    public NodeBase();

    internal void Attach(ITree<TThis> owner, object? argument);
    internal void Attach(TThis owner, object? argument);

    internal void Detach(ITree<TThis> owner, object? argument);
    internal void Detach(TThis owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class NodeBase<TThis> {

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public IReadOnlyList<TThis> Children { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    //public NodeBase();

    protected virtual void AddChild(TThis child, object? argument);
    protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<TThis, object?>? callback);
    protected int RemoveChildren(object? argument, Action<TThis, object?>? callback);
    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

    protected virtual void Sort(List<TThis> children);

}
public abstract partial class NodeBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public Activity_ Activity { get; private set; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    //public NodeBase();

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```

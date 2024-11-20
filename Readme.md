# Overview
The library that helps you implement a tree structure.
Let's look in more detail.
The tree has a root node.
Any node has any number of child nodes.
So as a result we get a tree-like structure.

# Reference
```
public interface ITree<T> where T : NodeBase<T> {

    protected T? Root { get; set; }

    protected void SetRoot(T? root, object? argument = null);

}
public abstract class NodeBase<TThis> where TThis : NodeBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public Activity_ Activity { get; }
    public ITree<TThis>? Tree { get; }

    public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public IReadOnlyList<TThis> Children { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public NodeBase();
    protected virtual void DisposeWhenDeactivate();

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

    protected virtual void AddChild(TThis child, object? argument = null);
    protected virtual void RemoveChild(TThis child, object? argument = null);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument = null);
    protected void RemoveChildren(IEnumerable<TThis> children, object? argument = null);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument = null);
    protected void RemoveSelf(object? argument = null);

    protected virtual void Sort(List<TThis> children);

}
public abstract class NodeBase2<TThis> : NodeBase<TThis> where TThis : NodeBase2<TThis> {
    
    public event Action<TThis, object?>? OnBeforeDescendantActivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantActivateEvent;
    public event Action<TThis, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantDeactivateEvent;

    public NodeBase2();

    protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

}
```

# Overview
The library that helps you implement the hierarchical tree structure.
Let's look in more detail.
The tree has a root node.
Any node has any number of child nodes.
So as a result we get a tree-like structure.

# Reference
```
public interface ITree {
}
public interface ITree<T> : ITree where T : NodeBase<T> {

    protected T? Root { get; set; }

    protected void SetRoot(T? root, object? argument = null);

}
public abstract class NodeBase {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public Activity_ Activity { get; }
    public ITree? Tree { get; }

    public bool IsRoot { get; }
    public NodeBase Root { get; }

    public NodeBase? Parent { get; }
    public IEnumerable<NodeBase> Ancestors { get; }
    public IEnumerable<NodeBase> AncestorsAndSelf { get; }

    public IReadOnlyList<NodeBase> Children { get; }
    public IEnumerable<NodeBase> Descendants { get; }
    public IEnumerable<NodeBase> DescendantsAndSelf { get; }

}
public abstract class NodeBase<TThis> : NodeBase where TThis : NodeBase<TThis> {

    public new ITree<TThis>? Tree { get; }

    public new bool IsRoot { get; }
    public new TThis Root { get; }
    
    public new TThis? Parent { get; }
    public new IEnumerable<TThis> Ancestors { get; }
    public new IEnumerable<TThis> AncestorsAndSelf { get; }
    
    public new IEnumerable<TThis> Children { get; }
    public new IEnumerable<TThis> Descendants { get; }
    public new IEnumerable<TThis> DescendantsAndSelf { get; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public event Action<TThis, object?>? OnBeforeDescendantActivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantActivateEvent;
    public event Action<TThis, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantDeactivateEvent;

    public NodeBase();
    protected virtual void DisposeWhenDeactivate();

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

    protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

    protected virtual void AddChild(TThis child, object? argument = null);
    protected virtual void RemoveChild(TThis child, object? argument = null);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument = null);
    protected void RemoveChildren(IEnumerable<TThis> children, object? argument = null);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument = null);
    protected void RemoveSelf(object? argument = null);

    protected virtual void Sort(List<NodeBase> children);

}
```

# Overview
The library that helps you implement a tree structure.

# Reference
```
public interface ITree<T> where T : NodeBase<T> {

    protected T? Root { get; }

    protected internal void SetRoot(T? root, object? argument = null);

}
public abstract class NodeBase<TThis> where TThis : NodeBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    private object? Owner { get; set; }
    public Activity_ Activity { get; private set; }

    public ITree<TThis>? Tree { get; }
    
    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }
    
    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    private List<TThis> Children_ { get; }
    public IReadOnlyList<TThis> Children { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public NodeBase();

    internal void Attach(ITree<TThis> owner, object? argument);
    internal void Detach(ITree<TThis> owner, object? argument);

    private void Attach(TThis owner, object? argument);
    private void Detach(TThis owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected abstract void OnDetach(object? argument);

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    private protected virtual void OnBeforeActivateInternal(object? argument);
    private protected virtual void OnAfterActivateInternal(object? argument);
    private protected virtual void OnBeforeDeactivateInternal(object? argument);
    private protected virtual void OnAfterDeactivateInternal(object? argument);

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

    private protected sealed override void OnBeforeActivateInternal(object? argument);
    private protected sealed override void OnAfterActivateInternal(object? argument);
    private protected sealed override void OnBeforeDeactivateInternal(object? argument);
    private protected sealed override void OnAfterDeactivateInternal(object? argument);

    protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

}
```

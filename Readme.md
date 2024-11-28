# Overview
The library that helps you implement a tree structure.

# Reference
```
namespace System.TreeMachine;
public interface ITree<T> where T : NodeBase<T> {
    
    protected T? Root { get; set; }

    protected void SetRoot(T? root, object? argument, Action<T>? dispose);
    protected void AddRoot(T root, object? argument );
    protected internal void RemoveRoot(T root, object? argument, Action<T>? dispose);

}
public abstract class NodeBase<TThis> where TThis : NodeBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    private protected object? Owner { get; private set; }
    public Activity_ Activity { get; private protected set; }

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

    public event Action<object?>? OnBeforeAttachEvent;
    public event Action<object?>? OnAfterAttachEvent;
    public event Action<object?>? OnBeforeDetachEvent;
    public event Action<object?>? OnAfterDetachEvent;

    private protected NodeBase();

    internal void Attach(ITree<TThis> owner, object? argument);
    internal void Detach(ITree<TThis> owner, object? argument);

    private void Attach(TThis owner, object? argument);
    private void Detach(TThis owner, object? argument);

    protected virtual void OnBeforeAttach(object? argument);
    protected abstract void OnAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected abstract void OnDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

    private protected abstract void Activate(object? argument);
    private protected abstract void Deactivate(object? argument);

    protected virtual void AddChild(TThis child, object? argument);
    protected virtual void RemoveChild(TThis child, object? argument, Action<T>? dispose);
    protected bool RemoveChild(Func<TThis, bool> predicate, object? argument, Action<T>? dispose);
    protected int RemoveChildren(Func<TThis, bool> predicate, object? argument, Action<T>? dispose);
    protected void RemoveSelf(object? argument, Action<T>? dispose);

    protected virtual void Sort(List<TThis> children);

}
public abstract class NodeBase2<TThis> : NodeBase<TThis> where TThis : NodeBase2<TThis> {

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public NodeBase2();

    private protected sealed override void Activate(object? argument);
    private protected sealed override void Deactivate(object? argument);

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract class NodeBase3<TThis> : NodeBase2<TThis> where TThis : NodeBase3<TThis> {
    
    public event Action<TThis, object?>? OnBeforeDescendantAttachEvent;
    public event Action<TThis, object?>? OnAfterDescendantAttachEvent;
    public event Action<TThis, object?>? OnBeforeDescendantDetachEvent;
    public event Action<TThis, object?>? OnAfterDescendantDetachEvent;

    public event Action<TThis, object?>? OnBeforeDescendantActivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantActivateEvent;
    public event Action<TThis, object?>? OnBeforeDescendantDeactivateEvent;
    public event Action<TThis, object?>? OnAfterDescendantDeactivateEvent;

    public NodeBase3();

    protected override void OnBeforeAttach(object? argument);
    protected override void OnAfterAttach(object? argument;
    protected override void OnBeforeDetach(object? argument);
    protected override void OnAfterDetach(object? argument);

    protected override void OnBeforeActivate(object? argument);
    protected override void OnAfterActivate(object? argument);
    protected override void OnBeforeDeactivate(object? argument);
    protected override void OnAfterDeactivate(object? argument);

    protected abstract void OnBeforeDescendantAttach(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantAttach(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDetach(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDetach(TThis descendant, object? argument);

    protected abstract void OnBeforeDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(TThis descendant, object? argument);
    protected abstract void OnBeforeDescendantDeactivate(TThis descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(TThis descendant, object? argument);

}
```

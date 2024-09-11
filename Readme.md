# Overview
The library for implementation of advanced hierarchical tree structure.
Let's look in more detail.
The tree has a root node.
Any node has any number of child nodes.
So as a result we get a tree-like structure.

# Reference
```
public interface ITree {
}
public interface ITree<T> : ITree where T : NodeBase<T> {
    protected T? Root { get; }
    protected void SetRoot(T? root, object? argument = null);
}
public abstract class NodeBase {
    public enum State_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }
}
public abstract class NodeBase<T> : NodeBase where T : NodeBase<T> {

    public State_ State { get; private set; }
    public ITree<T>? Tree { get; }
    public T? Parent { get; }
    public bool IsRoot { get; }
    public T Root { get; }
    public IEnumerable<T> Ancestors { get; }
    public IEnumerable<T> AncestorsAndSelf { get; }
    public IReadOnlyList<T> Children { get; }
    public IEnumerable<T> Descendants { get; }
    public IEnumerable<T> DescendantsAndSelf { get; }
    public Action<object?>? OnBeforeActivateEvent;
    public Action<object?>? OnAfterActivateEvent;
    public Action<object?>? OnBeforeDeactivateEvent;
    public Action<object?>? OnAfterDeactivateEvent;
    public Action<T, object?>? OnBeforeDescendantActivateEvent;
    public Action<T, object?>? OnAfterDescendantActivateEvent;
    public Action<T, object?>? OnBeforeDescendantDeactivateEvent;
    public Action<T, object?>? OnAfterDescendantDeactivateEvent;

    public NodeBase() {
    }

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

    protected abstract void OnBeforeDescendantActivate(T descendant, object? argument);
    protected abstract void OnAfterDescendantActivate(T descendant, object? argument);

    protected abstract void OnBeforeDescendantDeactivate(T descendant, object? argument);
    protected abstract void OnAfterDescendantDeactivate(T descendant, object? argument);

    protected virtual void AddChild(T child, object? argument = null);
    protected virtual void RemoveChild(T child, object? argument = null);
    protected bool RemoveChild(Func<T, bool> predicate, object? argument = null);
    protected void RemoveChildren(IEnumerable<T> children, object? argument = null);
    protected int RemoveChildren(Func<T, bool> predicate, object? argument = null);
    protected void RemoveSelf(object? argument = null);

    protected virtual void Sort(List<T> children);

}
```

# Example
```
internal class Tree : TreeBase<NodeBase2>, IDisposable {

    public Tree() {
        SetRoot( new RootNode() );
    }
    public void Dispose() {
        SetRoot( null );
    }

}
internal abstract class NodeBase2 : NodeBase<NodeBase2> {

    protected override void OnActivate(object? argument) {
        TestContext.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
    }

    protected override void OnBeforeDescendantActivate(NodeBase2 descendant, object? argument) {
    }
    protected override void OnAfterDescendantActivate(NodeBase2 descendant, object? argument) {
    }
    protected override void OnBeforeDescendantDeactivate(NodeBase2 descendant, object? argument) {
    }
    protected override void OnAfterDescendantDeactivate(NodeBase2 descendant, object? argument) {
    }

}
internal class RootNode : NodeBase2 {

    public RootNode() {
        AddChild( new A_Node() );
        AddChild( new B_Node() );
    }

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class A_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
        AddChild( new A1_Node() );
        AddChild( new A2_Node() );
    }
    protected override void OnDeactivate(object? argument) {
        RemoveChildren( i => true );
        base.OnDeactivate( argument );
    }

}
internal class B_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
        AddChild( new B1_Node() );
        AddChild( new B2_Node() );
    }
    protected override void OnDeactivate(object? argument) {
        //RemoveChildren( i => true );
        base.OnDeactivate( argument );
    }

}
internal class A1_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class A2_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B1_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B2_Node : NodeBase2 {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
```

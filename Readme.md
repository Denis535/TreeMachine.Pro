# Overview
The base classes for advanced hierarchical tree implementation.

# Reference
```
public interface IHierarchy {
}
public interface IHierarchy<T> : IHierarchy where T : NodeBase<T> {

    public T? Root { get; protected set; }

    public sealed void AddRoot(T root, object? argument = null);
    public sealed void RemoveRoot(T root, object? argument = null);
    public sealed void RemoveRoot(object? argument = null);

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
    public IHierarchy<T>? Hierarchy { get; }
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

}
```

# Example
```
internal class Hierarchy : IHierarchy<Node>, IDisposable {

    // Root
    Node? IHierarchy<Node>.Root { get; set; }
    protected Node? Root => ((IHierarchy<Node>) this).Root;

    // Constructor
    public Hierarchy() {
        AddRoot( new RootNode() );
    }
    public void Dispose() {
        RemoveRoot();
    }

    // AddRoot
    protected void AddRoot(Node root, object? argument = null) {
        ((IHierarchy<Node>) this).AddRoot( root, argument );
    }
    protected void RemoveRoot(Node root, object? argument = null) {
        ((IHierarchy<Node>) this).RemoveRoot( root, argument );
    }
    protected void RemoveRoot(object? argument = null) {
        ((IHierarchy<Node>) this).RemoveRoot( argument );
    }

}
internal abstract class Node : NodeBase<Node> {

    protected override void OnActivate(object? argument) {
        TestContext.WriteLine( "OnActivate: " + GetType().Name );
    }
    protected override void OnDeactivate(object? argument) {
        TestContext.WriteLine( "OnDeactivate: " + GetType().Name );
    }

    protected override void OnBeforeDescendantActivate(Node descendant, object? argument) {
    }
    protected override void OnAfterDescendantActivate(Node descendant, object? argument) {
    }
    protected override void OnBeforeDescendantDeactivate(Node descendant, object? argument) {
    }
    protected override void OnAfterDescendantDeactivate(Node descendant, object? argument) {
    }

}
// Root
internal class RootNode : Node {

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
// Level-1
internal class A_Node : Node {

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
internal class B_Node : Node {

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
// Level-2
internal class A1_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class A2_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B1_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
internal class B2_Node : Node {

    protected override void OnActivate(object? argument) {
        base.OnActivate( argument );
    }
    protected override void OnDeactivate(object? argument) {
        base.OnDeactivate( argument );
    }

}
```

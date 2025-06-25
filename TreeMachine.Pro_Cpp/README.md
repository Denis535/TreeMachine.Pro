# Overview
The library that allows you to easily implement a tree structure.

# Reference
```
namespace TreeMachine {
    template <typename T>
    class TreeBase {

        private:
        T *m_Root = nullptr;

        protected:
        [[nodiscard]] T *Root() const;

        protected:
        explicit TreeBase();

        public:
        explicit TreeBase(const TreeBase &other) = delete;
        explicit TreeBase(TreeBase &&other) = delete;
        virtual ~TreeBase();

        protected:
        virtual void AddRoot(T *const root, const any argument);                                                              // overriding methods must invoke base implementation
        virtual void RemoveRoot(T *const root, const any argument, const function<void(const T *const, const any)> callback); // overriding methods must invoke base implementation
        void RemoveRoot(const any argument, const function<void(const T *const, const any)> callback);

        public:
        TreeBase &operator=(const TreeBase &other) = delete;
        TreeBase &operator=(TreeBase &&other) = delete;
    };
}
```
```
namespace TreeMachine {
    template <typename TThis>
    class NodeBase {
        friend class TreeBase<TThis>;

        public:
        enum class EActivity : uint8_t {
            Inactive,
            Activating,
            Active,
            Deactivating,
        };

        private:
        variant<nullptr_t, TreeBase<TThis> *, TThis *> m_Owner = nullptr;

        private:
        EActivity m_Activity = EActivity::Inactive;

        private:
        list<TThis *> m_Children = list<TThis *>(0);

        private:
        function<void(const any)> m_OnBeforeAttachCallback = nullptr;
        function<void(const any)> m_OnAfterAttachCallback = nullptr;
        function<void(const any)> m_OnBeforeDetachCallback = nullptr;
        function<void(const any)> m_OnAfterDetachCallback = nullptr;

        private:
        function<void(const any)> m_OnBeforeActivateCallback = nullptr;
        function<void(const any)> m_OnAfterActivateCallback = nullptr;
        function<void(const any)> m_OnBeforeDeactivateCallback = nullptr;
        function<void(const any)> m_OnAfterDeactivateCallback = nullptr;

        public:
        [[nodiscard]] TreeBase<TThis> *Tree() const;
        [[nodiscard]] TreeBase<TThis> *TreeRecursive() const;

        public:
        [[nodiscard]] bool IsRoot() const;
        [[nodiscard]] const TThis *Root() const;
        [[nodiscard]] TThis *Root();

        public:
        [[nodiscard]] TThis *Parent() const;
        [[nodiscard]] vector<TThis *> Ancestors() const;
        [[nodiscard]] vector<const TThis *> AncestorsAndSelf() const;
        [[nodiscard]] vector<TThis *> AncestorsAndSelf();

        public:
        [[nodiscard]] EActivity Activity() const;

        public:
        [[nodiscard]] const list<TThis *> &Children() const;
        [[nodiscard]] vector<TThis *> Descendants() const;
        [[nodiscard]] vector<const TThis *> DescendantsAndSelf() const;
        [[nodiscard]] vector<TThis *> DescendantsAndSelf();

        public:
        [[nodiscard]] const function<void(const any)> &OnBeforeAttachCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterAttachCallback() const;
        [[nodiscard]] const function<void(const any)> &OnBeforeDetachCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterDetachCallback() const;

        public:
        [[nodiscard]] const function<void(const any)> &OnBeforeActivateCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterActivateCallback() const;
        [[nodiscard]] const function<void(const any)> &OnBeforeDeactivateCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterDeactivateCallback() const;

        protected:
        explicit NodeBase();

        public:
        explicit NodeBase(const NodeBase &other) = delete;
        explicit NodeBase(NodeBase &&other) = delete;
        virtual ~NodeBase();

        protected:
        void Attach(TreeBase<TThis> *const owner, const any argument);
        void Attach(TThis *const owner, const any argument);
        void Detach(TreeBase<TThis> *const owner, const any argument);
        void Detach(TThis *const owner, const any argument);

        private:
        void Activate(const any argument);
        void Deactivate(const any argument);

        protected:
        virtual void OnAttach(const any argument);       // overriding methods must invoke base implementation
        virtual void OnBeforeAttach(const any argument); // overriding methods must invoke base implementation
        virtual void OnAfterAttach(const any argument);  // overriding methods must invoke base implementation
        virtual void OnDetach(const any argument);       // overriding methods must invoke base implementation
        virtual void OnBeforeDetach(const any argument); // overriding methods must invoke base implementation
        virtual void OnAfterDetach(const any argument);  // overriding methods must invoke base implementation

        protected:
        virtual void OnActivate(const any argument);         // overriding methods must invoke base implementation
        virtual void OnBeforeActivate(const any argument);   // overriding methods must invoke base implementation
        virtual void OnAfterActivate(const any argument);    // overriding methods must invoke base implementation
        virtual void OnDeactivate(const any argument);       // overriding methods must invoke base implementation
        virtual void OnBeforeDeactivate(const any argument); // overriding methods must invoke base implementation
        virtual void OnAfterDeactivate(const any argument);  // overriding methods must invoke base implementation

        protected:
        virtual void AddChild(TThis *const child, const any argument); // overriding methods must invoke base implementation
        void AddChildren(const vector<TThis *> &children, const any argument);
        virtual void RemoveChild(TThis *const child, const any argument, const function<void(const TThis *const, const any)> callback); // overriding methods must invoke base implementation
        bool RemoveChild(const function<bool(const TThis *const)> predicate, const any argument, const function<void(const TThis *const, const any)> callback);
        int32_t RemoveChildren(const function<bool(const TThis *const)> predicate, const any argument, const function<void(const TThis *const, const any)> callback);
        int32_t RemoveChildren(const any argument, const function<void(const TThis *const, const any)> callback);
        void RemoveSelf(const any argument, const function<void(const TThis *const, const any)> callback);

        protected:
        virtual void Sort(list<TThis *> &children) const;

        public:
        void OnBeforeAttachCallback(const function<void(const any)> callback);
        void OnAfterAttachCallback(const function<void(const any)> callback);
        void OnBeforeDetachCallback(const function<void(const any)> callback);
        void OnAfterDetachCallback(const function<void(const any)> callback);

        public:
        void OnBeforeActivateCallback(const function<void(const any)> callback);
        void OnAfterActivateCallback(const function<void(const any)> callback);
        void OnBeforeDeactivateCallback(const function<void(const any)> callback);
        void OnAfterDeactivateCallback(const function<void(const any)> callback);

        public:
        NodeBase &operator=(const NodeBase &other) = delete;
        NodeBase &operator=(NodeBase &&other) = delete;
    };
}
```
```
namespace TreeMachine {
    template <typename TThis>
    class NodeBase2 : public NodeBase<TThis> {

        private:
        function<void(TThis *const, const any)> m_OnBeforeDescendantAttachCallback;
        function<void(TThis *const, const any)> m_OnAfterDescendantAttachCallback;
        function<void(TThis *const, const any)> m_OnBeforeDescendantDetachCallback;
        function<void(TThis *const, const any)> m_OnAfterDescendantDetachCallback;

        private:
        function<void(TThis *const, const any)> m_OnBeforeDescendantActivateCallback;
        function<void(TThis *const, const any)> m_OnAfterDescendantActivateCallback;
        function<void(TThis *const, const any)> m_OnBeforeDescendantDeactivateCallback;
        function<void(TThis *const, const any)> m_OnAfterDescendantDeactivateCallback;

        public:
        [[nodiscard]] const function<void(TThis *const, const any)> &OnBeforeDescendantAttachCallback();
        [[nodiscard]] const function<void(TThis *const, const any)> &OnAfterDescendantAttachCallback();
        [[nodiscard]] const function<void(TThis *const, const any)> &OnBeforeDescendantDetachCallback();
        [[nodiscard]] const function<void(TThis *const, const any)> &OnAfterDescendantDetachCallback();

        public:
        [[nodiscard]] const function<void(TThis *const, const any)> &OnBeforeDescendantActivateCallback();
        [[nodiscard]] const function<void(TThis *const, const any)> &OnAfterDescendantActivateCallback();
        [[nodiscard]] const function<void(TThis *const, const any)> &OnBeforeDescendantDeactivateCallback();
        [[nodiscard]] const function<void(TThis *const, const any)> &OnAfterDescendantDeactivateCallback();

        protected:
        explicit NodeBase2();

        public:
        explicit NodeBase2(const NodeBase2 &other) = delete;
        explicit NodeBase2(NodeBase2 &&other) = delete;
        ~NodeBase2() override;

        protected:
        void OnAttach(const any argument) override;       // overriding methods must invoke base implementation
        void OnBeforeAttach(const any argument) override; // overriding methods must invoke base implementation
        void OnAfterAttach(const any argument) override;  // overriding methods must invoke base implementation
        void OnDetach(const any argument) override;       // overriding methods must invoke base implementation
        void OnBeforeDetach(const any argument) override; // overriding methods must invoke base implementation
        void OnAfterDetach(const any argument) override;  // overriding methods must invoke base implementation
        virtual void OnBeforeDescendantAttach(TThis *const descendant, const any argument);
        virtual void OnAfterDescendantAttach(TThis *const descendant, const any argument);
        virtual void OnBeforeDescendantDetach(TThis *const descendant, const any argument);
        virtual void OnAfterDescendantDetach(TThis *const descendant, const any argument);

        protected:
        void OnActivate(const any argument) override;         // overriding methods must invoke base implementation
        void OnBeforeActivate(const any argument) override;   // overriding methods must invoke base implementation
        void OnAfterActivate(const any argument) override;    // overriding methods must invoke base implementation
        void OnDeactivate(const any argument) override;       // overriding methods must invoke base implementation
        void OnBeforeDeactivate(const any argument) override; // overriding methods must invoke base implementation
        void OnAfterDeactivate(const any argument) override;  // overriding methods must invoke base implementation
        virtual void OnBeforeDescendantActivate(TThis *const descendant, const any argument);
        virtual void OnAfterDescendantActivate(TThis *const descendant, const any argument);
        virtual void OnBeforeDescendantDeactivate(TThis *const descendant, const any argument);
        virtual void OnAfterDescendantDeactivate(TThis *const descendant, const any argument);

        public:
        void OnBeforeDescendantAttachCallback(const function<void(TThis *const, const any)> callback);
        void OnAfterDescendantAttachCallback(const function<void(TThis *const, const any)> callback);
        void OnBeforeDescendantDetachCallback(const function<void(TThis *const, const any)> callback);
        void OnAfterDescendantDetachCallback(const function<void(TThis *const, const any)> callback);

        public:
        void OnBeforeDescendantActivateCallback(const function<void(TThis *const, const any)> callback);
        void OnAfterDescendantActivateCallback(const function<void(TThis *const, const any)> callback);
        void OnBeforeDescendantDeactivateCallback(const function<void(TThis *const, const any)> callback);
        void OnAfterDescendantDeactivateCallback(const function<void(TThis *const, const any)> callback);

        public:
        NodeBase2 &operator=(const NodeBase2 &other) = delete;
        NodeBase2 &operator=(NodeBase2 &&other) = delete;
    };
}
```

# Example
```
#pragma once
#include "TreeMachine/TreeMachine.h"

namespace TreeMachine {
using namespace std;

    class Node : public NodeBase2<Node> {

        public:
        explicit Node() = default;
        explicit Node(Node &other) = delete;
        explicit Node(Node &&other) = delete;
        ~Node() override {
            NodeBase::RemoveChildren(
                nullptr,
                [](const auto *const child, [[maybe_unused]] const any arg) { delete child; });
        }

        protected:
        void OnAttach(const any argument) override {
            NodeBase2::OnAttach(argument);
            cout << "OnAttach: " << typeid(*this).name() << endl;
        }
        void OnBeforeAttach(const any argument) override {
            NodeBase2::OnBeforeAttach(argument);
        }
        void OnAfterAttach(const any argument) override {
            NodeBase2::OnAfterAttach(argument);
        }

        protected:
        void OnDetach(const any argument) override {
            cout << "OnDetach: " << typeid(*this).name() << endl;
            NodeBase2::OnDetach(argument);
        }
        void OnBeforeDetach(const any argument) override {
            NodeBase2::OnBeforeDetach(argument);
        }
        void OnAfterDetach(const any argument) override {
            NodeBase2::OnAfterDetach(argument);
        }

        protected:
        void OnBeforeDescendantAttach(Node *descendant, const any argument) override {
            NodeBase2::OnBeforeDescendantAttach(descendant, argument);
        }
        void OnAfterDescendantAttach(Node *descendant, const any argument) override {
            NodeBase2::OnAfterDescendantAttach(descendant, argument);
        }
        void OnBeforeDescendantDetach(Node *descendant, const any argument) override {
            NodeBase2::OnBeforeDescendantDetach(descendant, argument);
        }
        void OnAfterDescendantDetach(Node *descendant, const any argument) override {
            NodeBase2::OnAfterDescendantDetach(descendant, argument);
        }

        protected:
        void OnActivate(const any argument) override {
            NodeBase2::OnActivate(argument);
            cout << "OnActivate: " << typeid(*this).name() << endl;
        }
        void OnBeforeActivate(const any argument) override {
            NodeBase2::OnBeforeActivate(argument);
        }
        void OnAfterActivate(const any argument) override {
            NodeBase2::OnAfterActivate(argument);
        }

        protected:
        void OnDeactivate(const any argument) override {
            cout << "OnDeactivate: " << typeid(*this).name() << endl;
            NodeBase2::OnDeactivate(argument);
        }
        void OnBeforeDeactivate(const any argument) override {
            NodeBase2::OnBeforeDeactivate(argument);
        }
        void OnAfterDeactivate(const any argument) override {
            NodeBase2::OnAfterDeactivate(argument);
        }

        protected:
        void OnBeforeDescendantActivate(Node *descendant, const any argument) override {
            NodeBase2::OnBeforeDescendantActivate(descendant, argument);
        }
        void OnAfterDescendantActivate(Node *descendant, const any argument) override {
            NodeBase2::OnAfterDescendantActivate(descendant, argument);
        }
        void OnBeforeDescendantDeactivate(Node *descendant, const any argument) override {
            NodeBase2::OnBeforeDescendantDeactivate(descendant, argument);
        }
        void OnAfterDescendantDeactivate(Node *descendant, const any argument) override {
            NodeBase2::OnAfterDescendantDeactivate(descendant, argument);
        }

        public:
        using NodeBase::AddChild;
        using NodeBase::AddChildren;
        using NodeBase::RemoveChild;
        using NodeBase::RemoveChildren;
        using NodeBase::RemoveSelf;

        public:
        Node &operator=(const Node &other) = delete;
        Node &operator=(Node &&other) = delete;
    };
    class Root final : public Node {
    };
    class A final : public Node {
    };
    class B final : public Node {
    };
    class Tree final : public TreeBase<Node> {

        public:
        using TreeBase::Root;

        public:
        explicit Tree() = default;
        explicit Tree(Tree &other) = delete;
        explicit Tree(Tree &&other) = delete;
        ~Tree() override {
            if (this->Root() != nullptr) {
                TreeBase::RemoveRoot(
                    nullptr,
                    [](const auto *const root, [[maybe_unused]] const any arg) { delete root; });
            }
        }

        public:
        using TreeBase::AddRoot;
        using TreeBase::RemoveRoot;

        public:
        Tree &operator=(const Tree &other) = delete;
        Tree &operator=(Tree &&other) = delete;
    };
}
```

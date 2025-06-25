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

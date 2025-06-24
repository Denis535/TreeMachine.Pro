#pragma once
#include "TreeMachine/TreeMachine.h"

namespace TreeMachine {
    using namespace std;

    class Node : public NodeBase<Node> {

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
        void OnAttach([[maybe_unused]] const any argument) override {
            cout << "OnAttach: " << typeid(*this).name() << endl;
        }
        void OnDetach([[maybe_unused]] const any argument) override {
            cout << "OnDetach: " << typeid(*this).name() << endl;
        }
        // void OnBeforeDescendantAttach(NodeBase* descendant, const any argument) override {
        // }
        // void OnAfterDescendantAttach(NodeBase* descendant, const any argument) override {
        // }
        // void OnBeforeDescendantDetach(NodeBase* descendant, const any argument) override {
        // }
        // void OnAfterDescendantDetach(NodeBase* descendant, const any argument) override {
        // }

        protected:
        void OnActivate([[maybe_unused]] const any argument) override {
            cout << "OnActivate: " << typeid(*this).name() << endl;
        }
        void OnDeactivate([[maybe_unused]] const any argument) override {
            cout << "OnDeactivate: " << typeid(*this).name() << endl;
        }
        // void OnBeforeDescendantActivate(NodeBase* descendant, any argument) override {
        // }
        // void OnAfterDescendantActivate(NodeBase* descendant, any argument) override {
        // }
        // void OnBeforeDescendantDeactivate(NodeBase* descendant, any argument) override {
        // }
        // void OnAfterDescendantDeactivate(NodeBase* descendant, any argument) override {
        // }

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

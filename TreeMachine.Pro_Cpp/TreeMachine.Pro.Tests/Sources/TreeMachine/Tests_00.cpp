#include "gtest/gtest.h"
#include "TreeMachine/TreeMachine.h"

namespace TreeMachine {
    using namespace std;

    class Node : public NodeBase {

        public:
        explicit Node() {
        }
        explicit Node(Node &other) = delete;
        explicit Node(Node &&other) = delete;
        ~Node() override {
            this->RemoveChildren(
                []([[maybe_unused]] auto *const child) { return true; },
                nullptr,
                [](auto *const child, [[maybe_unused]] const any arg) { delete child; });
        }

        public:
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

        public:
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
    class Tree final : public TreeBase {

        public:
        using TreeBase::Root;

        public:
        explicit Tree() {
        }
        explicit Tree(Tree &other) = delete;
        explicit Tree(Tree &&other) = delete;
        ~Tree() override {
            if (this->Root() != nullptr) {
                this->RemoveRoot(
                    nullptr,
                    [](auto *const root, [[maybe_unused]] const any arg) { delete root; });
            }
        }

        public:
        using TreeBase::AddRoot;
        using TreeBase::RemoveRoot;

        public:
        Tree &operator=(const Tree &other) = delete;
        Tree &operator=(Tree &&other) = delete;
    };

    //     TEST(Tests_00, Test) { // NOLINT
    // #if defined(__clang__) && !defined(_MSC_VER)
    //         SUCCEED() << "Compiler: Clang" << endl;
    // #elif defined(__clang__) && defined(_MSC_VER)
    //         SUCCEED() << "Compiler: Clang-cl (Clang with MSVC compatibility)" << endl;
    // #elif defined(_MSC_VER)
    //         SUCCEED() << "Compiler: MSVC" << endl;
    // #elif defined(__GNUC__)
    //         SUCCEED() << "Compiler: GCC" << endl;
    // #else
    //         SUCCEED() << "Compiler: Unknown" << endl;
    // #endif
    //     }

    TEST(Tests_00, Test_00) { // NOLINT
        // new tree, add root, add children
        // remove children, remove root, delete tree
        auto *const tree = new Tree();

        tree->AddRoot(new Root(), nullptr);
        EXPECT_NE(tree->Root(), nullptr);
        EXPECT_EQ(tree->Root()->Tree(), tree);
        EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
        EXPECT_EQ(tree->Root()->Parent(), nullptr);
        EXPECT_EQ(tree->Root()->Activity(), NodeBase::EActivity::Active);
        EXPECT_EQ(tree->Root()->Children()->size(), 0);

        dynamic_cast<Root *>(tree->Root())->AddChild(new A(), nullptr);
        dynamic_cast<Root *>(tree->Root())->AddChild(new B(), nullptr);
        EXPECT_EQ(tree->Root()->Children()->size(), 2);
        for (const auto *const child : *tree->Root()->Children()) {
            EXPECT_EQ(child->Tree(), nullptr);
            EXPECT_EQ(child->TreeRecursive(), tree);
            EXPECT_EQ(child->Parent(), tree->Root());
            EXPECT_EQ(child->Activity(), NodeBase::EActivity::Active);
            EXPECT_EQ(child->Children()->size(), 0);
        }

        dynamic_cast<Root *>(tree->Root())->RemoveChildren([]([[maybe_unused]] auto *const child) { return true; }, nullptr, [](auto *const child, [[maybe_unused]] const auto arg) { delete child; });
        EXPECT_EQ(tree->Root()->Children()->size(), 0);

        tree->RemoveRoot(nullptr, [](auto *const root, [[maybe_unused]] const auto arg) { delete root; }); // NOLINT
        EXPECT_EQ(tree->Root(), nullptr);

        delete tree;
    }

    TEST(Tests_00, Test_01) { // NOLINT
        // new tree, add root, add children
        // delete tree
        auto *const tree = new Tree();

        tree->AddRoot(new Root(), nullptr);
        EXPECT_NE(tree->Root(), nullptr);
        EXPECT_EQ(tree->Root()->Tree(), tree);
        EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
        EXPECT_EQ(tree->Root()->Parent(), nullptr);
        EXPECT_EQ(tree->Root()->Activity(), NodeBase::EActivity::Active);
        EXPECT_EQ(tree->Root()->Children()->size(), 0);

        dynamic_cast<Root *>(tree->Root())->AddChild(new A(), nullptr);
        dynamic_cast<Root *>(tree->Root())->AddChild(new B(), nullptr);
        EXPECT_EQ(tree->Root()->Children()->size(), 2);
        for (const auto *const child : *tree->Root()->Children()) {
            EXPECT_EQ(child->Tree(), nullptr);
            EXPECT_EQ(child->TreeRecursive(), tree);
            EXPECT_EQ(child->Parent(), tree->Root());
            EXPECT_EQ(child->Activity(), NodeBase::EActivity::Active);
            EXPECT_EQ(child->Children()->size(), 0);
        }

        delete tree;
    }

    // TEST(Tests_00, Test_02) { // NOLINT
    //     // new tree, add root (with children)
    //     // remove root, delete tree
    //     auto *const tree = new Tree();
    //
    //     tree->AddRoot(new Root(), nullptr);
    //
    //     delete tree;
    // }

}

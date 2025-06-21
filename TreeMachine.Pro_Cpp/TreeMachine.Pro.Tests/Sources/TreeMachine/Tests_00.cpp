#include "gtest/gtest.h"
#include "TreeMachine/TreeMachine.h"

namespace TreeMachine {
    using namespace std;

    class Node : public NodeBase {

        public:
        explicit Node() = default;
        explicit Node(Node &other) = delete;
        explicit Node(Node &&other) = delete;
        ~Node() override = default;

        public:
        void OnAttach([[maybe_unused]] const any argument) override {
            // cout << "OnAttach" << endl;
        }
        void OnDetach([[maybe_unused]] const any argument) override {
            // cout << "OnDetach" << endl;
        }
        // void OnBeforeDescendantAttach(NodeBase descendant, const any argument) override {
        // }
        // void OnAfterDescendantAttach(NodeBase descendant, const any argument) override {
        // }
        // void OnBeforeDescendantDetach(NodeBase descendant, const any argument) override {
        // }
        // void OnAfterDescendantDetach(NodeBase descendant, const any argument) override {
        // }

        public:
        void OnActivate([[maybe_unused]] const any argument) override {
            cout << "OnActivate" << endl;
        }
        void OnDeactivate([[maybe_unused]] const any argument) override {
            cout << "OnDeactivate" << endl;
        }
        // void OnBeforeDescendantActivate(NodeBase descendant, any argument) override {
        // }
        // void OnAfterDescendantActivate(NodeBase descendant, any argument) override {
        // }
        // void OnBeforeDescendantDeactivate(NodeBase descendant, any argument) override {
        // }
        // void OnAfterDescendantDeactivate(NodeBase descendant, any argument) override {
        // }

        public:
        using NodeBase::AddChild;
        using NodeBase::RemoveChild;
        using NodeBase::RemoveChildren;
        using NodeBase::RemoveSelf;
        // void AddChild(Node *const child, const any argument) {
        //     NodeBase::AddChild(child, argument);
        // }
        // void RemoveChild(Node *const child, const any argument, const function<void(NodeBase *const, const any)> callback) {
        //     NodeBase::RemoveChild(child, argument, callback);
        // }
        // bool RemoveChild(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
        //     return NodeBase::RemoveChild(predicate, argument, callback);
        // }
        // int32_t RemoveChildren(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
        //     return NodeBase::RemoveChildren(predicate, argument, callback);
        // }
        // void RemoveSelf(const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
        //     NodeBase::RemoveSelf(argument, callback);
        // }

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
        explicit Tree() = default;
        explicit Tree(Tree &other) = delete;
        explicit Tree(Tree &&other) = delete;
        ~Tree() override = default;

        public:
        using TreeBase::SetRoot;
        using TreeBase::AddRoot;
        using TreeBase::RemoveRoot;
        // void SetRoot(Node *const root, const any argument, const function<void(NodeBase *const, const any)> callback) {
        //     TreeBase::SetRoot(root, argument, callback);
        // }
        // void AddRoot(Node *const root, const any argument) {
        //     TreeBase::AddRoot(root, argument);
        // }
        // void RemoveRoot(const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
        //     TreeBase::RemoveRoot(argument, callback);
        // }

        public:
        Tree &operator=(const Tree &other) = delete;
        Tree &operator=(Tree &&other) = delete;
    };

    TEST(Tests_00, Test) { // NOLINT
#if defined(__clang__) && !defined(_MSC_VER)
        cout << "Compiler: Clang" << endl;
#elif defined(__clang__) && defined(_MSC_VER)
        cout << "Compiler: Clang-cl (Clang with MSVC compatibility)" << endl;
#elif defined(_MSC_VER)
        cout << "Compiler: MSVC" << endl;
#elif defined(__GNUC__)
        cout << "Compiler: GCC" << endl;
#else
        cout << "Compiler: Unknown" << endl;
#endif
        SUCCEED();
    }

    TEST(Tests_00, Test_00) { // NOLINT
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

}

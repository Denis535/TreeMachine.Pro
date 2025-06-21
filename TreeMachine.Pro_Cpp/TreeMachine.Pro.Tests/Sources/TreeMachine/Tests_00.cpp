#include "gtest/gtest.h"
#include "TreeMachine/TreeMachine.h"

namespace TreeMachine {
    using namespace std;

    class Node final : public NodeBase {

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
        void AddChild(Node *const child, const any argument) {
            NodeBase::AddChild(child, argument);
        }
        void RemoveChild(Node *const child, const any argument, const function<void(NodeBase *const, const any)> callback) {
            NodeBase::RemoveChild(child, argument, callback);
        }
        bool RemoveChild(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
            return NodeBase::RemoveChild(predicate, argument, callback);
        }
        int32_t RemoveChildren(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
            return NodeBase::RemoveChildren(predicate, argument, callback);
        }
        void RemoveSelf(const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
            NodeBase::RemoveSelf(argument, callback);
        }

        public:
        Node &operator=(const Node &other) = delete;
        Node &operator=(Node &&other) = delete;
    };
    class Tree final : public TreeBase {

        public:
        explicit Tree() = default;
        explicit Tree(Tree &other) = delete;
        explicit Tree(Tree &&other) = delete;
        ~Tree() override = default;

        public:
        void SetRoot(Node *const root, const any argument, const function<void(NodeBase *const, const any)> callback) {
            TreeBase::SetRoot(root, argument, callback);
        }
        void AddRoot(Node *const root, const any argument) {
            TreeBase::AddRoot(root, argument);
        }
        void RemoveRoot(const any argument, const function<void(NodeBase *const, const any)> callback) { // NOLINT
            TreeBase::RemoveRoot(argument, callback);
        }

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
        tree->AddRoot(new Node(), nullptr);
        tree->RemoveRoot(nullptr, [](auto *root, [[maybe_unused]] auto arg) { delete root; });
        delete tree;
    }

}

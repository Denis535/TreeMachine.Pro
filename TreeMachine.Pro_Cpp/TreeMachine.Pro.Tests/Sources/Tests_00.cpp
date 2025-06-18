#include "gtest/gtest.h"
#include "TreeMachine/TreeMachine.h"

using namespace std;
using namespace TreeMachine;

class Node final : public NodeBase {
    public:
    explicit Node() = default;
    explicit Node(Node &other) = delete;
    explicit Node(Node &&other) = delete;
    ~Node() override = default;

    public:
    Node &operator=(const Node &other) = delete;
    Node &operator=(Node &&other) = delete;
};

class Tree final : TreeBase {
    public:
    explicit Tree() {
        SetRoot(new Node(), nullptr, nullptr);
    }

    explicit Tree(Tree &other) = delete;
    explicit Tree(Tree &&other) = delete;

    ~Tree() override {
        SetRoot(nullptr, nullptr, [](auto *root, [[maybe_unused]] auto arg) {
            delete root;
        });
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
    auto tree = Tree();
}

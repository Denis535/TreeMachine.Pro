#include "gtest/gtest.h"
#include "Tree.h"

namespace TreeMachine {
    using namespace std;

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
        auto *const tree = new Tree();
        auto *const root = new Root();
        auto *const a = new A();
        auto *const b = new B();

        for (const auto *const node : vector<Node *>{root, a, b}) {
            EXPECT_NE(node, nullptr);
            EXPECT_EQ(node->Tree(), nullptr);
            EXPECT_EQ(node->TreeRecursive(), nullptr);
            EXPECT_EQ(node->Parent(), nullptr);
            EXPECT_EQ(node->Activity(), Node::EActivity::Inactive);
            EXPECT_EQ(node->Children().size(), 0);
        }

        {
            // AddRoot
            tree->AddRoot(root, nullptr);
            EXPECT_NE(tree->Root(), nullptr);
            EXPECT_EQ(tree->Root()->Tree(), tree);
            EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
            EXPECT_EQ(tree->Root()->Parent(), nullptr);
            EXPECT_EQ(tree->Root()->Activity(), Node::EActivity::Active);
            EXPECT_EQ(tree->Root()->Children().size(), 0);
        }
        {
            // AddChildren
            tree->Root()->AddChildren(vector<Node *>{a, b}, nullptr);
            EXPECT_NE(tree->Root(), nullptr);
            EXPECT_EQ(tree->Root()->Tree(), tree);
            EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
            EXPECT_EQ(tree->Root()->Parent(), nullptr);
            EXPECT_EQ(tree->Root()->Activity(), Node::EActivity::Active);
            EXPECT_EQ(tree->Root()->Children().size(), 2);
            for (const auto *const child : tree->Root()->Children()) {
                EXPECT_EQ(child->Tree(), nullptr);
                EXPECT_EQ(child->TreeRecursive(), tree);
                EXPECT_EQ(child->Parent(), tree->Root());
                EXPECT_EQ(child->Activity(), Node::EActivity::Active);
                EXPECT_EQ(child->Children().size(), 0);
            }
        }
        {
            // RemoveChildren
            tree->Root()->RemoveChildren(nullptr, [](auto *const child, [[maybe_unused]] const auto arg) { delete child; });
            EXPECT_NE(tree->Root(), nullptr);
            EXPECT_EQ(tree->Root()->Tree(), tree);
            EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
            EXPECT_EQ(tree->Root()->Parent(), nullptr);
            EXPECT_EQ(tree->Root()->Activity(), Node::EActivity::Active);
            EXPECT_EQ(tree->Root()->Children().size(), 0);
        }
        {
            // RemoveRoot
            tree->RemoveRoot(nullptr, [](auto *const root, [[maybe_unused]] const auto arg) { delete root; }); // NOLINT
            EXPECT_EQ(tree->Root(), nullptr);
        }

        delete tree;
    }

    TEST(Tests_00, Test_01) { // NOLINT
        auto *const tree = new Tree();
        auto *const root = new Root();
        auto *const a = new A();
        auto *const b = new B();

        for (const auto *const node : vector<Node *>{root, a, b}) {
            EXPECT_NE(node, nullptr);
            EXPECT_EQ(node->Tree(), nullptr);
            EXPECT_EQ(node->TreeRecursive(), nullptr);
            EXPECT_EQ(node->Parent(), nullptr);
            EXPECT_EQ(node->Activity(), Node::EActivity::Inactive);
            EXPECT_EQ(node->Children().size(), 0);
        }

        {
            // AddRoot
            tree->AddRoot(root, nullptr);
            EXPECT_NE(tree->Root(), nullptr);
            EXPECT_EQ(tree->Root()->Tree(), tree);
            EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
            EXPECT_EQ(tree->Root()->Parent(), nullptr);
            EXPECT_EQ(tree->Root()->Activity(), Node::EActivity::Active);
            EXPECT_EQ(tree->Root()->Children().size(), 0);
        }
        {
            // AddChildren
            tree->Root()->AddChildren(vector<Node *>{a, b}, nullptr);
            EXPECT_NE(tree->Root(), nullptr);
            EXPECT_EQ(tree->Root()->Tree(), tree);
            EXPECT_EQ(tree->Root()->TreeRecursive(), tree);
            EXPECT_EQ(tree->Root()->Parent(), nullptr);
            EXPECT_EQ(tree->Root()->Activity(), Node::EActivity::Active);
            EXPECT_EQ(tree->Root()->Children().size(), 2);
            for (const auto *const child : tree->Root()->Children()) {
                EXPECT_EQ(child->Tree(), nullptr);
                EXPECT_EQ(child->TreeRecursive(), tree);
                EXPECT_EQ(child->Parent(), tree->Root());
                EXPECT_EQ(child->Activity(), Node::EActivity::Active);
                EXPECT_EQ(child->Children().size(), 0);
            }
        }

        delete tree;
    }

}

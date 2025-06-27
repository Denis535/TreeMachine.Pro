#include "gtest/gtest.h"
#include "Tree.h"

namespace TreeMachine {
    using namespace std;

    TEST(Tests_04, Test_00) { // NOLINT
        auto *const root = new Root();
        auto *const a = new A();
        auto *const b = new B();

        for (const auto *const node : vector<Node *>{root, a, b}) {
            EXPECT_NE(node, nullptr);
            EXPECT_EQ(node->Tree_NoRecursive(), nullptr);
            EXPECT_EQ(node->Tree(), nullptr);
            EXPECT_EQ(node->Parent(), nullptr);
            EXPECT_EQ(node->Activity(), Node::Activity_::Inactive);
            EXPECT_EQ(node->Children().size(), 0);
        }

        {
            // AddChildren
            root->AddChildren(vector<Node *>{a, b}, nullptr);
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree_NoRecursive(), nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::Activity_::Inactive);
            EXPECT_EQ(root->Children().size(), 2);
            for (const auto *const child : root->Children()) {
                EXPECT_EQ(child->Tree_NoRecursive(), nullptr);
                EXPECT_EQ(child->Tree(), nullptr);
                EXPECT_EQ(child->Parent(), root);
                EXPECT_EQ(child->Activity(), Node::Activity_::Inactive);
                EXPECT_EQ(child->Children().size(), 0);
            }
        }
        {
            // RemoveChildren
            root->RemoveChildren([]([[maybe_unused]] auto *const child) { return true; }, nullptr, [](auto *const child, [[maybe_unused]] const auto arg) { delete child; });
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree_NoRecursive(), nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::Activity_::Inactive);
            EXPECT_EQ(root->Children().size(), 0);
        }

        delete root;
    }

}

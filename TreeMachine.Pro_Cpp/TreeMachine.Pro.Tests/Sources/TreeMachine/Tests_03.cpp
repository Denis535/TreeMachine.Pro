#include "gtest/gtest.h"
#include "Tree.h"

namespace TreeMachine {
    using namespace std;

    TEST(Tests_04, Test_00) { // NOLINT
        auto *const root = new Root();
        auto *const a = new A();
        auto *const b = new B();

        {
            // AddChildren
            root->AddChildren(vector<Node *>{a, b}, nullptr);
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->IsRoot(), true);
            // EXPECT_EQ(root->Root(), root);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::Activity_::Inactive);
            EXPECT_EQ(root->Children().size(), 2);
            for (const auto *const child : root->Children()) {
                EXPECT_EQ(child->Tree(), nullptr);
                EXPECT_EQ(child->IsRoot(), false);
                EXPECT_EQ(child->Root(), root);
                EXPECT_EQ(child->Parent(), root);
                EXPECT_EQ(child->Activity(), Node::Activity_::Inactive);
                EXPECT_EQ(child->Children().size(), 0);
            }
        }
        {
            // RemoveChildren
            root->RemoveChildren(nullptr, [](auto *const child, [[maybe_unused]] const auto arg) { delete child; });
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->IsRoot(), true);
            // EXPECT_EQ(root->Root(), root);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::Activity_::Inactive);
            EXPECT_EQ(root->Children().size(), 0);
        }

        delete root;
    }

}

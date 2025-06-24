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
            EXPECT_EQ(node->Tree(), nullptr);
            EXPECT_EQ(node->TreeRecursive(), nullptr);
            EXPECT_EQ(node->Parent(), nullptr);
            EXPECT_EQ(node->Activity(), Node::EActivity::Inactive);
            EXPECT_EQ(node->Children().size(), 0);
        }

        {
            // AddChildren
            root->AddChildren(vector<Node *>{a, b}, nullptr);
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->TreeRecursive(), nullptr);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::EActivity::Inactive);
            EXPECT_EQ(root->Children().size(), 2);
            for (const auto *const child : root->Children()) {
                EXPECT_EQ(child->Tree(), nullptr);
                EXPECT_EQ(child->TreeRecursive(), nullptr);
                EXPECT_EQ(child->Parent(), root);
                EXPECT_EQ(child->Activity(), Node::EActivity::Inactive);
                EXPECT_EQ(child->Children().size(), 0);
            }
        }
        {
            // RemoveChildren
            root->RemoveChildren([]([[maybe_unused]] auto *const child) { return true; }, nullptr, [](auto *const child, [[maybe_unused]] const auto arg) { delete child; });
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->TreeRecursive(), nullptr);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::EActivity::Inactive);
            EXPECT_EQ(root->Children().size(), 0);
        }

        delete root;
    }

    TEST(Tests_04, Test_01) { // NOLINT
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
            // AddChildren
            root->AddChildren(vector<Node *>{a, b}, nullptr);
            EXPECT_NE(root, nullptr);
            EXPECT_EQ(root->Tree(), nullptr);
            EXPECT_EQ(root->TreeRecursive(), nullptr);
            EXPECT_EQ(root->Parent(), nullptr);
            EXPECT_EQ(root->Activity(), Node::EActivity::Inactive);
            EXPECT_EQ(root->Children().size(), 2);
            for (const auto *const child : root->Children()) {
                EXPECT_EQ(child->Tree(), nullptr);
                EXPECT_EQ(child->TreeRecursive(), nullptr);
                EXPECT_EQ(child->Parent(), root);
                EXPECT_EQ(child->Activity(), Node::EActivity::Inactive);
                EXPECT_EQ(child->Children().size(), 0);
            }
        }
        {
            // AddRoot
            tree->AddRoot(root, nullptr);
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
            // RemoveRoot
            tree->RemoveRoot(nullptr, [](auto *const root, [[maybe_unused]] const auto arg) { delete root; }); // NOLINT
            EXPECT_EQ(tree->Root(), nullptr);
        }

        delete tree;
    }

}

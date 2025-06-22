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

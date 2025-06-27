#pragma once
#include <algorithm>
#include <any>
#include <cassert>
#include <functional>
#include <list>
#include <variant>
#include <vector>
#include "../NodeBase.h"
#include "Helpers.h"

namespace TreeMachine {
    using namespace TreeMachine::Internal;

    template <typename TThis>
    TreeBase<TThis> *NodeBase<TThis>::Tree() const {
        if (auto *const *const tree = get_if<TreeBase<TThis> *>(&this->m_Owner)) {
            return *tree;
        }
        if (const auto *const *const node = get_if<TThis *>(&this->m_Owner)) {
            return (*node)->Tree();
        }
        return nullptr;
    }
    template <typename TThis>
    TreeBase<TThis> *NodeBase<TThis>::Tree_NoRecursive() const {
        if (auto *const *const tree = get_if<TreeBase<TThis> *>(&this->m_Owner)) {
            return *tree;
        }
        return nullptr;
    }

    template <typename TThis>
    bool NodeBase<TThis>::IsRoot() const {
        return this->Parent() == nullptr;
    }
    template <typename TThis>
    const TThis *NodeBase<TThis>::Root() const {
        if (const auto *const parent = this->Parent()) {
            return parent->Root();
        }
        return this;
    }
    template <typename TThis>
    TThis *NodeBase<TThis>::Root() {
        if (auto *const parent = this->Parent()) {
            return parent->Root();
        }
        return this;
    }

    template <typename TThis>
    TThis *NodeBase<TThis>::Parent() const {
        if (auto *const *const node = get_if<TThis *>(&this->m_Owner)) {
            return *node;
        }
        return nullptr;
    }
    template <typename TThis>
    vector<TThis *> NodeBase<TThis>::Ancestors() const {
        auto result = vector<TThis *>();
        if (auto *const parent = this->Parent()) {
            result.push_back(parent);
            auto ancestors = parent->Ancestors();
            result.insert(result.end(), ancestors.begin(), ancestors.end());
        }
        return result;
    }
    template <typename TThis>
    vector<const TThis *> NodeBase<TThis>::AncestorsAndSelf() const {
        auto result = vector<const TThis *>();
        result.push_back(this);
        auto ancestors = this->Ancestors();
        result.insert(result.end(), ancestors.begin(), ancestors.end());
        return result;
    }
    template <typename TThis>
    vector<TThis *> NodeBase<TThis>::AncestorsAndSelf() {
        auto result = vector<TThis *>();
        result.push_back(this);
        auto ancestors = this->Ancestors();
        result.insert(result.end(), ancestors.begin(), ancestors.end());
        return result;
    }

    template <typename TThis>
    typename NodeBase<TThis>::Activity_ NodeBase<TThis>::Activity() const {
        return this->m_Activity;
    }

    template <typename TThis>
    const list<TThis *> &NodeBase<TThis>::Children() const {
        return this->m_Children;
    }
    template <typename TThis>
    vector<TThis *> NodeBase<TThis>::Descendants() const {
        auto result = vector<TThis *>();
        for (auto *const child : this->m_Children) {
            result.push_back(child);
            auto descendants = child->Descendants();
            result.insert(result.end(), descendants.begin(), descendants.end());
        }
        return result;
    }
    template <typename TThis>
    vector<const TThis *> NodeBase<TThis>::DescendantsAndSelf() const {
        auto result = vector<const TThis *>();
        result.push_back(this);
        auto descendants = this->Descendants();
        result.insert(result.end(), descendants.begin(), descendants.end());
        return result;
    }
    template <typename TThis>
    vector<TThis *> NodeBase<TThis>::DescendantsAndSelf() {
        auto result = vector<TThis *>();
        result.push_back(this);
        auto descendants = this->Descendants();
        result.insert(result.end(), descendants.begin(), descendants.end());
        return result;
    }

    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnBeforeAttachCallback() const {
        return this->m_OnBeforeAttachCallback;
    }
    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnAfterAttachCallback() const {
        return this->m_OnAfterAttachCallback;
    }
    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnBeforeDetachCallback() const {
        return this->m_OnBeforeDetachCallback;
    }
    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnAfterDetachCallback() const {
        return this->m_OnAfterDetachCallback;
    }

    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnBeforeActivateCallback() const {
        return this->m_OnBeforeActivateCallback;
    }
    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnAfterActivateCallback() const {
        return this->m_OnAfterActivateCallback;
    }
    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnBeforeDeactivateCallback() const {
        return this->m_OnBeforeDeactivateCallback;
    }
    template <typename TThis>
    const function<void(const any)> &NodeBase<TThis>::OnAfterDeactivateCallback() const {
        return this->m_OnAfterDeactivateCallback;
    }

    template <typename TThis>
    NodeBase<TThis>::NodeBase() {
        static_assert(is_base_of_v<NodeBase, TThis>);
    }

    template <typename TThis>
    NodeBase<TThis>::~NodeBase() {
        assert(this->Tree_NoRecursive() == nullptr && "Node must have no tree");
        assert(this->Parent() == nullptr && "Node must have no parent");
        assert(this->m_Activity == Activity_::Inactive && "Node must be inactive");
        assert(this->m_Children.empty() && "Node must have no children");
    }

    template <typename TThis>
    void NodeBase<TThis>::Attach(TreeBase<TThis> *const tree, const any argument) {
        assert(tree != nullptr && "Argument 'tree' must be non-null");
        assert(this->Tree_NoRecursive() == nullptr && "Node must have no tree");
        assert(this->Parent() == nullptr && "Node must have no parent");
        assert(this->m_Activity == Activity_::Inactive && "Node must be inactive");
        {
            this->m_Owner = tree;
            this->OnBeforeAttach(argument);
            this->OnAttach(argument);
            this->OnAfterAttach(argument);
        }
        if constexpr (true) { // NOLINT
            this->Activate(argument);
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::Attach(TThis *const parent, const any argument) {
        assert(parent != nullptr && "Argument 'parent' must be non-null");
        assert(this->Tree_NoRecursive() == nullptr && "Node must have no tree");
        assert(this->Parent() == nullptr && "Node must have no parent");
        assert(this->m_Activity == Activity_::Inactive && "Node must be inactive");
        {
            this->m_Owner = parent;
            this->OnBeforeAttach(argument);
            this->OnAttach(argument);
            this->OnAfterAttach(argument);
        }
        if (parent->m_Activity == Activity_::Active) {
            this->Activate(argument);
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::Detach(TreeBase<TThis> *const tree, const any argument) {
        assert(tree != nullptr && "Argument 'tree' must be non-null");
        assert(this->Tree_NoRecursive() == tree && "Node must have tree");
        assert(this->Parent() == nullptr && "Node must have no parent");
        assert(this->m_Activity == Activity_::Active && "Node must be active");
        if constexpr (true) { // NOLINT
            this->Deactivate(argument);
        }
        {
            this->OnBeforeDetach(argument);
            this->OnDetach(argument);
            this->OnAfterDetach(argument);
            this->m_Owner = nullptr;
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::Detach(TThis *const parent, const any argument) {
        assert(parent != nullptr && "Argument 'parent' must be non-null");
        assert(this->Tree_NoRecursive() == nullptr && "Node must have no tree");
        assert(this->Parent() == parent && "Node must have parent");
        if (parent->m_Activity == Activity_::Active) {
            assert(this->m_Activity == Activity_::Active && "Node must be active");
            this->Deactivate(argument);
        } else {
            assert(this->m_Activity == Activity_::Inactive && "Node must be inactive");
        }
        {
            this->OnBeforeDetach(argument);
            this->OnDetach(argument);
            this->OnAfterDetach(argument);
            this->m_Owner = nullptr;
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::Activate(const any argument) {
        assert((this->Tree_NoRecursive() != nullptr || this->Parent() != nullptr) && "Node must have owner");
        assert((this->Tree_NoRecursive() != nullptr || this->Parent()->m_Activity == Activity_::Active || this->Parent()->m_Activity == Activity_::Activating) && "Node must have valid owner");
        assert(this->m_Activity == Activity_::Inactive && "Node must be inactive");
        this->OnBeforeActivate(argument);
        this->m_Activity = Activity_::Activating;
        {
            this->OnActivate(argument);
            for (auto *child : this->m_Children) {
                child->Activate(argument);
            }
        }
        this->m_Activity = Activity_::Active;
        this->OnAfterActivate(argument);
    }
    template <typename TThis>
    void NodeBase<TThis>::Deactivate(const any argument) {
        assert((this->Tree_NoRecursive() != nullptr || this->Parent() != nullptr) && "Node must have owner");
        assert((this->Tree_NoRecursive() != nullptr || this->Parent()->m_Activity == Activity_::Active || this->Parent()->m_Activity == Activity_::Deactivating) && "Node must have valid owner");
        assert(this->m_Activity == Activity_::Active && "Node must be active");
        this->OnBeforeDeactivate(argument);
        this->m_Activity = Activity_::Deactivating;
        {
            for (auto *child : reverse(this->m_Children)) {
                child->Deactivate(argument);
            }
            this->OnDeactivate(argument);
        }
        this->m_Activity = Activity_::Inactive;
        this->OnAfterDeactivate(argument);
    }

    template <typename TThis>
    void NodeBase<TThis>::OnAttach([[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase<TThis>::OnBeforeAttach(const any argument) {
        if (this->m_OnBeforeAttachCallback) {
            this->m_OnBeforeAttachCallback(argument);
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterAttach(const any argument) {
        if (this->m_OnAfterAttachCallback) {
            this->m_OnAfterAttachCallback(argument);
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::OnDetach([[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase<TThis>::OnBeforeDetach(const any argument) {
        if (this->m_OnBeforeDetachCallback) {
            this->m_OnBeforeDetachCallback(argument);
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterDetach(const any argument) {
        if (this->m_OnAfterDetachCallback) {
            this->m_OnAfterDetachCallback(argument);
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::OnActivate([[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase<TThis>::OnBeforeActivate(const any argument) {
        if (this->m_OnBeforeActivateCallback) {
            this->m_OnBeforeActivateCallback(argument);
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterActivate(const any argument) {
        if (this->m_OnAfterActivateCallback) {
            this->m_OnAfterActivateCallback(argument);
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::OnDeactivate([[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase<TThis>::OnBeforeDeactivate(const any argument) {
        if (this->m_OnBeforeDeactivateCallback) {
            this->m_OnBeforeDeactivateCallback(argument);
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterDeactivate(const any argument) {
        if (this->m_OnAfterDeactivateCallback) {
            this->m_OnAfterDeactivateCallback(argument);
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::AddChild(TThis *const child, const any argument) {
        assert(child != nullptr && "Argument 'child' must be non-null");
        assert(child->Tree_NoRecursive() == nullptr && "Argument 'child' must have no tree");
        assert(child->Parent() == nullptr && "Argument 'child' must have no parent");
        assert(child->m_Activity == Activity_::Inactive && "Argument 'child' must be inactive");
        assert(!contains(this->m_Children, child) && "Node must have no child");
        this->m_Children.push_back(child);
        this->Sort(this->m_Children);
        child->Attach(static_cast<TThis *>(this), argument);
    }
    template <typename TThis>
    void NodeBase<TThis>::AddChildren(const vector<TThis *> &children, const any argument) {
        assert(&children != nullptr && "Argument 'children' must be non-null");
        for (auto *const child : children) {
            this->AddChild(child, argument);
        }
    }
    template <typename TThis>
    void NodeBase<TThis>::RemoveChild(TThis *const child, const any argument, const function<void(const TThis *const, const any)> callback) {
        assert(child != nullptr && "Argument 'child' must be non-null");
        assert(child->Tree_NoRecursive() == nullptr && "Argument 'child' must have no tree");
        assert(child->Parent() == this && "Argument 'child' must have parent");
        if (this->m_Activity == Activity_::Active) {
            assert(child->m_Activity == Activity_::Active && "Argument 'child' must be active");
        } else {
            assert(child->m_Activity == Activity_::Inactive && "Argument 'child' must be inactive");
        }
        assert(contains(this->m_Children, child) && "Node must have child");
        child->Detach(static_cast<TThis *>(this), argument);
        this->m_Children.remove(child);
        if (callback) {
            callback(child, argument);
        }
    }
    template <typename TThis>
    bool NodeBase<TThis>::RemoveChild(const function<bool(const TThis *const)> predicate, const any argument, const function<void(const TThis *const, const any)> callback) {
        for (auto *const child : reverse(this->m_Children)) {
            if (predicate(child)) {
                this->RemoveChild(child, argument, callback);
                return true;
            }
        }
        return false;
    }
    template <typename TThis>
    int32_t NodeBase<TThis>::RemoveChildren(const function<bool(const TThis *const)> predicate, const any argument, const function<void(const TThis *const, const any)> callback) {
        int32_t count = 0;
        for (auto *const child : reverse(this->m_Children)) {
            if (predicate(child)) {
                this->RemoveChild(child, argument, callback);
                count++;
            }
        }
        return count;
    }
    template <typename TThis>
    int32_t NodeBase<TThis>::RemoveChildren(const any argument, const function<void(const TThis *const, const any)> callback) {
        int32_t count = 0;
        for (auto *const child : reverse(this->m_Children)) {
            this->RemoveChild(child, argument, callback);
            count++;
        }
        return count;
    }
    template <typename TThis>
    void NodeBase<TThis>::RemoveSelf(const any argument, const function<void(const TThis *const, const any)> callback) {
        if (auto *const parent = this->Parent()) {
            parent->RemoveChild(this, argument, callback);
        } else {
            assert(this->Tree_NoRecursive() != nullptr && "Node must have tree");
            this->Tree_NoRecursive()->RemoveRoot(this, argument, callback);
        }
    }

    template <typename TThis>
    void NodeBase<TThis>::Sort(list<TThis *> &children) const {
        (void)children;
    }

    template <typename TThis>
    void NodeBase<TThis>::OnBeforeAttachCallback(const function<void(const any)> callback) {
        this->m_OnBeforeAttachCallback = callback;
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterAttachCallback(const function<void(const any)> callback) {
        this->m_OnAfterAttachCallback = callback;
    }
    template <typename TThis>
    void NodeBase<TThis>::OnBeforeDetachCallback(const function<void(const any)> callback) {
        this->m_OnBeforeDetachCallback = callback;
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterDetachCallback(const function<void(const any)> callback) {
        this->m_OnAfterDetachCallback = callback;
    }

    template <typename TThis>
    void NodeBase<TThis>::OnBeforeActivateCallback(const function<void(const any)> callback) {
        this->m_OnBeforeActivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterActivateCallback(const function<void(const any)> callback) {
        this->m_OnAfterActivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase<TThis>::OnBeforeDeactivateCallback(const function<void(const any)> callback) {
        this->m_OnBeforeDeactivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase<TThis>::OnAfterDeactivateCallback(const function<void(const any)> callback) {
        this->m_OnAfterDeactivateCallback = callback;
    }

}

#include <algorithm>
#include <any>
#include <cassert>
#include <execution>
#include <functional>
#include <list>
#include <optional>
#include <variant>
#include "Headers/TreeMachine/NodeBase.h"
#include "Headers/TreeMachine/TreeBase.h"

namespace {

    // template <typename T, typename Predicate>
    // std::optional<T> find_first(const std::list<T> &list, Predicate predicate) {
    //     auto result = std::find_if(list.begin(), list.end(), predicate);
    //     if (result != list.end()) {
    //         return *result;
    //     }
    //     return std::nullopt;
    // }

    template <typename T, typename Predicate>
    std::optional<T> find_last(const std::list<T> &list, const Predicate predicate) {
        auto result = std::find_if(list.rbegin(), list.rend(), predicate);
        if (result != list.rend()) {
            return *result;
        }
        return std::nullopt;
    }

    template <typename T>
    bool contains(const std::list<T> &list, const T &item) {
        auto result = std::find(list.begin(), list.end(), item);
        return result != list.end();
    }

}
namespace TreeMachine {

    [[nodiscard]] void *NodeBase::Owner() const {
        if (auto *const tree = *get_if<TreeBase *>(&this->m_Owner)) {
            return tree;
        }
        if (auto *const node = *get_if<NodeBase *>(&this->m_Owner)) {
            return node;
        }
        return nullptr;
    }

    [[nodiscard]] TreeBase *NodeBase::Tree() const {
        if (auto *const tree = *get_if<TreeBase *>(&this->m_Owner)) {
            return tree;
        }
        if (const auto *const node = *get_if<NodeBase *>(&this->m_Owner)) {
            return node->Tree();
        }
        return nullptr;
    }

    [[nodiscard]] bool NodeBase::IsRoot() const {
        return this->Parent() == nullptr;
    }
    [[nodiscard]] const NodeBase *NodeBase::Root() const {
        if (auto *parent = this->Parent(); parent != nullptr) {
            return parent->Root();
        }
        return this;
    }
    [[nodiscard]] NodeBase *NodeBase::Root() {
        if (auto *parent = this->Parent(); parent != nullptr) {
            return parent->Root();
        }
        return this;
    }

    [[nodiscard]] NodeBase *NodeBase::Parent() const {
        if (auto *const node = *get_if<NodeBase *>(&this->m_Owner)) {
            return node;
        }
        return nullptr;
    }
    [[nodiscard]] list<NodeBase *> NodeBase::Ancestors() const {
        auto result = list<NodeBase *>();
        if (auto *parent = this->Parent(); parent != nullptr) {
            result.push_back(parent);
            auto ancestors = parent->Ancestors();
            result.insert(result.end(), ancestors.begin(), ancestors.end());
        }
        return result;
    }
    [[nodiscard]] list<const NodeBase *> NodeBase::AncestorsAndSelf() const {
        auto result = list<const NodeBase *>();
        result.push_back(this);
        auto descendants = this->Descendants();
        result.insert(result.end(), descendants.begin(), descendants.end());
        return result;
    }
    [[nodiscard]] list<NodeBase *> NodeBase::AncestorsAndSelf() {
        auto result = list<NodeBase *>();
        result.push_back(this);
        auto descendants = this->Descendants();
        result.insert(result.end(), descendants.begin(), descendants.end());
        return result;
    }

    [[nodiscard]] list<NodeBase *> NodeBase::Children() const {
        return this->m_Children;
    }
    [[nodiscard]] list<NodeBase *> NodeBase::Descendants() const {
        auto result = list<NodeBase *>();
        for (auto *child : this->m_Children) {
            result.push_back(child);
            auto descendants = child->Descendants();
            result.insert(result.end(), descendants.begin(), descendants.end());
        }
        return result;
    }
    [[nodiscard]] list<const NodeBase *> NodeBase::DescendantsAndSelf() const {
        auto result = list<const NodeBase *>();
        result.push_back(this);
        auto descendants = this->Descendants();
        result.insert(result.end(), descendants.begin(), descendants.end());
        return result;
    }
    [[nodiscard]] list<NodeBase *> NodeBase::DescendantsAndSelf() {
        auto result = list<NodeBase *>();
        result.push_back(this);
        auto descendants = this->Descendants();
        result.insert(result.end(), descendants.begin(), descendants.end());
        return result;
    }

    [[nodiscard]] function<void(const any)> NodeBase::OnBeforeAttachCallback() const {
        return this->m_OnBeforeAttachCallback;
    }
    [[nodiscard]] function<void(const any)> NodeBase::OnAfterAttachCallback() const {
        return this->m_OnAfterAttachCallback;
    }
    [[nodiscard]] function<void(const any)> NodeBase::OnBeforeDetachCallback() const {
        return this->m_OnBeforeDetachCallback;
    }
    [[nodiscard]] function<void(const any)> NodeBase::OnAfterDetachCallback() const {
        return this->m_OnAfterDetachCallback;
    }

    void NodeBase::OnBeforeAttachCallback(const function<void(const any)> callback) {
        this->m_OnBeforeAttachCallback = callback;
    }
    void NodeBase::OnAfterAttachCallback(const function<void(const any)> callback) {
        this->m_OnAfterAttachCallback = callback;
    }
    void NodeBase::OnBeforeDetachCallback(const function<void(const any)> callback) {
        this->m_OnBeforeDetachCallback = callback;
    }
    void NodeBase::OnAfterDetachCallback(const function<void(const any)> callback) {
        this->m_OnAfterDetachCallback = callback;
    }

    void NodeBase::Attach(TreeBase *const owner, const any argument) {
        assert(owner != nullptr && "Argument 'owner' must be non-null");
        assert(this->Owner() == nullptr && "Node must have no owner");
        //   assert(this->Activity == Activity_.Inactive && "Node must be inactive");
        {
            this->m_Owner = owner;
            this->OnBeforeAttach(argument);
            this->OnAttach(argument);
            this->OnAfterAttach(argument);
        }
        /*  {
              this->Activate(argument);
          }*/
    }
    void NodeBase::Detach(TreeBase *const owner, const any argument) {
        assert(owner != nullptr && "Argument 'owner' must be non-null");
        assert(this->Owner() == owner && "Node must have owner");
        //   assert(this->Activity == Activity_.Active && "Node must be active");
        /*  {
              this->Deactivate(argument);
          }*/
        {
            this->OnBeforeDetach(argument);
            this->OnDetach(argument);
            this->OnAfterDetach(argument);
            this->m_Owner = nullptr;
        }
    }

    void NodeBase::Attach(NodeBase *const owner, const any argument) {
        assert(owner != nullptr && "Argument 'owner' must be non-null");
        assert(this->Owner() == nullptr && "Node must have no owner");
        //   assert(this->Activity == Activity_.Inactive && "Node must be inactive");
        {
            this->m_Owner = owner;
            this->OnBeforeAttach(argument);
            this->OnAttach(argument);
            this->OnAfterAttach(argument);
        }
        /*   if (owner.Activity == Activity_.Active) {
               this->Activate(argument);
           } else {
           }*/
    }
    void NodeBase::Detach(NodeBase *const owner, const any argument) {
        assert(owner != nullptr && "Argument 'owner' must be non-null");
        assert(this->Owner() == owner && "Node must have owner");
        /*         if (owner.Activity == Activity_.Active) {
                     assert(this->Activity == Activity_.Active && "Node must be active");
                     this->Deactivate(argument);
                 } else {
                     assert(this->Activity == Activity_.Inactive && "Node must be inactive");
                 }*/
        {
            this->OnBeforeDetach(argument);
            this->OnDetach(argument);
            this->OnAfterDetach(argument);
            this->m_Owner = nullptr;
        }
    }

    void NodeBase::OnAttach([[maybe_unused]] const any argument) {
    }
    void NodeBase::OnBeforeAttach(const any argument) {
        if (this->m_OnBeforeAttachCallback) {
            this->m_OnBeforeAttachCallback(argument);
        }
    }
    void NodeBase::OnAfterAttach(const any argument) {
        if (this->m_OnAfterAttachCallback) {
            this->m_OnAfterAttachCallback(argument);
        }
    }

    void NodeBase::OnDetach([[maybe_unused]] const any argument) {
    }
    void NodeBase::OnBeforeDetach(const any argument) {
        if (this->m_OnBeforeDetachCallback) {
            this->m_OnBeforeDetachCallback(argument);
        }
    }
    void NodeBase::OnAfterDetach(const any argument) {
        if (this->m_OnAfterDetachCallback) {
            this->m_OnAfterDetachCallback(argument);
        }
    }

    void NodeBase::AddChild(NodeBase *const child, const any argument) {
        assert(child != nullptr && "Argument 'child' must be non-null");
        assert(child->Owner() == nullptr && "Argument 'child' must have no owner");
        // assert(child->Activity() == Activity_.Inactive && "Argument 'child' must be inactive");
        assert(!contains(this->m_Children, child) && "Node must have no child");
        this->m_Children.push_back(child);
        this->Sort(this->m_Children);
        child->Attach(this, argument);
    }
    void NodeBase::RemoveChild(NodeBase *const child, const any argument, const function<void(NodeBase *const, const any)> callback) {
        assert(child != nullptr && "Argument 'child' must be non-null");
        assert(child->Owner() == this && "Argument 'child' must have owner");
        // if (this.Activity() == Activity_.Active) {
        //     assert(child.Activity() == Activity_.Active && "Argument 'child' must be active");
        // } else {
        //     assert(child.Activity() == Activity_.Inactive && "Argument 'child' must be inactive");
        // }
        assert(contains(this->m_Children, child) && "Node must have child");
        child->Detach(this, argument);
        this->m_Children.remove(child);
        if (callback) {
            callback(child, argument);
        }
    }
    bool NodeBase::RemoveChild(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback) {
        if (const auto child = find_last(this->m_Children, predicate); child) {
            this->RemoveChild(*child, argument, callback);
            return true;
        }
        return false;
    }
    size_t NodeBase::RemoveChildren(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback) {
        auto children = vector<NodeBase *>();
        for (auto *child : this->m_Children) {
            if (predicate(child)) {
                children.push_back(child);
            }
        }
        for (auto *child : children) {
            this->RemoveChild(child, argument, callback);
        }
        return children.size();
    }
    void NodeBase::RemoveSelf(const any argument, const function<void(NodeBase *const, const any)> callback) {
        assert(this->Owner() != nullptr && "Node must have owner");
        if (auto *parent = this->Parent(); parent != nullptr) {
            parent->RemoveChild(this, argument, callback);
        } else {
            this->Tree()->RemoveRoot(this, argument, callback);
        }
    }

    void NodeBase::Sort(list<NodeBase *> &children) {
        (void)children;
    }

}

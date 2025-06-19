#include <any>
#include <cassert>
#include <functional>
#include <memory>
#include <variant>
#include <vector>
#include "Headers/TreeMachine/NodeBase.h"
#include "Headers/TreeMachine/TreeBase.h"

namespace TreeMachine {

    [[nodiscard]] void *NodeBase::Owner() const {
        if (const auto *const tree = get_if<TreeBase *>(&this->m_Owner)) {
            return *tree;
        }
        if (const auto *const node = get_if<NodeBase *>(&this->m_Owner)) {
            return *node;
        }
        return nullptr;
    }

    [[nodiscard]] TreeBase *NodeBase::Tree() const {
        if (const auto *const tree = get_if<TreeBase *>(&this->m_Owner)) {
            return *tree;
        }
        if (const auto *const node = get_if<NodeBase *>(&this->m_Owner)) {
            return (*node)->Tree();
        }
        return nullptr;
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
        assert(owner && "Argument 'owner' must be non-null");
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
        assert(owner && "Argument 'owner' must be non-null");
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
        assert(owner && "Argument 'owner' must be non-null");
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
        assert(owner && "Argument 'owner' must be non-null");
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

}

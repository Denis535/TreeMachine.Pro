#include <any>
#include <cassert>
#include <functional>
#include <memory>
#include <variant>
#include <vector>
#include "Headers/TreeMachine/NodeBase.h"
#include "Headers/TreeMachine/TreeBase.h"

namespace TreeMachine {

    [[nodiscard]] void * NodeBase::Owner() const {
        if (const auto *const tree = get_if<TreeBase *>(&this->m_Owner)) {
            return *tree;
        }
        if (const auto *const node = get_if<NodeBase *>(&this->m_Owner)) {
            return *node;
        }
        return nullptr;
    }
    [[nodiscard]] TreeBase * NodeBase::Tree() const {
        if (const auto *const tree = get_if<TreeBase *>(&this->m_Owner)) {
            return *tree;
        }
        if (const auto *const node = get_if<NodeBase *>(&this->m_Owner)) {
            return (*node)->Tree();
        }
        return nullptr;
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
    void NodeBase::OnBeforeAttach([[maybe_unused]] const any argument) {
        // this->OnBeforeAttachEvent ?.Invoke(argument);
    }
    void NodeBase::OnAfterAttach([[maybe_unused]] const any argument) {
        // this->OnAfterAttachEvent ?.Invoke(argument);
    }

    void NodeBase::OnDetach([[maybe_unused]] const any argument) {
    }
    void NodeBase::OnBeforeDetach([[maybe_unused]] const any argument) {
        // this->OnBeforeDetachEvent ?.Invoke(argument);
    }
    void NodeBase::OnAfterDetach([[maybe_unused]] const any argument) {
        // this->OnAfterDetachEvent ?.Invoke(argument);
    }

}

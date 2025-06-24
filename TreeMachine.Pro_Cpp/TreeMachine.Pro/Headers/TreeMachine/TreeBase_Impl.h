#pragma once
#include <any>
#include <cassert>
#include <functional>
#include "NodeBase.h"
#include "TreeBase.h"

namespace TreeMachine {

    inline TreeBase::TreeBase() = default;
    inline TreeBase::~TreeBase() {
        assert(this->m_Root == nullptr && "Tree must have no root");
    }

    inline [[nodiscard]] NodeBase *TreeBase::Root() const {
        return this->m_Root;
    }

    inline void TreeBase::AddRoot(NodeBase *const root, const any argument) {
        assert(root != nullptr && "Argument 'root' must be non-null");
        assert(root->Tree() == nullptr && "Argument 'root' must have no tree");
        assert(root->Parent() == nullptr && "Argument 'root' must have no parent");
        assert(root->m_Activity == NodeBase::EActivity::Inactive && "Argument 'root' must be inactive");
        this->m_Root = root;
        this->m_Root->Attach(this, argument);
    }
    inline void TreeBase::RemoveRoot(NodeBase *const root, const any argument, const function<void(const NodeBase *const, const any)> callback) {
        assert(root != nullptr && "Argument 'root' must be non-null");
        assert(root->Tree() == this && "Argument 'root' must have tree");
        assert(root->Parent() == nullptr && "Argument 'root' must have no parent");
        assert(root->m_Activity == NodeBase::EActivity::Active && "Argument 'root' must be active");
        this->m_Root->Detach(this, argument);
        this->m_Root = nullptr;
        if (callback) {
            callback(root, argument);
        }
    }
    inline void TreeBase::RemoveRoot(const any argument, const function<void(const NodeBase *const, const any)> callback) {
        assert(this->m_Root != nullptr && "Tree must have root");
        this->RemoveRoot(this->m_Root, argument, callback);
    }

}

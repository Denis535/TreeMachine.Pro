#include <any>
#include <cassert>
#include <functional>
#include "Headers/TreeMachine/NodeBase.h"
#include "Headers/TreeMachine/TreeBase.h"

namespace TreeMachine {

    [[nodiscard]] NodeBase *TreeBase::Root() const {
        return this->m_Root;
    }

    void TreeBase::SetRoot(NodeBase *const root, const any argument, const function<void(NodeBase *const, const any)> callback) {
        if (this->m_Root != nullptr) {
            this->RemoveRoot(this->m_Root, argument, callback);
        }
        if (root != nullptr) {
            this->AddRoot(root, argument);
        }
    }
    void TreeBase::AddRoot(NodeBase *const root, const any argument) {
        assert(root != nullptr && "Argument 'root' must be non-null");
        assert(root->Tree() == nullptr && "Argument 'root' must have no tree");
        assert(root->Parent() == nullptr && "Argument 'root' must have no parent");
        assert(root->m_Activity == NodeBase::EActivity::Inactive && "Argument 'root' must be inactive");
        this->m_Root = root;
        this->m_Root->Attach(this, argument);
    }
    void TreeBase::RemoveRoot(NodeBase *const root, const any argument, const function<void(NodeBase *const, const any)> callback) {
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

}

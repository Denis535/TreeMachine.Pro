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
        assert(root->Owner() == nullptr && "Argument 'root' must have no owner");
        // assert(root.Activity == NodeBase<T>.Activity_.Inactive && "Argument 'root' ({root}) must be inactive");
        this->m_Root = root;
        this->m_Root->Attach(this, argument);
    }
    void TreeBase::RemoveRoot(NodeBase *const root, const any argument, const function<void(NodeBase *const, const any)> callback) {
        assert(root != nullptr && "Argument 'root' must be non-null");
        assert(root->Owner() == this && "Argument 'root' must have owner");
        // assert(root.Activity == NodeBase<T>.Activity_.Active && "Argument 'root' ({root}) must be active");
        this->m_Root->Detach(this, argument);
        this->m_Root = nullptr;
        if (callback) {
            callback(root, argument);
        }
    }

}

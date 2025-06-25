#pragma once
#include <any>
#include <cassert>
#include <functional>
#include "TreeBase.h"

namespace TreeMachine {

    template <typename T>
    TreeBase<T>::TreeBase() {
        static_assert(is_base_of_v<NodeBase<T>, T>);
    }

    template <typename T>
    TreeBase<T>::~TreeBase() {
        assert(this->m_Root == nullptr && "Tree must have no root");
    }

    template <typename T>
    T *TreeBase<T>::Root() const {
        return this->m_Root;
    }

    template <typename T>
    void TreeBase<T>::AddRoot(T *const root, const any argument) {
        assert(root != nullptr && "Argument 'root' must be non-null");
        assert(root->Tree() == nullptr && "Argument 'root' must have no tree");
        assert(root->Parent() == nullptr && "Argument 'root' must have no parent");
        assert(root->m_Activity == NodeBase<T>::EActivity::Inactive && "Argument 'root' must be inactive");
        assert(this->m_Root == nullptr && "Tree must have no root");
        this->m_Root = root;
        this->m_Root->Attach(this, argument);
    }
    template <typename T>
    void TreeBase<T>::RemoveRoot(T *const root, const any argument, const function<void(const T *const, const any)> callback) {
        assert(root != nullptr && "Argument 'root' must be non-null");
        assert(root->Tree() == this && "Argument 'root' must have tree");
        assert(root->Parent() == nullptr && "Argument 'root' must have no parent");
        assert(root->m_Activity == NodeBase<T>::EActivity::Active && "Argument 'root' must be active");
        assert(this->m_Root != nullptr && "Tree must have root");
        this->m_Root->Detach(this, argument);
        this->m_Root = nullptr;
        if (callback) {
            callback(root, argument);
        }
    }
    template <typename T>
    void TreeBase<T>::RemoveRoot(const any argument, const function<void(const T *const, const any)> callback) {
        assert(this->m_Root != nullptr && "Tree must have root");
        this->RemoveRoot(this->m_Root, argument, callback);
    }

}

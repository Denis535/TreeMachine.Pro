#pragma once
#include <any>
#include <functional>
#include "../NodeBase2.h"
#include "Helpers.h"

namespace TreeMachine {
    using namespace TreeMachine::Internal;

    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnBeforeDescendantAttachCallback() {
        return this->m_OnBeforeDescendantAttachCallback;
    }
    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnAfterDescendantAttachCallback() {
        return this->m_OnAfterDescendantAttachCallback;
    }
    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnBeforeDescendantDetachCallback() {
        return this->m_OnBeforeDescendantDetachCallback;
    }
    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnAfterDescendantDetachCallback() {
        return this->m_OnAfterDescendantDetachCallback;
    }

    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnBeforeDescendantActivateCallback() {
        return this->m_OnBeforeDescendantActivateCallback;
    }
    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnAfterDescendantActivateCallback() {
        return this->m_OnAfterDescendantActivateCallback;
    }
    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnBeforeDescendantDeactivateCallback() {
        return this->m_OnBeforeDescendantDeactivateCallback;
    }
    template <typename TThis>
    const function<void(TThis *const, const any)> &NodeBase2<TThis>::OnAfterDescendantDeactivateCallback() {
        return this->m_OnAfterDescendantDeactivateCallback;
    }

    template <typename TThis>
    NodeBase2<TThis>::NodeBase2() {
        static_assert(is_base_of_v<NodeBase2, TThis>);
    }

    template <typename TThis>
    NodeBase2<TThis>::~NodeBase2() = default;

    template <typename TThis>
    void NodeBase2<TThis>::OnAttach(const any argument) {
        NodeBase<TThis>::OnAttach(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeAttach(const any argument) {
        for (auto *const ancestor : reverse(this->Ancestors())) {
            if (ancestor->m_OnBeforeDescendantAttachCallback) {
                ancestor->m_OnBeforeDescendantAttachCallback(static_cast<TThis *>(this), argument);
            }
            static_cast<NodeBase2 *>(ancestor)->OnBeforeDescendantAttach(static_cast<TThis *>(this), argument);
        }
        NodeBase<TThis>::OnBeforeAttach(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterAttach(const any argument) {
        NodeBase<TThis>::OnAfterAttach(argument);
        for (auto *const ancestor : this->Ancestors()) {
            static_cast<NodeBase2 *>(ancestor)->OnAfterDescendantAttach(static_cast<TThis *>(this), argument);
            if (ancestor->m_OnAfterDescendantAttachCallback) {
                ancestor->m_OnAfterDescendantAttachCallback(static_cast<TThis *>(this), argument);
            }
        }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnDetach(const any argument) {
        NodeBase<TThis>::OnDetach(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDetach(const any argument) {
        for (auto *const ancestor : reverse(this->Ancestors())) {
            if (ancestor->m_OnBeforeDescendantDetachCallback) {
                ancestor->m_OnBeforeDescendantDetachCallback(static_cast<TThis *>(this), argument);
            }
            static_cast<NodeBase2 *>(ancestor)->OnBeforeDescendantDetach(static_cast<TThis *>(this), argument);
        }
        NodeBase<TThis>::OnBeforeDetach(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDetach(const any argument) {
        NodeBase<TThis>::OnAfterDetach(argument);
        for (auto *const ancestor : this->Ancestors()) {
            static_cast<NodeBase2 *>(ancestor)->OnAfterDescendantDetach(static_cast<TThis *>(this), argument);
            if (ancestor->m_OnAfterDescendantDetachCallback) {
                ancestor->m_OnAfterDescendantDetachCallback(static_cast<TThis *>(this), argument);
            }
        }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantAttach([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantAttach([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDetach([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDetach([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnActivate(const any argument) {
        NodeBase<TThis>::OnActivate(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeActivate(const any argument) {
        for (auto *const ancestor : reverse(this->Ancestors())) {
            if (ancestor->m_OnBeforeDescendantActivateCallback) {
                ancestor->m_OnBeforeDescendantActivateCallback(static_cast<TThis *>(this), argument);
            }
            static_cast<NodeBase2 *>(ancestor)->OnBeforeDescendantActivate(static_cast<TThis *>(this), argument);
        }
        NodeBase<TThis>::OnBeforeActivate(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterActivate(const any argument) {
        NodeBase<TThis>::OnAfterActivate(argument);
        for (auto *const ancestor : this->Ancestors()) {
            static_cast<NodeBase2 *>(ancestor)->OnAfterDescendantActivate(static_cast<TThis *>(this), argument);
            if (ancestor->m_OnAfterDescendantActivateCallback) {
                ancestor->m_OnAfterDescendantActivateCallback(static_cast<TThis *>(this), argument);
            }
        }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnDeactivate(const any argument) {
        NodeBase<TThis>::OnDeactivate(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDeactivate(const any argument) {
        for (auto *const ancestor : reverse(this->Ancestors())) {
            if (ancestor->m_OnBeforeDescendantDeactivateCallback) {
                ancestor->m_OnBeforeDescendantDeactivateCallback(static_cast<TThis *>(this), argument);
            }
            static_cast<NodeBase2 *>(ancestor)->OnBeforeDescendantDeactivate(static_cast<TThis *>(this), argument);
        }
        NodeBase<TThis>::OnBeforeDeactivate(argument);
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDeactivate(const any argument) {
        NodeBase<TThis>::OnAfterDeactivate(argument);
        for (auto *const ancestor : this->Ancestors()) {
            static_cast<NodeBase2 *>(ancestor)->OnAfterDescendantDeactivate(static_cast<TThis *>(this), argument);
            if (ancestor->m_OnAfterDescendantDeactivateCallback) {
                ancestor->m_OnAfterDescendantDeactivateCallback(static_cast<TThis *>(this), argument);
            }
        }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantActivate([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantActivate([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDeactivate([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDeactivate([[maybe_unused]] TThis *const descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantAttachCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnBeforeDescendantAttachCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantAttachCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnAfterDescendantAttachCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDetachCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnBeforeDescendantDetachCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDetachCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnAfterDescendantDetachCallback = callback;
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantActivateCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnBeforeDescendantActivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantActivateCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnAfterDescendantActivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDeactivateCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnBeforeDescendantDeactivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDeactivateCallback(const function<void(TThis *const, const any)> callback) {
        this->m_OnAfterDescendantDeactivateCallback = callback;
    }

}

#pragma once
#include <any>
#include <functional>
#include "NodeBase2.h"

namespace TreeMachine {

    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnBeforeDescendantAttachCallback() {
        return this->m_OnBeforeDescendantAttachCallback;
    }
    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnAfterDescendantAttachCallback() {
        return this->m_OnAfterDescendantAttachCallback;
    }
    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnBeforeDescendantDetachCallback() {
        return this->m_OnBeforeDescendantDetachCallback;
    }
    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnAfterDescendantDetachCallback() {
        return this->m_OnAfterDescendantDetachCallback;
    }

    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnBeforeDescendantActivateCallback() {
        return this->m_OnBeforeDescendantActivateCallback;
    }
    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnAfterDescendantActivateCallback() {
        return this->m_OnAfterDescendantActivateCallback;
    }
    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnBeforeDescendantDeactivateCallback() {
        return this->m_OnBeforeDescendantDeactivateCallback;
    }
    template <typename TThis>
    const function<void(TThis, const any)> &NodeBase2<TThis>::OnAfterDescendantDeactivateCallback() {
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
        // foreach (var ancestor in this.Ancestors.Reverse()) {
        //     ancestor.OnBeforeDescendantAttachEvent ?.Invoke((TThis)this, argument);
        //     ancestor.OnBeforeDescendantAttach((TThis)this, argument);
        // }
        NodeBase<TThis>::OnBeforeAttach(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterAttach(const any argument) {
        NodeBase<TThis>::OnAfterAttach(argument);
        // foreach (var ancestor in this.Ancestors) {
        //     ancestor.OnAfterDescendantAttach((TThis)this, argument);
        //     ancestor.OnAfterDescendantAttachEvent ?.Invoke((TThis)this, argument);
        // }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnDetach(const any argument) {
        NodeBase<TThis>::OnDetach(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDetach(const any argument) {
        // foreach (var ancestor in this.Ancestors.Reverse()) {
        //     ancestor.OnBeforeDescendantDetachEvent ?.Invoke((TThis)this, argument);
        //     ancestor.OnBeforeDescendantDetach((TThis)this, argument);
        // }
        NodeBase<TThis>::OnBeforeDetach(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDetach(const any argument) {
        NodeBase<TThis>::OnAfterDetach(argument);
        // foreach (var ancestor in this.Ancestors) {
        //     ancestor.OnAfterDescendantDetach((TThis)this, argument);
        //     ancestor.OnAfterDescendantDetachEvent ?.Invoke((TThis)this, argument);
        // }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantAttach([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantAttach([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDetach([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDetach([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnActivate(const any argument) {
        NodeBase<TThis>::OnActivate(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeActivate(const any argument) {
        // foreach (var ancestor in this.Ancestors.Reverse()) {
        //     ancestor.OnBeforeDescendantActivateEvent ?.Invoke((TThis)this, argument);
        //     ancestor.OnBeforeDescendantActivate((TThis)this, argument);
        // }
        NodeBase<TThis>::OnBeforeActivate(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterActivate(const any argument) {
        NodeBase<TThis>::OnAfterActivate(argument);
        // foreach (var ancestor in this.Ancestors) {
        //     ancestor.OnAfterDescendantActivate((TThis)this, argument);
        //     ancestor.OnAfterDescendantActivateEvent ?.Invoke((TThis)this, argument);
        // }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnDeactivate(const any argument) {
        NodeBase<TThis>::OnDeactivate(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDeactivate(const any argument) {
        // foreach (var ancestor in this.Ancestors.Reverse()) {
        //     ancestor.OnBeforeDescendantDeactivateEvent ?.Invoke((TThis)this, argument);
        //     ancestor.OnBeforeDescendantDeactivate((TThis)this, argument);
        // }
        NodeBase<TThis>::OnBeforeDeactivate(argument);
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDeactivate(const any argument) {
        NodeBase<TThis>::OnAfterDeactivate(argument);
        // foreach (var ancestor in this.Ancestors) {
        //     ancestor.OnAfterDescendantDeactivate((TThis)this, argument);
        //     ancestor.OnAfterDescendantDeactivateEvent ?.Invoke((TThis)this, argument);
        // }
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantActivate([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantActivate([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDeactivate([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDeactivate([[maybe_unused]] TThis *descendant, [[maybe_unused]] const any argument) {
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantAttachCallback(function<void(TThis, const any)> callback) {
        this->m_OnBeforeDescendantAttachCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantAttachCallback(function<void(TThis, const any)> callback) {
        this->m_OnAfterDescendantAttachCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDetachCallback(function<void(TThis, const any)> callback) {
        this->m_OnBeforeDescendantDetachCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDetachCallback(function<void(TThis, const any)> callback) {
        this->m_OnAfterDescendantDetachCallback = callback;
    }

    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantActivateCallback(function<void(TThis, const any)> callback) {
        this->m_OnBeforeDescendantActivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantActivateCallback(function<void(TThis, const any)> callback) {
        this->m_OnAfterDescendantActivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnBeforeDescendantDeactivateCallback(function<void(TThis, const any)> callback) {
        this->m_OnBeforeDescendantDeactivateCallback = callback;
    }
    template <typename TThis>
    void NodeBase2<TThis>::OnAfterDescendantDeactivateCallback(function<void(TThis, const any)> callback) {
        this->m_OnAfterDescendantDeactivateCallback = callback;
    }

}

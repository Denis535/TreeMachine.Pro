#pragma once
#include <any>
#include <functional>
#include <variant>

namespace TreeMachine {
    using namespace std;

    class NodeBase {
        friend class TreeBase;

        private:
        variant<nullptr_t, TreeBase *, NodeBase *> m_Owner = nullptr;

        function<void(const any)> m_OnBeforeAttachCallback = nullptr;
        function<void(const any)> m_OnAfterAttachCallback = nullptr;
        function<void(const any)> m_OnBeforeDetachCallback = nullptr;
        function<void(const any)> m_OnAfterDetachCallback = nullptr;

        public:
        explicit NodeBase() = default;
        explicit NodeBase(const NodeBase &other) = delete;
        explicit NodeBase(NodeBase &&other) = delete;
        virtual ~NodeBase() = default;

        private:
        [[nodiscard]] void *Owner() const;

        public:
        [[nodiscard]] TreeBase *Tree() const;

        [[nodiscard]] function<void(const any)> OnBeforeAttachCallback() const;
        [[nodiscard]] function<void(const any)> OnAfterAttachCallback() const;
        [[nodiscard]] function<void(const any)> OnBeforeDetachCallback() const;
        [[nodiscard]] function<void(const any)> OnAfterDetachCallback() const;

        void OnBeforeAttachCallback(const function<void(const any)> callback);
        void OnAfterAttachCallback(const function<void(const any)> callback);
        void OnBeforeDetachCallback(const function<void(const any)> callback);
        void OnAfterDetachCallback(const function<void(const any)> callback);

        private:
        void Attach(TreeBase *const owner, const any argument);
        void Detach(TreeBase *const owner, const any argument);

        void Attach(NodeBase *const owner, const any argument);
        void Detach(NodeBase *const owner, const any argument);

        protected:
        virtual void OnAttach(const any argument);
        virtual void OnBeforeAttach(const any argument);
        virtual void OnAfterAttach(const any argument);

        virtual void OnDetach(const any argument);
        virtual void OnBeforeDetach(const any argument);
        virtual void OnAfterDetach(const any argument);

        public:
        NodeBase &operator=(const NodeBase &other) = delete;
        NodeBase &operator=(NodeBase &&other) = delete;
    };
}

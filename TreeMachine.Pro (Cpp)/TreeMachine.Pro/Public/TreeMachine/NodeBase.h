#pragma once
#include <any>
#include <variant>

namespace TreeMachine {
    using namespace std;

    class NodeBase {
        friend class TreeBase;

        private:
        variant<nullptr_t, TreeBase *, NodeBase *> m_Owner = nullptr;

        public:
        explicit NodeBase() = default;
        explicit NodeBase(const NodeBase &other) = delete;
        explicit NodeBase(NodeBase &&other) = delete;
        virtual ~NodeBase() = default;

        private:
        [[nodiscard]] void *const Owner() const;

        public:
        [[nodiscard]] TreeBase *const Tree() const;

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

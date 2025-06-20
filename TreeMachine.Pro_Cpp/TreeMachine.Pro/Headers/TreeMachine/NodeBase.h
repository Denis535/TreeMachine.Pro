#pragma once
#include <any>
#include <functional>
#include <list>
#include <variant>

namespace TreeMachine {
    using namespace std;

    class NodeBase {
        friend class TreeBase;

        public:
        enum class EActivity : uint8_t {
            Inactive,
            Activating,
            Active,
            Deactivating,
        };

        private:
        variant<nullptr_t, TreeBase *, NodeBase *> m_Owner = nullptr;

        private:
        EActivity m_Activity = EActivity::Inactive;

        private:
        list<NodeBase *> m_Children = list<NodeBase *>(0);

        private:
        function<void(const any)> m_OnBeforeAttachCallback = nullptr;
        function<void(const any)> m_OnAfterAttachCallback = nullptr;
        function<void(const any)> m_OnBeforeDetachCallback = nullptr;
        function<void(const any)> m_OnAfterDetachCallback = nullptr;

        private:
        function<void(const any)> m_OnBeforeActivateCallback = nullptr;
        function<void(const any)> m_OnAfterActivateCallback = nullptr;
        function<void(const any)> m_OnBeforeDeactivateCallback = nullptr;
        function<void(const any)> m_OnAfterDeactivateCallback = nullptr;

        public:
        [[nodiscard]] TreeBase *Tree() const;
        [[nodiscard]] TreeBase *TreeRecursive() const;

        public:
        [[nodiscard]] bool IsRoot() const;
        [[nodiscard]] const NodeBase *Root() const;
        [[nodiscard]] NodeBase *Root();

        public:
        [[nodiscard]] NodeBase *Parent() const;
        [[nodiscard]] list<NodeBase *> Ancestors() const;
        [[nodiscard]] list<const NodeBase *> AncestorsAndSelf() const;
        [[nodiscard]] list<NodeBase *> AncestorsAndSelf();

        public:
        [[nodiscard]] EActivity Activity() const;

        public:
        [[nodiscard]] const list<NodeBase *> *Children() const;
        [[nodiscard]] list<NodeBase *> Descendants() const;
        [[nodiscard]] list<const NodeBase *> DescendantsAndSelf() const;
        [[nodiscard]] list<NodeBase *> DescendantsAndSelf();

        public:
        [[nodiscard]] function<void(const any)> OnBeforeAttachCallback() const;
        [[nodiscard]] function<void(const any)> OnAfterAttachCallback() const;
        [[nodiscard]] function<void(const any)> OnBeforeDetachCallback() const;
        [[nodiscard]] function<void(const any)> OnAfterDetachCallback() const;

        public:
        [[nodiscard]] function<void(const any)> OnBeforeActivateCallback() const;
        [[nodiscard]] function<void(const any)> OnAfterActivateCallback() const;
        [[nodiscard]] function<void(const any)> OnBeforeDeactivateCallback() const;
        [[nodiscard]] function<void(const any)> OnAfterDeactivateCallback() const;

        public:
        explicit NodeBase() = default;
        explicit NodeBase(const NodeBase &other) = delete;
        explicit NodeBase(NodeBase &&other) = delete;
        virtual ~NodeBase() = default;

        private:
        void Attach(TreeBase *const owner, const any argument);
        void Attach(NodeBase *const owner, const any argument);

        private:
        void Detach(TreeBase *const owner, const any argument);
        void Detach(NodeBase *const owner, const any argument);

        private:
        void Activate(const any argument);
        void Deactivate(const any argument);

        protected:
        virtual void OnAttach(const any argument);
        virtual void OnBeforeAttach(const any argument);
        virtual void OnAfterAttach(const any argument);

        protected:
        virtual void OnDetach(const any argument);
        virtual void OnBeforeDetach(const any argument);
        virtual void OnAfterDetach(const any argument);

        protected:
        virtual void OnActivate(const any argument);
        virtual void OnBeforeActivate(const any argument);
        virtual void OnAfterActivate(const any argument);

        protected:
        virtual void OnDeactivate(const any argument);
        virtual void OnBeforeDeactivate(const any argument);
        virtual void OnAfterDeactivate(const any argument);

        protected:
        virtual void AddChild(NodeBase *const child, const any argument);
        virtual void RemoveChild(NodeBase *const child, const any argument, const function<void(NodeBase *const, const any)> callback);
        bool RemoveChild(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback);
        int32_t RemoveChildren(const function<bool(NodeBase *const)> predicate, const any argument, const function<void(NodeBase *const, const any)> callback);
        void RemoveSelf(const any argument, const function<void(NodeBase *const, const any)> callback);

        protected:
        virtual void Sort(list<NodeBase *> &children);

        public:
        void OnBeforeAttachCallback(const function<void(const any)> callback);
        void OnAfterAttachCallback(const function<void(const any)> callback);
        void OnBeforeDetachCallback(const function<void(const any)> callback);
        void OnAfterDetachCallback(const function<void(const any)> callback);

        public:
        void OnBeforeActivateCallback(const function<void(const any)> callback);
        void OnAfterActivateCallback(const function<void(const any)> callback);
        void OnBeforeDeactivateCallback(const function<void(const any)> callback);
        void OnAfterDeactivateCallback(const function<void(const any)> callback);

        public:
        NodeBase &operator=(const NodeBase &other) = delete;
        NodeBase &operator=(NodeBase &&other) = delete;
    };
}

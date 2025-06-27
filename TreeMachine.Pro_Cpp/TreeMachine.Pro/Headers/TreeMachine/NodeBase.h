#pragma once
#include <any>
#include <functional>
#include <list>
#include <variant>

namespace TreeMachine {
    using namespace std;

    template <typename T>
    class TreeBase;

    template <typename TThis>
    class NodeBase {
        friend class TreeBase<TThis>;

        public:
        enum class Activity_ : uint8_t { // NOLINT
            Inactive,
            Activating,
            Active,
            Deactivating,
        };

        private:
        variant<nullptr_t, TreeBase<TThis> *, TThis *> m_Owner = nullptr;

        private:
        Activity_ m_Activity = Activity_::Inactive;

        private:
        list<TThis *> m_Children = list<TThis *>(0);

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
        [[nodiscard]] TreeBase<TThis> *Tree() const;

        public:
        [[nodiscard]] TreeBase<TThis> *Tree_NoRecursive() const; // NOLINT

        public:
        [[nodiscard]] bool IsRoot() const;
        [[nodiscard]] const TThis *Root() const;
        [[nodiscard]] TThis *Root();

        public:
        [[nodiscard]] TThis *Parent() const;
        [[nodiscard]] vector<TThis *> Ancestors() const;
        [[nodiscard]] vector<const TThis *> AncestorsAndSelf() const;
        [[nodiscard]] vector<TThis *> AncestorsAndSelf();

        public:
        [[nodiscard]] Activity_ Activity() const;

        public:
        [[nodiscard]] const list<TThis *> &Children() const;
        [[nodiscard]] vector<TThis *> Descendants() const;
        [[nodiscard]] vector<const TThis *> DescendantsAndSelf() const;
        [[nodiscard]] vector<TThis *> DescendantsAndSelf();

        public:
        [[nodiscard]] const function<void(const any)> &OnBeforeAttachCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterAttachCallback() const;
        [[nodiscard]] const function<void(const any)> &OnBeforeDetachCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterDetachCallback() const;

        public:
        [[nodiscard]] const function<void(const any)> &OnBeforeActivateCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterActivateCallback() const;
        [[nodiscard]] const function<void(const any)> &OnBeforeDeactivateCallback() const;
        [[nodiscard]] const function<void(const any)> &OnAfterDeactivateCallback() const;

        protected:
        explicit NodeBase();

        public:
        explicit NodeBase(const NodeBase &other) = delete;
        explicit NodeBase(NodeBase &&other) = delete;
        virtual ~NodeBase();

        private:
        void Attach(TreeBase<TThis> *const tree, const any argument);
        void Attach(TThis *const parent, const any argument);
        void Detach(TreeBase<TThis> *const tree, const any argument);
        void Detach(TThis *const parent, const any argument);

        private:
        void Activate(const any argument);
        void Deactivate(const any argument);

        protected:
        virtual void OnAttach(const any argument);       // overriding methods must invoke base implementation
        virtual void OnBeforeAttach(const any argument); // overriding methods must invoke base implementation
        virtual void OnAfterAttach(const any argument);  // overriding methods must invoke base implementation
        virtual void OnDetach(const any argument);       // overriding methods must invoke base implementation
        virtual void OnBeforeDetach(const any argument); // overriding methods must invoke base implementation
        virtual void OnAfterDetach(const any argument);  // overriding methods must invoke base implementation

        protected:
        virtual void OnActivate(const any argument);         // overriding methods must invoke base implementation
        virtual void OnBeforeActivate(const any argument);   // overriding methods must invoke base implementation
        virtual void OnAfterActivate(const any argument);    // overriding methods must invoke base implementation
        virtual void OnDeactivate(const any argument);       // overriding methods must invoke base implementation
        virtual void OnBeforeDeactivate(const any argument); // overriding methods must invoke base implementation
        virtual void OnAfterDeactivate(const any argument);  // overriding methods must invoke base implementation

        protected:
        virtual void AddChild(TThis *const child, const any argument); // overriding methods must invoke base implementation
        void AddChildren(const vector<TThis *> &children, const any argument);
        virtual void RemoveChild(TThis *const child, const any argument, const function<void(const TThis *const, const any)> callback); // overriding methods must invoke base implementation
        bool RemoveChild(const function<bool(const TThis *const)> predicate, const any argument, const function<void(const TThis *const, const any)> callback);
        int32_t RemoveChildren(const function<bool(const TThis *const)> predicate, const any argument, const function<void(const TThis *const, const any)> callback);
        int32_t RemoveChildren(const any argument, const function<void(const TThis *const, const any)> callback);
        void RemoveSelf(const any argument, const function<void(const TThis *const, const any)> callback);

        protected:
        virtual void Sort(list<TThis *> &children) const;

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

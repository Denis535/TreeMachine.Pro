#pragma once
#include <any>
#include <functional>

namespace TreeMachine {
    using namespace std;

    template <typename TThis>
    class NodeBase2 : public NodeBase<TThis> {

        private:
        function<void(TThis *, const any)> m_OnBeforeDescendantAttachCallback;
        function<void(TThis *, const any)> m_OnAfterDescendantAttachCallback;
        function<void(TThis *, const any)> m_OnBeforeDescendantDetachCallback;
        function<void(TThis *, const any)> m_OnAfterDescendantDetachCallback;

        private:
        function<void(TThis *, const any)> m_OnBeforeDescendantActivateCallback;
        function<void(TThis *, const any)> m_OnAfterDescendantActivateCallback;
        function<void(TThis *, const any)> m_OnBeforeDescendantDeactivateCallback;
        function<void(TThis *, const any)> m_OnAfterDescendantDeactivateCallback;

        public:
        [[nodiscard]] const function<void(TThis *, const any)> &OnBeforeDescendantAttachCallback();
        [[nodiscard]] const function<void(TThis *, const any)> &OnAfterDescendantAttachCallback();
        [[nodiscard]] const function<void(TThis *, const any)> &OnBeforeDescendantDetachCallback();
        [[nodiscard]] const function<void(TThis *, const any)> &OnAfterDescendantDetachCallback();

        public:
        [[nodiscard]] const function<void(TThis *, const any)> &OnBeforeDescendantActivateCallback();
        [[nodiscard]] const function<void(TThis *, const any)> &OnAfterDescendantActivateCallback();
        [[nodiscard]] const function<void(TThis *, const any)> &OnBeforeDescendantDeactivateCallback();
        [[nodiscard]] const function<void(TThis *, const any)> &OnAfterDescendantDeactivateCallback();

        protected:
        explicit NodeBase2();

        public:
        explicit NodeBase2(const NodeBase2 &other) = delete;
        explicit NodeBase2(NodeBase2 &&other) = delete;
        ~NodeBase2() override;

        protected:
        void OnAttach(const any argument) override;       // overriding methods must invoke base implementation
        void OnBeforeAttach(const any argument) override; // overriding methods must invoke base implementation
        void OnAfterAttach(const any argument) override;  // overriding methods must invoke base implementation
        void OnDetach(const any argument) override;       // overriding methods must invoke base implementation
        void OnBeforeDetach(const any argument) override; // overriding methods must invoke base implementation
        void OnAfterDetach(const any argument) override;  // overriding methods must invoke base implementation
        virtual void OnBeforeDescendantAttach(TThis *descendant, const any argument);
        virtual void OnAfterDescendantAttach(TThis *descendant, const any argument);
        virtual void OnBeforeDescendantDetach(TThis *descendant, const any argument);
        virtual void OnAfterDescendantDetach(TThis *descendant, const any argument);

        protected:
        void OnActivate(const any argument) override;         // overriding methods must invoke base implementation
        void OnBeforeActivate(const any argument) override;   // overriding methods must invoke base implementation
        void OnAfterActivate(const any argument) override;    // overriding methods must invoke base implementation
        void OnDeactivate(const any argument) override;       // overriding methods must invoke base implementation
        void OnBeforeDeactivate(const any argument) override; // overriding methods must invoke base implementation
        void OnAfterDeactivate(const any argument) override;  // overriding methods must invoke base implementation
        virtual void OnBeforeDescendantActivate(TThis *descendant, const any argument);
        virtual void OnAfterDescendantActivate(TThis *descendant, const any argument);
        virtual void OnBeforeDescendantDeactivate(TThis *descendant, const any argument);
        virtual void OnAfterDescendantDeactivate(TThis *descendant, const any argument);

        public:
        void OnBeforeDescendantAttachCallback(function<void(TThis *, const any)> callback);
        void OnAfterDescendantAttachCallback(function<void(TThis *, const any)> callback);
        void OnBeforeDescendantDetachCallback(function<void(TThis *, const any)> callback);
        void OnAfterDescendantDetachCallback(function<void(TThis *, const any)> callback);

        public:
        void OnBeforeDescendantActivateCallback(function<void(TThis *, const any)> callback);
        void OnAfterDescendantActivateCallback(function<void(TThis *, const any)> callback);
        void OnBeforeDescendantDeactivateCallback(function<void(TThis *, const any)> callback);
        void OnAfterDescendantDeactivateCallback(function<void(TThis *, const any)> callback);

        public:
        NodeBase2 &operator=(const NodeBase2 &other) = delete;
        NodeBase2 &operator=(NodeBase2 &&other) = delete;
    };
}

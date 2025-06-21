#pragma once
#include <any>
#include <functional>

namespace TreeMachine {
    using namespace std;

    class TreeBase {
        friend class NodeBase;

        private:
        NodeBase *m_Root = nullptr;

        protected:
        [[nodiscard]] NodeBase *Root() const;

        public:
        explicit TreeBase() = default;
        explicit TreeBase(const TreeBase &other) = delete;
        explicit TreeBase(TreeBase &&other) = delete;
        virtual ~TreeBase() = default;

        protected:
        virtual void SetRoot(NodeBase *const root, const any argument, const function<void(NodeBase *const, const any)> callback);
        virtual void AddRoot(NodeBase *const root, const any argument);
        virtual void RemoveRoot(NodeBase *const root, const any argument, const function<void(NodeBase *const, const any)> callback);
        void RemoveRoot(const any argument, const function<void(NodeBase *const, const any)> callback);

        public:
        TreeBase &operator=(const TreeBase &other) = delete;
        TreeBase &operator=(TreeBase &&other) = delete;
    };
}

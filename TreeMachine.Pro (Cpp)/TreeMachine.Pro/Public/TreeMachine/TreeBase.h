#pragma once
#include <any>
#include <functional>
#include <variant>

namespace TreeMachine {
    using namespace std;

    class TreeBase {
        friend class NodeBase;

        private:
        NodeBase *m_Root = nullptr;

        public:
        explicit TreeBase() = default;
        explicit TreeBase(const TreeBase &other) = delete;
        explicit TreeBase(TreeBase &&other) = delete;
        virtual ~TreeBase() = default;

        protected:
        [[nodiscard]] NodeBase *const Root() const;
        virtual void SetRoot(NodeBase *const root, const any argument, const function<const void(NodeBase *const, const any)> callback);
        virtual void AddRoot(NodeBase *const root, const any argument);
        virtual void RemoveRoot(NodeBase *const root, const any argument, const function<const void(NodeBase *const, const any)> callback);

        public:
        TreeBase &operator=(const TreeBase &other) = delete;
        TreeBase &operator=(TreeBase &&other) = delete;
    };
}

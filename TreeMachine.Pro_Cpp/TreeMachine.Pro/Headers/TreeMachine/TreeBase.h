#pragma once
#include <any>
#include <functional>

namespace TreeMachine {
    using namespace std;

    // template <typename T, enable_if_t<is_base_of_v<NodeBase, T>>>
    class TreeBase {
        friend class NodeBase;

        private:
        NodeBase *m_Root = nullptr;

        protected:
        [[nodiscard]] NodeBase *Root() const;

        protected:
        explicit TreeBase();

        public:
        explicit TreeBase(const TreeBase &other) = delete;
        explicit TreeBase(TreeBase &&other) = delete;
        virtual ~TreeBase();

        protected:
        virtual void AddRoot(NodeBase *const root, const any argument);
        virtual void RemoveRoot(NodeBase *const root, const any argument, const function<void(const NodeBase *const, const any)> callback);
        virtual void RemoveRoot(const any argument, const function<void(const NodeBase *const, const any)> callback);

        public:
        TreeBase &operator=(const TreeBase &other) = delete;
        TreeBase &operator=(TreeBase &&other) = delete;
    };
}

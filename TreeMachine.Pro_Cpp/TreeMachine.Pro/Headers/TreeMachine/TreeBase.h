#pragma once
#include <any>
#include <functional>

namespace TreeMachine {
    using namespace std;

    template <typename T>
    class TreeBase {

        private:
        T *m_Root = nullptr;

        protected:
        [[nodiscard]] T *Root() const;

        protected:
        explicit TreeBase();

        public:
        explicit TreeBase(const TreeBase &other) = delete;
        explicit TreeBase(TreeBase &&other) = delete;
        virtual ~TreeBase();

        protected:
        virtual void AddRoot(T *const root, const any argument);                                                              // overriding methods must invoke base implementation
        virtual void RemoveRoot(T *const root, const any argument, const function<void(const T *const, const any)> callback); // overriding methods must invoke base implementation
        void RemoveRoot(const any argument, const function<void(const T *const, const any)> callback);

        public:
        TreeBase &operator=(const TreeBase &other) = delete;
        TreeBase &operator=(TreeBase &&other) = delete;
    };
}

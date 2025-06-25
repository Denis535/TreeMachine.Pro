#pragma once
#include <list>
#include <vector>

namespace TreeMachine::Internal {
    using namespace std;

    template <typename T>
    vector<T> reverse(const vector<T> &source) {
        auto result = vector<T>();
        copy(source.rbegin(), source.rend(), back_inserter(result));
        return result;
    }

    template <typename T>
    list<T> reverse(const list<T> &source) {
        auto result = list<T>();
        copy(source.rbegin(), source.rend(), back_inserter(result));
        return result;
    }

    template <typename T>
    bool contains(const list<T> &list, const T &item) {
        auto result = find(list.begin(), list.end(), item);
        return result != list.end();
    }

}

#include "NodeBase.h"
#include "NodeBase2.h"
#include "TreeBase.h"

#include "Impl_NodeBase.h"
#include "Impl_NodeBase2.h"
#include "Impl_TreeBase.h"

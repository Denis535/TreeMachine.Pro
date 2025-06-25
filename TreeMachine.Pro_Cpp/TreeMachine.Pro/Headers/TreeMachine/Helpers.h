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
    bool contains(const list<T> &source, const T &item) {
        auto result = find(source.begin(), source.end(), item);
        return result != source.end();
    }

}

if not exist "subprojects" (mkdir "subprojects")
meson wrap install catch2

meson setup "build/debug"   "TreeMachine.Pro"       --native-file=meson-native-file.ini --buildtype=debug
meson setup "build/release" "TreeMachine.Pro"       --native-file=meson-native-file.ini --buildtype=release

meson setup "tests"         "TreeMachine.Pro.Tests" --native-file=meson-native-file.ini --buildtype=debug

pause

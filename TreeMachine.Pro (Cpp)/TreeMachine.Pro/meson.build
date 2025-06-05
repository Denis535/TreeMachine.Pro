project('TreeMachine.Pro', ['cpp'], version : '1.0.0')

tree_machine_pro = static_library(
  'TreeMachine.Pro',
  files(
    'Private/TreeMachine/TreeBase.cpp',
    'Private/TreeMachine/NodeBase.cpp',
    'pch/pch.cpp',
  ),
  include_directories: include_directories('Public'),
  implicit_include_directories: false,
  cpp_pch: 'pch/pch.h',
  dependencies: [
  ],
  install: true,
  name_prefix: ''
)

install_subdir(
  'Public',
  install_dir: 'include',
  strip_directory: true
)

# tree_machine_pro_dep = declare_dependency(
#  include_directories: include_directories('Public'),
#  link_with: tree_machine_pro
# )
# meson.override_dependency('TreeMachine.Pro', tree_machine_pro_dep)

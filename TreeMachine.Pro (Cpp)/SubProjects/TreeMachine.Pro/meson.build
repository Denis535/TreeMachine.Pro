project('TreeMachine.Pro', ['cpp'])

static_library(
  'TreeMachine.Pro',
  files(
    'Private/TreeMachine/TreeBase.cpp',
    'Private/TreeMachine/NodeBase.cpp',
    'pch/pch.cpp',
  ),
  include_directories: include_directories('Public'),
  implicit_include_directories: false,
  cpp_pch: 'pch/pch.h',
  install: true,
  name_prefix: ''
)

install_subdir(
  'Public',
  install_dir: 'include',
  strip_directory: true
)

project('TreeMachine.Pro.Tests', ['cpp'])

tree_machine_pro_tests = executable(
  'TreeMachine.Pro.Tests',
  files(
    'Private/Tests_00.cpp',
    'pch/pch.cpp',
  ),
  include_directories: include_directories('Public'),
  implicit_include_directories: false,
  cpp_pch: 'pch/pch.h',
  dependencies: [
    # dependency('TreeMachine.Pro').get_variable('tree_machine_pro_dep'),
    # dependency('catch2').get_variable('catch2_with_main_dep'),
  ]
)

test('TreeMachine.Pro.Tests', tree_machine_pro_tests)

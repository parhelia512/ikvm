﻿IKVM.CoreLib.Modules

This is mostly a C# version of the java.lang.module and jdk.internal.module classes for loading JPMS modules. It operates almost exactly the same. You create a ModuleConfiguration, you add ModuleFinders, it loads modules, it resolves their relations. The goal is to mirror the functions and logic for the static compilation tools. The importer and exporter will use this to build a module graph from existing Java code for transpilation.

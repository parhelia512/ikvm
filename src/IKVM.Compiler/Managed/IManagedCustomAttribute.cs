﻿using System.Collections.Generic;

namespace IKVM.Compiler.Managed
{

    /// <summary>
    /// Describes a managed custom attribute type.
    /// </summary>
    internal interface IManagedCustomAttribute
    {

        /// <summary>
        /// Gets the type of the attribute.
        /// </summary>
        IManagedType AttributeType { get; }

        /// <summary>
        /// Gets the constructor method that represents the constructor that should be invoked to initialize the attribute.
        /// </summary>
        IManagedMethod Constructor { get; }

        /// <summary>
        /// Gets the list of positional arguments specified for the attribute.
        /// </summary>
        IReadOnlyList<IManagedCustomAttributeTypedArgument> ConstructorArguments { get; }

        /// <summary>
        /// Gets the list of named arguments specified for the attribute instance.
        /// </summary>
        IReadOnlyList<IManagedCustomAttributeNamedArgument> NamedArguments { get; }

    }

}
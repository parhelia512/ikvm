﻿using System;
using System.Collections.Generic;

namespace IKVM.JTReg.TestAdapter.Core
{

    [Serializable]
    public class JTRegTestOptions
    {

        /// <summary>
        /// Gets or sets the number of partitions to generate.
        /// </summary>
        public int PartitionCount { get; set; } = 8;

        /// <summary>
        /// Gets or sets the factor by which to multiply various timeouts.
        /// </summary>
        public float TimeoutFactor { get; set; } = 1.0f;

        /// <summary>
        /// Gets or sets the number of concurrent tests to execute.
        /// </summary>
        public int Concurrency { get; set; } = 0;

        /// <summary>
        /// Set of relative or absolute file names to add as exclude lists.
        /// </summary>
        public List<string> ExcludeListFiles { get; } = new List<string>()
        {
            "ProblemList.txt",
            "ExcludeList.txt",
        };

        /// <summary>
        /// Set of relative or absolute file names to add as exclude lists.
        /// </summary>
        public List<string> AdditionalExcludeListFiles { get; } = new List<string>()
        {

        };

        /// <summary>
        /// Set of relative or absolute file names to add as include lists.
        /// </summary>
        public List<string> IncludeListFiles { get; } = new List<string>()
        {
            "IncludeList.txt",
        };

        /// <summary>
        /// Set of relative or absolute file names to add as include lists.
        /// </summary>
        public List<string> AdditionalIncludeListFiles { get; } = new List<string>()
        {

        };

    }

}

﻿using System.Collections.Generic;
using Wyam.Common.Modules;

namespace Wyam.Common.Execution
{
    /// <summary>
    /// A read-only pipeline.
    /// </summary>
    public interface IReadOnlyPipeline : IReadOnlyModuleList
    {
        /// <summary>
        /// The name of the pipeline.
        /// </summary>
        string Name { get; }
    }
}
﻿using System;
using System.Collections.Generic;
using Statiq.Common;

namespace Statiq.Testing
{
    /// <summary>
    /// A pipeline with utility methods for easier testing.
    /// </summary>
    public class TestPipeline : IPipeline
    {
        public ModuleList InputModules { get; set; } = new ModuleList();

        public ModuleList ProcessModules { get; set;  } = new ModuleList();

        public ModuleList TransformModules { get; set;  } = new ModuleList();

        public ModuleList OutputModules { get; set;  } = new ModuleList();

        public HashSet<string> Dependencies { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public bool Isolated { get; set; }

        public ExecutionPolicy ExecutionPolicy { get; set; }

        public TestPipeline(params IModule[] processModules)
        {
            ProcessModules.AddRange(processModules);
        }

        public TestPipeline(IEnumerable<IModule> processModules)
        {
            ProcessModules.AddRange(processModules);
        }
    }
}

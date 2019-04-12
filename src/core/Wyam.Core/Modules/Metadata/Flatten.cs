﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Wyam.Common.Documents;
using Wyam.Common.Execution;
using Wyam.Common.Meta;
using Wyam.Common.Modules;
using Wyam.Common.Util;

namespace Wyam.Core.Modules.Metadata
{
    /// <summary>
    /// Flattens a tree structure given child documents are stored in a given metadata key ("Children" by default).
    /// The flattened documents are returned in no particular order.
    /// </summary>
    /// <metadata cref="Keys.Children" usage="Input"/>
    /// <category>Metadata</category>
    public class Flatten : IModule
    {
        private readonly string _childrenKey = Keys.Children;

        /// <summary>
        /// Creates a new flatten module.
        /// </summary>
        public Flatten()
        {
        }

        /// <summary>
        /// Creates a new flatten module with the specified children key.
        /// </summary>
        /// <param name="childrenKey">The metadata key that contains the children.</param>
        public Flatten(string childrenKey)
        {
            _childrenKey = childrenKey;
        }

        /// <inheritdoc />
        public Task<IEnumerable<IDocument>> ExecuteAsync(IReadOnlyList<IDocument> inputs, IExecutionContext context)
        {
            // Use a stack so we don't overflow the call stack with recursive calls for deep trees
            Stack<IDocument> stack = new Stack<IDocument>();
            foreach (IDocument root in inputs)
            {
                stack.Push(root);
            }
            List<IDocument> results = new List<IDocument>();
            while (stack.Count > 0)
            {
                IDocument current = stack.Pop();
                results.Add(current);
                IEnumerable<IDocument> children = current.DocumentList(_childrenKey);
                if (children != null)
                {
                    foreach (IDocument child in children)
                    {
                        stack.Push(child);
                    }
                }
            }
            return Task.FromResult<IEnumerable<IDocument>>(results);
        }
    }
}
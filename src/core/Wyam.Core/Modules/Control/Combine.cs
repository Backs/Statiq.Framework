﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyam.Common.Documents;
using Wyam.Common.Modules;
using Wyam.Common.Execution;
using Wyam.Common.Util;

namespace Wyam.Core.Modules.Control
{
    /// <summary>
    /// Combines all of the input documents into a single output document.
    /// </summary>
    /// <remarks>
    /// The first input document serves as the basis for the combine operation. The content of every
    /// following input document is appended to the existing combined content, and the metadata of
    /// every following document replaces that of the previous documents (any metadata for which
    /// keys don't exist in the following documents is retained). A single output document with
    /// the combined content and metadata is output.
    /// </remarks>
    /// <category>Control</category>
    public class Combine : IModule
    {
        /// <inheritdoc />
        public async Task<IEnumerable<IDocument>> ExecuteAsync(IReadOnlyList<IDocument> inputs, IExecutionContext context)
        {
            IDocument result = null;
            await context.ForEachAsync(inputs, async input =>
            {
                result = result == null
                    ? input
                    : await context.GetDocumentAsync(result, result.Content + input.Content, input);
            });
            return new[] { result };
        }
    }
}

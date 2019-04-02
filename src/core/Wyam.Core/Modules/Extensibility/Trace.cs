﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Wyam.Common.Configuration;
using Wyam.Common.Documents;
using Wyam.Common.Execution;
using Wyam.Common.Modules;

namespace Wyam.Core.Modules.Extensibility
{
    /// <summary>
    /// Outputs trace messages during execution.
    /// </summary>
    /// <remarks>
    /// This module has no effect on documents and the input documents are passed through to output documents.
    /// </remarks>
    /// <category>Extensibility</category>
    public class Trace : ContentModule
    {
        private TraceEventType _traceEventType = TraceEventType.Information;

        /// <summary>
        /// Outputs the string value of the specified object to trace.
        /// </summary>
        /// <param name="content">The content to trace.</param>
        public Trace(object content)
            : base(content)
        {
        }

        /// <summary>
        /// Outputs the string value of the returned object to trace. This allows
        /// you to trace different content depending on the execution context.
        /// </summary>
        /// <param name="content">A delegate that returns the content to trace.</param>
        public Trace(AsyncContextConfig content)
            : base(content)
        {
        }

        /// <summary>
        /// Outputs the string value of the returned object to trace. This allows
        /// you to trace different content for each document depending on the input document.
        /// </summary>
        /// <param name="content">A delegate that returns the content to trace.</param>
        public Trace(AsyncDocumentConfig content)
            : base(content)
        {
        }

        /// <summary>
        /// The specified modules are executed against an empty initial document and the
        /// resulting document content is output to trace.
        /// </summary>
        /// <param name="modules">The modules to execute.</param>
        public Trace(params IModule[] modules)
            : base(modules)
        {
        }

        /// <summary>
        /// Sets the event type to trace.
        /// </summary>
        /// <param name="traceEventType">The event type to trace.</param>
        /// <returns>The current module instance.</returns>
        public Trace EventType(TraceEventType traceEventType)
        {
            _traceEventType = traceEventType;
            return this;
        }

        /// <inheritdoc />
        protected override Task<IEnumerable<IDocument>> ExecuteAsync(object content, IDocument input, IExecutionContext context)
        {
            Common.Tracing.Trace.TraceEvent(_traceEventType, content.ToString());
            return Task.FromResult<IEnumerable<IDocument>>(new[] { input });
        }
    }
}

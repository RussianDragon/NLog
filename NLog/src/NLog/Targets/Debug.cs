// 
// Copyright (c) 2004,2005 Jaroslaw Kowalski <jkowalski@users.sourceforge.net>
// 
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of the Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

using System.Threading;

namespace NLog.Targets
{
    /// <summary>
    /// Counts logging messages but does not output them anywhere. Provides 
    /// the counter of logged messages and remembers the latest one.
    /// </summary>
    /// <example>
    /// <p>
    /// To set up the target in the <a href="config.html">configuration file</a>, 
    /// use the following syntax:
    /// </p>
    /// <xml src="examples/targets/Debug/DebugTarget.nlog" />
    /// <p>
    /// This assumes just one target and a single rule. More configuration
    /// options are described <a href="config.html">here</a>.
    /// </p>
    /// <p>
    /// To set up the log target programmatically use code like this:
    /// </p>
    /// <cs src="examples/targets/Debug/DebugTarget.cs" />
    /// </example>
    [Target("Debug")]
    public sealed class DebugTarget: Target
    {
        private int _counter = 0;
        private string _lastMessage = String.Empty;

        /// <summary>
        /// Increases the number of messages.
        /// </summary>
        /// <param name="ev">The logging event.</param>
        protected internal override void Append(LogEventInfo ev)
        {
            _counter++;
            _lastMessage = CompiledLayout.GetFormattedMessage(ev);
        }

        /// <summary>
        /// The number of times this target has been called.
        /// </summary>
        public int Counter
        {
            get { return _counter; }
        }

        /// <summary>
        /// The last message rendered by this target.
        /// </summary>
        public string LastMessage
        {
            get { return _lastMessage; }
        }
    }
}

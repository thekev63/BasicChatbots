using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Threading.Tasks;
// for logging
using Microsoft.Bot.Builder.History;
using Microsoft.Bot.Connector;
using System.IO;

// disable warnings for now
#pragma warning disable 1998

namespace FileLogging
{
    public class FileActivityLogger : IActivityLogger
    {
        TextWriter tw;

        // TODO - create a constructor taking a TextWriter as input, assign to tw

        public async Task LogAsync(IActivity activity)
        {
			// TODO - complete this method
			// Log activity to output and to a file
			// Apply a filter for extra credit
        }
    }
}

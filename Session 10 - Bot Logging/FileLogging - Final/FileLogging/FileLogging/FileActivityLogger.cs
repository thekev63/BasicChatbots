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

        // constructor
        public FileActivityLogger(TextWriter inputFile)
        {
            this.tw = inputFile;
        }

        public async Task LogAsync(IActivity activity)
        {
            // apply a filter
            if(!((Microsoft.Bot.Connector.Activity)activity).Text.ToLower().Contains("bot"))
            {
                // log to output as before
                Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} " +
                    $"- Message:{activity.AsMessageActivity().Text}");

                // log also to a file
                tw.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} " +
                    $"- Message:{activity.AsMessageActivity().Text}", true);
            }

            // save changes by clearing the buffer but keep the buffer open
            tw.Flush();
        }
    }
}

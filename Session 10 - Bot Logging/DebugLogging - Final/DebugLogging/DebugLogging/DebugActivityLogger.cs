using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Threading.Tasks;
// for logging
using Microsoft.Bot.Builder.History;
using Microsoft.Bot.Connector;

// disable warnings for now
#pragma warning disable 1998

namespace DebugLogging
{
    public class DebugActivityLogger : IActivityLogger
    {
        public async Task LogAsync(IActivity activity)
        {
            Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} " +
                $"- Message:{activity.AsMessageActivity()?.Text}");
        }
    }
}

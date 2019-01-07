using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace AddingChoices.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
		// TODO: add constants for 2 choices

        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;
			// TODO - Create a choice dialog to present the two member choices you created above
			// Use the below function as the callback
            PromptDialog.Choice(context: context,
                resume: this.AfterMenuSelection,
                options: new List<string>() { CSharp, Nodejs },
                prompt: "What is your choice of platform ?");
        }
        private async Task AfterMenuSelection(IDialogContext context, IAwaitable<string> result)
        {
			// TODO - await the result
			// Create a switch to postback a response for each possible choice
            context.Done(this);
        }
    }
}
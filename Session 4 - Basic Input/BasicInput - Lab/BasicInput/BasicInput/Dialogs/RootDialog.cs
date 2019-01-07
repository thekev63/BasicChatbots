using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BasicInput.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
		// TODO - add class member to save the response

        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;
			// TODO - create a prompt dialog that does the following:
			// 1. serves as an input dialog
			// 2. prompts for input of the user's name
			// 3. invokes a response function, ResumeGetName
        }
        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> userResponse)
        {
			// TODO - implement the following:
			// use await to pull the string from the response
			// assign to your class member
			// post a welcome message back to the user with their name
            context.Done(this);
        }
    }
}
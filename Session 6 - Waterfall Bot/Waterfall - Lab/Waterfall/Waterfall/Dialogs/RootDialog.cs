using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Waterfall.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
		// TODO add members for name, quest, color

        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
			// TODO - complete this method
			// Create a dialog to get the user's name
			// Call the ResumeGetName callback method
        }
        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> uName)
        {
			// TODO - complete this method
			// Get the name from the response and assign to member variable
			// Create a new dialog to get the quest information
			// Call the ResumeGetQuest callback method
        }
        public virtual async Task ResumeGetQuest(IDialogContext context, IAwaitable<string> uQuest)
        {
			// TODO - complete this method
			// Get the quest from the response and assign to member variable
			// Create a new dialog to get the color information
			// Call the ResumeGetColor callback method
        }
        public virtual async Task ResumeGetColor(IDialogContext context, IAwaitable<string> uColor)
        {
			// TODO - complete this method
			// Get the color from the response and assign to member variable
			// Post back name, quest and color information
			// Extra credit, post a result :)
            context.Done(this);
        }
    }
}
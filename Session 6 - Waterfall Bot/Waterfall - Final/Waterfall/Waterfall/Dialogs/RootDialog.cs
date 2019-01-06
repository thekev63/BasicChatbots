using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HelloWorld.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        string userName;
        string userQuest;
        string userColor;

        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetName,
                prompt: "What is your name?"
            );
        }
        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> uName)
        {
            string response = await uName;
            this.userName = response;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetQuest,
                prompt: "What is your quest?",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }
        public virtual async Task ResumeGetQuest(IDialogContext context, IAwaitable<string> uQuest)
        {
            string response = await uQuest;
            this.userQuest = response;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetColor,
                prompt: "What is your favorite color?",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }
        public virtual async Task ResumeGetColor(IDialogContext context, IAwaitable<string> uColor)
        {
            string response = await uColor;
            this.userColor = response;

            await context.PostAsync("Information entered by you:"
                + "\n\n" + "Name : "
                + userName + "\n" + "Quest : "
                + userQuest + "\n" + "Color : " + userColor);


            Random rnd = new Random();
            int result = rnd.Next(0, 2);
            if (result == 0 || userName == "King Arthur of Camelot")
                await context.PostAsync("You may pass, sir knight.");
            else
                await context.PostAsync("Yeeaaaaaaaaahhhhhhhhh - splat!");

            context.Done(this);
        }
    }
}
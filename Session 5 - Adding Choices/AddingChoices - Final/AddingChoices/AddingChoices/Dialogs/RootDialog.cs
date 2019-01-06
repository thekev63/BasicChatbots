using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace HelloWorld.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string CSharp = "C#";
        private const string Nodejs = "Node.js";

        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;

            PromptDialog.Choice(context: context,
                resume: this.AfterMenuSelection,
                options: new List<string>() { CSharp, Nodejs },
                prompt: "What is your choice of platform ?");
        }
        private async Task AfterMenuSelection(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            switch (optionSelected)
            {
                case CSharp:
                    await context.PostAsync("Choice selected by you : C#");
                    break;
                case Nodejs:
                    await context.PostAsync("Choice selected by you : Node.js");
                    break;
            }
            context.Done(this);
        }
    }
}
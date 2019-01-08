using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using QnAMakerDialog;

namespace QNA.Dialogs
{
    [Serializable]
    [QnAMakerService("https://kmqnademo.azurewebsites.net/qnamaker", "0dea50c6-4225-4f01-bf05-dfec836663f7", 
        "20dc628e-56d2-4176-a7e8-681511bbdae9")]
    public class RootDialog : QnAMakerDialog<object>
    {

    }
}
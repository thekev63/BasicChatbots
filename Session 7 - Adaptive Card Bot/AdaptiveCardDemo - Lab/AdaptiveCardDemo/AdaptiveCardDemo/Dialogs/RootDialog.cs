using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AdaptiveCards;
using System.Collections.Generic;

namespace HelloWorld.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var reply = context.MakeMessage();
			// TODO - complete this method
			// if input text is equal to "hi", create the input card by calling CreateInputCard
			// otherwise if message is not null
			// determine if the message value is Entry or Thanks
			// If Entry, create a display card, displaying all values from the input card
			// If Thanks, post back a thank you

        }

        public Attachment CreateInputCard()
        {
			// TODO - complete this method
            AdaptiveCard inputCard = new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {
                    new TextBlock() { Text = "Form",Weight = TextWeight.Bolder },
                }
            };
			// TODO - add a TextBlock to the card to display quest information
			// TODO - and three TextInputs to the card, one for the name, one for the Quest and one for the color
			// TODO - add a SubmitAction to the card
			// TODO - create an attachment for the card and return the attachment
        }

        public Attachment CreateDisplayCard(IMessageActivity message)
        {
            dynamic val = message.Value;
            string name = val.Name.ToString();
            string quest = val.Quest.ToString();
            string color = val.Color.ToString();

            // TODO = complete this method
			// create facts for name, quest, color
            // Create the fact set for the facts

            // Next up the hierarchy is the items list for facts collection
            List<CardElement> factItems = new List<CardElement>() { fs };

            // These are then grouped into a column
            Column colFacts = new Column() { Items = factItems };
            List<Column> lsFacts = new List<Column>() { colFacts };

            // TODO - Add columns to a ColumnSet
            // Finally add it to the items list

            // TODO - Add facts and two TextBlocks to the items list

            // items go into a container
            Container cntFacts = new Container() { Items = items };

            // container goes into the body of the card
            List<CardElement> crdBody = new List<CardElement>() { cntFacts };

            // TODO - define the action
            SubmitAction btnSubmit = new SubmitAction()
            {
                Title = "",
                DataJson = ""
            };

            List<ActionBase> submitAction = new List<ActionBase>() { btnSubmit };

            // TODO - add body and actions to a new card

			// Return as an attachment
            return attachment;
        }

    }
}
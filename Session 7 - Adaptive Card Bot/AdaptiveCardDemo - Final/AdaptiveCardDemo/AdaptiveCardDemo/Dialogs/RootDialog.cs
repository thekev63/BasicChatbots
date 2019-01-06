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

            if (message.Text != null && message.Text.Equals("hi"))
            {
                var attachment = CreateInputCard();
                reply.Attachments.Add(attachment);
                await context.PostAsync(reply);
                context.Wait(this.MessageReceivedAsync);
            }

            if (message.Value != null)
            {
                // Got an Action Submit
                dynamic value = message.Value;
                string submitType = value.Type.ToString();
                switch (submitType)
                {
                    case "Entry":
                        var attachment1 = CreateDisplayCard(message);
                        reply.Attachments.Add(attachment1);
                        await context.PostAsync(reply);
                        context.Wait(this.MessageReceivedAsync);
                        return;
                    case "Thanks":
                        await context.PostAsync("Thank you for your response.");
                        context.Wait(this.MessageReceivedAsync);
                        return;
                }

            }

        }

        public Attachment CreateInputCard()
        {
            AdaptiveCard inputCard = new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {
                    new TextBlock() { Text = "Form",Weight = TextWeight.Bolder },
                }
            };
            inputCard.Body.Add(new TextBlock()
            {
                Text = "Quest Information for Review.",
                Weight = TextWeight.Normal
            });
            inputCard.Body.Add(new TextInput()
            {
                Id = "Name",
                Style = TextInputStyle.Text
            });
            inputCard.Body.Add(new TextInput()
            {
                Id = "Quest",
                Style = TextInputStyle.Text
            });

            inputCard.Body.Add(new TextInput()
            {
                Id = "Color",
                Style = TextInputStyle.Text
            });
            inputCard.Actions.Add(new SubmitAction()
            {
                Title = "Submit",
                DataJson = "{\"Type\": \"Entry\" }"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = inputCard
            };
            return attachment;
        }

        public Attachment CreateDisplayCard(IMessageActivity message)
        {
            dynamic val = message.Value;
            string name = val.Name.ToString();
            string quest = val.Quest.ToString();
            string color = val.Color.ToString();

            // first the facts
            AdaptiveCards.Fact factName = new AdaptiveCards.Fact()
            {
                Title = "Name:",
                Value = name
            };

            AdaptiveCards.Fact factQuest = new AdaptiveCards.Fact()
            {
                Title = "Quest:",
                Value = quest
            };

            AdaptiveCards.Fact factColor = new AdaptiveCards.Fact()
            {
                Title = "Color:",
                Value = color
            };

            // Create the fact collection
            FactSet fs = new FactSet()
            {
                Facts = new List<AdaptiveCards.Fact>()
                {
                    factName,
                    factQuest,
                    factColor
                }
            };

            // Next up the hierarchy is the items list for facts collection
            List<CardElement> factItems = new List<CardElement>() { fs };

            // These are then grouped into a column
            Column colFacts = new Column() { Items = factItems };
            List<Column> lsFacts = new List<Column>() { colFacts };

            // Column is rolled up into a ColumnSet
            ColumnSet csFacts = new ColumnSet() { Columns = lsFacts };

            // Finally add it to the items list
            List<CardElement> items = new List<CardElement>();

            // these also go into the list
            items.Add(new TextBlock()
            {
                Text = "Summary",
                Weight = TextWeight.Bolder,
                Size = TextSize.Medium
            });

            items.Add(new TextBlock()
            {
                Text = "Please confirm the following information entered by you.",
                Weight = TextWeight.Normal,
            });

            items.Add(csFacts);

            // items go into a container
            Container cntFacts = new Container() { Items = items };

            // container goes into the body of the card
            List<CardElement> crdBody = new List<CardElement>() { cntFacts };

            // define the action
            SubmitAction btnSubmit = new SubmitAction()
            {
                Title = "Confirm",
                DataJson = "{ \"Type\": \"Thanks\" }"
            };

            List<ActionBase> submitAction = new List<ActionBase>() { btnSubmit };

            // finally we can put them together into the card
            AdaptiveCard card = new AdaptiveCard() { Body = crdBody, Actions = submitAction };

            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
            return attachment;
        }

        /*

        public Attachment CreateAdapativeCard()
        {
            AdaptiveCard card = new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {
                    new TextBlock() { Text = "Form",Weight = TextWeight.Bolder },
                }
            };
            card.Body.Add(new TextBlock()
            {
                Text = "Quest Information for Review.",
                Weight = TextWeight.Normal
            });
            card.Body.Add(new TextInput()
            {
                Id = "Name",
                Style = TextInputStyle.Text
            });

            card.Body.Add(new TextInput()
            {
                Id = "Quest",
                Style = TextInputStyle.Text
            });

            card.Body.Add(new TextInput()
            {
                Id = "Color",
                Style = TextInputStyle.Text
            });

            card.Actions.Add(new SubmitAction()
            {
                Title = "Submit",
                DataJson = "{\"Type\": \"Entry\" }"
            });

            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
            return attachment;

        }
        

        public Attachment FormAdapativeCard(IMessageActivity message)
        {
            dynamic val = message.Value;
            string name = val.Name.ToString();
            string quest = val.Quest.ToString();
            string color = val.Color.ToString();

            // first the facts
            AdaptiveCards.Fact factName = new AdaptiveCards.Fact()
            {
                Title = "Name:",
                Value = name
            };

            AdaptiveCards.Fact factQuest = new AdaptiveCards.Fact()
            {
                Title = "Quest:",
                Value = quest
            };

            AdaptiveCards.Fact factColor = new AdaptiveCards.Fact()
            {
                Title = "Color:",
                Value = color
            };

            // Create the fact collection
            FactSet fs = new FactSet()
            {
                Facts = new List<AdaptiveCards.Fact>()
                {
                    factName,
                    factQuest,
                    factColor
                }
            };

            // Next up the hierarchy is the items list for facts collection
            List<CardElement> factItems = new List<CardElement>() { fs };

            // These are then grouped into a column
            Column colFacts = new Column(){ Items = factItems };
            List<Column> lsFacts = new List<Column>() { colFacts };
            
            // Column is rolled up into a ColumnSet
            ColumnSet csFacts = new ColumnSet() { Columns = lsFacts };

            // Finally add it to the items list
            List<CardElement> items = new List<CardElement>();

            items.Add(new TextBlock()
            {
                Text = "Summary",
                Weight = TextWeight.Bolder,
                Size = TextSize.Medium
            });

            items.Add(new TextBlock()
            {
                Text = "Please confirm the following information entered by you.",
                Weight = TextWeight.Normal,
            });

            items.Add(csFacts);

            // items go into a container
            Container cntFacts = new Container() { Items = items };

            // container goes into the body of the card
            List<CardElement> crdBody = new List<CardElement>() {cntFacts };

            // define the action
            SubmitAction btnSubmit = new SubmitAction()
            {
                Title = "Confirm",
                DataJson = "{ \"Type\": \"Thanks\" }"
            };

            List<ActionBase> submitAction = new List<ActionBase>() { btnSubmit };

            // finally we can put them together into the card
            AdaptiveCard card = new AdaptiveCard() { Body = crdBody, Actions = submitAction };

            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
            return attachment;
        }
        */
    }
}
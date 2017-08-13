using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine;

namespace PersonalBuddy
{
    class BotConnection
    {
        public DirectLineClient Client = new DirectLineClient("xNAfb9rYDqk.cwA.AG0.MMnwIrwLXLHkPIptUaztO3h3MihzzbC8MzMaRmM6UOs");
        public Conversation MainConversation;
        public ChannelAccount Account;

        public BotConnection(string accountName)
        {
            MainConversation = Client.Conversations.StartConversation();
            Account = new ChannelAccount { Id = accountName, Name = accountName };
        }

        public void SendMessage(string message)
        {
            Activity activity = new Activity
            {
                From = Account,
                Text = message,
                Type = ActivityTypes.Message
            };
            Client.Conversations.PostActivity(MainConversation.ConversationId, activity);
        }

        public async Task GetMessagesAsync(ObservableCollection<MessageListItem> collection)
        {
            string watermark = null;

            while (true)
            {
                Debug.WriteLine("Reading message every 3 seconds");

                var activitySet = await Client.Conversations.GetActivitiesAsync(MainConversation.ConversationId, watermark);
                watermark = activitySet?.Watermark;

                foreach (Activity activity in activitySet.Activities)
                {
                    if (activity.From.Name == "personalbot")
                    {
                        collection.Add(new MessageListItem(activity.Text, activity.From.Name));
                    }
                }

                await Task.Delay(3000);
            }
        }


    }
}

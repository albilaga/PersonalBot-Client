using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PersonalBuddy
{
    public partial class MainPage : ContentPage
    {
        //Initialize a connection with ID and Name
        BotConnection connection = new BotConnection("Albi");

        //ObservableCollection to store the messages to be displayed
        ObservableCollection<MessageListItem> messageList = new ObservableCollection<MessageListItem>();

        public MainPage()
        {
            InitializeComponent();

            //Bind the ListView to the ObservableCollection
            MessageListView.ItemsSource = messageList;

            //Start listening to messages
            var messageTask = connection.GetMessagesAsync(messageList);

        }

        //Send method for message entry
        public void Send(object sender, EventArgs args)
        {
            //Get text in entry
            var message = ((Entry)sender).Text;

            if (message.Length > 0)
            {
                //Clear entry
                ((Entry)sender).Text = "";

                //Make object to be placed in ListView
                var messageListItem = new MessageListItem(message, connection.Account.Name);
                messageList.Add(messageListItem);

                //Send the message to the bot
                connection.SendMessage(message);
            }
        }

    }
}

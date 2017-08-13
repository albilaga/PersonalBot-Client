namespace PersonalBuddy
{
    class MessageListItem
    {
        public string Text { get; set; }
        public string Sender { get; set; }

        public MessageListItem(string text, string sender)
        {
            Text = text;
            Sender = sender;
        }
    }
}

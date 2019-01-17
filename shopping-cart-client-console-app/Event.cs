using System;

namespace shopping_cart_client_console_app
{
    public class Event
    {
        public Event(string name, string type = "notification")
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Type = type;
        }

        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }
    }
}
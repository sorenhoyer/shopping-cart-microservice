using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class EventStore
    {
        public static List<Event> events = new List<Event>();

        public List<Event> getEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            List<Event> filteredEvents = new List<Event>();

            for (int i = 0; i < events.Count; i++)
            {
                if (i >= firstEventSequenceNumber && i <= lastEventSequenceNumber)
                {
                    filteredEvents.Add(events[i]);
                }
            }

            return filteredEvents;
        }

        public void Add(Event ev)
        {
            events.Add(ev);
        }

        public void Remove(string ev)
        {
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].Id.ToString() == ev)
                {
                    events.RemoveAt(i);
                    Console.WriteLine("Removed event: " + ev);
                }
            }
        }
    }
}

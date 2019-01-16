using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class EventStore
    {
        public static List<string> events = new List<string>();

        public List<string> getEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            List<string> filteredEvents = new List<string>();

            for (int i = 0; i < events.Count; i++)
            {
                if (i >= firstEventSequenceNumber && i <= lastEventSequenceNumber)
                {
                    filteredEvents.Add(events[i]);
                }
            }

            return filteredEvents;
        }

        public void Add(string ev)
        {
            events.Add(ev);
        }

        public void Remove(string ev)
        {
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i] == ev)
                {
                    events.RemoveAt(i);
                    Console.WriteLine("Removed event: " + ev);
                }
            }
        }
    }
}

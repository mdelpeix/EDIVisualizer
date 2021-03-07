using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginApp.Classes
{

    public class Message
    {

        public string MessageId { get; set; }
        public int CountSegments { get; set; }
        public Segment UNH { get; private set; }
        public List<Segment> Segments { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Title { get; set; }

        public Message(string messageId, int countSegments, string UNHContent)
        {
            MessageId = messageId;
            CountSegments = countSegments;
            Segment UNH = new Segment("UNH", string.Format("UNH+{0}+{1}", messageId, UNHContent));
            List<string> words = UNHContent.Split(':').ToList();
            Type = words[0];
            Version = words[1];
            Release = words[2];
            Title = string.Format("{0} {1}{2}", this.Type, this.Version, this.Release);
            this.UNH = UNH;
        }

    }
}

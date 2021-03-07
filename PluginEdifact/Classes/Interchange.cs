using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PluginApp.Classes
{
    public class Interchange
    {

        public char TerminatorSegment { get; private set; }
        public string TerminatorPattern;
        public char ComponentSeparator { get; private set; }
        public char DataElementSeparator { get; private set; }
        public char DecimalNotification { get; private set; }
        public char ReleaseIndicator { get; private set; }
        public char Reserved { get; private set; }
        public Segment UNA { get { return this.GetSegment("UNA").First(); } private set { } }
        public Segment UNB { get { return this.GetSegment("UNB").First(); } private set { } }
        public Segment UNT { get { return this.GetSegment("UNT").First(); } private set { } }
        public Segment UNZ { get { return this.GetSegment("UNZ").First(); } private set { } }
        public List<Segment> Segments;
        public List<Message> Messages;
        public List<FunctionnalGroup> FunctionnalGroups;
        public string Content;


        public Interchange(string UNASegment, string content)
        {

            Content = content;
            string unaSegmentContent = string.Empty;

            if (UNASegment.StartsWith("UNA"))
            {
                char[] elems = UNASegment.ToCharArray();
                ComponentSeparator = elems[3];
                DataElementSeparator = elems[4];
                DecimalNotification = elems[5];
                ReleaseIndicator = elems[6];
                Reserved = elems[7];
                TerminatorSegment = elems[8];
                unaSegmentContent = UNASegment.Substring(3, UNASegment.Length - 3);
            }
            else
            {
                ComponentSeparator = ':';
                DataElementSeparator = '+';
                DecimalNotification = '.';
                ReleaseIndicator = '?';
                Reserved = ' ';
                TerminatorSegment = '\'';
                unaSegmentContent = "{NONE}";
            }

            TerminatorPattern = string.Format(@"[A-Z][A-Z][A-Z].*?(?<!\{1}){0}", TerminatorSegment, ReleaseIndicator);

            Segments = new List<Segment>();
            Segments.Add(new Segment("UNA", unaSegmentContent));
            Segments.Add(this.CreateSegment("UNB"));

            //Obtenir tous les messages présents dans l'interchange
            this.Messages = this.GetMessages();

            foreach (Message message in this.Messages)
            {
                Segments.Add(new Segment(message.UNH.Content, message.Content));
                Segments.Add(this.CreateSegmentUNT(message));
            }

            Segments.Add(this.CreateSegment("UNZ"));


        }

        private List<Message> GetMessages()
        {

            string SegmentPattern = string.Format(@"UNH\+(?<tag>\w*)\+((?<text>.*)\{0}\r?\n?)*UNT\+(?<nbr>\d*)\+\k<tag>\{0}\r?\n?", TerminatorSegment);
            MatchCollection substrings = Regex.Matches(Content, SegmentPattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            Message message = null;
            List<Message> messageList = new List<Message>();

            try
            {

                if (substrings.Count > 0)
                {

                    foreach (Match match in substrings)
                    {

                        GroupCollection groups = match.Groups;
                        message = new Message(groups["tag"].Value, int.Parse(groups["nbr"].Value), groups["text"].Captures[0].Value);
                        List<Segment> Segments = new List<Segment>();
                        for (int i = 1; i < groups["text"].Captures.Count; i++)
                        {
                            Segment sgmt = new Segment(groups["text"].Captures[i].Value.Substring(0, 3), groups["text"].Captures[i].Value);
                            Segments.Add(sgmt);
                        }
                        message.Segments = Segments;
                        messageList.Add(message);
                    }

                }
                else
                    throw new Exception("No message was found (UNB0062 must be equal to UNT0062 Message reference number)");

            }
            catch (Exception ex)
            {
                message = null;
                throw ex;
            }

            return messageList;

        }

        private Segment CreateSegment(string SegmentName)
        {

            string SegmentPattern = string.Format(@"{0}.*?(?<!\{2}){1}", SegmentName, TerminatorSegment, ReleaseIndicator);

            MatchCollection substrings = Regex.Matches(Content, SegmentPattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
            Segment segment = null;

            try
            {
                string tmp = substrings[0].Value;
                segment = new Segment(SegmentName, tmp.Substring(3, tmp.Length - 4));
            }
            catch (Exception)
            {
                segment = null;
            }

            return segment;

        }

        private Segment CreateSegmentUNT(Message message)
        {
            Segment segment = null;

            Regex reg = new Regex(@"UNH\+(.*?)\+");
            Match match = reg.Match(message.UNH.Content);
            string numMessage = string.Empty;
            if (match.Success)
                numMessage = match.Groups[1].Value;

            segment = new Segment("UNT", string.Format("+{0}+{1}", message.Segments.Count + 2, numMessage));
            return segment;
        }


        private List<Segment> GetSegment(string SegmentName)
        {

            var query = from s in Segments
                        where s.Name == SegmentName
                        select s;

            if (query.Count() > 0)
                return query.ToList();
            else
                return null;

        }

        public bool HasFunctionnalGroups()
        {
            if (this.GetSegment("UNG") != null)
                return true;
            else
                return false;
        }


        public int CountAllSegments()
        {
            int compt = 0;
            compt = this.Segments.Count;
            foreach (Message mess in this.Messages)
                compt += mess.Segments.Count;
            return compt;
        }
    }
}

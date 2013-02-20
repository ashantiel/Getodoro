using System.Collections.Generic;

namespace Getodoro
{
    public class Tasks
    {
        public Tasks()
        {
            this.LastID = 0;
        }

        [System.Xml.Serialization.XmlArray]
        public List<TaskObject> TaskList { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int LastID { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int BreakOther { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int BreakByBoss { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int BreakByCall { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int BreakByMeeting { get; set; }
    }
}
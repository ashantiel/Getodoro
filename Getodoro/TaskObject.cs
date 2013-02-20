using System;
using System.ComponentModel;

namespace Getodoro
{
    public class TaskObject : INotifyPropertyChanged
    {
        private String _title;
        private int _timeSpendInMinutes;

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskObject()
        {
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public String Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                    PropertyChanged(this, new PropertyChangedEventArgs("TitleWithTime"));
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int TimeSpendInMinutes
        {
            get { return this._timeSpendInMinutes; }
            set
            {
                this._timeSpendInMinutes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeSpendInMinutes"));
                    PropertyChanged(this, new PropertyChangedEventArgs("TitleWithTime"));
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public DateTime LastWorkDate { get; set; }

        [System.Xml.Serialization.XmlIgnore()]
        public String TitleWithTime
        {
            get
            {
                return String.Format("{0,3:D1}:{1:D2}  {2}", (int)Math.Floor((decimal)this.TimeSpendInMinutes / 60), this.TimeSpendInMinutes % 60, this.Title);
            }
            set { }
        }
    }
}
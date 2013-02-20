using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace Getodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String UserName { get; set; }

        public ObservableCollection<TaskObject> TheList { get; set; }

        private int lastId { get; set; }

        private int checkedId { get; set; }
        private int workOnID { get; set; }

        private int howBreaks { get; set; }

        private String AppName = "Getodoro";
        private bool _isWorking = false;
        private DispatcherTimer _dispatcherTimer;
        private TimeTick _time = new TimeTick(25);

        public MainWindow()
        {
            InitializeComponent();
            this.UserName = Environment.UserName;
            Tasks tasks = this.DeserializeFromXML();
            this.lastId = tasks.LastID;
            this.TheList = new ObservableCollection<TaskObject>(tasks.TaskList ?? new List<TaskObject>());
            this.DataContext = this;
            this.checkedId = -1;

            this._dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this._dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            this._dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            //this._dispatcherTimer.Start();
        }

        //public void CreateTheList()
        //{
        //    TheList = new ObservableCollection<TaskObject>();
        //    this.DataContext = this;
        //}

        public void AddToList(TaskObject obj)
        {
            this.TheList.Add(obj);
            this.DataContext = this;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.SerializeToXML(new Tasks { TaskList = this.TheList.ToList(), LastID = this.lastId });
            this.Close();
        }

        private void expander1_Collapsed(object sender, RoutedEventArgs e)
        {
            this.Height -= 110;
        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {
            this.Height += 110;
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (_isWorking)
            {
                this.buttonStart.Content = "Start";
                this._dispatcherTimer.Stop();
                this._time = new TimeTick(1500);
                this._isWorking = false;
            }
            else
            {
                this.workOnID = this.checkedId;
                this.buttonStart.Content = "Stop";
                this._dispatcherTimer.Start();
                this._isWorking = true;
            }
        }

        private void buttonBreak_Click(object sender, RoutedEventArgs e)
        {
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask newTask = new AddTask();
            newTask.ShowDialog();

            if (newTask.DialogResult.HasValue && newTask.DialogResult.Value)
            {
                this.lastId++;
                this.AddToList(new TaskObject { Title = newTask.TaskName, TimeSpendInMinutes = 0, ID = this.lastId });
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkedId >= 0)
            {
                this.TheList.Remove(this.TheList.FirstOrDefault(x => x.ID == this.checkedId));
                this.DataContext = this;
            }
        }

        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            this.buttonStart.IsEnabled = true;
            this.checkedId = (int)rb.Tag;
        }

        public void SerializeToXML(Tasks tasks)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tasks));
            TextWriter textWriter = new StreamWriter(String.Format("{0}-{1}.xml", this.UserName, this.AppName));
            serializer.Serialize(textWriter, tasks);
            textWriter.Close();
        }

        public Tasks DeserializeFromXML()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Tasks));
            Tasks tasks;
            try
            {
                TextReader textReader = new StreamReader(String.Format("{0}-{1}.xml", this.UserName, this.AppName));
                tasks = (Tasks)deserializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (System.IO.FileNotFoundException e)
            {
                tasks = new Tasks();
            }

            return tasks;
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkedId >= 0)
            {
                var task = this.TheList.FirstOrDefault(x => x.ID == this.checkedId);

                if (task != null)
                {
                    AddTask newTask = new AddTask(task.Title);
                    newTask.ID = task.ID;
                    newTask.ShowDialog();

                    if (newTask.DialogResult.HasValue && newTask.DialogResult.Value)
                    {
                        this.TheList[this.TheList.IndexOf(task)].Title = newTask.TaskName;
                    }
                    this.DataContext = this;
                }
            }
        }

        private void buttonFinish_Click(object sender, RoutedEventArgs e)
        {
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (this._time.Tick())
            {
                WorkEnd();
            }
            this.mainTimer.Text = String.Format("{0,2}:{1:D2}", this._time.Minute, this._time.Second);
        }

        private void WorkEnd()
        {
            this.howBreaks++;
            this.TheList.FirstOrDefault(x => x.ID == this.workOnID).TimeSpendInMinutes += this._time.Minutes;
            if (this.howBreaks >= 4)
            {
                this._time = new TimeTick(15);
                this.howBreaks = 0;
            }
            else
            {
                this._time = new TimeTick(5); 
            }
        }
    }
}
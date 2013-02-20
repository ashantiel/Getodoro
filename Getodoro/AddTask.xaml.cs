using System;
using System.Windows;

namespace Getodoro
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public String TaskName { get; set; }

        public int ID { get; set; }

        public AddTask()
        {
            InitializeComponent();
        }

        public AddTask(String taskName)
        {
            InitializeComponent();
            this.textBox1.Text = taskName;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.TaskName = this.textBox1.Text;
            this.DialogResult = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Getodoro
{
    /// <summary>
    /// Interaction logic for MetroMessageBox.xaml
    /// </summary>
    public partial class MetroMessageBox : Window
    {
        private MessageBoxButton _buttons = MessageBoxButton.OK;

        public MetroMessageBox()
        {
            Left = -50;
            Top = System.Windows.SystemParameters.PrimaryScreenHeight/2 -(150);
            Width = System.Windows.SystemParameters.PrimaryScreenWidth + 100;
            InitializeComponent();
        }
 
        protected override void OnActivated(EventArgs e) {
            if (Owner != null) {
                if (Owner.WindowState == WindowState.Maximized) {
                    Left = 0;
                    Top = (Owner.Height - 200)/2;
                    Width = Owner.Width;
                }
                else {
                    Left = 0;
                    Top = Owner.Top + ((Owner.Height - 200)/2);
                    Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                    
                }
            }
 
            base.OnActivated(e);
        }
        public Visibility IsNoButtonVisible
        {
            get { if (_buttons == MessageBoxButton.YesNo || _buttons == MessageBoxButton.YesNoCancel) return Visibility.Visible; else return Visibility.Hidden; }
        }

        public Visibility IsYesButtonVisible
        {
            get { if (_buttons == MessageBoxButton.YesNo || _buttons == MessageBoxButton.YesNoCancel) return Visibility.Visible; else return Visibility.Hidden; }
        }

        public Visibility IsCancelButtonVisible
        {
            get { if (_buttons == MessageBoxButton.OKCancel || _buttons == MessageBoxButton.YesNoCancel) return Visibility.Visible; else return Visibility.Hidden; }
        }

        public Visibility IsOkButtonVisible
        {
            get { if (_buttons == MessageBoxButton.OK || _buttons == MessageBoxButton.OKCancel) return Visibility.Visible; else return Visibility.Hidden;  }
        }
    }
}

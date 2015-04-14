using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caros.Recovery
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Environment.GetCommandLineArgs().Any())
            {
                Environment.Exit(1);
                return;
            }

            var argument = Environment.GetCommandLineArgs().Last();
            var messageParts = argument.Split(new[] { " @ " }, StringSplitOptions.None);

            labelError.Text = "SYSTEM FAULT"
                + Environment.NewLine
                + messageParts[0]
                + Environment.NewLine
                + messageParts[1];
        }
    }
}

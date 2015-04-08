using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Caros
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private const double EmulatorScale = 0.7;

        public MainView()
        {
            InitializeComponent();
            ScaleWindow();
        }

        [Conditional("DEBUG")]
        private void ScaleWindow()
        {
            var scale = EmulatorScale;
            LayoutRoot.LayoutTransform = new ScaleTransform(scale, scale);
            this.Width *= scale;
            this.Height *= scale;
        }
    }
}

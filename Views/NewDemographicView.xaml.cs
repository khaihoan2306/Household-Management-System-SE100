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
using System.Windows.Shapes;

namespace Household_Management_System.Views
{
    /// <summary>
    /// Interaction logic for NewDemographicView.xaml
    /// </summary>
    public partial class NewDemographicView : Window
    {
        public NewDemographicView()
        {
            InitializeComponent();
        }

        private void btnBirth_Click(object sender, RoutedEventArgs e)
        {
            PopupBirthCalendar.IsOpen = true;
        }

        private void btnDateProvide_Click(object sender, RoutedEventArgs e)
        {
            PopupDateProvideCalendar.IsOpen = true;
        }
    }
}

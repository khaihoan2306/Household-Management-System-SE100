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
    /// Interaction logic for HouseholdView.xaml
    /// </summary>
    public partial class HouseholdView : UserControl
    {
        public HouseholdView()
        {
            InitializeComponent();
        }

        private void HouseholdList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void HouseholdList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

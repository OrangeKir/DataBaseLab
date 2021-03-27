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

namespace DataBaseLab.StructureView.View_Elements
{
    /// <summary>
    /// Interaction logic for MountlyProfitStruct.xaml
    /// </summary>
    public partial class MountlyProfitStruct : UserControl
    {
        public MountlyProfitStruct(string inDate, string[] inWeekProfit, string inMountlyProfit)
        {
            InitializeComponent();

            DateText.Text = inDate;

            SundayText.Text = inWeekProfit[0];
            MondayText.Text = inWeekProfit[1];
            TuesdayText.Text = inWeekProfit[2];
            WednesdayText.Text = inWeekProfit[3];
            ThursdayText.Text = inWeekProfit[4];
            FridayText.Text = inWeekProfit[5];
            SaturdayText.Text = inWeekProfit[6];

            MountlyProfitText.Text = inMountlyProfit;
        }
    }
}

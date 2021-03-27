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
    /// Interaction logic for ManagerStruct.xaml
    /// </summary>
    public partial class ManagerStruct : UserControl
    {
        public ManagerStruct(string inId, string inName, string inCurentProfit)
        {
            InitializeComponent();

            IdText.Text = inId;
            ManagerNameText.Text = inName;
            CurentProfitText.Text = inCurentProfit;
        }
    }
}

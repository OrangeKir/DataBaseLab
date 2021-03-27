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
    /// Interaction logic for ProfitArchiveStruct.xaml
    /// </summary>
    public partial class ProfitArchiveStruct : UserControl
    {
        public ProfitArchiveStruct(string inId, string inDate, string inName, string inProfit)
        {
            InitializeComponent();

            IdText.Text = inId;
            DateText.Text = inDate;
            ManagerNameText.Text = inName;
            ProfitText.Text = inProfit;
        }
    }
}

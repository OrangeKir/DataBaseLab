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
    /// Interaction logic for BuyStruct.xaml
    /// </summary>
    public partial class BuyStruct : UserControl
    {
        public BuyStruct(string inId, string inName, string inAmount, string inAgentId, string inDate)
        {
            InitializeComponent();

            IdText.Text = inId;
            ProductNameText.Text = inName;
            AmountText.Text = inAmount;
            AgentIdText.Text = inAgentId;
            DateText.Text = inDate;
        }
    }
}

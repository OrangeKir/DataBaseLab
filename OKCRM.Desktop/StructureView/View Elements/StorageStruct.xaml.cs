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
    /// Interaction logic for StorageStruct.xaml
    /// </summary>
    public partial class StorageStruct : UserControl
    {
        public StorageStruct(string inId, string inName, string inAmount, string inMinAmount, string inSectionId, string inAgentId, string inPrice)
        {
            InitializeComponent();

            IdText.Text = inId;
            ProductNameText.Text = inName;
            AmountText.Text = inAmount;
            MinAmountText.Text = inMinAmount;
            SectionIdText.Text = inSectionId;
            AgentIdText.Text = inAgentId;
            PriceText.Text = inPrice;
        }
    }
}

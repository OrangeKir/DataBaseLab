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
using System.Text.RegularExpressions;

namespace DataBaseLab.StructureView.Add_Elements
{
    /// <summary>
    /// Interaction logic for AddAgentElement.xaml
    /// </summary>
    public partial class AddAgentElement : UserControl
    {
        Structure_Blank parent;
        public AddAgentElement(Structure_Blank inParent)
        {
            parent = inParent;
            InitializeComponent();
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameText != null)
            {
                parent.addName = NameText.Text;
            }
        }

    }
}

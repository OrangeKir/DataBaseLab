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
    /// Interaction logic for AgentStruct.xaml
    /// </summary>
    public partial class AgentStruct : UserControl
    {
        public AgentStruct(string inIdText, string inNameText)
        {
            InitializeComponent();
            IdText.Text = inIdText;
            NameText.Text = inNameText;
        }
    }
}

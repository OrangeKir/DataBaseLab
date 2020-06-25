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
    /// Interaction logic for AddStorageElement.xaml
    /// </summary>
    public partial class AddStorageElement : UserControl
    {
        Structure_Blank parent;
        public AddStorageElement(Structure_Blank inParent)
        {
            parent = inParent;
            InitializeComponent();

        }

        private void ProductNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AmountText != null)
            {
                if (AmountText.Text != "")
                    parent.addName = ProductNameText.Text;
            }
        }
        private void AmountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AmountText != null)
            {
                if (AmountText.Text == "")
                    AmountText.Text = "0";
                parent.addAmount = Convert.ToInt32(AmountText.Text);
            }
        }

        private void MinAmountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MinAmountText != null)
            {
                if (MinAmountText.Text == "")
                    MinAmountText.Text = "0";
                parent.addMinAmount = Convert.ToInt32(MinAmountText.Text);
            }
        }

        private void SectionIdText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SectionIdText != null)
            {
                if (SectionIdText.Text == "")
                    SectionIdText.Text = "0";
                parent.addSectionId = Convert.ToInt32(SectionIdText.Text);
            }
        }

        private void AgentIdText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AgentIdText != null)
            {
                if (AgentIdText.Text == "")
                    AgentIdText.Text = "0";
                parent.addAgentId = Convert.ToInt32(AgentIdText.Text);
            }
        }

        private void PriceText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PriceText != null)
            {
                if (PriceText.Text == "")
                    PriceText.Text = "0";
                parent.addPrice = Convert.ToDouble(PriceText.Text);
            }
        }



        private void AmountText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void MinAmountText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void SectionIdText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void AgentIdText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PriceText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

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
        public AddStorageElement()
        {
            InitializeComponent();

        }
        private void AmountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AmountText.Text == "")
                AmountText.Text = "0";
        }

        private void MinAmountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MinAmountText.Text == "")
                MinAmountText.Text = "0";
        }

        private void SectionIdText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SectionIdText.Text == "")
                SectionIdText.Text = "0";
        }

        private void AgentIdText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AgentIdText.Text == "")
                AgentIdText.Text = "0";
        }

        private void PriceText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PriceText.Text == "")
                PriceText.Text = "0";
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

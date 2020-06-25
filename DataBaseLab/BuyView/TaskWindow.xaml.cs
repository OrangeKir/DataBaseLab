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
using System.Text.RegularExpressions;
using OKDT;

namespace DataBaseLab.BuyView
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindowBuy : Window
    {
        private Buy_Blank Parent;
        private int locAmount;
        private int locId;
        private int locKey;
        public TaskWindowBuy(int inId, int inAmount, Buy_Blank inParent, int key)
        {
            locId = inId;
            locAmount = inAmount;
            locKey = key;
            Parent = inParent;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Accept_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (locKey == 0)
            {
                if (Parent.IsSale == 0)
                {
                    Buy_Blank BB = Parent;
                    BB.Buff = new ChangeInfo
                    {
                        Id = locId,
                        Amount = Convert.ToInt32(DataField.Text)
                    };
                    this.Close();
                }
                if (Convert.ToInt32(DataField.Text) > 0)
                {
                    Buy_Blank BB = Parent;
                    BB.Buff = new ChangeInfo
                    {
                        Id = locId,
                        Amount = Convert.ToInt32(DataField.Text)
                    };
                    this.Close();
                }
                return;
            }
            if (locKey == 1)
            {
                if (Convert.ToInt32(DataField.Text) > 0 && locAmount >= Convert.ToInt32(DataField.Text))
                {
                    Buy_Blank BB = Parent;
                    BB.ChangeCourtAmount = Convert.ToInt32(DataField.Text);
                    this.Close();
                }
                return;
            }
        }
    }
}

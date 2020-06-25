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

namespace DataBaseLab.SaleView
{
    /// <summary>
    /// Interaction logic for PositionBlank.xaml
    /// </summary>
    public partial class PositionBlank : UserControl
    {
        private int Id { get; set; }
        private string Names { get; set; }//имя продукта
        private int ProductId { get; set; }//id продукта
        private int Amount { get; set; }//
        private double Price { get; set; }
        private int IsLeft { get; set; }
        private Sale_Blank LocParent { get; set; }

        public PositionBlank(int inId, string inNames, int inAmount, int inProductId, double inPrice, Sale_Blank inParent, int inIsLeft)
        {
            Id = inId;
            Names = inNames;
            Amount = inAmount;
            ProductId = inProductId;
            LocParent = inParent;
            Price = inPrice;
            IsLeft = inIsLeft;

            InitializeComponent();
            AmountBlank.Text = Convert.ToString(Amount);
            NameBlank.Text = Names;
        }

        public string GetNames()
        {
            return Names;
        }
        public int GetAmount()
        {
            return Amount;
        }
        public int GetProductId()
        {
            return ProductId;
        }
        public int GetId()
        {
            return Id;
        }
        public double GetPrice()
        {
            return Price;
        }

        public void ChangeBackground(SolidColorBrush brush)
        {
            this.Background = brush;
        }

        public void ChangeAmount(int newAmount)
        {
            Amount = newAmount;
            AmountBlank.Text = Convert.ToString(Amount);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsLeft == 1)
            {
                TaskWindow task = new TaskWindow(Id, Amount, LocParent, 0);
                task.Show();
            }
            else
            {
                if(LocParent.SelectedBuff == this)
                {
                    LocParent.SelectedBuff.ChangeBackground(Brushes.LightGray);
                    LocParent.SelectedBuff = null;
                    return;
                }
                if (LocParent.SelectedBuff != null)
                    LocParent.SelectedBuff.ChangeBackground(Brushes.LightGray);
                //this.Background = Brushes.PaleVioletRed;
                LocParent.SelectedBuff = this;
            }
        }
    }
}

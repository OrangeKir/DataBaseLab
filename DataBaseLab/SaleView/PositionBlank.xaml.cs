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
        private SolidColorBrush[] brushes = { Brushes.Red, Brushes.LightGray };
        private int Id { get; set; }
        private string Names { get; set; }//имя продукта
        private int ProductId { get; set; }//id продукта
        private int Amount { get; set; }//
        private double Price { get; set; }
        private int Position { get; set; }

        public PositionBlank(int inId, string inNames, int inAmount, int inProductId, double inPrice, int inPosition)
        {
            Id = inId;
            Names = inNames;
            Amount = inAmount;
            ProductId = inProductId;
            Price = inPrice;
            Position = inPosition;

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
        public int GetPosition()
        {
            return Position;
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
            if (Background == brushes[0])
                Background = brushes[1];
            else
                Background = brushes[0];
        }
    }
}

using Blank;
using DataBaseLab.StructureView;
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

namespace DataBaseLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int managerId = -1;
        public int sectionId = -1;
        public MainWindow()
        {
            StartWindow SW = new StartWindow(this);
            InitializeComponent();
            SW.Show();
        }
        //События левой панели

        //Нажатия
            private void User_MouseDown(object sender, MouseButtonEventArgs e)
            {
                StartWindow SW = new StartWindow(this);
                SW.Show();
                ContentPart.Content = null;
            }

            private void Sell_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (managerId > -1 && sectionId > -1)
                    ContentPart.Content = new SaleView.Sale_Blank(sectionId, managerId);
            }

            private void Buy_MouseDown(object sender, MouseButtonEventArgs e)
            {
            if (managerId > -1 && sectionId > -1)
                ContentPart.Content = new Buy_Blank(sectionId, managerId);
            }

            private void Corect_MouseDown(object sender, MouseButtonEventArgs e)
            {
                ContentPart.Content = new Structure_Blank();
            }

            private void Result_MouseDown(object sender, MouseButtonEventArgs e)
            {

            }

            private void Expand_MouseDown(object sender, MouseButtonEventArgs e)    //дописана
            {
                if (LeftPanelGrid.Width.Value == 50)
                {
                    LeftPanelGrid.Width = new GridLength(250);
                    LeftClickPanel.Width = new GridLength(1, GridUnitType.Star);
                    return;
                }
                LeftPanelGrid.Width = new GridLength(50);
                LeftClickPanel.Width = new GridLength(0, GridUnitType.Star);
        }


            //Что-то там еще
    }
}

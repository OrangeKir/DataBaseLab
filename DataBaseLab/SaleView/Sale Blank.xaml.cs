using OKDT;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace DataBaseLab.SaleView
{
    //здесь положение элемента positionBlank распределяется следующим образом
    //0 - склад
    //1 - корзина


    public partial class Sale_Blank : Page
    {
        public PositionBlank SelectedBuff;
        PositionBlank[] Storage;
        PositionBlank[] Court;
        public readonly int IsSale = 1;
        private int SectionId;
        private int ManagerId;



        public List<Buy> CourtData;

        public Sale_Blank(int inSectionId, int inManagerId)
        {
            ManagerId = inManagerId;
            SectionId = inSectionId;
            Court = new PositionBlank[0];
            InitializeComponent();

            ReadStorage();
        }

        private async void ReadStorage()
        {
            int i;
            ProductInfo[] ReadInfo;
            ReadInfo = StructWorkClass.ReadBasicStorage(SectionId);
            Storage = new PositionBlank[ReadInfo.Length];
            for (i = 0; i < ReadInfo.Length; i++)
            {
                Storage[i] = new PositionBlank(i, ReadInfo[i].Name, ReadInfo[i].Amount, ReadInfo[i].Id, ReadInfo[i].Price, 0);
                Storage[i].MouseDown += PositionBlank_Click;
                StorageViewer.Children.Add(Storage[i]);
            }
        }

        private void PositionBlank_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is PositionBlank)
            {
                if (SelectedBuff != null)
                    SelectedBuff.ChangeBackground(Brushes.LightGray);
                
                if (SelectedBuff == sender as PositionBlank)
                {
                    ChangeRow.Height = new GridLength(0);
                    SelectedBuff = null;
                }
                else
                {

                    
                    //SelectedBuff.ChangeBackground(Brushes.LightGray);
                    SelectedBuff = sender as PositionBlank;
                    ProdName.Text = SelectedBuff.GetNames();
                    if (SelectedBuff.GetPosition() == 1)
                    {
                        InsertAmount.Text = Convert.ToString(SelectedBuff.GetAmount());
                        ChangeRow.Height = new GridLength(0);
                    }
                    else
                    {
                        InsertAmount.Text = "0";
                        ChangeRow.Height = new GridLength(50);
                    }
                }

            }
        }

        private void AddCart()
        {
            int CurId = SelectedBuff.GetId();
            int CurAmount = Convert.ToInt32(InsertAmount.Text);
            int a = Court.Length;
            Array.Resize(ref Court, Court.Length + 1);

            Court[a] = new PositionBlank(CurId, Storage[CurId].GetNames(), CurAmount, Storage[CurId].GetProductId(), Storage[CurId].GetAmount(), 1);
            Court[a].MouseDown += PositionBlank_Click;

            Storage[CurId].ChangeAmount(Storage[CurId].GetAmount() - CurAmount);

            CourtViewer.Children.Add(Court[a]);
        }

        private void BuyBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i;
            ChangeInfo[] changeInfo = new ChangeInfo[Court.Length];
            for (i = 0; i < Court.Length; i++)
            {
                changeInfo[i] = new ChangeInfo();
                changeInfo[i].Id = Court[i].GetProductId();
                changeInfo[i].Amount = Court[i].GetAmount();
            }
            SaleClass.SaleWork(changeInfo, ManagerId, SectionId);

            CourtViewer.Children.Clear();
            Court = new PositionBlank[0];

            StorageViewer.Children.Clear();
            Storage = new PositionBlank[0];

            ReadStorage();
        }


        private void ClearBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int buff;
            int i;
            int id;
            SelectedBuff = null;
            for (i = 0; i < Court.Length; i++)
            {
                id = Court[i].GetId();
                buff = Storage[id].GetAmount() + Court[i].GetAmount();
                Storage[id].ChangeAmount(buff);

            }
            CourtViewer.Children.Clear();
            Court = new PositionBlank[0];
        }

        private void DeleteCourtElement()
        {
            if (SelectedBuff != null)
            {
                int id;
                int buff;
                int i;
                int j = 0;
                PositionBlank[] NewCourt = new PositionBlank[Court.Length - 1];
                id = SelectedBuff.GetId();
                buff = Storage[id].GetAmount() + SelectedBuff.GetAmount();
                Storage[id].ChangeAmount(buff);
                CourtViewer.Children.Remove(SelectedBuff);
                for (i = 0; i < Court.Length; i++)
                {
                    if (Court[i] != SelectedBuff)
                    {
                        NewCourt[j] = Court[i];
                        j++;
                    }
                }
                Court = NewCourt;
            }
        }

        private void DeleteBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteCourtElement();
        }

        private void ChangeBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedBuff != null && SelectedBuff.GetPosition() == 1)
            {
                InsertAmount.Text = Convert.ToString(SelectedBuff.GetAmount());
                ChangeRow.Height = new GridLength(50);
            }
        }

        private void AbortBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedBuff.GetPosition() == 0)
                InsertAmount.Text = "0";
            else
                InsertAmount.Text = Convert.ToString(SelectedBuff.GetAmount());
        }

        private void AcceptBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedBuff.GetPosition() == 0)
            {
                if (Convert.ToInt32(InsertAmount.Text) <= SelectedBuff.GetAmount())
                    AddCart();
                return;
            }
            if (Storage[SelectedBuff.GetId()].GetAmount() + SelectedBuff.GetAmount() >= Convert.ToInt32(InsertAmount.Text))
            {
                Storage[SelectedBuff.GetId()].ChangeAmount(Storage[SelectedBuff.GetId()].GetAmount() + SelectedBuff.GetAmount() - Convert.ToInt32(InsertAmount.Text));
                SelectedBuff.ChangeAmount(Convert.ToInt32(InsertAmount.Text));
            }
            if (SelectedBuff.GetAmount() < 1)
            {
                DeleteCourtElement();
                ChangeRow.Height = new GridLength(0);
            }
        }

        private void InsertAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

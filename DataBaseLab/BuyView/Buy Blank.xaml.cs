using DataBaseLab.BuyView;
using OKDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Buy.xaml
    /// </summary>
    /// 


    public partial class Buy_Blank : Page
    {
        public PositionBlankBuy SelectedBuff;
        PositionBlankBuy[] Storage;
        PositionBlankBuy[] Court;
        public readonly int IsSale = 1;
        private int SectionId;
        private int ManagerId;

        //Начало колхоза им. Ленина
        private void AddCart(int CurId, int CurAmount)
        {
            int a = Court.Length;
            Array.Resize(ref Court, Court.Length + 1);
            Court[a] = new PositionBlankBuy(CurId, Storage[CurId].GetNames(), CurAmount, Storage[CurId].GetProductId(), Storage[CurId].GetAmount(), this, 0);

            CourtViewer.Children.Add(Court[a]);

            Storage[CurId].ChangeAmount(Storage[CurId].GetAmount() + CurAmount);
        }

        private static ChangeInfo buff;

        public ChangeInfo Buff
        {
            set
            {
                buff = value;
                AddCart(buff.Id, buff.Amount);
            }
            get { return buff; }
        }

        private PositionBlankBuy ChangingCourt;
        public int ChangeCourtAmount
        {
            set
            {
                int buff = value;
                int res;
                int id = ChangingCourt.GetId();
                if(buff >= ChangingCourt.GetAmount())
                {
                    DeleteCourtElement();
                    return;
                }
                res = Storage[id].GetAmount();
                ChangingCourt.ChangeAmount(ChangingCourt.GetAmount() - buff);
                res -= buff;
                Storage[id].ChangeAmount(res);
            }
        }
        //Конец колхоза им. Ленина

        public List<Buy> CourtData;

        public Buy_Blank(int inSectionId, int inManagerId)
        {
            ManagerId = inManagerId;
            SectionId = inSectionId;
            Court = new PositionBlankBuy[0];
            InitializeComponent();

            ReadStorage();
        }

        private void ReadStorage()
        {
            int i;
            StructWorkClass SWC = new StructWorkClass();
            ProductInfo[] ReadInfo;
            ReadInfo = SWC.ReadBasicStorage(SectionId);
            StorageViewer.Children.Clear();
            Storage = new PositionBlankBuy[ReadInfo.Length];
            for (i = 0; i < ReadInfo.Length; i++)
            {
                Storage[i] = new PositionBlankBuy(i, ReadInfo[i].Name, ReadInfo[i].Amount, ReadInfo[i].Id, ReadInfo[i].Price, this, 1);
                StorageViewer.Children.Add(Storage[i]);
            }
        }
        private void ReadFireStorage()
        {
            int i, j;
            StructWorkClass SWC = new StructWorkClass();
            ProductInfo[] ReadInfo;
            ReadInfo = SWC.ReadBasicStorage(SectionId);
            StorageViewer.Children.Clear();
            Storage = new PositionBlankBuy[0];
            j = 0;
            for (i = 0; i < ReadInfo.Length; i++)
            {
                if (ReadInfo[i].Amount < ReadInfo[i].MinAmount)
                {
                    Array.Resize(ref Storage, Storage.Length + 1);
                    Storage[j] = new PositionBlankBuy(j, ReadInfo[i].Name, ReadInfo[i].Amount, ReadInfo[i].Id, ReadInfo[i].Price, this, 1);
                    StorageViewer.Children.Add(Storage[j]);
                    j++;
                }
            }
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
            BuyClass BC = new BuyClass();
            BC.BuyWork(changeInfo);

            CourtViewer.Children.Clear();
            Court = new PositionBlankBuy[0];

            StorageViewer.Children.Clear();
            Storage = new PositionBlankBuy[0];

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
            Court = new PositionBlankBuy[0];
        }

        private void DeleteCourtElement()
        {
            if(SelectedBuff!=null)
            {
                int id;
                int buff;
                int i;
                int j = 0;
                PositionBlankBuy[] NewCourt = new PositionBlankBuy[Court.Length - 1];
                id = SelectedBuff.GetId();
                buff = Storage[id].GetAmount() - SelectedBuff.GetAmount();
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
            if(SelectedBuff!=null)
            {
                ChangingCourt = SelectedBuff;
                TaskWindowBuy TW = new TaskWindowBuy(0, ChangingCourt.GetAmount(), this, 1);
                TW.Show();
            }
        }

        private void All_Btm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReadStorage();
        }

        private void Need_btm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReadFireStorage();
        }
    }
}

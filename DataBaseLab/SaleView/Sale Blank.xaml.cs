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

        private void ReadStorage()
        {
            int i;
            StructWorkClass SWC = new StructWorkClass();
            ProductInfo[] ReadInfo;
            ReadInfo = SWC.ReadBasicStorage(SectionId);
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
            if(sender is PositionBlank)
            {
                SelectedBuff = sender as PositionBlank;


            }
        }

        private void AddCart()
        {
            int CurId = SelectedBuff.GetId();
            int CurAmount = 0;
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
            SaleClass SC = new SaleClass();
            SC.SaleWork(changeInfo, ManagerId, SectionId);

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
            if (SelectedBuff != null)
            {

            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is PositionBlank pb)
            {
                pb.Background = Brushes.PaleVioletRed;
            }
            
        }




        //Начало колхоза им. Ленина


        private static ChangeInfo buff;

        private PositionBlank ChangingCourt;
        public int ChangeCourtAmount
        {
            set
            {
                int buff = value;
                int id = ChangingCourt.GetId();
                if (buff >= ChangingCourt.GetAmount())
                {
                    DeleteCourtElement();
                    return;
                }
                ChangingCourt.ChangeAmount(ChangingCourt.GetAmount() - buff);
                buff += Storage[id].GetAmount();
                Storage[id].ChangeAmount(buff);
            }
        }
        //Конец колхоза им. Ленина
    }
}

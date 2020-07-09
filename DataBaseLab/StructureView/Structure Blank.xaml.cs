using Blank;
using DataBaseLab.StructureView.Add_Elements;
using DataBaseLab.StructureView.View_Elements;
using OKDT;
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

namespace DataBaseLab.StructureView
{
    /// <summary>
    /// Interaction logic for Structure_Blank.xaml
    /// </summary>
    public partial class Structure_Blank : Page
    {
        private AddStorageElement writeStorage;
        private AddAgentElement writeConterAgent;
        private AddManagerElement writeManager;

        private int addStatus;
        //Переменные для записи в базу данных
        public string addName = "";
        public int addAmount;
        public int addMinAmount;
        public int addSectionId;
        public int addAgentId;
        public double addPrice;

        //Блок программ
        public Structure_Blank()
        {
            addStatus = 0;
            InitializeComponent();
        }


        private void SalesBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i;
            string id, amount, managerid, sectionid, price, date;
            OKDT.Sale[] readSales;
            SaleStruct[] saleChilds;
            OKDT.Storage[] readStorage;
            OKDT.Storage StorageBuff;

            SaleStruct Header = new SaleStruct("Id", "Имя продукта", "Объем", "Id сотрудника", "Id отдела", "Сумма", "Дата");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();
            OKDT.SaleClass SC = new OKDT.SaleClass();

            AddRow.Height = new GridLength(0);
            StructureViewStack.Children.Clear();
            readSales = SC.ReadSales();
            readStorage = SWC.ReadFullStorage();
            saleChilds = new SaleStruct[readSales.Length];
            HeaderFrame.Content = Header;
            for (i = 0; i < saleChilds.Length; i++)
            {
                StorageBuff = readStorage.Where(a => a.Id == readSales[i].ProductId).FirstOrDefault();

                id = Convert.ToString(readSales[i].Id);
                amount = Convert.ToString(readSales[i].Amount);
                managerid = Convert.ToString(readSales[i].ManagerId);
                sectionid = Convert.ToString(readSales[i].SectionId);
                price = Convert.ToString(readSales[i].Profit) + " руб.";
                date = readSales[i].Date.ToString("dd/MM/yyyy HH:mm:ss");

                saleChilds[i] = new SaleStruct(id, StorageBuff.Name, amount, managerid, sectionid, price, date);
                StructureViewStack.Children.Add(saleChilds[i]);
            }
        }

        private void BuysBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i;
            string id, amount, agentid, date;
            OKDT.Buy[] readBuys;
            OKDT.Storage[] readStorage;
            OKDT.Storage StorageBuff;
            BuyStruct[] buyChilds;

            BuyStruct Header = new BuyStruct("Id", "Имя продукта", "Объем", "Id поставщика", "Дата");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();
            OKDT.BuyClass BC = new OKDT.BuyClass();

            AddRow.Height = new GridLength(0);
            StructureViewStack.Children.Clear();
            readBuys = BC.ReadBuys();
            readStorage = SWC.ReadFullStorage();
            buyChilds = new BuyStruct[readBuys.Length];
            HeaderFrame.Content = Header;

            for (i = 0; i < buyChilds.Length; i++)
            {
                StorageBuff = readStorage.Where(a => a.Id == readBuys[i].ProductId).FirstOrDefault();

                id = Convert.ToString(readBuys[i].Id);
                amount = Convert.ToString(readBuys[i].Amount);
                agentid = Convert.ToString(readBuys[i].ConterAgentId);

                buyChilds[i] = new BuyStruct(id, StorageBuff.Name, amount, agentid, "Заглушка даты");
                StructureViewStack.Children.Add(buyChilds[i]);
            }
        }

        private void StoragesBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RefreshStorage();
        }
        private void RefreshStorage()
        {
            int i;
            string id, name, amount, minAmount, sectionId, agentId, price;
            OKDT.Storage[] readStorages;
            StorageStruct[] storageChilds;

            StorageStruct Header = new StorageStruct("Id", "Имя продукта", "Объем", "Минимальный объем", "Id отдела", "Id поставщика", "Стоимость");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();

            StructureViewStack.Children.Clear();
            readStorages = SWC.ReadFullStorage();
            storageChilds = new StorageStruct[readStorages.Length];
            AddRow.Height = new GridLength(50);
            addStatus = 1;
            HeaderFrame.Content = Header;

            addName = "Product name";
            addAmount = 0;
            addMinAmount = 0;
            addSectionId = 0;
            addAgentId = 0;
            addPrice = 0;

            writeStorage = new AddStorageElement();
            AddFrame.Content = writeStorage;

            for (i = 0; i < readStorages.Length; i++)
            {
                id = Convert.ToString(readStorages[i].Id);
                name = readStorages[i].Name;
                amount = Convert.ToString(readStorages[i].Amount);
                minAmount = Convert.ToString(readStorages[i].MinAmount);
                sectionId = Convert.ToString(readStorages[i].SectionId);
                agentId = Convert.ToString(readStorages[i].AgentId);
                price = Convert.ToString(readStorages[i].Price);

                storageChilds[i] = new StorageStruct(id, name, amount, minAmount, sectionId, agentId, price);
                StructureViewStack.Children.Add(storageChilds[i]);
            }
        }

        private void AgentBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RefreshAgent();
        }

        private void RefreshAgent()
        {
            int i;
            string id, name;
            OKDT.ConterAgent[] readAgent;
            AgentStruct[] agentChilds;

            AgentStruct Header = new AgentStruct("Id", "Имя поставщика");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();

            readAgent = SWC.ReadAgent();
            StructureViewStack.Children.Clear();
            agentChilds = new AgentStruct[readAgent.Length];
            AddRow.Height = new GridLength(50);
            addStatus = 3;
            HeaderFrame.Content = Header;

            addName = "Agent Name";
            writeConterAgent = new AddAgentElement();
            AddFrame.Content = writeConterAgent;

            for (i = 0; i < readAgent.Length; i++)
            {
                id = Convert.ToString(readAgent[i].Id);
                name = readAgent[i].Name;
                agentChilds[i] = new AgentStruct(id, name);
                StructureViewStack.Children.Add(agentChilds[i]);
            }
        }

        private void ManagerBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RefreshManager();
        }

        private void RefreshManager()
        {
            int i;
            string id, name, profit;
            OKDT.Manager[] readManager;
            ManagerStruct[] managerChilds;

            ManagerStruct Header = new ManagerStruct("Id", "ФИО", "Текущая прибыль");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();
            
            readManager = SWC.ReadManager();
            StructureViewStack.Children.Clear();
            managerChilds = new ManagerStruct[readManager.Length];
            AddRow.Height = new GridLength(50);
            addStatus = 2;
            HeaderFrame.Content = Header;

            addName = "Manager Name";
            writeManager = new AddManagerElement();
            AddFrame.Content = writeManager;

            for (i = 0; i < readManager.Length; i++)
            {
                id = Convert.ToString(readManager[i].Id);
                name = readManager[i].Name;
                profit = Convert.ToString(readManager[i].CurentProfit);
                managerChilds[i] = new ManagerStruct(id, name, profit);
                StructureViewStack.Children.Add(managerChilds[i]);
            }
        }

        private void ProfitArchivesBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i;
            string id, date, name, profit;
            OKDT.ProfitArchive[] readProfitArchive;
            ProfitArchiveStruct[] profitArchiveChilds;
            OKDT.Manager[] readManager;

            ProfitArchiveStruct Header = new ProfitArchiveStruct("Id", "Дата", "Имя сотрудника", "Прибыль");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();

            readProfitArchive = SWC.ReadProfitArchive();
            readManager = SWC.ReadManager();
            StructureViewStack.Children.Clear();
            profitArchiveChilds = new ProfitArchiveStruct[readProfitArchive.Length];
            AddRow.Height = new GridLength(0);
            addStatus = 0;
            HeaderFrame.Content = Header;

            for (i = 0; i < readProfitArchive.Length; i++)
            {
                id = Convert.ToString(readProfitArchive[i].Id);
                date = readProfitArchive[i].Mounth.ToString("dd/MM/yyyy");
                name = readManager.Where(a => a.Id == readProfitArchive[i].ManagerId).FirstOrDefault().Name;
                profit = Convert.ToString(readProfitArchive[i].Profit);

                profitArchiveChilds[i] = new ProfitArchiveStruct(id, date, name, profit);
                StructureViewStack.Children.Add(profitArchiveChilds[i]);
            }
        }

        private void DailyProfitsBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i;
            string date, profit, dayOfWeek;
            OKDT.DailyProfit[] readDailyProfit;
            DailyProfitStruct[] dailyProfitChilds;

            DailyProfitStruct Header = new DailyProfitStruct("Дата", "Прибыль", "День недели");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();

            readDailyProfit = SWC.ReadDailyProfit();
            StructureViewStack.Children.Clear();
            dailyProfitChilds = new DailyProfitStruct[readDailyProfit.Length];
            AddRow.Height = new GridLength(0);
            addStatus = 0;
            HeaderFrame.Content = Header;

            for (i = 0; i < readDailyProfit.Length; i++)
            {
                date = readDailyProfit[i].Date.ToString("dd/MM/yyyy");
                profit = Convert.ToString(readDailyProfit[i].Profit) + "руб.";
                dayOfWeek = GetDayOfWeek(readDailyProfit[i].DayOfWeek);

                dailyProfitChilds[i] = new DailyProfitStruct(date, profit, dayOfWeek);
                StructureViewStack.Children.Add(dailyProfitChilds[i]);
            }
        }
        
        private string GetDayOfWeek(int a)
        {
            switch(a)
            {
                case (1):
                    return "Понедельник";
                case (2):
                    return "Вторник";
                case (3):
                    return "Среда";
                case (4):
                    return "Четверг";
                case (5):
                    return "Пятница";
                case (6):
                    return "Суббота";
                default:
                    return "Воскресенье";

            }
        }

        private void DailySectionsProfitsBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i;
            string date, sectionId, profit;
            OKDT.DailySectionProfit[] readDailySectionProfit;
            DailySectionProfitStruct[] dailySectionProfitChilds;

            DailySectionProfitStruct Header = new DailySectionProfitStruct("Дата", "Id отдела", "Прибыль");
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();

            readDailySectionProfit = SWC.ReadDailySectionProfit();
            StructureViewStack.Children.Clear();
            dailySectionProfitChilds = new DailySectionProfitStruct[readDailySectionProfit.Length];
            AddRow.Height = new GridLength(0);
            addStatus = 0;
            HeaderFrame.Content = Header;

            for (i = 0; i < readDailySectionProfit.Length; i++)
            {
                date = readDailySectionProfit[i].Date.ToString("dd/MM/yyyy");
                sectionId = Convert.ToString(readDailySectionProfit[i].SectionId);
                profit = Convert.ToString(readDailySectionProfit[i].Profit);

                dailySectionProfitChilds[i] = new DailySectionProfitStruct(date, sectionId, profit);
                StructureViewStack.Children.Add(dailySectionProfitChilds[i]);
            }
        }

        private void MountlyProfitsBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i, j;
            string date, mountlyProfit;
            string[] midWeekProfit = { "Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб" };
            MountlyProfitStruct[] mountlyProfitChild;
            MountlyProfitStruct Header = new MountlyProfitStruct("Дата", midWeekProfit, "Сумма");
            List<MountlyStruct> readMountlyProfit;

            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();
            StructureViewStack.Children.Clear();
            readMountlyProfit = SWC.CompositeMountlyProfit();
            mountlyProfitChild = new MountlyProfitStruct[readMountlyProfit.Count];
            HeaderFrame.Content = Header;

            for (i = 0; i < mountlyProfitChild.Length; i++)
            {
                for (j = 0; j < 7; j++)
                    midWeekProfit[j] = Convert.ToString(readMountlyProfit[i].MidWeekProfit[j]);
                date = readMountlyProfit[i].date;
                mountlyProfit = Convert.ToString(readMountlyProfit[i].MounthProfit);

                mountlyProfitChild[i] = new MountlyProfitStruct(date, midWeekProfit, mountlyProfit);
                StructureViewStack.Children.Add(mountlyProfitChild[i]);
            }
        }

        private void AcceptBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OKDT.StructWorkClass SWC = new OKDT.StructWorkClass();
            OKDT.Storage dataStorage;
            OKDT.Manager dataManager;
            OKDT.ConterAgent dataAgent;
            if (addStatus == 1)
            {
                dataStorage = new OKDT.Storage()
                {
                    Name = writeStorage.ProductNameText.Text,
                    Amount = Convert.ToInt32(writeStorage.AmountText.Text),
                    MinAmount = Convert.ToInt32(writeStorage.MinAmountText.Text),
                    SectionId = Convert.ToInt32(writeStorage.SectionIdText.Text),
                    AgentId = Convert.ToInt32(writeStorage.AgentIdText.Text),
                    Price = Convert.ToDouble(writeStorage.PriceText.Text)
                };
                if (writeStorage.ProductNameText.Text != "")
                {
                    SWC.WriteStorage(dataStorage);
                    RefreshStorage();
                }
            }
            else if (addStatus == 2)
            {
                dataManager = new OKDT.Manager()
                {
                    Name = writeManager.ManagerNameText.Text,
                    CurentProfit = 0
                };

                if (writeManager.ManagerNameText.Text != "")
                {
                    SWC.WriteManager(dataManager);
                    RefreshManager();
                }
            }
            else if (addStatus == 3)
            {
                dataAgent = new OKDT.ConterAgent()
                {
                    Name = writeConterAgent.NameText.Text
                };
                if (writeConterAgent.NameText.Text != "")
                {
                    SWC.WriteAgent(dataAgent);
                    RefreshAgent();
                }
            }
        }

        private void CancelBtm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (addStatus == 1)
            {
                writeStorage = new AddStorageElement();
                AddFrame.Content = writeStorage;
            }
            else if (addStatus == 2)
            {
                writeManager = new AddManagerElement();
                AddFrame.Content = writeManager;
            }
            else if (addStatus == 3)
            {
                writeConterAgent = new AddAgentElement();
                AddFrame.Content = writeConterAgent;
            }
        }
    }
}

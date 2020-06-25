using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace OKDT
{
    class SaleClass
    {
public Sale[] ReadSales()
{
    Sale[] ReadProfit = new Sale[0];
    using (var dbContext = new MyDbContext())
    {
        try
        {
            ReadProfit = dbContext.Sales.ToArray();
        }
        catch
        {
            return ReadProfit;
        }
    }
    return ReadProfit;
}

        public int SaleWork(ChangeInfo[] SaleArray, int CurManagerId, int CurSectionId)
        {
            List<Storage> StorageInfo;
            List<Manager> managers;
            List<DailySectionProfit> DSP;
            Manager managerBuff = new Manager();
            Sale WriteInfo = new Sale();
            Storage storageBuff = new Storage();
            MountlyProfit mountlyProfitBuff;
            DailyProfit dailyProfitBuff;
            DailySectionProfit dailySectionBuff;
            DateTime date = new DateTime();
            int i;
            int pos;
            int trigger;
            int lenght;
            int SectionNums;
            int buff;
            int cnt;

            DailySectionProfit dailySectionProfit = new DailySectionProfit();
            DailyProfit dailyProfit = new DailyProfit();
            MountlyProfit mountlyProfit = new MountlyProfit();

            double TotalPrice = 0;
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    lenght = dbContext.Storages.Count();
                }
                catch
                {
                    return 1;
                }


                StorageInfo = dbContext.Storages.ToList();

                for (i = 0; i < SaleArray.Length; i++)
                {
                    trigger = 0;
                    for (pos = 0; pos < lenght; pos++)
                        if (SaleArray[i].Id == StorageInfo[pos].Id)
                        {
                            trigger = 1;
                            break;
                        }
                    if (trigger == 1)
                    {
                        WriteInfo.ManagerId = CurManagerId;
                        WriteInfo.SectionId = CurSectionId;
                        WriteInfo.Amount = SaleArray[i].Amount;
                        WriteInfo.ProductId = SaleArray[i].Id;
                        WriteInfo.Date = DateTime.Now;
                        buff = SaleArray[i].Id;

                        storageBuff = dbContext.Storages
                            .Where(c => c.Id == buff)
                            .FirstOrDefault();
                        storageBuff.Amount -= SaleArray[i].Amount;

                        WriteInfo.Profit = StorageInfo[pos].Price * WriteInfo.Amount;

                        TotalPrice += StorageInfo[pos].Price * WriteInfo.Amount;

                        dbContext.Sales.Add(WriteInfo);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        //место для обработки ошибки
                    }
                }

                //обработка ежемесячной прибыли сотрудника
                managers = dbContext.Managers.ToList();
                managerBuff = dbContext.Managers
                            .Where(c => c.Id == CurManagerId)
                            .FirstOrDefault();
                managerBuff.CurentProfit += TotalPrice;

                //обработка ежедневной прибыли магазина
                try
                {
                    lenght = dbContext.DailyProfits.Count();
                }
                catch
                {
                    lenght = 0;
                }
                if (lenght > 0)
                {
                    buff = dbContext.DailyProfits.Max(p => p.Id);
                    dailyProfitBuff = dbContext.DailyProfits.Where(a => a.Id == buff).FirstOrDefault();
                    date = dailyProfitBuff.Date;
                    if (dailyProfitBuff.Date == DateTime.Today)
                        dailyProfitBuff.Profit += TotalPrice;
                    else
                    {
                        dailyProfit.Date = DateTime.Today;
                        dailyProfit.DayOfWeek = (int)DateTime.Today.DayOfWeek;
                        dailyProfit.Profit = TotalPrice;
                        dbContext.DailyProfits.Add(dailyProfit);
                    }
                }
                else
                {
                    dailyProfit.Date = DateTime.Today;
                    dailyProfit.DayOfWeek = (int)System.DateTime.Today.DayOfWeek;
                    dailyProfit.Profit = TotalPrice;
                    dbContext.DailyProfits.Add(dailyProfit);
                }

                //обработка ежемесячной прибыли магазина
                try
                {
                    lenght = dbContext.MountlyProfits.Count();
                }
                catch
                {

                    lenght = 0;
                }
                if (lenght > 0)
                {
                    buff = dbContext.MountlyProfits.Max(p => p.Id);
                    mountlyProfitBuff = dbContext.MountlyProfits.Where(a => a.Id == buff).FirstOrDefault();
                    date = mountlyProfitBuff.Date;
                    if (date.Year == DateTime.Today.Year && date.Month == DateTime.Today.Month)
                        mountlyProfitBuff.Profit += TotalPrice;
                    else
                    {
                        mountlyProfit.Date = DateTime.Today;
                        mountlyProfit.Date.AddDays(1 - mountlyProfit.Date.Day);
                        mountlyProfit.Profit = TotalPrice;
                        dbContext.MountlyProfits.Add(mountlyProfit);
                    }
                }
                else
                {
                    mountlyProfit.Date = DateTime.Today;
                    mountlyProfit.Date.AddDays(1 - dailyProfit.Date.Day);
                    mountlyProfit.Profit = TotalPrice;
                    dbContext.MountlyProfits.Add(mountlyProfit);
                }

                //обработка ежедневной прибыли отдела
                try
                {
                    lenght = dbContext.DailySectionProfits.Count();
                }
                catch
                {
                    lenght = 0;
                }
                if (lenght > 0)
                {
                    try
                    {
                        SectionNums = dbContext.Sections.Count();
                    }
                    catch
                    {
                        return 2;
                    }
                    cnt = Math.Min(SectionNums, lenght);
                    DSP = dbContext.DailySectionProfits.ToList();
                    for (i = 0; i < cnt; i++)
                    {

                        if (DSP[lenght - 1 - i].SectionId == CurSectionId)
                        {
                            if (DSP[lenght - 1 - i].Date != DateTime.Today)
                            {
                                i = cnt;
                                break;
                            }
                            buff = DSP[lenght - 1 - i].Id;
                            dailySectionBuff = dbContext.DailySectionProfits.Where(a => a.Id == buff).FirstOrDefault();
                            dailySectionBuff.Profit += TotalPrice;
                            break;
                        }
                    }
                    if (i == cnt)
                    {
                        dailySectionProfit.Date = DateTime.Today;
                        dailySectionProfit.SectionId = CurSectionId;
                        dailySectionProfit.Profit = TotalPrice;
                        dbContext.DailySectionProfits.Add(dailySectionProfit);
                    }
                }
                else
                {
                    dailySectionProfit.Date = DateTime.Today;
                    dailySectionProfit.SectionId = CurSectionId;
                    dailySectionProfit.Profit = TotalPrice;
                    dbContext.DailySectionProfits.Add(dailySectionProfit);
                }
                dbContext.SaveChanges();
            }
            return 0;
        }
    }
}
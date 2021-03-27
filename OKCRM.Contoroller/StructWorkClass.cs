namespace OKCRM.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    class MountlyStruct
    {
        public string date;
        public double[] MidWeekProfit = new double[7];
        public double MounthProfit;
    }

    static class StructWorkClass
    {
        //работа со складом

        public static ProductInfo[] ReadBasicStorage(int SectionId)
        {
            ProductInfo[] StorageArray = new ProductInfo[0];
            List<Storage> Buff;
            int i;
            int j;
            int lenght;
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    lenght = dbContext.Storages.Count();
                }
                catch
                {
                    return StorageArray;
                }
                Buff = dbContext.Storages.ToList();
            }
            j = 0;
            for (i = 0; i < lenght; i++)
            {
                if(SectionId==Buff[i].SectionId)
                {
                    Array.Resize(ref StorageArray, StorageArray.Length + 1);
                    StorageArray[j] = new ProductInfo();
                    StorageArray[j].Id = Buff[i].Id;
                    StorageArray[j].Amount = Buff[i].Amount;
                    StorageArray[j].MinAmount = Buff[i].MinAmount;
                    StorageArray[j].Name = Buff[i].Name;
                    StorageArray[j].Price = Buff[i].Price;
                    j++;
                }
            }
            return StorageArray;
        }

        public static Storage[] ReadFullStorage()
        {
            Storage[] StorageArray = new Storage[0];
            int lenght;
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    lenght = dbContext.Storages.Count();
                }
                catch
                {
                    return StorageArray;
                }

                StorageArray = new Storage[lenght];
                StorageArray = dbContext.Storages.ToArray();
            }
            return StorageArray;
        }

        public static int WriteStorage(Storage WriteStorage)
        {
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    dbContext.Storages.Add(WriteStorage);
                    dbContext.SaveChanges();
                }
                catch
                {
                    return 1;
                }
            }
            return 0;
        }

        //работа с сотрудниками
        public static Manager[] ReadManager()
        {
            Manager[] StorageArray = new Manager[0];
            int lenght;
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    lenght = dbContext.Managers.Count();
                }
                catch
                {
                    return StorageArray;
                }

                StorageArray = new Manager[lenght];
                StorageArray = dbContext.Managers.ToArray();
            }
            return StorageArray;
        }

        public static int WriteManager(Manager WriteManager)
        {
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    WriteManager.Status = DateTime.Today;
                    dbContext.Managers.Add(WriteManager);
                    dbContext.SaveChanges();
                }
                catch
                {
                    return 1;
                }
            }
            return 0;
        }

        public static int ManagersProfitCheck()
        {
            List<Manager> managers;
            ProfitArchive managersProfit;
            int lenght;
            int buff;
            int ArchiveSize;
            DateTime date = new DateTime();
            int i;
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    managers = dbContext.Managers.ToList();
                }
                catch
                {
                    return -1;
                }
                try
                {
                    ArchiveSize = dbContext.ProfitArchives.Count();
                }
                catch
                {
                    ArchiveSize = 0;
                }
                if (ArchiveSize > 0)
                {
                    buff = dbContext.ProfitArchives.Max(p => p.Id);
                    date = dbContext.ProfitArchives.Where(a => a.Id == buff).FirstOrDefault().Mounth;
                }
                if (date.Month == DateTime.Today.Month && date.Year == DateTime.Today.Month)
                    return -2;

                lenght = managers.Count();
                for (i = 0; i < lenght; i++)
                {
                    try
                    {
                        if (managers[i].CurentProfit != 0)
                        {
                            managersProfit = new ProfitArchive()
                            {
                                ManagerId = managers[i].Id,
                                Mounth = DateTime.Today,
                                Profit = managers[i].CurentProfit,
                            };
                            dbContext.Managers.Where(a => a.Id == managers[i].Id).FirstOrDefault().CurentProfit = 0;
                            dbContext.SaveChanges();
                        }
                    }
                    catch
                    {
                        return (i + 1);
                    }
                }
            }
            return 0;
        }

        //работа с агентами
        public static ConterAgent[] ReadAgent()
        {
            ConterAgent[] ReadArray = new ConterAgent[0];
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    ReadArray = dbContext.CounterAgents.ToArray();
                }
                catch
                {
                    return ReadArray;
                }
            }
            return ReadArray;
        }
        public static int WriteAgent(ConterAgent WriteAgents)
        {
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    dbContext.CounterAgents.Add(WriteAgents);
                    dbContext.SaveChanges();
                }
                catch
                {
                    return 1;
                }
            }
            return 0;
        }

        //работа с прибылью
        public static ProfitArchive[] ReadProfitArchive()
        {
            ProfitArchive[] ReadProfit = new ProfitArchive[0];
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    ReadProfit = dbContext.ProfitArchives.ToArray();
                }
                catch
                {
                    return ReadProfit;
                }
            }
            return ReadProfit;
        }

        public static DailyProfit[] ReadDailyProfit()
        {
            DailyProfit[] ReadProfit = new DailyProfit[0];
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    ReadProfit = dbContext.DailyProfits.ToArray();
                }
                catch
                {
                    return ReadProfit;
                }
            }
            return ReadProfit;
        }

        public static DailySectionProfit[] ReadDailySectionProfit()
        {
            DailySectionProfit[] ReadProfit = new DailySectionProfit[0];
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    ReadProfit = dbContext.DailySectionProfits.ToArray();
                }
                catch
                {
                    return ReadProfit;
                }
            }
            return ReadProfit;
        }

        public static MountlyProfit[] ReadMountlyProfit()
        {
            MountlyProfit[] ReadProfit = new MountlyProfit[0];
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    ReadProfit = dbContext.MountlyProfits.ToArray();
                }
                catch
                {
                    return ReadProfit;
                }
            }
            return ReadProfit;
        }

        public static List<MountlyStruct> CompositeMountlyProfit()
        {
            int i, j, n, lenght;
            double midProfit;
            DailyProfit[] profitByDayOfWeek;
            List<MountlyStruct> returnList = new List<MountlyStruct>();
            MountlyStruct buff;
            MountlyProfit[] readMountlyProfits = ReadMountlyProfit();
            DailyProfit[] readDailyProfit = ReadDailyProfit();

            lenght = readMountlyProfits.Length;

            for (i = 0; i < lenght; i++)
            {
                buff = new MountlyStruct()
                {
                    date = readMountlyProfits[i].Date.ToString("MM/yyyy"),
                    MounthProfit = readMountlyProfits[i].Profit
                };
                for (j = 0; j < 7; j++)
                {
                    midProfit = 0;
                    profitByDayOfWeek = readDailyProfit
                                        .Where(a => a.Date.Month == readMountlyProfits[i].Date.Month && a.Date.Year == readMountlyProfits[i].Date.Year && a.DayOfWeek == j)
                                        .ToArray();
                    for (n = 0; n < profitByDayOfWeek.Length; n++)
                    {
                        midProfit += profitByDayOfWeek[n].Profit;
                    }
                    if (profitByDayOfWeek.Length != 0)
                        midProfit /= profitByDayOfWeek.Length;
                    buff.MidWeekProfit[j] = midProfit;
                }
                returnList.Add(buff);
            }
            return returnList;
        }
    }
}

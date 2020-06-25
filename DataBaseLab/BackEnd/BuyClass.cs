using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OKDT
{
    class BuyClass
    {
        public Buy[] ReadBuys()
        {
            Buy[] ReadProfit = new Buy[0];
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    ReadProfit = dbContext.Buys.ToArray();
                }
                catch
                {
                    return ReadProfit;
                }
            }
            return ReadProfit;
        }

        public int BuyWork(ChangeInfo[] BuyArray)
        {
            Buy WriteInfo;
            Storage storageBuff = new Storage();
            int i;
            int lenght;
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    lenght = dbContext.Storages.Count();
                }
                catch
                {
                    return 1;
                }

                for (i = 0; i < BuyArray.Length; i++)
                {
                    storageBuff = dbContext.Storages
                            .Where(c => c.Id == BuyArray[i].Id)
                            .FirstOrDefault();

                    if (storageBuff != null)
                    {
                        storageBuff.Amount += BuyArray[i].Amount;

                        WriteInfo = new Buy()
                        {
                            ProductId = BuyArray[i].Id,
                            Amount = BuyArray[i].Amount,
                            ConterAgentId = storageBuff.AgentId
                        };
                        dbContext.Buys.Add(WriteInfo);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        //место для обработки ошибки
                    }
                }
            }
            return 0;
        }
    }
}

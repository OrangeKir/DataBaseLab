namespace OKCRM.Controller
{
    using System.Collections.Generic;
    using System.Linq;
    
    static class BuyHelper
    {
        public static List<Buy> ReadBuys()
        {
            var ReadProfit = new List<Buy>();
            using (var dbContext = new MyDbContext())
            {
                try
                {
                    ReadProfit = dbContext.Buys.ToList();
                }
                catch
                {
                    return ReadProfit;
                }
            }
            return ReadProfit;
        }

        public static int BuyWork(List<ChangeInfo> buyList)
        {
            Buy WriteInfo;
            Storage storageBuff = new Storage();
            using(var dbContext = new MyDbContext())
            {
                try
                {
                    dbContext.Storages.Count();
                }
                catch
                {
                    return 1;
                }

                buyList.Select()
                
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

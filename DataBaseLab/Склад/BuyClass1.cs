using System;
using System.Linq;

namespace Blank
{
    class BuyClass
    {
        string[] AgentName;
        string[] ProductName;
        int[] ProductAmount;
        int ProductNum;
        int UserId;

        private int NumInsert()
        {
            int trigger;
            int key = 0;
            while (true)
            {
                trigger = 0;
                try
                {
                    key = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    trigger = 1;
                    Console.Write("Ошибка ввода, попробуйте еще раз\n>>");
                }
                if (trigger == 0)
                    break;
            }
            return key;
        }

        private void BuyOrder()
        {
            int i;
            int key;
            int agentId;
            int Amount;
            Console.Write("Введите id товара для закупки>> ");
            key = NumInsert();
            if (key < 0 | key > ProductName.Length - 1)
            {
                Console.WriteLine("Такого товара нет");
                return;
            }
            else if (key == 0)
                return;
            Console.Write("Введите кол-во докупаемого товара>> ");
            while(true)
            {
                Amount = NumInsert();
                if (Amount > 0)
                    break;
                Console.Write("Ошибка, число должно быть положительно. Введите кол-во корректно>> ");
            }
            Console.WriteLine("Список контрагентов для закупки:");
            for (i = 0; i < AgentName.Length; i++)
                Console.WriteLine($"{i + 1})\t{AgentName[i]}");
            Console.Write("Введите id контрагента>> ");
            while(true)
            {
                agentId = NumInsert();
                if (agentId > 0 && agentId < (AgentName.Length + 1))
                    break;
                Console.Write("Такого id нет. Попробуйте еще раз>> ");
            }
            using(var context = new MyDbContext())
            {
                var curent = new Buy()
                {
                    ManagerId = UserId,
                    ConterAgentId = agentId,
                    ProductId = key,
                    ProdAmount = Amount,
                };
                var UpdateBuff = context.Storages.Where(c => c.Id == key).FirstOrDefault();
                UpdateBuff.Amount = UpdateBuff.Amount + Amount;
                context.Buys.Add(curent);
                context.SaveChanges();
            }
        }

        public void Buy(int inUserId)
        {          
            int i;
            int key;
            int agentnum;
            UserId = inUserId;

            using(var context = new MyDbContext())
            {
                try
                {
                    ProductNum = context.Storages.Count();
                }
                catch
                {
                    Console.WriteLine("Дальнейшая работа невозможна из-за отсутсвия товаров в базе данных");
                    return;
                }
                ProductAmount = new int[ProductNum];
                ProductName = new string[ProductNum];
                var buff = context.Storages.ToList();
                for (i = 0; i < ProductNum; i++)
                {
                    ProductName[i] = buff[i].Name;
                    ProductAmount[i] = buff[i].Amount;
                }
                try
                {
                    agentnum = context.Agents.Count();
                }
                catch
                {
                    Console.WriteLine("Дальнейшая работа невозможна из-за отсутсвия контрагентов в базе данных");
                    return;
                }
                AgentName = new string[agentnum];
                var buff1 = context.Agents.ToList();
                for (i = 0; i < agentnum; i++)
                    AgentName[i] = buff1[i].Name;
            }
            Console.WriteLine("Все позиции товаров:");
            for (i = 0; i < ProductName.Length; i++)
            {
                Console.WriteLine($"{i + 1})\t{ProductName[i].ToString(),-15}{ProductAmount[i].ToString(),10} шт.");
            }
            while (true)
            {
                Console.Write("Выбирите действие:\n\t1)Докупить позицию\n\t0)Выйти\n>>");
                key = NumInsert();
                switch (key)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        BuyOrder();
                        break;
                    default:
                        Console.Write("Такой команды нет, повторите ввод>>");
                        break;
                }
            }
        }
    }
}

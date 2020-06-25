using System;
using System.Linq;

namespace Blank
{

    class SaleClass
    {
        private string[] AgentNames;
        private string[] ProductNames;
        private int[] ProductAmount;
        private int UserId;
        
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

        private void SaleOrder()
        {
            int i;
            int key;
            int agentId;
            int Amount;
            Console.Write("Введите id товара для продажи>> ");
            key = NumInsert();
            if (key < 0 | key > ProductNames.Length - 1)
            {
                Console.WriteLine("Такого товара нет");
                return;
            }
            else if (key == 0)
                return;
            Console.WriteLine("Введите кол-во продоваемого товара");
            while (true)
            {
                Amount = NumInsert();
                if(Amount>ProductAmount[key])
                    Console.WriteLine("Число запрашиваемого товара больше чем есть на складе. Попробуйте еще раз");
                else if (Amount > 0)
                    break;
                else 
                    Console.Write("Ошибка, число должно быть положительно. Введите кол-во корректно>> ");

            }
            Console.WriteLine("Список контрагентов для продажи:");
            for (i = 0; i < AgentNames.Length; i++)
                Console.WriteLine($"{i + 1})\t{AgentNames[i]}");
            Console.Write("Введите id контрагента>> ");
            while (true)
            {
                agentId = NumInsert();
                if (agentId > 0 & agentId < AgentNames.Length + 1)
                    break;
                Console.Write("Такого id нет. Попробуйте еще раз>> ");
            }
            using (var context = new MyDbContext())
            {
                var curent = new Sale()
                {
                    ManagerId = UserId,
                    ConterAgentId = agentId,
                    ProductId = key,
                    ProdAmount = Amount,
                };
                var UpdateBuff = context.Storages.Where(c => c.Id == key).FirstOrDefault();
                UpdateBuff.Amount = UpdateBuff.Amount - Amount;
                context.Sales.Add(curent);
                context.SaveChanges();
            }
        }
        public void SaleWork(int inUserId)
        {
            int[] ProductId = new int[0];
            int[] CartAmount = new int[0];
            int i;
            int ProductNum;
            int AgentNum;
            int key = 0;
            Console.Clear();
            UserId = inUserId;
            using (var context = new MyDbContext())
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
                ProductNames = new string[ProductNum];
                var buff = context.Storages.ToList();
                for (i = 0; i < ProductNum; i++)
                {
                    ProductNames[i] = buff[i].Name;
                    ProductAmount[i] = buff[i].Amount;
                }
                try
                {
                    AgentNum = context.Agents.Count();
                }
                catch
                {
                    Console.WriteLine("Дальнейшая работа невозможна из-за отсутсвия контрагентов в базе данных");
                    return;
                }
                AgentNames = new string[AgentNum];
                var buff1 = context.Agents.ToList();
                for (i = 0; i < AgentNum; i++)
                    AgentNames[i] = buff1[i].Name;
            }
            Console.WriteLine("Все позиции товаров:");
            for (i = 0; i < ProductNames.Length; i++)
            {
                Console.WriteLine($"{i + 1})\t{ProductNames[i].ToString(),-15}{ProductAmount[i].ToString(),10} шт.");
            }
            while (true)
            {
                Console.Write("Выбор операции\n\t1)Оформить продажу\n\t0)Выход\n>>");
                key = NumInsert();
                switch(key)
                {
                    case 1:
                        SaleOrder();
                        break;
                    case 0:
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine(">>Такой команды нет");
                        break;
                }

            }
        }
    }
}

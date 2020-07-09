using System;

using System.Linq;

namespace Blank
{
    class StructWorkClass
    {
        string path = "";


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
                    Console.Write("Ошибка ввода, попробуйте еще раз>> ");
                }
                if (trigger == 0)
                    break;
            }
            return key;
        }

        private void ReadUserTable()
        {
            int i;
            int posleng;
            int unitleng;
            string[] PositionList = new string[0];
            string[] SubUnitList = new string[0];
            
            using (var context = new MyDbContext())
            {
                var pos = new Position();
                var subUn = new SubUnit();
                var curent = new User();
                try
                {
                    posleng = context.Positions.Count();
                }
                catch
                {
                    posleng = 0;
                }
                try
                {
                    unitleng = context.SubUnits.Count();
                }
                catch
                {
                    unitleng = 0;  
                }
                PositionList = new string[posleng];
                SubUnitList = new string[unitleng];
                if (posleng > 0)
                {
                    var posbuff = context.Positions.ToList();
                    for (i = 0; i < posleng; i++)
                        PositionList[i] = posbuff[i].PositionName;
                }
                if (unitleng > 0)
                {
                    var unitbuff = context.SubUnits.ToList();
                    for (i = 0; i < unitleng; i++)
                        SubUnitList[i] = unitbuff[i].SubUnitName;
                }
                i = 2;
                //while (true)
                //{
                //    if (ws.Cells[i, 1].Value == null)
                //        break;
                //    curent.Name = ws.Cells[i, 1].Value;
                //    PosTrigger = -1;
                //    UnitTrigger = -1;

                //    for (j = 0; j < PositionList.Length; j++)
                //    {
                //        if(PositionList[j]==ws.Cells[i,2].Value)
                //        {
                //            PosTrigger = j;
                //            break;
                //        }
                //    }
                //    for (j = 0; j < SubUnitList.Length; j++)
                //    {
                //        if(SubUnitList[j]==ws.Cells[i,3].Value)
                //        {
                //            UnitTrigger = j;
                //            break;
                //        }
                //    }

                //    if (PosTrigger < 0)
                //    {
                //        curent.Position = PositionList.Length + 1;
                //        Array.Resize(ref PositionList, PositionList.Length + 1);
                //        PositionList[PositionList.Length - 1] = ws.Cells[i, 2].Value;
                //        pos.PositionName = ws.Cells[i, 2].Value;
                //        context.Positions.Add(pos); 
                //    }
                //    else
                //        curent.Position = PosTrigger + 1;

                //    if (UnitTrigger < 0)
                //    {
                //        curent.SubUnit = SubUnitList.Length + 1;
                //        Array.Resize(ref SubUnitList, SubUnitList.Length + 1);
                //        SubUnitList[SubUnitList.Length - 1] = ws.Cells[i, 3].Value;
                //        subUn.SubUnitName = ws.Cells[i, 3].Value;
                //        context.SubUnits.Add(subUn);
                //    }
                //    else
                //        curent.SubUnit = UnitTrigger + 1;

                //    context.Users.Add(curent);
                //    context.SaveChanges();
                //    i++;
                //}
            }
            Console.WriteLine("Копирование таблицы пользователей прошло успешно");
        }
        public void ReadTable()
        {
            int trigger;
            Console.Write("Введите расположение таблицы\n>>");
            while(true)
            {
                trigger = 0;
                path = Console.ReadLine();
                try
                {
                    //wb = excel.Workbooks.Open(path);
                }
                catch
                {
                    trigger = 1;
                }
                if (trigger == 0)
                    break;
                Console.Write("Не найден файл Excel по указанной ссылке. Попробуйте еще раз\n>>");
            }
            Console.WriteLine("Файл Excel успешно открыт");
            //ReadAgentTable();
            ReadUserTable();
        }
        public void ViewStruct()
        {
            User[] Users;
            string[] Positions;
            string[] SubUnits;
            int UsersLeng;
            int PositionsLeng;
            int SubUnitsLeng;
            int i;
            using (var context = new MyDbContext())
            {
                try
                {
                    UsersLeng = context.Users.Count();
                }
                catch
                {
                    Console.WriteLine("База пользователей пуста");
                    return;
                }
                try
                {
                    PositionsLeng = context.Positions.Count();
                }
                catch
                {
                    PositionsLeng = 0;
                }
                try
                {
                    SubUnitsLeng = context.Positions.Count();
                }
                catch
                {
                    SubUnitsLeng = 0;
                }
                var UsersList = context.Users.ToList();
                Users = new User[UsersLeng];

                Positions = new string[PositionsLeng];
                if (PositionsLeng > 0)
                {
                    var PositionsList = context.Positions.ToList();
                    for (i = 0; i < PositionsLeng; i++)
                        Positions[i] = PositionsList[i].PositionName;
                }

                SubUnits = new string[SubUnitsLeng];
                if (SubUnitsLeng > 0)
                {
                    var SubUnitList = context.SubUnits.ToList();
                    for (i = 0; i < SubUnitsLeng; i++)
                        SubUnits[i] = SubUnitList[i].SubUnitName;
                }
                Console.WriteLine("Id\tФИО\t\t\tДолжность\tПодструктура");
                for (i = 0; i < UsersLeng; i++)
                {
                    Console.Write($"{i + 1}){UsersList[i].Name}\t");
                    if (PositionsLeng > 0)
                        Console.Write($"{Positions[UsersList[i].Position - 1], 24}");
                    Console.Write("\t");
                    if (SubUnitsLeng > 0)
                        Console.Write(SubUnits[UsersList[i].SubUnit - 1]);
                    Console.WriteLine();
                }
            }
        }
        public void AddProduct()
        {
            Console.Write("Введите название товара>> ");
            var prod = new Storage();
            prod.Name = Console.ReadLine();
            Console.Write("Введите текущее количество товара>> ");
            prod.Amount = NumInsert();
            using (var context = new MyDbContext())
            {
                context.Storages.Add(prod);
                context.SaveChanges();
            }
        }
    }

}
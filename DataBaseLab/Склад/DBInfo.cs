using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Useful
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }

        //информация о товаре

        public DbSet<Storage> Storages { get; set; }                        //информация о позиции товара
        public DbSet<Sale> Sales { get; set; }                              //информация о продаже товара +
        public DbSet<Buy> Buys { get; set; }                                //информация о закупке товара +

        //информация о частях магазина

        public DbSet<ConterAgent> CounterAgents { get; set; }               //информация о поставщике товара
        public DbSet<Manager> Managers { get; set; }                        //информация о сотруднике
        public DbSet<Section> Sections { get; set; }                        //список отделов магазина
        public DbSet<StoreInfo> StoreInfos { get; set; }                    //информация о магазине

        //информация о прибыли

        public DbSet<ProfitArchive> ProfitArchives { get; set; }            //архив прибыли сотрудников +
        public DbSet<DailySectionProfit> DailySectionProfits { get; set; }  //ежедневная прибыль по отделу
        public DbSet<DailyProfit> DailyProfits { get; set; }                //ежедневная прибыль магазина
        public DbSet<MountlyProfit> MountlyProfits { get; set; }            //ежемесячная прибыль магазина
    }

//структуры для упрощенной работы внутри программы

    public class ProductInfo    //для храниения информации в окнах покупки и продажи
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int MinAmount { get; set; }
    }

    public class ChangeInfo     //для передачи информации в окнах покупки и продажи в бд
    {
        public int Id { get; set; }
        public int Amount { get; set; }
    }

    //структуры для записи в БД

    //информация о товаре
    public class Storage        //информация о позиции товара
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int MinAmount { get; set; }

        [NotMapped]
        public int[] SectionId
        {
            get
            {
                int[] ReturnValue;
                int i;
                int j = 0;
                int leng = 0;
                int buff = 0;
                string a = "" + WriteSectionId;
                for (i = 0; i < a.Length; i++)
                    if (a[i] == 0)
                        leng++;
                ReturnValue = new int[leng];
                for (i = 0; i < a.Length; i++)
                {
                    if (a[i] == 0)
                    {
                        ReturnValue[j] = buff;
                        j++;
                        buff = 0;
                    }
                    else
                    {
                        buff = buff * 10 + a[i];
                    }
                }
                return ReturnValue;
            }
            set
            {
                int i;
                string a = "";
                for (i = 0; i < SectionId.Length; i++)
                    a += $"{SectionId[i]} ";
                WriteSectionId = a;
            }
        }
        private string WriteSectionId { get; set; }

        [NotMapped]
        public int[] SectionAmount
        {
            get
            {
                int[] ReturnValue;
                int i;
                int j = 0;
                int leng = 0;
                int buff = 0;
                string a = "" + WriteSectionAmount;
                for (i = 0; i < a.Length; i++)
                    if (a[i] == 0)
                        leng++;
                ReturnValue = new int[leng];
                for (i = 0; i < a.Length; i++)
                {
                    if (a[i] == 0)
                    {
                        ReturnValue[j] = buff;
                        j++;
                        buff = 0;
                    }
                    else
                    {
                        buff = buff * 10 + a[i];
                    }
                }
                return ReturnValue;
            }
            set
            {
                int i;
                string a = "";
                for (i = 0; i < SectionAmount.Length; i++)
                    a += $"{SectionAmount[i]} ";
                WriteSectionAmount = a;
            }
        }
        public string WriteSectionAmount { get; set; }

        public int AgentId { get; set; }
        public double Price { get; set; }
    }

        public class Sale           //информация о продаже товара
    {
        public int Id { get; set; }

        [NotMapped]
        public int[] ProductId
        {
            get
            {
                int[] ReturnValue;
                int i;
                int j = 0;
                int leng = 0;
                int buff = 0;
                string a = "" + WriteProductId;
                for (i = 0; i < a.Length; i++)
                    if (a[i] == 0)
                        leng++;
                ReturnValue = new int[leng];
                for (i = 0; i < a.Length; i++)
                {
                    if (a[i] == 0)
                    {
                        ReturnValue[j] = buff;
                        j++;
                        buff = 0;
                    }
                    else
                    {
                        buff = buff * 10 + a[i];
                    }
                }
                return ReturnValue;
            }
            set
            {
                int i;
                string a = "";
                for (i = 0; i < ProductId.Length; i++)
                    a += $"{ProductId[i]} ";
                WriteProductId = a;
            }
        }
        private string WriteProductId { get; set; }         //строка для записи в БД

        [NotMapped]
        public int[] ProdAmount
        {
            get
            {
                int[] ReturnValue;
                int i;
                int j = 0;
                int leng = 0;
                int buff = 0;
                string a = "" + WriteProdAmount;
                for (i = 0; i < a.Length; i++)
                    if (a[i] == 0)
                        leng++;
                ReturnValue = new int[leng];
                for (i = 0; i < a.Length; i++)
                {
                    if(a[i]==0)
                    {
                        ReturnValue[j] = buff;
                        j++;
                        buff = 0;
                    }
                    else
                    {
                        buff = buff * 10 + a[i];
                    }
                }
                return ReturnValue;
            }
            set
            {
                int i;
                string a = "";
                for (i = 0; i < ProdAmount.Length; i++)
                    a += $"{ProdAmount[i]} ";
                WriteProdAmount = a;
            }
        }
        private string WriteProdAmount { get; set; }        //строка для записи в БД

        [NotMapped]
        public double[] PositionProfit                         //прибыль за каждую позицию
        {
            get 
            {
                double[] ReturnValue;
                int i;
                int j = 0;
                int leng = 0;
                int buff = 0;
                string a = "" + WritePositionProfit;
                for (i = 0; i < a.Length; i++)
                    if (a[i] == 0)
                        leng++;
                ReturnValue = new double[leng];
                for (i = 0; i < a.Length; i++)
                {
                    if (a[i] == 0)
                    {
                        ReturnValue[j] = buff;
                        j++;
                        buff = 0;
                    }
                    else
                    {
                        buff = buff * 10 + a[i];
                    }
                }
                return ReturnValue;
            }
            set 
            {
                int i;
                string a = "";
                for (i = 0; i < PositionProfit.Length; i++)
                    a += $"{ProductId[i]} ";
                WritePositionProfit = a;
            } 
        }
        private string WritePositionProfit { get; set; }    //строка для записи в БД

        public int ManagerId { get; set; }
        public int SectionId { get; set; }
        public double Profit { get; set; }
    }

    public class Buy            //информация о закупке товара
    {
        public int Id { get; set; }
        public int[] ProductId { get; set; }        //надо подшаманить
        private string WriteProductId { get; set; }     //строка для записи в БД
        public int[] ProdAmount { get; set; }       //надо подшаманить
        private string WriteProdAmount { get; set; }    //строка для записи в БД
        public int[] ConterAgentId { get; set; }    //надо подшаманить
        private string WriteConterAgentId { get; set; } //строка для записи в БД
    }

    //информация о частях магазина

    public class ConterAgent    //информация о поставщике товара
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Inn { get; set; }
        public int Kpp { get; set; }
        public int Ogrn { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
    }
    public class Manager        //информация о сотруднике
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurentProfit { get; set; }       //выручка за текущий календарный месяц
        public int Status { get; set; }             //информация о последнем учете ЗП
    }
    public class Section        //список отделов ммагазина
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StoreInfo      //информация о магазине
    {
        public string Name { get; set; }
        public int NewMounthDate { get; set; }
    }

    //информация о прибыли

    public class ProfitArchive  //архив прибыли сотрудников
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int[] ManagerId { get; set; }        //надо подшаманить
        public double[] Profit { get; set; }        //надо подшаманить
    }

    public class DailySectionProfit //информация о прибыли по отделу
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int SectionId { get; set; }
        public double Profit { get; set; }
    }

    public class DailyProfit        //ежедневная прибыль магазина
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Profit { get; set; }
    }
    public class MountlyProfit      //ежемесячная прибыль магазина
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Profit { get; set; }
    }
}
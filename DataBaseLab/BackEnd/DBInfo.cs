 using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OKDT
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
        public double Price { get; set; }
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
        public int SectionId { get; set; }
        public int AgentId { get; set; }
        public double Price { get; set; }
    }


    public class Sale           //информация о продаже товара
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ManagerId { get; set; }
        public int SectionId { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }
    }

    public class Buy            //информация о закупке товара
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ConterAgentId { get; set; }
    }

    //информация о частях магазина

    public class ConterAgent    //информация о поставщике товара
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Manager        //информация о сотруднике
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CurentProfit { get; set; }       //выручка за текущий календарный месяц
        public DateTime Status { get; set; }             //информация о последнем учете ЗП
    }
    public class Section        //список отделов магазина
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //информация о прибыли

    public class ProfitArchive  //архив прибыли сотрудников
    {
        public int Id { get; set; }
        public DateTime Mounth { get; set; }
        public int ManagerId { get; set; }
        public double Profit { get; set; }
    }

    public class DailySectionProfit //информация о прибыли по отделу
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SectionId { get; set; }
        public double Profit { get; set; }
    }

    public class DailyProfit        //ежедневная прибыль магазина
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Profit { get; set; }
        public int DayOfWeek { get; set; }
    }
    public class MountlyProfit      //ежемесячная прибыль магазина
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Profit { get; set; }
    }
}
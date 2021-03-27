namespace OKCRM.Controller
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class MyDbContext : DbContext
    {
        /// <summary> ctor </summary>
        public MyDbContext() : base("DbConnectionString")
        { }

        //информация о товаре

        /// <summary> Информация о позиции товара </summary>
        public DbSet<Storage> Storages { get; set; }
        
        /// <summary> Информация о продаже товара </summary>
        public DbSet<Sale> Sales { get; set; }
        
        /// <summary> Информация о закупке товара </summary>
        public DbSet<Buy> Buys { get; set; }

        //информация о частях магазина

        /// <summary> Информация о поставщике товара </summary>
        public DbSet<ConterAgent> CounterAgents { get; set; }
        
        /// <summary> Информация о сотруднике </summary>
        public DbSet<Manager> Managers { get; set; }
        
        /// <summary> Список отделов магазина </summary>
        public DbSet<Section> Sections { get; set; }

        //информация о прибыли

        /// <summary> Архив прибыли сотрудников </summary>
        public DbSet<ProfitArchive> ProfitArchives { get; set; }
        
        /// <summary> Ежедневная прибыль по отделу </summary>
        public DbSet<DailySectionProfit> DailySectionProfits { get; set; }
        
        /// <summary> Ежедневная прибыль магазина </summary>
        public DbSet<DailyProfit> DailyProfits { get; set; }
        
        /// <summary> Ежемесячная прибыль магазина </summary>
        public DbSet<MountlyProfit> MountlyProfits { get; set; }
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
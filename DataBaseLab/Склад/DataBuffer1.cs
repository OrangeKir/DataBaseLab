using System.Data.Entity;

namespace Blank
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }
        public DbSet<ConterAgent> Agents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SubUnit> SubUnits { get; set; }

        public DbSet<Storage> Storages { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProdAmount { get; set; }
        public int ManagerId { get; set; }
        public int ConterAgentId { get; set; }
    }

    public class Buy
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProdAmount { get; set; }
        public int ManagerId { get; set; }
        public int ConterAgentId { get; set; }
    }
    public class ConterAgent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Inn { get; set; }
        public int Kpp { get; set; }
        public int Ogrn { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int SubUnit { get; set; }
    }
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
    }
    public class SubUnit
    {
        public int Id { get; set; }
        public string SubUnitName { get; set; }
    }
}
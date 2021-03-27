namespace OKCRM.Model
{
    using System;

    /// <summary>
    /// Информация
    /// </summary>
    public class ProductInfo    //для храниения информации в окнах покупки и продажи
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int MinAmount { get; set; }
        public double Price { get; set; }
    }
}
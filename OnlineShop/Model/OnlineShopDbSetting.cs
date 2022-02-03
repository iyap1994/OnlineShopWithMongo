namespace OnlineShop.Model
{
    public class OnlineShopDbSetting : IOnlineShopDbSetting
    {
        public string ProductsCollectionName { get; set; }
        public string OrdersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IOnlineShopDbSetting
    {
        string ProductsCollectionName { get; set; }
        string OrdersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

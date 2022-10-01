namespace Dashboard.ViewModels
{
    public class ShopViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateShopViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdateShopViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}

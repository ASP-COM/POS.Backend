namespace POS.DB.Enums
{
    public enum OrderStatus
    {
        Pending = 0,
        Canceled,
        Paid,
        Archived
    }

    public enum PaymentMethod
    {
        None = 0,
        Cash,
        Card,
        FullVoucher,
        FullLoyaltyPoints
    }

    public enum ItemType
    {
        Product = 0,
        Service,
        Dish
    }
}

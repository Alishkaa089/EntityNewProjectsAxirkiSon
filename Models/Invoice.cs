public class Invoice
{
    public int Id { get; set; }
    public List<Order> Orders { get; set; }
    public int CustomerId { get; set; }
    public int CashierId { get; set; }
    public DateTime Date { get; set; }

    public Invoice()
    {
        Orders = new List<Order>();
    }


}


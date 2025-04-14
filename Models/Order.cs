public class Order
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string CustomerName { get; set; }
    public List<Order> Orders { get; set; }
    public DateTime OrderDate { get; set; }
    public Invoice Invoice { get; set; }
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    public int CashierId { get; set; }

}
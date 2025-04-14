using System;

public class Product
{

    public int Id { get; set; }
    public string Barcode { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public void GenerateBarcode()
    {
        Barcode = Guid.NewGuid().ToString();
    }

}


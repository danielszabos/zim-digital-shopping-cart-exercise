namespace ShoppingCart.Test;

public class CartTests
{
    [Fact]
    public void EmptyCart_HasZeroItems()
    {
        var cart = new Cart();

        Assert.Equal(0, cart.ItemCount);
    }

    [Fact]
    
    public void AddingOneProduct_ItemCountIsOne()
    {
        
        var pen = new Product("PEN", "Pen", 120);

        var cart = new Cart();

        cart.Add(pen);
        

        Assert.Equal(1, cart.ItemCount);
    }
}

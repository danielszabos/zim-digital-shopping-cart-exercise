namespace ShoppingCart.Test;

public class CartTests
{
    [Fact]
    public void EmptyCart_HasZeroItems()
    {
        var cart = new Cart();

        Assert.Equal(0, cart.ItemCount);
    }
}

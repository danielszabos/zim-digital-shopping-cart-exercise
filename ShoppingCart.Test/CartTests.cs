namespace ShoppingCart.Test;

public class CartTests
{
    private static readonly Product Pen = Products.Pen;
    private static readonly Product Mug = Products.Mug;

    [Fact]
    public void EmptyCart_HasZeroItems()
    {
        var cart = new Cart();

        Assert.Equal(0, cart.ItemCount);
    }

    [Fact]
    public void AddingOneProduct_ItemCountIsOne()
    {
        var cart = new Cart();

        cart.Add(Pen);

        Assert.Equal(1, cart.ItemCount);
    }

    [Fact]
    public void AddingSameProductTwice_IncreasesQuantityOnOneLine()
    {
        var cart = new Cart();

        cart.Add(Pen);
        cart.Add(Pen);

        var line = Assert.Single(cart.Lines);
        Assert.Equal(Pen, line.Key);
        Assert.Equal(2, line.Value);
    }

    [Fact]
    public void TwoPensAndOneMug_ItemCountIsThree()
    {
        var cart = new Cart();

        cart.Add(Pen);
        cart.Add(Pen);
        cart.Add(Mug);

        Assert.Equal(3, cart.ItemCount);
    }
}

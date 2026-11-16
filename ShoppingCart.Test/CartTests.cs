namespace ShoppingCart.Test;

public class CartTests
{
    private static readonly Product Pen = Products.Pen;
    private static readonly Product Notebook = Products.Notebook;
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

    [Fact]
    public void EmptyCart_TotalIsZero()
    {
        var cart = new Cart();

        Assert.Equal(0, cart.TotalInPence);
    }

    [Fact]
    public void OneMug_TotalIsSixPounds()
    {
        var cart = new Cart();

        cart.Add(Mug);

        Assert.Equal(600, cart.TotalInPence);
    }

    [Fact]
    public void OnePenAndOneMug_TotalIsSumOfPrices()
    {
        var cart = new Cart();

        cart.Add(Pen);
        cart.Add(Mug);

        Assert.Equal(720, cart.TotalInPence);
    }

    [Fact]
    public void TwoPens_TakesTenPercentOffTheLine()
    {
        var cart = new Cart();

        cart.Add(Pen);
        cart.Add(Pen);

        Assert.Equal(2, cart.ItemCount);
        Assert.Equal(216, cart.TotalInPence);
    }

    [Fact]
    public void ThreeNotebooks_TakesTenPercentOffTheLine()
    {
        var cart = new Cart();

        cart.Add(Notebook);
        cart.Add(Notebook);
        cart.Add(Notebook);

        Assert.Equal(3, cart.ItemCount);
        Assert.Equal(675, cart.TotalInPence);
    }

    [Fact]
    public void TwoPensOneMugAndThreeNotebooks_DiscountsOnlyTheMultiBuyLines()
    {
        var cart = new Cart();

        cart.Add(Pen);
        cart.Add(Pen);
        cart.Add(Mug);
        cart.Add(Notebook);
        cart.Add(Notebook);
        cart.Add(Notebook);

        Assert.Equal(6, cart.ItemCount);
        Assert.Equal(1491, cart.TotalInPence);
    }

    [Theory]
    [InlineData(99, 2, 178)] // 198 less 10% = 178.2, rounds down
    [InlineData(11, 2, 20)]  // 22 less 10% = 19.8, rounds up
    [InlineData(75, 3, 203)] // 225 less 10% = 202.5, a half rounds up
    public void DiscountedLine_IsRoundedToTheNearestPenny(int priceInPence, int quantity, int expectedTotal)
    {
        var product = new Product("TEST", "Test product", priceInPence);
        var cart = new Cart();

        for (var i = 0; i < quantity; i++)
        {
            cart.Add(product);
        }

        Assert.Equal(expectedTotal, cart.TotalInPence);
    }
}

namespace ShoppingCart;

public class Cart
{
    private const decimal MultiBuyDiscount = 0.10m;

    private readonly Dictionary<Product, int> _quantities = new();

    public IReadOnlyDictionary<Product, int> Lines => _quantities.AsReadOnly();

    public int ItemCount => _quantities.Values.Sum();

    public int TotalInPence => _quantities.Sum(line => LineTotalInPence(line.Key, line.Value));

    public void Add(Product product)
    {
        if (_quantities.ContainsKey(product))
        {
            _quantities[product] += 1;
        }
        else
        {
            _quantities[product] = 1;
        }
    }

    public void Remove(Product product)
    {
        if (!_quantities.ContainsKey(product))
        {
            return;
        }

        if (_quantities[product] > 1)
        {
            _quantities[product] -= 1;
        }
        else
        {
            _quantities.Remove(product);
        }
    }

    private static int LineTotalInPence(Product product, int quantity)
    {
        int fullPrice = product.PriceInPence * quantity;

        if (quantity <= 1)
        {
            return fullPrice;
        }

        decimal discountedPrice = fullPrice * (1 - MultiBuyDiscount);
        return (int)Math.Round(discountedPrice, MidpointRounding.AwayFromZero);
    }
}

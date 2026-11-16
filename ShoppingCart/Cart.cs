namespace ShoppingCart;

public class Cart
{
    private readonly Dictionary<Product, int> _quantities = new();

    public int ItemCount => _quantities.Values.Sum();

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
}

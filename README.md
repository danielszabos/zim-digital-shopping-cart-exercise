# Shopping Cart Kata

A small in-memory shopping cart in C# (.NET 8). You can add and remove items, count them, and get the total with a 10% multi-buy discount. Tests use xUnit.

## How to run
From the repository root:

```
dotnet build
dotnet test
```

## Project layout

```
ShoppingCart/            class library
  Product.cs             a product: code, name and price in pence
  Products.cs            the three products from the brief (pen, notebook, mug)
  Cart.cs                add, remove, count and total, including the discount
ShoppingCart.Test/       xUnit tests
  CartTests.cs           one or more tests per requirement, plus the brief's worked examples
```

## How it works

- `Product` is a record. Two products with the same code, name and price count as equal, so they can be used as dictionary keys.
- `Cart` keeps a private `Dictionary<Product, int>` that maps each product to its quantity. Adding a product that's already there increases its quantity, so the cart has one line per product.
- `ItemCount` adds up the quantities.
- `TotalInPence` adds up the line totals. A line's total is price × quantity, with 10% off when the quantity is more than one, rounded to the nearest penny.
- Prices are stored as whole pence (`int`) so nothing gets lost to rounding. The 10% calculation uses `decimal` and is rounded once, per line.

## Assumptions

- **"More than one of the same product"** means a quantity of 2 or more. The 10% comes off the whole line, every unit, not just the extra ones. This matches the worked examples (2 pens = £2.16).
- **Rounding:** each discounted line is rounded to the nearest penny, and an exact half rounds up (202.5p becomes 203p). C#'s `Math.Round` uses banker's rounding by default (202.5 becomes 202), so I set `MidpointRounding.AwayFromZero` explicitly. None of the brief's products produce a half penny, so a test uses made-up prices to cover it.
- **Totals are returned in pence.** Showing them as pounds (£14.91) is left to whatever displays them.
- **A product is identified by all of its values.** Two products with the same code but different prices would be separate lines.
- **`Add` adds one item at a time.**
- **`Remove` takes away one item.** When a product's quantity reaches 0 its line is removed. Removing a product that isn't in the cart does nothing rather than throwing an error.
- No validation of null products or negative prices. I treated that as out of scope for the exercise.

## Where I got to

Requirements 1 to 4 are done and tested, including every worked example from the brief, plus the optional "remove an item". I didn't build a console app.


## How I used AI

I used Claude Code throughout.

- **Learning C#.** I know Java but hadn't written C# before. I started by asking Claude to explain C# concepts (records, properties, `Dictionary`, xUnit) and to review my code rather than write it. I wrote the `Product` record, the first tests and a first version of `Cart.Add`. Claude helped me read compiler errors and pointed out a bug in my `Add`: I was checking whether the whole cart was empty instead of whether that product was already in it.
- **Writing the rest.** The logic for adding items was my idea: if the product isn't in the cart yet, add it with a quantity of 1, otherwise increase its quantity, so each product only ever has one line. Once that was clear, I asked Claude to write the code for it using my logic, and then the total, discount and remove steps, so I could use my time efficiently. It wrote them test-first, in small commits marked `Co-Authored-By: Claude`, and we went through each step together, with Claude explaining the C# I hadn't used before.


## What I'd do next with more time

- Let `Add` take a quantity, for example `Add(pen, 3)`.
- Look products up by code from a catalogue, so a price change can't split one product into two lines.
- Use a small `Money` type instead of raw `int` pence, with formatting to pounds built in.
- Add input validation (null products, negative prices).
- Add a small console app to try the cart by hand.

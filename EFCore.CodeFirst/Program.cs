
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();

using(var _context = new ApplicationDbContext())
{
    // Custom Detached
    var product = await _context.Products.FirstAsync();
    Console.WriteLine($"First State: {_context.Entry(product).State}");

    _context.Entry(product).State = EntityState.Detached;
    Console.WriteLine($"Next State: {_context.Entry(product).State}");

    product.Name = "Kiwi";
    await _context.SaveChangesAsync();
    Console.WriteLine($"Last State: {_context.Entry(product).State}");

    
    // Deleted State
    var product2 = await _context.Products.FirstAsync();
    Console.WriteLine($"First State: {_context.Entry(product2).State}");

    _context.Remove(product2);
    //_context.Entry(product).State = EntityState.Deleted;
    Console.WriteLine($"Next State: {_context.Entry(product2).State}");

    await _context.SaveChangesAsync();
    Console.WriteLine($"Last State: {_context.Entry(product2).State}");
    

    // Modified State
    var product3 = await _context.Products.FirstAsync();
    Console.WriteLine($"First State: {_context.Entry(product3).State}");

    product.Stock = 1000;
    Console.WriteLine($"Next State: {_context.Entry(product3).State}");

    await _context.SaveChangesAsync();
    Console.WriteLine($"Last State: {_context.Entry(product3).State}");
    

    // Added State
    var newProduct = new Product { Name = "Apple", Price = 500, Stock = 50, Barcode = "143" };

    Console.WriteLine($"First State: {_context.Entry(newProduct).State}");

    //await _context.AddAsync(newProduct);

    _context.Entry(newProduct).State = EntityState.Added;

    Console.WriteLine($"Next State: {_context.Entry(newProduct).State}");

    await _context.SaveChangesAsync();

    Console.WriteLine($"Last State: {_context.Entry(newProduct).State}");
    

    
    var products = await _context.Products.ToListAsync();

    products.ForEach(p =>
    {
        var state = _context.Entry(p).State;  // Unchanged

        Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} - {p.Stock} State: {state}");
    });
    
}
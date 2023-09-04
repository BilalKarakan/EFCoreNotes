using DatabaseFirst.Scaffold.Models;
using Microsoft.EntityFrameworkCore;


using (var context = new AppDbContext())
{
    List<Product> products = await context.Products.ToListAsync();

    products.ForEach(p =>
    {
        Console.WriteLine($"{p.Id}: {p.Name} {p.Price} {p.Stock}");
    });
}

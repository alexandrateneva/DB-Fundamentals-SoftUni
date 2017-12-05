namespace ProductsShop.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Newtonsoft.Json;
    using ProductsShop.Data;
    using ProductsShop.Models;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new ProductsShopContext())
            {
                ResetDatabase(context);

                ImportUsersFromJson(context);

                ImportCategoriesFromJson(context);

                ImportProductsFromJson(context);

                ImportCategoryProducts(context);

                GetProductsInPriceRangeFromJson(context);

                GetAllSoldProductsFromJson(context);

                GetAllCategoriesFromJson(context);

                GetAllUsersAndProductsFromJson(context);

                ImportUsersFromXml(context);

                ImportCategoriesFromXml(context);

                ImportProductsFromXml(context);

                GetProductsInPriceRangeFromXml(context);

                GetAllSoldProductsFromXml(context);

                GetAllCategoriesFromXml(context);

                GetAllUsersAndProductsFromXml(context);
            }
        }

        private static void ResetDatabase(ProductsShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("ProductsShop database created successfully.");
        }

        private static void ImportCategoryProducts(ProductsShopContext context)
        {
            var productsIds = context.Products.Select(p => p.Id).ToArray();
            var categoryIds = context.Categories.Select(c => c.Id).ToArray();

            var rnd = new Random();
            var categoryProducts = new List<CategoryProducts>();

            foreach (var productId in productsIds)
            {
                var usedCategoryIds = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    var categoryIndex = rnd.Next(0, categoryIds.Length);
                    var categoryId = categoryIds[categoryIndex];

                    while (usedCategoryIds.Contains(categoryId))
                    {
                        categoryIndex = rnd.Next(0, categoryIds.Length);
                        categoryId = categoryIds[categoryIndex];
                    }

                    usedCategoryIds[i] = categoryId;

                    var categoryProduct = new CategoryProducts
                    {
                        ProductId = productId,
                        CategoryId = categoryId
                    };

                    categoryProducts.Add(categoryProduct);
                }
            }

            context.CategoryProducts.AddRange(categoryProducts);

            context.SaveChanges();

            Console.WriteLine($"{categoryProducts.Count} category and products combinations were added.");
        }

        // Import and Export -> Json

        private static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }

        private static void ImportUsersFromJson(ProductsShopContext context)
        {
            const string path = "Resources/users.json";

            var users = ImportJson<User>(path);

            context.Users.AddRange(users);

            context.SaveChanges();

            Console.WriteLine($"{users.Length} users were imported from file: {path}");
        }

        private static void ImportCategoriesFromJson(ProductsShopContext context)
        {
            const string path = "Resources/categories.json";

            var categories = ImportJson<Category>(path);

            context.Categories.AddRange(categories);

            context.SaveChanges();

            Console.WriteLine($"{categories.Length} categories were imported from file: {path}");
        }

        private static void ImportProductsFromJson(ProductsShopContext context)
        {
            const string path = "Resources/products.json";

            var products = ImportJson<Product>(path);

            var rnd = new Random();

            var usersIds = context.Users.Select(u => u.Id).ToArray();

            foreach (var product in products)
            {
                var sellerIndex = rnd.Next(0, usersIds.Length);
                var sellerId = usersIds[sellerIndex];

                var buyerIndex = rnd.Next(0, usersIds.Length);
                int? buyerId = usersIds[buyerIndex];

                while (sellerId == buyerId)
                {
                    buyerIndex = rnd.Next(0, usersIds.Length);
                    buyerId = usersIds[buyerIndex];
                }

                if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                {
                    buyerId = null;
                }

                product.SellerId = sellerId;
                product.BuyerId = buyerId;
            }

            context.Products.AddRange(products);

            context.SaveChanges();

            Console.WriteLine($"{products.Length} products were imported from file: {path}");
        }

        private static void GetProductsInPriceRangeFromJson(ProductsShopContext context)
        {
            const string path = "ProductsInPriceRange500to1000.json";

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(e => new
                {
                    e.Name,
                    e.Price,
                    SellerName = $"{e.Seller.FirstName} {e.Seller.LastName}"
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText(path, jsonString);
        }

        private static void GetAllSoldProductsFromJson(ProductsShopContext context)
        {
            const string path = "SoldProducts.json";

            var sellers = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Select(e => new
                {
                    SellerFirstName = e.FirstName,
                    SellerLastName = e.LastName,
                    SoldProducts = e.ProductsSold.Select(sp => new
                    {
                        ProductName = sp.Name,
                        sp.Price,
                        BuyerFirstName = sp.Buyer.FirstName,
                        BuyerLastName = sp.Buyer.LastName
                    })
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(sellers, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText(path, jsonString);
        }

        private static void GetAllCategoriesFromJson(ProductsShopContext context)
        {
            const string path = "AllCategories.json";

            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(e => new
                {
                    CategoryName = e.Name,
                    NumberOfProducts = e.CategoryProducts.Count,
                    AveragePrice = e.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = e.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText(path, jsonString);
        }

        private static void GetAllUsersAndProductsFromJson(ProductsShopContext context)
        {
            const string path = "UsersAndProducts.json";

            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(e => e.ProductsSold.Count)
                .ThenBy(e => e.LastName)
                .Select(s => new
                {
                    s.FirstName,
                    s.LastName,
                    s.Age,
                    SoldProducts = s.ProductsSold.Select(ps => new
                    {
                        SoldProductCount = s.ProductsSold.Count,
                        products = s.ProductsSold.Select(p => new
                        {
                            ProductName = p.Name,
                            Price = p.Price
                        })
                    }).ToArray()
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText(path, jsonString);
        }

        // Import and Export -> XML

        private static void ImportUsersFromXml(ProductsShopContext context)
        {
            const string path = "Resources/users.xml";

            var xmlString = File.ReadAllText(path);

            var xml = XDocument.Parse(xmlString);

            var root = xml.Root.Elements();

            var users = new List<User>();

            foreach (var xElement in root)
            {
                var firstName = xElement.Attribute("firstName")?.Value;
                var lastName = xElement.Attribute("lastName").Value;
                int? age = null;
                if (xElement.Attribute("age")?.Value != null)
                {
                    age = int.Parse(xElement.Attribute("age")?.Value);
                }

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            Console.WriteLine($"{users.Count} users were imported from file: {path}");
        }

        private static void ImportCategoriesFromXml(ProductsShopContext context)
        {
            const string path = "Resources/categories.xml";

            var xmlString = File.ReadAllText(path);

            var xml = XDocument.Parse(xmlString);

            var root = xml.Root.Elements();

            var categories = new List<Category>();

            foreach (var xElement in root)
            {
                var name = xElement.Element("name").Value;

                var category = new Category
                {
                    Name = name
                };

                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            Console.WriteLine($"{categories.Count} categories were imported from file: {path}");
        }

        private static void ImportProductsFromXml(ProductsShopContext context)
        {
            const string path = "Resources/products.xml";

            var xmlString = File.ReadAllText(path);

            var xml = XDocument.Parse(xmlString);

            var root = xml.Root.Elements();

            var products = new List<Product>();

            var rnd = new Random();

            var usersIds = context.Users.Select(u => u.Id).ToArray();

            foreach (var xElement in root)
            {
                var name = xElement.Element("name").Value;
                var price = decimal.Parse(xElement.Element("price").Value);

                var product = new Product
                {
                    Name = name,
                    Price = price
                };

                var sellerIndex = rnd.Next(0, usersIds.Length);
                var sellerId = usersIds[sellerIndex];

                var buyerIndex = rnd.Next(0, usersIds.Length);
                int? buyerId = usersIds[buyerIndex];

                while (sellerId == buyerId)
                {
                    buyerIndex = rnd.Next(0, usersIds.Length);
                    buyerId = usersIds[buyerIndex];
                }

                if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                {
                    buyerId = null;
                }

                product.SellerId = sellerId;
                product.BuyerId = buyerId;

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            Console.WriteLine($"{products.Count} products were imported from file: {path}");
        }

        private static void GetProductsInPriceRangeFromXml(ProductsShopContext context)
        {
            const string path = "../ProductsShop.App/ProductsInPriceRange1000to2000.xml";

            var products = context.Products
                .Where(p => p.BuyerId != null && p.Price >= 1000 && p.Price <= 2000)
                .OrderBy(p => p.Price)
                .Select(e => new
                {
                    e.Name,
                    e.Price,
                    Buyer = e.Buyer.FirstName != null ? e.Buyer.FirstName + " " + e.Buyer.LastName : e.Buyer.LastName
                })
                .ToArray();

            XDocument xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("products",
                from p in products
                select
                new XElement("product",
                    new XAttribute("name", p.Name),
                    new XAttribute("price", p.Price.ToString()),
                    new XAttribute("buyer", p.Buyer)
                )
            ));

            xmlDoc.Save(path);
        }

        private static void GetAllSoldProductsFromXml(ProductsShopContext context)
        {
            const string path = "../ProductsShop.App/SoldProducts.xml";

            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Select(e => new
                {
                    firstName = e.FirstName,
                    lastName = e.LastName,
                    soldProducts = e.ProductsSold.Select(sp => new
                    {
                        name = sp.Name,
                        price = sp.Price,
                    }).ToArray(),
                }).ToArray();

            XDocument xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("users",
                from u in users
                select
                new XElement("user",
                    u.firstName == null ? null : new XAttribute("first-name", u.firstName),
                    new XAttribute("last-name", u.lastName),
                    new XElement("sold-products",
                        from sp in u.soldProducts
                        select
                        new XElement("product",
                            new XElement("name", sp.name),
                            new XElement("price", sp.price)
                        )))));

            xmlDoc.Save(path);
        }

        private static void GetAllCategoriesFromXml(ProductsShopContext context)
        {
            const string path = "../ProductsShop.App/AllCategories.xml";

            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(e => new
                {
                    CategoryName = e.Name,
                    NumberOfProducts = e.CategoryProducts.Count,
                    AveragePrice = e.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = e.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .ToArray();

            XDocument xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("categories",
                from c in categories
                select
                new XElement("category",
                    new XAttribute("name", c.CategoryName),
                        new XElement("products-count", c.NumberOfProducts),
                        new XElement("average-price", c.AveragePrice),
                        new XElement("total-revenue", c.TotalRevenue)
                )));

            xmlDoc.Save(path);
        }

        private static void GetAllUsersAndProductsFromXml(ProductsShopContext context)
        {
            const string path = "../ProductsShop.App/UsersAndProducts.xml";

            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(e => e.ProductsSold.Count)
                .ThenBy(e => e.LastName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProductsCount = u.ProductsSold.Count,
                    Products = u.ProductsSold.Select(p => new
                    {
                        p.Name,
                        p.Price
                    }).ToArray(),
                }).ToArray();

            XDocument xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("users",
                    new XAttribute("count", users.Length),
                    from u in users
                    select
                    new XElement("user",
                        u.FirstName == null ? null : new XAttribute("first-name", u.FirstName),
                        new XAttribute("last-name", u.LastName),
                        u.Age == null ? null : new XAttribute("age", u.Age),
                            new XElement("sold-products", new XAttribute("count", u.SoldProductsCount),
                            from p in u.Products
                            select
                                new XElement("product",
                                    new XAttribute("name", p.Name),
                                    new XAttribute("price", p.Price)))
                    )));

            xmlDoc.Save(path);
        }
    }
}

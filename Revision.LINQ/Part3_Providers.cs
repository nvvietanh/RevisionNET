using System.Xml.Linq;
using System.Text.Json;
using Revision.LINQ.Models;
using Revision.LINQ.Data;

namespace Revision.LINQ
{
    /// <summary>
    /// PHẦN 3: CÁC LOẠI LINQ (PROVIDERS)
    /// Người thuyết trình 3
    /// </summary>
    public class Part3_Providers
    {
        public static void Demo()
        {
            Console.WriteLine("=== CÁC 'HƯƠNG VỊ' CỦA LINQ (PROVIDERS) ===\n");
            Console.WriteLine("LINQ là 'THỐNG NHẤT' - Cùng cú pháp, nhưng 'bộ phiên dịch' khác nhau:\n");
            Console.WriteLine("*** TẤT CẢ DEMO XOAY QUANH QUẢN LÝ SẢN PHẨM ***\n");

            DemoLinqToObjects();
            DemoLinqToEntities();
            DemoLinqToXml();
            DemoLinqToJson();
            DemoPLinq();
            ShowOtherProviders();

            Console.WriteLine("\n=== TỔNG KẾT ===");
            Console.WriteLine("+ LINQ cung cấp CÚ PHÁP THỐNG NHẤT cho nhiều nguồn dữ liệu");
            Console.WriteLine("+ Provider quyết định cách 'dịch' và 'thực thi' truy vấn");
            Console.WriteLine("+ Hiểu Provider giúp tối ưu hiệu năng (đặc biệt với CSDL)\n");
        }

        /// <summary>
        /// 1. LINQ to Objects - Truy vấn trong bộ nhớ
        /// </summary>
        private static void DemoLinqToObjects()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("1. LINQ to Objects");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Truy vấn: IEnumerable<T> (List, Array, ...)");
            Console.WriteLine("- Nơi thực thi: Trong bộ nhớ (In-Memory)");
            Console.WriteLine("=================================================\n");

            // Ví dụ với List<Product>
            List<Product> products = new()
            {
                new Product(1, "Laptop Dell XPS 13", "Điện tử", 25000000, 15),
                new Product(2, "Sách: Clean Code", "Sách", 250000, 50),
                new Product(3, "iPhone 15 Pro", "Điện tử", 30000000, 8),
                new Product(4, "Bàn phím cơ Keychron", "Điện tử", 2500000, 25)
            };
            
            var dienTu = products.Where(p => p.Category == "Điện tử").ToList();
            
            Console.WriteLine($"Sản phẩm điện tử: {string.Join(", ", dienTu.Select(p => p.Name))}");

            // Ví dụ với LINQ chaining
            var topExpensive = products
                .Where(p => p.Price > 1000000)
                .OrderByDescending(p => p.Price)
                .Take(2)
                .ToList();

            Console.WriteLine($"\nTop 2 sản phẩm đắt nhất (giá > 1tr):");
            foreach (var p in topExpensive)
            {
                Console.WriteLine($"  - {p.Name}: {p.Price:N0} VNĐ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 2. LINQ to Entities - Sử dụng SQL Server thật
        /// </summary>
        private static void DemoLinqToEntities()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("2. LINQ to Entities (Entity Framework Core)");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Truy vấn: IQueryable<T> (DbSet<Product>)");
            Console.WriteLine("- Nơi thực thi: CSDL SQL Server");
            Console.WriteLine("- Được 'dịch' thành SQL và chạy ở Database");
            Console.WriteLine("- (*) ĐÂY LÀ LÝ DO 'DEFERRED EXECUTION' QUAN TRỌNG!");
            Console.WriteLine("=================================================\n");

            // Kiểm tra và tạo database
            if (!DatabaseHelper.EnsureDatabaseCreated())
            {
                Console.WriteLine("\n⏭️  Bỏ qua demo LINQ to Entities (chưa có database)");
                Console.WriteLine("Xem code ví dụ bên dưới:\n");
                ShowEntityFrameworkExample();
                return;
            }

            try
            {
                using var db = new ProductDbContext();

                // Demo 1: Lấy sản phẩm điện tử
                Console.WriteLine("\n--- Demo 1: Lấy các sản phẩm Điện tử ---");
                Console.WriteLine("Code LINQ:");
                Console.WriteLine("  var electronics = db.Products");
                Console.WriteLine("      .Where(p => p.Category == \"Điện tử\")");
                Console.WriteLine("      .OrderByDescending(p => p.Price)");
                Console.WriteLine("      .Take(5);");
                Console.WriteLine("\n-> Truy vấn CHƯA CHẠY! Chỉ là 'kế hoạch' (IQueryable)\n");

                var electronics = db.Products
                    .Where(p => p.Category == "Điện tử")
                    .OrderByDescending(p => p.Price)
                    .Take(5);

                Console.WriteLine("Khi duyệt qua kết quả -> BÂY GIỜ MỚI CHẠY SQL!");
                Console.WriteLine("\nTop 5 sản phẩm Điện tử đắt nhất:");
                foreach (var p in electronics)
                {
                    Console.WriteLine($"  - {p.Name}: {p.Price:N0} VNĐ (Tồn: {p.Stock})");
                }

                Console.WriteLine("\n(!) SQL được EF Core tạo ra (tương tự):");
                Console.WriteLine("SELECT TOP 5 [Id], [Name], [Category], [Price], [Stock]");
                Console.WriteLine("FROM [Products]");
                Console.WriteLine("WHERE [Category] = N'Điện tử'");
                Console.WriteLine("ORDER BY [Price] DESC");
                Console.WriteLine("-> Chỉ lấy 5 bản ghi từ CSDL, KHÔNG kéo hết!\n");

                // Demo 2: Tính tổng giá trị tồn kho
                Console.WriteLine("--- Demo 2: Tính tổng giá trị tồn kho theo danh mục ---");
                var inventory = db.Products
                    .GroupBy(p => p.Category)
                    .Select(g => new
                    {
                        DanhMuc = g.Key,
                        TongSoLuong = g.Sum(p => p.Stock),
                        TongGiaTri = g.Sum(p => p.Price * p.Stock),
                        SoSanPham = g.Count()
                    })
                    .ToList();

                Console.WriteLine("\nGiá trị tồn kho theo danh mục:");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"  {item.DanhMuc}:");
                    Console.WriteLine($"    - Số sản phẩm: {item.SoSanPham}");
                    Console.WriteLine($"    - Tổng số lượng: {item.TongSoLuong}");
                    Console.WriteLine($"    - Tổng giá trị: {item.TongGiaTri:N0} VNĐ");
                }

                // Demo 3: Kiểm tra sản phẩm sắp hết hàng
                Console.WriteLine("\n--- Demo 3: Sản phẩm cần nhập thêm (tồn kho < 15) ---");
                var lowStock = db.Products
                    .Where(p => p.Stock < 15)
                    .OrderBy(p => p.Stock)
                    .Select(p => new { p.Name, p.Stock })
                    .ToList();

                if (lowStock.Any())
                {
                    Console.WriteLine("Sản phẩm cần nhập thêm:");
                    foreach (var item in lowStock)
                    {
                        Console.WriteLine($"  - {item.Name}: còn {item.Stock} sản phẩm");
                    }
                }
                else
                {
                    Console.WriteLine("Tất cả sản phẩm đều đủ hàng!");
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(!!) LỖI: {ex.Message}");
                Console.WriteLine("\nHiển thị code ví dụ thay thế:\n");
                ShowEntityFrameworkExample();
            }
        }

        /// <summary>
        /// Hiển thị code ví dụ Entity Framework (khi không có DB)
        /// </summary>
        private static void ShowEntityFrameworkExample()
        {
            Console.WriteLine("Code ví dụ LINQ to Entities:");
            Console.WriteLine("  using var db = new ProductDbContext();");
            Console.WriteLine("  var products = db.Products                    // IQueryable<Product>");
            Console.WriteLine("      .Where(p => p.Category == \"Điện tử\")    // Chưa chạy");
            Console.WriteLine("      .OrderByDescending(p => p.Price)          // Chưa chạy");
            Console.WriteLine("      .Take(5);                                 // Chưa chạy");
            Console.WriteLine("  ");
            Console.WriteLine("  foreach (var p in products)                   // <- BÂY GIỜ MỚI CHẠY!");
            Console.WriteLine("  {");
            Console.WriteLine("      // EF Core dịch toàn bộ thành 1 câu SQL:");
            Console.WriteLine("      // SELECT TOP 5 * FROM Products");
            Console.WriteLine("      // WHERE Category = 'Điện tử' ORDER BY Price DESC");
            Console.WriteLine("  }");

            Console.WriteLine("\n(!) Lợi ích: Không kéo toàn bộ dữ liệu lên bộ nhớ,");
            Console.WriteLine("   mà chỉ lấy đúng 5 bản ghi cần thiết từ CSDL!\n");
        }

        /// <summary>
        /// 3. LINQ to XML - Đọc file XML thật
        /// </summary>
        private static void DemoLinqToXml()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("3. LINQ to XML");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Truy vấn: XDocument, XElement");
            Console.WriteLine("- Nơi thực thi: Trong bộ nhớ");
            Console.WriteLine("- File: Data/products.xml");
            Console.WriteLine("=================================================\n");

            try
            {
                // Đọc file XML
                string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.xml");
                
                if (!File.Exists(xmlPath))
                {
                    Console.WriteLine($"(!!) File không tồn tại: {xmlPath}");
                    Console.WriteLine("Vui lòng đảm bảo file Data/products.xml có trong project\n");
                    return;
                }

                XDocument doc = XDocument.Load(xmlPath);
                Console.WriteLine($"(+) Đã đọc file XML: {xmlPath}\n");

                // Demo 1: Lấy sản phẩm giá cao (> 5 triệu)
                Console.WriteLine("--- Demo 1: Sản phẩm giá > 5.000.000 ---");
                var expensiveProducts = doc.Descendants("Product")
                    .Where(p => decimal.Parse(p.Element("Price")?.Value ?? "0") > 5000000)
                    .Select(p => new
                    {
                        Id = p.Attribute("Id")?.Value,
                        Name = p.Element("Name")?.Value,
                        Price = decimal.Parse(p.Element("Price")?.Value ?? "0"),
                        Category = p.Element("Category")?.Value
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList();

                Console.WriteLine($"Tìm thấy {expensiveProducts.Count} sản phẩm:");
                foreach (var p in expensiveProducts)
                {
                    Console.WriteLine($"  - [{p.Id}] {p.Name}: {p.Price:N0} VNĐ ({p.Category})");
                }

                // Demo 2: Thống kê theo danh mục
                Console.WriteLine("\n--- Demo 2: Thống kê theo danh mục ---");
                var categoryStats = doc.Descendants("Product")
                    .GroupBy(p => p.Element("Category")?.Value ?? "Unknown")
                    .Select(g => new
                    {
                        Category = g.Key,
                        Count = g.Count(),
                        TotalStock = g.Sum(p => int.Parse(p.Element("Stock")?.Value ?? "0")),
                        AvgPrice = g.Average(p => decimal.Parse(p.Element("Price")?.Value ?? "0"))
                    })
                    .ToList();

                foreach (var stat in categoryStats)
                {
                    Console.WriteLine($"  {stat.Category}:");
                    Console.WriteLine($"    - Số sản phẩm: {stat.Count}");
                    Console.WriteLine($"    - Tổng tồn kho: {stat.TotalStock}");
                    Console.WriteLine($"    - Giá TB: {stat.AvgPrice:N0} VNĐ");
                }

                Console.WriteLine("\n(!) LINQ to XML giúp truy vấn XML dễ dàng như List!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(!!) LỖI: {ex.Message}\n");
            }
        }

        /// <summary>
        /// 4. LINQ to JSON - Đọc file JSON thật
        /// </summary>
        private static void DemoLinqToJson()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("4. LINQ to JSON");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Sử dụng System.Text.Json");
            Console.WriteLine("- Truy vấn cấu trúc JSON");
            Console.WriteLine("- Nơi thực thi: Trong bộ nhớ");
            Console.WriteLine("- File: Data/products.json");
            Console.WriteLine("=================================================\n");

            try
            {
                // Đọc file JSON
                string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");
                
                if (!File.Exists(jsonPath))
                {
                    Console.WriteLine($"(!!) File không tồn tại: {jsonPath}");
                    Console.WriteLine("Vui lòng đảm bảo file Data/products.json có trong project\n");
                    return;
                }

                string jsonContent = File.ReadAllText(jsonPath);
                Console.WriteLine($"(+) Đã đọc file JSON: {jsonPath}\n");

                // Parse JSON thành object
                using JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
                var productsArray = jsonDoc.RootElement.GetProperty("products");

                // Demo 1: Sản phẩm sách
                Console.WriteLine("--- Demo 1: Danh sách sách ---");
                var books = productsArray.EnumerateArray()
                    .Where(p => p.GetProperty("category").GetString() == "Sách")
                    .Select(p => new
                    {
                        Name = p.GetProperty("name").GetString(),
                        Price = p.GetProperty("price").GetDecimal(),
                        Stock = p.GetProperty("stock").GetInt32()
                    })
                    .ToList();

                Console.WriteLine($"Tìm thấy {books.Count} cuốn sách:");
                foreach (var book in books)
                {
                    Console.WriteLine($"  - {book.Name}: {book.Price:N0} VNĐ (Còn {book.Stock})");
                }

                // Demo 2: Tổng giá trị tồn kho
                Console.WriteLine("\n--- Demo 2: Tổng giá trị tồn kho ---");
                var totalValue = productsArray.EnumerateArray()
                    .Sum(p => p.GetProperty("price").GetDecimal() * p.GetProperty("stock").GetInt32());

                Console.WriteLine($"Tổng giá trị tồn kho: {totalValue:N0} VNĐ");

                // Demo 3: Sản phẩm tồn kho thấp
                Console.WriteLine("\n--- Demo 3: Sản phẩm tồn kho < 15 ---");
                var lowStock = productsArray.EnumerateArray()
                    .Where(p => p.GetProperty("stock").GetInt32() < 15)
                    .Select(p => new
                    {
                        Name = p.GetProperty("name").GetString(),
                        Stock = p.GetProperty("stock").GetInt32()
                    })
                    .OrderBy(p => p.Stock)
                    .ToList();

                if (lowStock.Any())
                {
                    Console.WriteLine("Cần nhập thêm:");
                    foreach (var item in lowStock)
                    {
                        Console.WriteLine($"  - {item.Name}: còn {item.Stock}");
                    }
                }

                Console.WriteLine("\n(!) LINQ giúp truy vấn JSON như truy vấn Collection!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(!!) LỖI: {ex.Message}\n");
            }
        }

        /// <summary>
        /// 5. PLINQ - Parallel LINQ với Product Management
        /// </summary>
        private static void DemoPLinq()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("5. PLINQ (Parallel LINQ)");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Mở rộng của LINQ to Objects");
            Console.WriteLine("- Chạy SONG SONG (parallel) để tăng tốc xử lý");
            Console.WriteLine("- Chỉ cần thêm .AsParallel() vào truy vấn!");
            Console.WriteLine("=================================================\n");

            // Tạo danh sách sản phẩm lớn để demo
            Console.WriteLine("Tạo 100.000 sản phẩm giả lập để test hiệu năng...\n");
            var largeProductList = Enumerable.Range(1, 100000)
                .Select(i => new Product
                {
                    Id = i,
                    Name = $"Sản phẩm {i}",
                    Category = i % 3 == 0 ? "Điện tử" : (i % 3 == 1 ? "Sách" : "Gia dụng"),
                    Price = 100000 + (i * 1000),
                    Stock = i % 50
                })
                .ToList();

            // Demo: Tìm sản phẩm giá cao và tồn kho thấp
            Console.WriteLine("Tìm sản phẩm: Giá > 50 triệu VÀ Tồn kho < 10\n");

            // LINQ thường
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var resultLinq = largeProductList
                .Where(p => p.Price > 50000000 && p.Stock < 10)
                .OrderByDescending(p => p.Price)
                .Take(100)
                .ToList();
            sw.Stop();
            var timeLinq = sw.ElapsedMilliseconds;

            // PLINQ (song song)
            sw.Restart();
            var resultPLinq = largeProductList
                .AsParallel()
                .Where(p => p.Price > 50000000 && p.Stock < 10)
                .OrderByDescending(p => p.Price)
                .Take(100)
                .ToList();
            sw.Stop();
            var timePLinq = sw.ElapsedMilliseconds;

            Console.WriteLine($"Kết quả:");
            Console.WriteLine($"  LINQ thường:  {resultLinq.Count} sản phẩm ({timeLinq} ms)");
            Console.WriteLine($"  PLINQ:        {resultPLinq.Count} sản phẩm ({timePLinq} ms)");
            if (timePLinq > 0)
            {
                Console.WriteLine($"  Tăng tốc:     {(double)timeLinq / timePLinq:F2}x");
            }
            
            Console.WriteLine("\n(!) PLINQ tự động phân chia công việc cho nhiều CPU cores!");
            Console.WriteLine($"(!) Số cores khả dụng: {Environment.ProcessorCount}\n");
        }

        /// <summary>
        /// Hiển thị các Provider khác
        /// </summary>
        private static void ShowOtherProviders()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("CÁC LOẠI LINQ KHÁC (NÂNG CAO)");
            Console.WriteLine("=================================================");
            Console.WriteLine("- LINQ to SQL (SQL Server - công nghệ cũ)");
            Console.WriteLine("- LINQ to DataSet (DataTable, DataSet - ADO.NET)");
            Console.WriteLine("- LINQ to CSV / Excel (thư viện bên thứ 3)");
            Console.WriteLine("- LINQ to REST / OData (Web API)");
            Console.WriteLine("- LINQ to NoSQL (MongoDB, RavenDB, ...)");
            Console.WriteLine("- LINQ to SharePoint / Twitter / Google ...");
            Console.WriteLine("- Custom LINQ Provider (tự viết provider riêng)");
            Console.WriteLine("=================================================");
        }
    }
}

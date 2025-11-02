namespace Revision.LINQ.Data
{
    /// <summary>
    /// Helper class để kiểm tra và hướng dẫn setup database
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Hiển thị hướng dẫn setup database
        /// </summary>
        public static void ShowSetupInstructions()
        {
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine("     HƯỚNG DẪN CÀI ĐẶT DATABASE CHO LINQ DEMO");
            Console.WriteLine("=================================================================");
            Console.WriteLine();
            Console.WriteLine("Để chạy demo LINQ to Entities, bạn cần:");
            Console.WriteLine();
            Console.WriteLine("[1] Cài đặt EF Core Tools (nếu chưa có):");
            Console.WriteLine("    dotnet tool install --global dotnet-ef");
            Console.WriteLine();
            Console.WriteLine("[2] Tạo Migration:");
            Console.WriteLine("    dotnet ef migrations add InitialCreate");
            Console.WriteLine();
            Console.WriteLine("[3] Tạo Database:");
            Console.WriteLine("    dotnet ef database update");
            Console.WriteLine();
            Console.WriteLine("HOẶC chạy script PowerShell (từ thư mục gốc):");
            Console.WriteLine("    .\\setup-database.ps1");
            Console.WriteLine();
            Console.WriteLine("Connection String hiện tại:");
            Console.WriteLine("    Server=.\\FINTERN;Database=ProductMngtLINQ;");
            Console.WriteLine("    Trusted_Connection=True;TrustServerCertificate=True");
            Console.WriteLine();
            Console.WriteLine("LƯU Ý:");
            Console.WriteLine("    - Đảm bảo SQL Server đang chạy");
            Console.WriteLine("    - Kiểm tra tên server instance (.\\FINTERN)");
            Console.WriteLine("    - Windows Authentication phải được bật");
            Console.WriteLine();
        }

        /// <summary>
        /// Kiểm tra và tạo database nếu cần
        /// </summary>
        public static bool EnsureDatabaseCreated()
        {
            try
            {
                using var db = new ProductDbContext();
                
                // Kiểm tra kết nối
                if (!db.Database.CanConnect())
                {
                    Console.WriteLine("[LỖI] Không thể kết nối đến SQL Server!");
                    ShowSetupInstructions();
                    return false;
                }

                // Tạo database nếu chưa tồn tại
                bool created = db.Database.EnsureCreated();
                
                if (created)
                {
                    Console.WriteLine("[OK] Database 'ProductMngtLINQ' đã được tạo thành công!");
                    Console.WriteLine($"[OK] Đã seed {db.Products.Count()} sản phẩm mẫu.");
                }
                else
                {
                    Console.WriteLine($"[OK] Kết nối database thành công! ({db.Products.Count()} sản phẩm)");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LỖI] {ex.Message}");
                ShowSetupInstructions();
                return false;
            }
        }

        /// <summary>
        /// Hiển thị thông tin database
        /// </summary>
        public static void ShowDatabaseInfo()
        {
            try
            {
                using var db = new ProductDbContext();
                
                Console.WriteLine();
                Console.WriteLine("=================================================================");
                Console.WriteLine("                   THÔNG TIN DATABASE");
                Console.WriteLine("=================================================================");
                Console.WriteLine();
                Console.WriteLine($"Database: ProductMngtLINQ");
                Console.WriteLine($"Tổng số sản phẩm: {db.Products.Count()}");
                
                var categories = db.Products.GroupBy(p => p.Category)
                    .Select(g => new { Category = g.Key, Count = g.Count() })
                    .ToList();
                
                Console.WriteLine($"\nDanh mục:");
                foreach (var cat in categories)
                {
                    Console.WriteLine($"  - {cat.Category}: {cat.Count} sản phẩm");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LỖI] Không thể lấy thông tin database: {ex.Message}");
            }
        }
    }
}

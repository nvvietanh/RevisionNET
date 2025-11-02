using Revision.LINQ.Models;

namespace Revision.LINQ
{
    /// <summary>
    /// PHẦN 2: HAI CÁCH VIẾT LINQ VÀ DEFERRED EXECUTION
    /// Người thuyết trình 2
    /// </summary>
    public class Part2_Syntax
    {
        public static void Demo()
        {
            Console.WriteLine("=== PHẦN 2: CÚ PHÁP LINQ VÀ KHÁI NIỆM CỐT LÕI ===\n");

            List<Product> products = new List<Product>
            {
                new Product(1, "Laptop Dell XPS 13", "Điện tử", 25000000, 15),
                new Product(2, "Sách: Clean Code", "Sách", 250000, 50),
                new Product(3, "iPhone 15 Pro", "Điện tử", 30000000, 8),
                new Product(4, "Bàn phím cơ Keychron", "Điện tử", 2500000, 25),
                new Product(5, "Sách: Design Patterns", "Sách", 320000, 30)
            };

            Console.WriteLine("Danh sách sản phẩm (5 sản phẩm):");
            foreach (var p in products)
            {
                Console.WriteLine($"  - {p.Name}: {p.Price:C}");
            }
            Console.WriteLine();

            // === 1. HAI CÁCH VIẾT LINQ ===
            Console.WriteLine("=================================================");
            Console.WriteLine("1. HAI CÁCH VIẾT LINQ");
            Console.WriteLine("=================================================\n");

            // a. Query Syntax (Cú pháp truy vấn - giống SQL)
            Console.WriteLine("a. QUERY SYNTAX (giống SQL):");
            var dienTuQuery = from p in products
                              where p.Category == "Điện tử"
                              select p;

            Console.WriteLine("Code: var dienTuQuery = from p in products");
            Console.WriteLine("                        where p.Category == \"Điện tử\"");
            Console.WriteLine("                        select p;");
            Console.WriteLine("Kết quả:");
            foreach (var p in dienTuQuery)
            {
                Console.WriteLine($"  - {p.Name}");
            }
            Console.WriteLine();

            // b. Method Syntax (Cú pháp phương thức - dùng Lambda)
            Console.WriteLine("b. METHOD SYNTAX (dùng Lambda Expression):");
            Console.WriteLine("Ví dụ đơn giản:");
            var dienTuMethod = products.Where(p => p.Category == "Điện tử");

            Console.WriteLine("Code: var dienTuMethod = products.Where(p => p.Category == \"Điện tử\");");
            Console.WriteLine("Kết quả:");
            foreach (var p in dienTuMethod)
            {
                Console.WriteLine($"  - {p.Name}");
            }
            Console.WriteLine();

            // c. Method Syntax với Chain dài - Điểm mạnh của Method Syntax
            Console.WriteLine("c. METHOD SYNTAX với CHAIN (nối chuỗi) - Sức mạnh thực sự!");
            Console.WriteLine("Yêu cầu: Lấy Tên và Giá (đã giảm 10%) của các sản phẩm 'Điện tử'");
            Console.WriteLine("         có giá trên 5 triệu, sắp xếp theo giá giảm dần,");
            Console.WriteLine("         chỉ lấy 3 sản phẩm đầu tiên\n");

            var ketQua = products
                .Where(p => p.Category == "Điện tử" && p.Price > 5000000)
                .OrderByDescending(p => p.Price)
                .Take(3)
                .Select(p => new
                {
                    TenSanPham = p.Name,
                    GiaGoc = p.Price,
                    GiaKhuyenMai = p.Price * 0.9m
                });

            Console.WriteLine("Code:");
            Console.WriteLine("var ketQua = products");
            Console.WriteLine("    .Where(p => p.Category == \"Điện tử\" && p.Price > 5000000)");
            Console.WriteLine("    .OrderByDescending(p => p.Price)");
            Console.WriteLine("    .Take(3)");
            Console.WriteLine("    .Select(p => new");
            Console.WriteLine("    {");
            Console.WriteLine("        TenSanPham = p.Name,");
            Console.WriteLine("        GiaGoc = p.Price,");
            Console.WriteLine("        GiaKhuyenMai = p.Price * 0.9");
            Console.WriteLine("    });");
            Console.WriteLine();
            Console.WriteLine("Kết quả:");
            foreach (var item in ketQua)
            {
                Console.WriteLine($"  - {item.TenSanPham}");
                Console.WriteLine($"    Giá gốc: {item.GiaGoc:C}");
                Console.WriteLine($"    Giá khuyến mãi (giảm 10%): {item.GiaKhuyenMai:C}");
            }
            Console.WriteLine();

            Console.WriteLine(">> LƯU Ý: C# Compiler sẽ dịch Query Syntax -> Method Syntax khi biên dịch!");
            Console.WriteLine(">> Method Syntax linh hoạt hơn, có thể 'chain' (nối chuỗi) nhiều toán tử");
            Console.WriteLine(">> Code rõ ràng, mạch lạc, đúng nghiệp vụ!\n");

            // === 2. DEFERRED EXECUTION (THỰC THI TRỄ) ===
            Console.WriteLine("=================================================");
            Console.WriteLine("2. DEFERRED EXECUTION (THỰC THI TRỄ) - QUAN TRỌNG NHẤT!");
            Console.WriteLine("=================================================\n");

            Console.WriteLine("Tạo truy vấn LINQ:");
            var query = products.Where(p =>
            {
                Console.WriteLine($"  -> Đang kiểm tra sản phẩm: {p.Name}...");
                return p.Price > 5000000;
            });

            Console.WriteLine("(+) Đã tạo xong truy vấn, nhưng CHƯA THẤY gì được in ra!");
            Console.WriteLine("-> Vì truy vấn CHƯA CHẠY, nó chỉ là 'kế hoạch'\n");

            Console.WriteLine("Bây giờ duyệt qua kết quả bằng foreach:");
            foreach (var item in query)
            {
                Console.WriteLine($"  (+) Kết quả: {item.Name} - {item.Price:C}");
            }
            Console.WriteLine();

            Console.WriteLine("Hoặc gọi .ToList() để thực thi ngay:");
            var queryList = products.Where(p =>
            {
                Console.WriteLine($"  -> Kiểm tra {p.Name} trong .ToList()");
                return p.Category == "Sách";
            }).ToList();
            Console.WriteLine($"(+) Đã thực thi! Kết quả: {queryList.Count} sản phẩm Sách");
            Console.WriteLine();

            Console.WriteLine(">> QUAN TRỌNG: Deferred Execution cho phép:");
            Console.WriteLine("   1. Gom nhiều điều kiện (Where, OrderBy, Select...) thành MỘT câu SQL duy nhất");
            Console.WriteLine("   2. Tối ưu hiệu năng với CSDL (không kéo toàn bộ dữ liệu lên bộ nhớ)");
            Console.WriteLine("   3. Truy vấn chỉ chạy khi THỰC SỰ cần kết quả\n");
        }
    }
}
// 

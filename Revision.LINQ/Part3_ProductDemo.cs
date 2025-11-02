using Revision.LINQ.Models;

namespace Revision.LINQ
{
    /// <summary>
    /// PHẦN 3: DEMO THỰC TÉ VỚI SẢN PHẨM (CHAINING)
    /// Người thuyết trình 3
    /// </summary>
    public class Part3_ProductDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== DEMO THỰC TẾ: QUẢN LÝ SẢN PHẨM ===\n");

            // Tạo danh sách sản phẩm
            List<Product> products = new List<Product>
            {
                new Product(1, "Laptop Dell XPS 13", "Điện tử", 25000000, 15),
                new Product(2, "Sách: Clean Code", "Sách", 250000, 50),
                new Product(3, "iPhone 15 Pro", "Điện tử", 30000000, 8),
                new Product(4, "Sách: Design Patterns", "Sách", 320000, 30),
                new Product(5, "Bàn phím cơ Keychron", "Điện tử", 2500000, 25),
                new Product(6, "Sách: C# in Depth", "Sách", 450000, 20),
                new Product(7, "Tai nghe Sony WH-1000XM5", "Điện tử", 8000000, 12),
                new Product(8, "Sách: The Pragmatic Programmer", "Sách", 380000, 18),
                new Product(9, "Chuột Logitech MX Master 3", "Điện tử", 2200000, 40),
                new Product(10, "Sách: Refactoring", "Sách", 420000, 15)
            };

            Console.WriteLine("DANH SÁCH SẢN PHẨM:");
            foreach (var p in products.Take(5))
            {
                Console.WriteLine($"  {p}");
            }
            Console.WriteLine("  ... (tổng {0} sản phẩm)\n", products.Count);

            // === YÊU CẦU 1: Tìm sản phẩm thuộc "Điện tử" ===
            Console.WriteLine("=================================================");
            Console.WriteLine("YÊU CẦU 1: Tìm sản phẩm thuộc 'Điện tử'");
            Console.WriteLine("=================================================");
            var dienTu = products.Where(p => p.Category == "Điện tử");
            
            Console.WriteLine("Code: products.Where(p => p.Category == \"Điện tử\")");
            Console.WriteLine($"Kết quả ({dienTu.Count()} sản phẩm):");
            foreach (var p in dienTu)
            {
                Console.WriteLine($"  - {p.Name}: {p.Price:C}");
            }
            Console.WriteLine();

            // === YÊU CẦU 2: Truy vấn phức tạp với CHAINING ===
            Console.WriteLine("=================================================");
            Console.WriteLine("YÊU CẦU 2: Sách có giá > 100k, lấy tên + giá giảm 10%, sắp xếp giảm dần");
            Console.WriteLine("=================================================");
            Console.WriteLine("\nCode (Method Chaining):");
            Console.WriteLine("var ketQua = products");
            Console.WriteLine("    .Where(p => p.Category == \"Sách\" && p.Price > 100000)");
            Console.WriteLine("    .OrderByDescending(p => p.Price)");
            Console.WriteLine("    .Select(p => new");
            Console.WriteLine("    {");
            Console.WriteLine("        TenSanPham = p.Name,");
            Console.WriteLine("        GiaKhuyenMai = p.Price * 0.9m");
            Console.WriteLine("    });\n");

            var ketQua = products
                .Where(p => p.Category == "Sách" && p.Price > 100000)
                .OrderByDescending(p => p.Price)
                .Select(p => new
                {
                    TenSanPham = p.Name,
                    GiaKhuyenMai = p.Price * 0.9m
                });

            Console.WriteLine("Kết quả:");
            foreach (var item in ketQua)
            {
                Console.WriteLine($"  - {item.TenSanPham}");
                Console.WriteLine($"    Giá KM: {item.GiaKhuyenMai:C} (giảm 10%)");
            }
            Console.WriteLine();

            Console.WriteLine(">> NHẬN XÉT:");
            Console.WriteLine("   + Code RÕ RÀNG, MẠCH LẠC, thể hiện đúng nghiệp vụ");
            Console.WriteLine("   + Dễ dàng thêm/bớt điều kiện (ví dụ: thêm .Where(p => p.Stock > 10))");
            Console.WriteLine("   + Method Chaining giúp đọc code từ trên xuống như đọc câu chuyện\n");

            // === YÊU CẦU 3: Thống kê ===
            Console.WriteLine("=================================================");
            Console.WriteLine("YÊU CẦU 3: Thống kê");
            Console.WriteLine("=================================================");
            var tongSanPham = products.Count();
            var tongGiaTriKho = products.Sum(p => p.Price * p.Stock);
            var giaTrungBinh = products.Average(p => p.Price);
            var sanPhamDatNhat = products.OrderByDescending(p => p.Price).First();
            var coSanPhamHetHang = products.Any(p => p.Stock == 0);

            Console.WriteLine($"Tổng số sản phẩm: {tongSanPham}");
            Console.WriteLine($"Tổng giá trị kho: {tongGiaTriKho:C}");
            Console.WriteLine($"Giá trung bình: {giaTrungBinh:C}");
            Console.WriteLine($"Sản phẩm đắt nhất: {sanPhamDatNhat.Name} ({sanPhamDatNhat.Price:C})");
            Console.WriteLine($"Có sản phẩm hết hàng: {(coSanPhamHetHang ? "CÓ" : "KHÔNG")}");
            Console.WriteLine();
        }
    }
}

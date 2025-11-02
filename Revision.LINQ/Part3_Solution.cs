using Revision.LINQ.Models;

namespace Revision.LINQ
{
    public class Part3_Solution
    {
        // Dữ liệu dùng chung cho tất cả các demo
        private static List<Product> GetDanhSachSanPham()
        {
            return new List<Product>
            {
                new Product(1, "Laptop Dell XPS 13", "Dien tu", 25000000, 15),
                new Product(2, "Sach: Clean Code", "Sach", 250000, 50),
                new Product(3, "iPhone 15 Pro", "Dien tu", 30000000, 8),
                new Product(4, "Sach: Design Patterns", "Sach", 320000, 30),
                new Product(5, "Ban phim co Keychron", "Dien tu", 2500000, 25),
                new Product(6, "Sach: C# in Depth", "Sach", 450000, 20),
                new Product(7, "Tai nghe Sony WH-1000XM5", "Dien tu", 8000000, 12),
                new Product(8, "Chuot Logitech MX Master 3", "Dien tu", 2200000, 40)
            };
        }

        public static void Demo()
        {
            Console.WriteLine("===================================================================");
            Console.WriteLine("           SO SÁNH: TRADITIONAL vs LINQ");
            Console.WriteLine("===================================================================\n");
            
            BaiToan1_TimSanPhamTheoDanhMuc();
            Console.WriteLine("\n" + new string('=', 80) + "\n");
            
            BaiToan2_Top5SanPhamGiaCaoNhat();
            Console.WriteLine("\n" + new string('=', 80) + "\n");
            
            BaiToan3_KiemTraSanPhamHetHang();
            Console.WriteLine("\n" + new string('=', 80) + "\n");
            
            TongKetSoSanh();
        }

        /// <summary>
        /// BÀI TOÁN 1: Tìm sản phẩm theo danh mục
        /// SO SÁNH:
        /// - Lines of Code: Traditional (7 dòng) vs LINQ (1 dòng)
        /// - Time Complexity: O(n) vs O(n) - TƯƠNG ĐƯƠNG
        /// - Space Complexity: Traditional tạo List mới, LINQ dùng deferred execution
        /// - Readability: LINQ rõ ràng hơn nhiều
        /// </summary>
        private static void BaiToan1_TimSanPhamTheoDanhMuc()
        {
            var danhSachSanPham = GetDanhSachSanPham();
            
            Console.WriteLine("===================================================================");
            Console.WriteLine("  BÀI TOÁN 1: Tìm sản phẩm danh mục 'Dien tu'");
            Console.WriteLine("===================================================================");
            
            // ========== CÁCH TRUYỀN THỐNG (TRADITIONAL) ==========
            Console.WriteLine("\n--- CÁCH TRUYỀN THỐNG ---");
            Console.WriteLine("  - Lines of Code: 7 dòng");
            Console.WriteLine("  - Time Complexity: O(n)");
            Console.WriteLine("  - Space Complexity: O(k) - tạo List mới với k phần tử tìm được");
            Console.WriteLine("  - Nhược điểm: Dài dòng, phải quản lý vòng lặp và điều kiện\n");
            
            // Code truyền thống
            List<Product> sanPhamDienTu_Traditional = new List<Product>();
            foreach (var sp in danhSachSanPham)
            {
                if (sp.Category == "Dien tu")
                {
                    sanPhamDienTu_Traditional.Add(sp);
                }
            }
            
            Console.WriteLine("Kết quả TRADITIONAL:");
            foreach (var sp in sanPhamDienTu_Traditional)
            {
                Console.WriteLine($"  - {sp.Name}");
            }
            
            // ========== CÁCH DÙNG LINQ ==========
            Console.WriteLine("\n--- CÁCH DÙNG LINQ ---");
            Console.WriteLine("  - Lines of Code: 1 dòng");
            Console.WriteLine("  - Time Complexity: O(n)");
            Console.WriteLine("  - Space Complexity: O(1) - deferred execution, chỉ tạo khi cần");
            Console.WriteLine("  - Ưu điểm: Ngắn gọn, declarative, dễ đọc, dễ maintain\n");
            
            // Code LINQ
            var sanPhamDienTu_LINQ = danhSachSanPham.Where(sp => sp.Category == "Dien tu");
            
            Console.WriteLine("Code: var sanPhamDienTu = danhSachSanPham.Where(sp => sp.Category == \"Dien tu\");");
            Console.WriteLine($"\nKết quả LINQ ({sanPhamDienTu_LINQ.Count()} sản phẩm):");
            foreach (var sp in sanPhamDienTu_LINQ)
            {
                Console.WriteLine($"  - {sp.Name}");
            }
            
            Console.WriteLine("\n>> SO SÁNH: LINQ giảm 85% code, dễ đọc hơn, tiết kiệm bộ nhớ!");
        }

        /// <summary>
        /// BÀI TOÁN 2: Top 5 sản phẩm có giá cao nhất
        /// SO SÁNH:
        /// - Lines of Code: Traditional (20+ dòng) vs LINQ (3 dòng)
        /// - Time Complexity: Traditional O(n²) - bubble sort vs LINQ O(n log n) - QuickSort
        /// - LINQ NHANH HƠN NHIỀU với dữ liệu lớn!
        /// </summary>
        private static void BaiToan2_Top5SanPhamGiaCaoNhat()
        {
            var danhSachSanPham = GetDanhSachSanPham();
            
            Console.WriteLine("===================================================================");
            Console.WriteLine("  BÀI TOÁN 2: Top 5 sản phẩm có giá cao nhất");
            Console.WriteLine("===================================================================");
            
            // ========== CÁCH TRUYỀN THỐNG (TRADITIONAL) ==========
            Console.WriteLine("\n--- CÁCH TRUYỀN THỐNG ---");
            Console.WriteLine("  - Lines of Code: 20+ dòng (bubble sort + vòng lặp)");
            Console.WriteLine("  - Time Complexity: O(n^2) - CHẬM với dữ liệu lớn!");
            Console.WriteLine("  - Space Complexity: O(n) - phải copy toàn bộ list");
            Console.WriteLine("  - Nhược điểm: Dài dòng, dễ lỗi, hiệu năng kém\n");
            
            // Code truyền thống
            List<Product> danhSachCopy = new List<Product>(danhSachSanPham);
            
            // Bubble sort - O(n²)
            for (int i = 0; i < danhSachCopy.Count - 1; i++)
            {
                for (int j = i + 1; j < danhSachCopy.Count; j++)
                {
                    if (danhSachCopy[i].Price < danhSachCopy[j].Price)
                    {
                        var temp = danhSachCopy[i];
                        danhSachCopy[i] = danhSachCopy[j];
                        danhSachCopy[j] = temp;
                    }
                }
            }
            
            // Lấy top 5
            List<string> top5Ten_Traditional = new List<string>();
            int count = 0;
            foreach (var sp in danhSachCopy)
            {
                if (count >= 5) break;
                top5Ten_Traditional.Add(sp.Name);
                count++;
            }
            
            Console.WriteLine("Kết quả TRADITIONAL (Top 5):");
            foreach (var ten in top5Ten_Traditional)
            {
                Console.WriteLine($"  - {ten}");
            }
            
            // ========== CÁCH DÙNG LINQ ==========
            Console.WriteLine("\n--- CÁCH DÙNG LINQ ---");
            Console.WriteLine("  - Lines of Code: 3 dòng (method chaining)");
            Console.WriteLine("  - Time Complexity: O(n log n) - NHANH hơn nhiều!");
            Console.WriteLine("  - Space Complexity: O(k) - chỉ tạo collection cho kết quả");
            Console.WriteLine("  - Ưu điểm: Ngắn gọn, hiệu năng tốt, readable, chainable\n");
            
            // Code LINQ
            var top5Ten_LINQ = danhSachSanPham
                .OrderByDescending(sp => sp.Price)
                .Take(5)
                .Select(sp => sp.Name);
            
            Console.WriteLine("Code: var top5Ten = danhSachSanPham");
            Console.WriteLine("                      .OrderByDescending(sp => sp.Price)");
            Console.WriteLine("                      .Take(5)");
            Console.WriteLine("                      .Select(sp => sp.Name);");
            Console.WriteLine("\nKết quả LINQ (Top 5):");
            foreach (var ten in top5Ten_LINQ)
            {
                Console.WriteLine($"  - {ten}");
            }
            
            Console.WriteLine("\n>> SO SÁNH: LINQ giảm 85% code, NHANH hơn (O(n log n) vs O(n^2))!");
        }

        /// <summary>
        /// BÀI TOÁN 3: Kiểm tra có sản phẩm hết hàng
        /// SO SÁNH:
        /// - Lines of Code: Traditional (6 dòng) vs LINQ (1 dòng)
        /// - Time Complexity: Cả hai O(n) nhưng LINQ có short-circuit evaluation
        /// - LINQ tối ưu hơn vì dừng ngay khi tìm thấy
        /// </summary>
        private static void BaiToan3_KiemTraSanPhamHetHang()
        {
            var danhSachSanPham = GetDanhSachSanPham();
            
            Console.WriteLine("===================================================================");
            Console.WriteLine("  BÀI TOÁN 3: Kiểm tra có sản phẩm hết hàng");
            Console.WriteLine("===================================================================");
            
            // ========== CÁCH TRUYỀN THỐNG (TRADITIONAL) ==========
            Console.WriteLine("\n--- CÁCH TRUYỀN THỐNG ---");
            Console.WriteLine("  - Lines of Code: 6 dòng (vòng lặp + biến cờ)");
            Console.WriteLine("  - Time Complexity: O(n) - worst case duyệt hết");
            Console.WriteLine("  - Space Complexity: O(1) - chỉ dùng 1 biến boolean");
            Console.WriteLine("  - Nhược điểm: Phải quản lý biến cờ, dễ quên break\n");
            
            // Code truyền thống
            bool coSanPhamHetHang_Traditional = false;
            foreach (var sp in danhSachSanPham)
            {
                if (sp.Stock == 0)
                {
                    coSanPhamHetHang_Traditional = true;
                    break; // Quan trọng! Dễ quên
                }
            }
            
            Console.WriteLine($"Kết quả TRADITIONAL: {(coSanPhamHetHang_Traditional ? "CÓ" : "KHÔNG")}");
            
            // ========== CÁCH DÙNG LINQ ==========
            Console.WriteLine("\n--- CÁCH DÙNG LINQ ---");
            Console.WriteLine("  - Lines of Code: 1 dòng");
            Console.WriteLine("  - Time Complexity: O(n) - nhưng có short-circuit tự động");
            Console.WriteLine("  - Space Complexity: O(1)");
            Console.WriteLine("  - Ưu điểm: Tự động dừng khi tìm thấy, không cần quản lý cờ\n");
            
            // Code LINQ
            var coSanPhamHetHang_LINQ = danhSachSanPham.Any(sp => sp.Stock == 0);
            
            Console.WriteLine("Code: var coSanPhamHetHang = danhSachSanPham.Any(sp => sp.Stock == 0);");
            Console.WriteLine($"\nKết quả LINQ: {(coSanPhamHetHang_LINQ ? "CÓ" : "KHÔNG")}");
            
            Console.WriteLine("\n>> SO SÁNH: LINQ giảm 83% code, tự động short-circuit, an toàn hơn!");
        }

        /// <summary>
        /// Tổng kết so sánh giữa Traditional và LINQ
        /// </summary>
        private static void TongKetSoSanh()
        {
            Console.WriteLine("===================================================================");
            Console.WriteLine("           TỔNG KẾT SO SÁNH: TRADITIONAL vs LINQ");
            Console.WriteLine("===================================================================\n");
            
            Console.WriteLine("Tiêu chí                    Traditional      LINQ            Người thắng");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Lines of Code               30-40 dòng       10-15 dòng      LINQ");
            Console.WriteLine("Readability                 Thấp            Cao             LINQ");
            Console.WriteLine("Maintainability             Khó             Dễ              LINQ");
            Console.WriteLine("Time Complexity (Sort)      O(n^2)          O(n log n)      LINQ");
            Console.WriteLine("Space Complexity            Nhiều biến      Deferred exec   LINQ");
            Console.WriteLine("Error-prone                 Cao             Thấp            LINQ");
            Console.WriteLine("Type Safety                 Thấp            Cao             LINQ");
            Console.WriteLine("Performance (small data)    Hơi nhanh       Tương đương     Ngang");
            Console.WriteLine("Performance (large data)    Chậm            Nhanh           LINQ");
            Console.WriteLine("Reusability                 Thấp            Cao             LINQ");
            Console.WriteLine("-----------------------------------------------------------------------\n");
            
            Console.WriteLine("===================================================================");
            Console.WriteLine("  KẾT LUẬN");
            Console.WriteLine("===================================================================");
            Console.WriteLine("+ LINQ giảm 70-85% số dòng code");
            Console.WriteLine("+ Dễ đọc, dễ hiểu, dễ bảo trì hơn nhiều");
            Console.WriteLine("+ Hiệu năng tốt hơn với dữ liệu lớn (đặc biệt sorting)");
            Console.WriteLine("+ An toàn kiểu, ít lỗi hơn");
            Console.WriteLine("+ Declarative: Tập trung vào MUỐN GÌ (WHAT) thay vì LÀM THẾ NÀO (HOW)");
            Console.WriteLine("===================================================================\n");
        }
    }
}

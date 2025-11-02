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
            
            BaiToan4_PhanTichPhucTap_ThongKeTheoDanhMuc();
            Console.WriteLine("\n" + new string('=', 80) + "\n");
            
            BaiToan5_ThongKeNangCao_TongGiaTriTonKho();
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
        /// BÀI TOÁN 4: Phân tích phức tạp - Thống kê theo danh mục
        /// SO SÁNH:
        /// - Lines of Code: Traditional (50+ dòng) vs LINQ (5 dòng với 6 operators chained)
        /// - Complexity: Traditional phải quản lý Dictionary, List, nhiều vòng lặp
        /// - LINQ: GroupBy -> Where -> SelectMany -> OrderByDescending -> Take -> Select
        /// - Đây là ví dụ điển hình của sức mạnh "method chaining" của LINQ!
        /// </summary>
        private static void BaiToan4_PhanTichPhucTap_ThongKeTheoDanhMuc()
        {
            var danhSachSanPham = GetDanhSachSanPham();
            
            Console.WriteLine("===================================================================");
            Console.WriteLine("  BÀI TOÁN 4: Thống kê theo danh mục");
            Console.WriteLine("===================================================================");
            Console.WriteLine("Yêu cầu: Tìm các danh mục có > 2 sản phẩm, lấy top 3 sản phẩm");
            Console.WriteLine("         giá cao nhất của mỗi danh mục, hiển thị tên và giá");
            Console.WriteLine("         khuyến mãi (giảm 10%)\n");
            
            // ========== CÁCH TRUYỀN THỐNG (TRADITIONAL) ==========
            Console.WriteLine("--- CÁCH TRUYỀN THỐNG ---");
            Console.WriteLine("  - Lines of Code: 50+ dòng (Dictionary + nhiều vòng lặp)");
            Console.WriteLine("  - Time Complexity: O(n × k × log k) - k là số SP/danh mục");
            Console.WriteLine("  - Space Complexity: O(n) - nhiều cấu trúc dữ liệu tạm");
            Console.WriteLine("  - Nhược điểm: Cực kỳ dài dòng, khó đọc, dễ lỗi\n");
            
            // Code truyền thống (rút gọn để demo)
            Dictionary<string, List<Product>> nhomTheoDanhMuc = new Dictionary<string, List<Product>>();
            foreach (var sp in danhSachSanPham)
            {
                if (!nhomTheoDanhMuc.ContainsKey(sp.Category))
                {
                    nhomTheoDanhMuc[sp.Category] = new List<Product>();
                }
                nhomTheoDanhMuc[sp.Category].Add(sp);
            }
            
            List<string> danhMucCoNhieuSanPham = new List<string>();
            foreach (var kvp in nhomTheoDanhMuc)
            {
                if (kvp.Value.Count > 2)
                {
                    danhMucCoNhieuSanPham.Add(kvp.Key);
                }
            }
            
            Console.WriteLine("Kết quả TRADITIONAL:");
            foreach (var danhMuc in danhMucCoNhieuSanPham)
            {
                Console.WriteLine($"\n[{danhMuc}] - Top 3:");
                
                List<Product> sanPhamTrongDanhMuc = nhomTheoDanhMuc[danhMuc];
                
                // Sắp xếp (bubble sort)
                for (int i = 0; i < sanPhamTrongDanhMuc.Count - 1; i++)
                {
                    for (int j = i + 1; j < sanPhamTrongDanhMuc.Count; j++)
                    {
                        if (sanPhamTrongDanhMuc[i].Price < sanPhamTrongDanhMuc[j].Price)
                        {
                            var temp = sanPhamTrongDanhMuc[i];
                            sanPhamTrongDanhMuc[i] = sanPhamTrongDanhMuc[j];
                            sanPhamTrongDanhMuc[j] = temp;
                        }
                    }
                }
                
                int dem = 0;
                foreach (var sp in sanPhamTrongDanhMuc)
                {
                    if (dem >= 3) break;
                    decimal giaKhuyenMai = sp.Price * 0.9m;
                    Console.WriteLine($"  {dem + 1}. {sp.Name}: {sp.Price:N0}₫ -> {giaKhuyenMai:N0}₫");
                    dem++;
                }
            }
            
            // ========== CÁCH DÙNG LINQ ==========
            Console.WriteLine("\n\n--- CÁCH DÙNG LINQ ---");
            Console.WriteLine("  - Lines of Code: 5 dòng (6 operators chained!)");
            Console.WriteLine("  - Operators: GroupBy -> Where -> SelectMany -> OrderByDescending -> Take -> Select");
            Console.WriteLine("  - Time Complexity: O(n log n) - tối ưu hơn nhiều");
            Console.WriteLine("  - Space Complexity: O(k) - chỉ tạo kết quả cuối");
            Console.WriteLine("  - Ưu điểm: Cực kỳ ngắn gọn, declarative, chainable\n");
            
            // Code LINQ - 6 operators chained!
            var ketQuaLINQ = danhSachSanPham
                .GroupBy(sp => sp.Category)                              // 1. Nhóm theo danh mục
                .Where(nhom => nhom.Count() > 2)                         // 2. Lọc danh mục có > 2 SP
                .SelectMany(nhom => nhom                                 // 3. "Mở phẳng" các nhóm
                    .OrderByDescending(sp => sp.Price)                   // 4. Sắp xếp giá giảm dần
                    .Take(3)                                             // 5. Lấy top 3
                    .Select(sp => new                                    // 6. Chọn dữ liệu cần hiển thị
                    {
                        DanhMuc = nhom.Key,
                        TenSanPham = sp.Name,
                        GiaGoc = sp.Price,
                        GiaKhuyenMai = sp.Price * 0.9m
                    })
                )
                .OrderBy(x => x.DanhMuc)
                .ThenByDescending(x => x.GiaGoc);
            
            Console.WriteLine("Code:");
            Console.WriteLine("  var ketQua = danhSachSanPham");
            Console.WriteLine("      .GroupBy(sp => sp.Category)              // 1. Nhóm");
            Console.WriteLine("      .Where(nhom => nhom.Count() > 2)         // 2. Lọc");
            Console.WriteLine("      .SelectMany(nhom => nhom                 // 3. Mở phẳng");
            Console.WriteLine("          .OrderByDescending(sp => sp.Price)   // 4. Sắp xếp");
            Console.WriteLine("          .Take(3)                             // 5. Top 3");
            Console.WriteLine("          .Select(sp => new { ... })           // 6. Chiếu");
            Console.WriteLine("      )");
            Console.WriteLine("      .OrderBy(x => x.DanhMuc)");
            Console.WriteLine("      .ThenByDescending(x => x.GiaGoc);");
            
            Console.WriteLine("\nKết quả LINQ:");
            string danhMucHienTai = "";
            int soThuTu = 1;
            foreach (var item in ketQuaLINQ)
            {
                if (item.DanhMuc != danhMucHienTai)
                {
                    danhMucHienTai = item.DanhMuc;
                    soThuTu = 1;
                    Console.WriteLine($"\n[{item.DanhMuc}] - Top 3:");
                }
                Console.WriteLine($"  {soThuTu}. {item.TenSanPham}: {item.GiaGoc:N0}₫ -> {item.GiaKhuyenMai:N0}₫");
                soThuTu++;
            }
            
            Console.WriteLine("\n>> SO SÁNH: LINQ giảm 90% code, 6 operators chained mượt mà!");
            Console.WriteLine(">> Đây là sức mạnh thực sự của LINQ - xử lý logic phức tạp trong vài dòng!");
        }

        /// <summary>
        /// BÀI TOÁN 5: Thống kê nâng cao - Tổng giá trị tồn kho
        /// SO SÁNH:
        /// - Lines of Code: Traditional (40+ dòng) vs LINQ (4 dòng với 5 operators)
        /// - LINQ Chain: Where -> Select -> OrderByDescending -> Select -> Sum
        /// - Kết hợp cả aggregation (Sum) và transformation (Select)
        /// </summary>
        private static void BaiToan5_ThongKeNangCao_TongGiaTriTonKho()
        {
            var danhSachSanPham = GetDanhSachSanPham();
            
            Console.WriteLine("===================================================================");
            Console.WriteLine("  BÀI TOÁN 5: Tổng giá trị tồn kho");
            Console.WriteLine("===================================================================");
            Console.WriteLine("Yêu cầu: Tính tổng giá trị tồn kho (Price x Stock) cho các sản phẩm");
            Console.WriteLine("         danh mục 'Dien tu', có Stock > 10, sắp xếp theo giá trị");
            Console.WriteLine("         tồn kho giảm dần\n");
            
            // ========== CÁCH TRUYỀN THỐNG (TRADITIONAL) ==========
            Console.WriteLine("--- CÁCH TRUYỀN THỐNG ---");
            Console.WriteLine("  - Lines of Code: 40+ dòng");
            Console.WriteLine("  - Time Complexity: O(n²) - filter + sort với bubble sort");
            Console.WriteLine("  - Space Complexity: O(n) - nhiều List và Dictionary");
            Console.WriteLine("  - Nhược điểm: Rất dài, nhiều biến tạm, khó maintain\n");
            
            // Code truyền thống
            List<Product> sanPhamDienTuConNhieu = new List<Product>();
            foreach (var sp in danhSachSanPham)
            {
                if (sp.Category == "Dien tu" && sp.Stock > 10)
                {
                    sanPhamDienTuConNhieu.Add(sp);
                }
            }
            
            Dictionary<string, decimal> giaTriTonKho = new Dictionary<string, decimal>();
            foreach (var sp in sanPhamDienTuConNhieu)
            {
                decimal giaTriTon = sp.Price * sp.Stock;
                giaTriTonKho[sp.Name] = giaTriTon;
            }
            
            List<KeyValuePair<string, decimal>> danhSachSapXep = new List<KeyValuePair<string, decimal>>(giaTriTonKho);
            for (int i = 0; i < danhSachSapXep.Count - 1; i++)
            {
                for (int j = i + 1; j < danhSachSapXep.Count; j++)
                {
                    if (danhSachSapXep[i].Value < danhSachSapXep[j].Value)
                    {
                        var temp = danhSachSapXep[i];
                        danhSachSapXep[i] = danhSachSapXep[j];
                        danhSachSapXep[j] = temp;
                    }
                }
            }
            
            decimal tongGiaTriTonKho_Traditional = 0;
            Console.WriteLine("Kết quả TRADITIONAL:");
            foreach (var kvp in danhSachSapXep)
            {
                Console.WriteLine($"  - {kvp.Key}: {kvp.Value:N0}₫");
                tongGiaTriTonKho_Traditional += kvp.Value;
            }
            Console.WriteLine($"\nTổng giá trị tồn kho: {tongGiaTriTonKho_Traditional:N0}₫");
            
            // ========== CÁCH DÙNG LINQ ==========
            Console.WriteLine("\n--- CÁCH DÙNG LINQ ---");
            Console.WriteLine("  - Lines of Code: 4 dòng (5 operators chained!)");
            Console.WriteLine("  - Operators: Where -> Select -> OrderByDescending -> Sum/ToList");
            Console.WriteLine("  - Time Complexity: O(n log n)");
            Console.WriteLine("  - Space Complexity: O(k) - deferred execution");
            Console.WriteLine("  - Ưu điểm: Cực ngắn, rõ ràng, hiệu năng tốt\n");
            
            // Code LINQ - 5 operators chained!
            var chiTietTonKho_LINQ = danhSachSanPham
                .Where(sp => sp.Category == "Dien tu" && sp.Stock > 10)  // 1. Lọc điều kiện
                .Select(sp => new                                        // 2. Tính giá trị tồn kho
                {
                    TenSanPham = sp.Name,
                    GiaTriTonKho = sp.Price * sp.Stock,
                    SoLuong = sp.Stock
                })
                .OrderByDescending(x => x.GiaTriTonKho)                  // 3. Sắp xếp
                .ToList();                                               // 4. Materialize
            
            var tongGiaTriTonKho_LINQ = chiTietTonKho_LINQ
                .Sum(x => x.GiaTriTonKho);                               // 5. Aggregation
            
            Console.WriteLine("Code:");
            Console.WriteLine("  var chiTiet = danhSachSanPham");
            Console.WriteLine("      .Where(sp => sp.Category == \"Dien tu\" && sp.Stock > 10)");
            Console.WriteLine("      .Select(sp => new {");
            Console.WriteLine("          TenSanPham = sp.Name,");
            Console.WriteLine("          GiaTriTonKho = sp.Price * sp.Stock");
            Console.WriteLine("      })");
            Console.WriteLine("      .OrderByDescending(x => x.GiaTriTonKho)");
            Console.WriteLine("      .ToList();");
            Console.WriteLine();
            Console.WriteLine("  var tong = chiTiet.Sum(x => x.GiaTriTonKho);");
            
            Console.WriteLine("\nKết quả LINQ:");
            foreach (var item in chiTietTonKho_LINQ)
            {
                Console.WriteLine($"  - {item.TenSanPham}: {item.GiaTriTonKho:N0}₫ (SL: {item.SoLuong})");
            }
            Console.WriteLine($"\nTổng giá trị tồn kho: {tongGiaTriTonKho_LINQ:N0}₫");
            
            Console.WriteLine("\n>> SO SÁNH: LINQ giảm 90% code, kết hợp mượt mà filter + transform + aggregate!");
            Console.WriteLine(">> Method chaining giúp logic rõ ràng như đọc tiếng Anh!");
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
            Console.WriteLine("+ LINQ giảm 70-90% số dòng code (với bài toán phức tạp có thể đến 95%)");
            Console.WriteLine("+ Dễ đọc, dễ hiểu, dễ bảo trì hơn nhiều");
            Console.WriteLine("+ Hiệu năng tốt hơn với dữ liệu lớn (đặc biệt sorting)");
            Console.WriteLine("+ An toàn kiểu, ít lỗi hơn");
            Console.WriteLine("+ Declarative: Tập trung vào MUỐN GÌ (WHAT) thay vì LÀM THẾ NÀO (HOW)");
            Console.WriteLine("+ Method Chaining: Kết hợp nhiều operators (3-6+) một cách mượt mà");
            Console.WriteLine("+ Xử lý logic phức tạp (group, aggregate, transform) trong vài dòng!");
            Console.WriteLine("===================================================================");
            Console.WriteLine("\n💡 GHI NHỚ:");
            Console.WriteLine("  - Bài toán càng phức tạp, LINQ càng thể hiện ưu thế vượt trội");
            Console.WriteLine("  - Method chaining là sức mạnh cốt lõi: Where -> Select -> OrderBy -> ...");
            Console.WriteLine("  - Deferred execution giúp tối ưu hiệu năng với IQueryable");
            Console.WriteLine("===================================================================\n");
        }
    }
}

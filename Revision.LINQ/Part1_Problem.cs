using Revision.LINQ.Models;

namespace Revision.LINQ
{
    public class Part1_Problem
    {
        public static void Demo()
        {
            Console.WriteLine("=== PHẦN 1: BÀI TOÁN VÀ CÁCH GIẢI TRUYỀN THỐNG ===\n");

            List<Product> danhSachSanPham = new List<Product>
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

            Console.WriteLine("DANH SÁCH SẢN PHẨM:");
            foreach (var sp in danhSachSanPham)
            {
                Console.WriteLine($"  {sp}");
            }
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("BÀI TOÁN 1: Tìm sản phẩm danh mục 'Dien tu' (CÁCH TRUYỀN THỐNG)");
            Console.WriteLine("=================================================");
            List<Product> sanPhamDienTu = new List<Product>();
            foreach (var sp in danhSachSanPham)
            {
                if (sp.Category == "Dien tu")
                {
                    sanPhamDienTu.Add(sp);
                }
            }
            
            Console.WriteLine($"Tìm thấy {sanPhamDienTu.Count} sản phẩm:");
            foreach (var sp in sanPhamDienTu)
            {
                Console.WriteLine($"  {sp.Name}");
            }
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("BÀI TOÁN 2: Top 5 sản phẩm có giá cao nhất (CÁCH TRUYỀN THỐNG)");
            Console.WriteLine("=================================================");
            
            List<Product> danhSachCopy = new List<Product>(danhSachSanPham);
            
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
            
            List<string> top5Ten = new List<string>();
            int count = 0;
            foreach (var sp in danhSachCopy)
            {
                if (count >= 5) break;
                top5Ten.Add(sp.Name);
                count++;
            }
            
            Console.WriteLine("Top 5 sản phẩm:");
            foreach (var ten in top5Ten)
            {
                Console.WriteLine($"  - {ten}");
            }
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("BÀI TOÁN 3: Kiểm tra có sản phẩm hết hàng (CÁCH TRUYỀN THỐNG)");
            Console.WriteLine("=================================================");
            bool coSanPhamHetHang = false;
            foreach (var sp in danhSachSanPham)
            {
                if (sp.Stock == 0)
                {
                    coSanPhamHetHang = true;
                    break;
                }
            }
            
            Console.WriteLine($"Có sản phẩm hết hàng: {(coSanPhamHetHang ? "CÓ" : "KHÔNG")}");
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("BÀI TOÁN 4: Thống kê theo danh mục (CÁCH TRUYỀN THỐNG)");
            Console.WriteLine("=================================================");
            Console.WriteLine("Yêu cầu: Tìm các danh mục có > 2 sản phẩm, lấy top 3 sản phẩm giá cao nhất");
            Console.WriteLine("         của mỗi danh mục, hiển thị tên và giá khuyến mãi (giảm 10%)");
            Console.WriteLine();
            
            // Bước 1: Nhóm sản phẩm theo danh mục
            Dictionary<string, List<Product>> nhomTheoDanhMuc = new Dictionary<string, List<Product>>();
            foreach (var sp in danhSachSanPham)
            {
                if (!nhomTheoDanhMuc.ContainsKey(sp.Category))
                {
                    nhomTheoDanhMuc[sp.Category] = new List<Product>();
                }
                nhomTheoDanhMuc[sp.Category].Add(sp);
            }
            
            // Bước 2: Lọc danh mục có > 2 sản phẩm
            List<string> danhMucCoNhieuSanPham = new List<string>();
            foreach (var kvp in nhomTheoDanhMuc)
            {
                if (kvp.Value.Count > 2)
                {
                    danhMucCoNhieuSanPham.Add(kvp.Key);
                }
            }
            
            // Bước 3: Với mỗi danh mục, lấy top 3 sản phẩm giá cao nhất
            foreach (var danhMuc in danhMucCoNhieuSanPham)
            {
                Console.WriteLine($"\n[{danhMuc}] - Top 3 sản phẩm:");
                
                List<Product> sanPhamTrongDanhMuc = nhomTheoDanhMuc[danhMuc];
                
                // Sắp xếp theo giá giảm dần (bubble sort)
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
                
                // Lấy top 3 và tính giá khuyến mãi
                int dem = 0;
                foreach (var sp in sanPhamTrongDanhMuc)
                {
                    if (dem >= 3) break;
                    decimal giaKhuyenMai = sp.Price * 0.9m;
                    Console.WriteLine($"  - {sp.Name}: {sp.Price:N0}₫ -> {giaKhuyenMai:N0}₫ (sau giảm 10%)");
                    dem++;
                }
            }
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("BÀI TOÁN 5: Tổng giá trị tồn kho (CÁCH TRUYỀN THỐNG)");
            Console.WriteLine("=================================================");
            Console.WriteLine("Yêu cầu: Tính tổng giá trị tồn kho (Price * Stock) cho các sản phẩm");
            Console.WriteLine("         danh mục 'Dien tu', có Stock > 10, sắp xếp theo giá trị giảm dần");
            Console.WriteLine();
            
            // Bước 1: Lọc sản phẩm điện tử có stock > 10
            List<Product> sanPhamDienTuConNhieu = new List<Product>();
            foreach (var sp in danhSachSanPham)
            {
                if (sp.Category == "Dien tu" && sp.Stock > 10)
                {
                    sanPhamDienTuConNhieu.Add(sp);
                }
            }
            
            // Bước 2: Tính giá trị tồn kho cho mỗi sản phẩm
            Dictionary<string, decimal> giaTriTonKho = new Dictionary<string, decimal>();
            foreach (var sp in sanPhamDienTuConNhieu)
            {
                decimal giaTriTon = sp.Price * sp.Stock;
                giaTriTonKho[sp.Name] = giaTriTon;
            }
            
            // Bước 3: Sắp xếp theo giá trị tồn kho giảm dần
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
            
            // Bước 4: Tính tổng và hiển thị
            decimal tongGiaTriTonKho = 0;
            Console.WriteLine("Chi tiết giá trị tồn kho:");
            foreach (var kvp in danhSachSapXep)
            {
                Console.WriteLine($"  - {kvp.Key}: {kvp.Value:N0}₫");
                tongGiaTriTonKho += kvp.Value;
            }
            Console.WriteLine($"\nTổng giá trị tồn kho: {tongGiaTriTonKho:N0}₫");
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("NHẬN XÉT VỀ CÁCH TRUYỀN THỐNG:");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Code DÀI DÒNG, khó đọc, khó bảo trì");
            Console.WriteLine("- Phải viết NHIỀU vòng lặp, biến tạm, điều kiện");
            Console.WriteLine("- Tập trung vào 'LÀM THẾ NÀO?' (HOW) thay vì 'MUỐN GÌ?' (WHAT)");
            Console.WriteLine("- Dễ gây lỗi logic (quên break, sai điều kiện, ...)");
            Console.WriteLine("- Với bài toán phức tạp: Code có thể lên đến 50-100 dòng!");
            Console.WriteLine("- Khó debug, khó test, khó mở rộng");
            Console.WriteLine("\n=> CẦN MỘT CÁCH TỐT HƠN -> LINQ!\n");
        }
    }
}

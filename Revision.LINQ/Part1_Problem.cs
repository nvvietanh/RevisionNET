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
            Console.WriteLine("NHẬN XÉT VỀ CÁCH TRUYỀN THỐNG:");
            Console.WriteLine("=================================================");
            Console.WriteLine("- Code DÀI DÒNG, khó đọc, khó bảo trì");
            Console.WriteLine("- Phải viết NHIỀU vòng lặp, biến tạm, điều kiện");
            Console.WriteLine("- Tập trung vào 'LÀM THẾ NÀO?' (HOW) thay vì 'MUỐN GÌ?' (WHAT)");
            Console.WriteLine("- Dễ gây lỗi logic (quên break, sai điều kiện, ...)");
            Console.WriteLine("\n=> CẦN MỘT CÁCH TỐT HƠN -> LINQ!\n");
        }
    }
}

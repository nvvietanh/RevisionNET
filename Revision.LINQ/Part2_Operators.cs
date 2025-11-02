using Revision.LINQ.Models;

namespace Revision.LINQ
{
    public class Part2_Operators
    {
        public static void Demo()
        {
            Console.WriteLine("=== PHAN 2: CAC TOAN TU TRUY VAN CO BAN ===\n");

            List<Product> products = new List<Product>
            {
                new Product(1, "Laptop Dell XPS 13", "Dien tu", 25000000, 15),
                new Product(2, "Sach: Clean Code", "Sach", 250000, 50),
                new Product(3, "iPhone 15 Pro", "Dien tu", 30000000, 8),
                new Product(4, "Sach: Design Patterns", "Sach", 320000, 30),
                new Product(5, "Ban phim co Keychron", "Dien tu", 2500000, 25),
                new Product(6, "Sach: C# in Depth", "Sach", 450000, 20),
                new Product(7, "Tai nghe Sony WH-1000XM5", "Dien tu", 8000000, 12)
            };

            Console.WriteLine("=================================================");
            Console.WriteLine("a. WHERE (Filtering - Loc)");
            Console.WriteLine("=================================================");
            var dienTu = products.Where(p => p.Category == "Dien tu");
            
            Console.WriteLine("Code: products.Where(p => p.Category == \"Dien tu\")");
            Console.WriteLine($"Ket qua ({dienTu.Count()} san pham):");
            foreach (var p in dienTu)
            {
                Console.WriteLine($"  - {p.Name}");
            }
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("b. SELECT (Projection - Chieu/Bien doi)");
            Console.WriteLine("=================================================");
            var tenVaGia = products.Select(p => new { Ten = p.Name, Gia = p.Price });
            
            Console.WriteLine("Code: products.Select(p => new { Ten = p.Name, Gia = p.Price })");
            Console.WriteLine("Ket qua (Anonymous Type - Kieu an danh):");
            foreach (var item in tenVaGia.Take(3))
            {
                Console.WriteLine($"  - Ten: {item.Ten}, Gia: {item.Gia:C}");
            }
            Console.WriteLine("  ...");
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("c. ORDERBY / ORDERBYDESCENDING / THENBY (Sorting)");
            Console.WriteLine("=================================================");
            var sapXep = products
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.Price);
            
            Console.WriteLine("Code: products.OrderBy(p => p.Category).ThenByDescending(p => p.Price)");
            Console.WriteLine("Ket qua (sap xep theo danh muc, sau do gia giam dan):");
            foreach (var p in sapXep)
            {
                Console.WriteLine($"  - {p.Name} ({p.Category}) - Gia: {p.Price:C}");
            }
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("d. FIRST / FIRSTORDEFAULT (Element Operators)");
            Console.WriteLine("=================================================");
            
            var sanPhamDauTien = products.First();
            Console.WriteLine($"First(): {sanPhamDauTien.Name}");
            
            var sanPhamId3 = products.FirstOrDefault(p => p.Id == 3);
            Console.WriteLine($"FirstOrDefault(p => p.Id == 3): {sanPhamId3?.Name}");
            
            var sanPhamId999 = products.FirstOrDefault(p => p.Id == 999);
            Console.WriteLine($"FirstOrDefault(p => p.Id == 999): {(sanPhamId999 == null ? "null (khong tim thay)" : sanPhamId999.Name)}");
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("e. COUNT / SUM / AVERAGE / ANY (Aggregation)");
            Console.WriteLine("=================================================");
            
            var tongSanPham = products.Count();
            var tongGiaTri = products.Sum(p => p.Price * p.Stock);
            var giaTrungBinh = products.Average(p => p.Price);
            var coSanPhamHetHang = products.Any(p => p.Stock == 0);
            
            Console.WriteLine($"Count(): {tongSanPham} san pham");
            Console.WriteLine($"Sum(p => p.Price * p.Stock): {tongGiaTri:C} (tong gia tri kho)");
            Console.WriteLine($"Average(p => p.Price): {giaTrungBinh:C}");
            Console.WriteLine($"Any(p => p.Stock == 0): {(coSanPhamHetHang ? "CO" : "KHONG")} san pham het hang");
            Console.WriteLine();
        }
    }
}

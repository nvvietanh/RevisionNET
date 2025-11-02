namespace Revision.LINQ
{
    /// <summary>
    /// Chương trình demo LINQ
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=================================================================");
            Console.WriteLine("     GIỚI THIỆU VỀ LINQ (Language-Integrated Query)");
            Console.WriteLine("=================================================================");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("\n=================================================================");
                Console.WriteLine("                          MENU DEMO");
                Console.WriteLine("=================================================================");
                Console.WriteLine("  PHẦN 1");
                Console.WriteLine("    [1] Bài toán & Cách giải truyền thống");
                Console.WriteLine();
                Console.WriteLine("  PHẦN 2");
                Console.WriteLine("    [2] Hai cách viết LINQ & Deferred Execution");
                Console.WriteLine("    [3] Các toán tử truy vấn cơ bản");
                Console.WriteLine();
                Console.WriteLine("  PHẦN 3");
                Console.WriteLine("    [4] Giải quyết bài toán bằng LINQ");
                Console.WriteLine("    [5] Demo: Quản lý Sản phẩm");
                Console.WriteLine("    [6] Các loại LINQ Providers");
                Console.WriteLine();
                Console.WriteLine("  KHÁC");
                Console.WriteLine("    [A] Chạy tất cả demo");
                Console.WriteLine("    [0] Thoát");
                Console.WriteLine("=================================================================");
                Console.Write("\nChọn demo (1-6, A, 0): ");

                var choice = Console.ReadLine()?.ToUpper();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Part1_Problem.Demo();
                        break;
                    case "2":
                        Part2_Syntax.Demo();
                        break;
                    case "3":
                        Part2_Operators.Demo();
                        break;
                    case "4":
                        Part3_Solution.Demo();
                        break;
                    case "5":
                        Part3_ProductDemo.Demo();
                        break;
                    case "6":
                        Part3_Providers.Demo();
                        break;
                    case "A":
                        RunAllDemos();
                        break;
                    case "0":
                        Console.WriteLine("Cảm ơn bạn đã xem demo!");
                        return;
                    default:
                        Console.WriteLine("[LỖI] Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                Console.WriteLine("\n" + new string('=', 65));
                Console.Write("Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void RunAllDemos()
        {
            Console.WriteLine("=================================================================");
            Console.WriteLine("              CHẠY TẤT CẢ DEMO");
            Console.WriteLine("=================================================================");
            Console.WriteLine();

            // Phần 1
            Part1_Problem.Demo();
            Pause();

            // Phần 2
            Part2_Syntax.Demo();
            Pause();

            Part2_Operators.Demo();
            Pause();

            // Phần 3
            Part3_Solution.Demo();
            Pause();

            Part3_ProductDemo.Demo();
            Pause();

            Part3_Providers.Demo();

            Console.WriteLine("\n=================================================================");
            Console.WriteLine("              ĐÃ HOÀN THÀNH TẤT CẢ DEMO!");
            Console.WriteLine("=================================================================");
        }

        static void Pause()
        {
            Console.WriteLine("\n" + new string('-', 65));
            Console.Write("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

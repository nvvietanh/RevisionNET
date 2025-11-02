namespace Revision.LINQ.Models
{
    /// <summary>
    /// Model Sinh viên sử dụng trong demo LINQ
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Khoa { get; set; }
        public string QueQuan { get; set; }
        public double DiemTB { get; set; }

        public Student(int id, string hoTen, string khoa, string queQuan, double diemTB)
        {
            Id = id;
            HoTen = hoTen;
            Khoa = khoa;
            QueQuan = queQuan;
            DiemTB = diemTB;
        }

        public override string ToString()
        {
            return $"[{Id}] {HoTen} - Khoa: {Khoa} - Quê: {QueQuan} - ĐTB: {DiemTB}";
        }
    }
}

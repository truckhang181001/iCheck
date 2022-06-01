using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    [Serializable]
    public class SuKien
    {
        public SuKien(string tenevent, DateTime thoigian, string diadiem)
        {

            string maE = thoigian.ToString("yyyyMMdd") + Program.RemoveUnicode(tenevent);
            this.maEvent = maE;
            this.tenEvent = tenevent;
            this.thoiGian = thoigian;
            this.diaDiem = diadiem;
            this.dsThamGia = new List<SinhVien>();
        }
        public SuKien(string tenevent, DateTime thoigian, string diadiem, List<SinhVien> dsthamgia)
        {

            string maE = thoigian.ToString("yyyyMMdd") + Program.RemoveUnicode(tenevent);
            this.maEvent = maE;
            this.tenEvent = tenevent;
            this.thoiGian = thoigian;
            this.diaDiem = diadiem;
            this.dsThamGia = dsthamgia;
            this.soLuong = this.dsThamGia.Count;
        }

        private string maEvent;
        private string tenEvent;
        private DateTime thoiGian;
        private string diaDiem;
        private List<SinhVien> dsThamGia;
        private int soLuong;

        public string MaEvent { get => maEvent;}
        public string TenEvent { get => tenEvent; set => tenEvent = value; }
        public DateTime ThoiGian { get => thoiGian; set => thoiGian = value; }
        public string DiaDiem { get => diaDiem; set => diaDiem = value; }
        public List<SinhVien> DsThamGia { get => dsThamGia; set => dsThamGia = value; }


    }
}

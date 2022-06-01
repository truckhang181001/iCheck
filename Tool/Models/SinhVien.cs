using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    [Serializable]
    public class SinhVien
    {

        public SinhVien(string name, string mssv, string nienkhoa, string khoavien, string email)
        {
            this.hoVaTen = name;
            this.mSSV = mssv;
            this.nienKhoa = nienkhoa;
            this.khoaVien = khoavien;
            this.email = email;
            this.code = HashCode(string.Format("{0}!#%&({1}@$^*)",name,mssv));
        }

        private string hoVaTen;
        private string mSSV;
        private string nienKhoa;
        private string khoaVien;
        private string email;
        private string code;

        public string HoVaTen { get => hoVaTen; set => hoVaTen = value; }
        public string MSSV { get => mSSV; set => mSSV = value; }
        public string NienKhoa { get => nienKhoa; set => nienKhoa = value; }
        public string KhoaVien { get => khoaVien; set => khoaVien = value; }
        public string Email { get => email; set => email = value; }
        public string Code { get => code;}

        private string HashCode(string code)
        {
            string key="";
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(code);
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(temp);
            foreach (byte item in hash)
                key += item.ToString();
            return key;
        }

    }
}

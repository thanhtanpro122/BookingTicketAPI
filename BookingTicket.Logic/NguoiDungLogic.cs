using BookingTicket.Domain.DataModels;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingTicket.Logic
{
    public class NguoiDungLogic : INguoiDungLogic
    {
        private readonly BookingTicketContext context;

        public NguoiDungLogic(BookingTicketContext context)
        {
            this.context = context;
        }

        public void Add(NguoiDung e)
        {
            context.NguoiDungs.Add(e);
            context.SaveChanges();
        }

        public bool Delete(NguoiDung e)
        {
            var nguoiDung = context.NguoiDungs.Find(e.UserID);
            if (nguoiDung == null)
            {
                return false;
            }
            context.NguoiDungs.Remove(e);
            context.SaveChanges();
            return true;
        }

        public bool DeleteById(params object[] keys)
        {
            NguoiDung nguoiDung = context.NguoiDungs.Find(keys);
            if (nguoiDung == null)
            {
                return false;
            }

            context.NguoiDungs.Remove(nguoiDung);
            context.SaveChanges();
            return true;
        }

        public NguoiDung Get(Func<NguoiDung, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<NguoiDung> GetAll()
        {
            return context.NguoiDungs.ToList();
        }

        public List<NguoiDung> GetAll(Func<NguoiDung, bool> predicate)
        {
            return context.NguoiDungs.ToList();
        }

        public async Task<List<NguoiDung>> GetAllAsync()
        {
            return await context.NguoiDungs.ToListAsync();
        }

        public Task<List<NguoiDung>> GetAllAsync(Func<NguoiDung, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<NguoiDung> GetAsync(Func<NguoiDung, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public NguoiDung GetById(params object[] keys)
        {
            return context.NguoiDungs.Find(keys);
        }

        public Task<NguoiDung> GetByIdAsync(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public string GetMD5(string matkhau)
        {
            var str = "";
            var buffer = Encoding.UTF8.GetBytes(matkhau);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (var b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }       

        public bool Modify(NguoiDung e)
        {
            var nguoiDung = context.NguoiDungs.Find(e.UserID);
            if (nguoiDung == null)
            {
                return false;
            }
            if (CheckTonTai(e.SDT))
            {
                return false;
            }
            nguoiDung.SDT = e.SDT;
            nguoiDung.HoTen = e.HoTen;
            nguoiDung.Email = e.Email;
            nguoiDung.MatKhau = GetMD5(e.MatKhau);
            nguoiDung.GioiTinh = e.GioiTinh;
            nguoiDung.DiaChi = e.DiaChi;
            nguoiDung.NgaySinh = e.NgaySinh;
            nguoiDung.UpdateTime = e.UpdateTime;
            nguoiDung.CreatedTime = e.CreatedTime;
            nguoiDung.Avatar = e.Avatar;
            return true;
        }

        public bool Register(string sdt, string email, string matkhau)
        {
            var createTime = DateTime.Now;
            try
            {
                var nguoiDung = new NguoiDung
                {
                    SDT = sdt,
                    Email = email,
                    MatKhau = GetMD5(matkhau),
                    CreatedTime = createTime
                };
                context.Add(nguoiDung);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool CheckTonTai(string sdt)
        {
            return context.NguoiDungs.Where(e => e.SDT == sdt).Count() > 0;
        }

        public NguoiDung CheckUserNameAndPass(string username, string password)
        {
            var pass = GetMD5(password);
            return context.NguoiDungs.FirstOrDefault(e => e.SDT == username && e.MatKhau == pass);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status">
        ///  0: là chưa đi 
        ///  1: đã đi 
        ///  2: đã hủy
        /// </param>
        /// <returns></returns>
        public List<DSVe> GetDSVe(long userId, int status)
        {
            var dsve = context.Ves
                .Include(e => e.ThongtinDatCho)
                .ThenInclude(e => e.DieuHanh)
                .ThenInclude(e => e.TuyenXe)
                .Where(e => e.Status == status && e.ThongtinDatCho.MaKH == userId)
                .Select(e => new DSVe
                {
                    DiaDiemDen = e.ThongtinDatCho.DieuHanh.TuyenXe.DiaDiemDen,
                    DiaDiemDi = e.ThongtinDatCho.DieuHanh.TuyenXe.DiaDiemDi,
                    GiaVe = e.GiaVe,
                    NgayDat = e.ThongtinDatCho.NgayDat,
                    NgayHuy = e.UpdateTime,
                    ThoiGianKetThuc = e.ThongtinDatCho.DieuHanh.TuyenXe.ThoiGIanKetThuc,
                    ThoiGianKhoiHanh = e.ThongtinDatCho.DieuHanh.TuyenXe.ThoiGianKhoiHanh
                })
                .ToList();

            return dsve;
        }

        public bool DatVe(DatVe datVe)
        {
            try
            {
                var thongtindatcho = new ThongTinDatCho
                {
                    SoLuongVe = datVe.soluongve,
                    HoTenNguoiDat = datVe.Hoten,
                    SDT = datVe.sdt,
                    HinhThucThanhToan = datVe.HinhThucThanhToan,
                    NgayDat = datVe.ngayDat,
                    MaKH = datVe.MaKH,
                    MaDieuHanh = datVe.maDieuHanh
                };
                context.ThongTinDatChos.Add(thongtindatcho);
                context.SaveChanges();

                foreach (var item in datVe.maChoNgoi)
                {
                    var ve = new Ve
                    {
                        GiaVe = datVe.GiaVe,
                        MaDatCho = thongtindatcho.MadatCho,
                        Status = 0,
                        MaChoNgoi = item
                    };
                    context.Ves.Add(ve);

                    var chongoi = context.ChoNgois.Find((long)item);
                    if (chongoi == null || chongoi.TinhTrang == 1)
                    {
                        return false;
                    }
                    chongoi.TinhTrang = 1;
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

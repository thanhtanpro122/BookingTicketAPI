using BookingTicket.Domain.DataModels;
using BookingTicket.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingTicket.Logic
{
    public interface INguoiDungLogic
    {
        List<NguoiDung> GetAll();
        Task<List<NguoiDung>> GetAllAsync();
        List<NguoiDung> GetAll(Func<NguoiDung, bool> predicate);
        Task<List<NguoiDung>> GetAllAsync(Func<NguoiDung, bool> predicate);
        NguoiDung GetById(params object[] keys);
        Task<NguoiDung> GetByIdAsync(params object[] keys);

        NguoiDung Get(Func<NguoiDung, bool> predicate);
        Task<NguoiDung> GetAsync(Func<NguoiDung, bool> predicate);

        void Add(NguoiDung e);
        bool DeleteById(params object[] keys);
        bool Delete(NguoiDung e);
        bool Modify(NguoiDung e);

        bool Register(string sdt, string email, string matkhau);

        String GetMD5(string matkhau);
        bool CheckTonTai(string sdt);

        NguoiDung CheckUserNameAndPass(string username, string password);

        List<DSVe> GetDSVe(long userId, int status);

        bool DatVe(DatVe datVe);
      
    }
}

using BookingTicket.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Logic
{
    public interface ITuyenXeLogic
    {
        List<string> GetDiaDiemDi();
        List<string> GetDiaDiemDen();

        List<TuyenXe> GetDSTuyenXe(string diadiemdi, string diadiemden, DateTime ngayKhoiHanh);

        List<ChoNgoiItem> GetDSChoNgoi(long maDieuHanh);
    }
}

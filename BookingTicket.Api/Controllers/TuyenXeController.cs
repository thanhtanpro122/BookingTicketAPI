using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTicket.Domain.ViewModels;
using BookingTicket.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTicket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuyenXeController : ControllerBase
    {
        private readonly ITuyenXeLogic tuyenXeLogic;

        public TuyenXeController(ITuyenXeLogic tuyenXeLogic)
        {
            this.tuyenXeLogic = tuyenXeLogic;
        }

        [HttpGet("diadiemdi")]
        public List<string> GetAllDiaDiemDi()
        {
            return tuyenXeLogic.GetDiaDiemDi();
        }

        [HttpGet("diadiemden")]
        public List<string> GetAllDiaDiemDen()
        {
            return tuyenXeLogic.GetDiaDiemDen();
        }

        [HttpGet("dstuyenxe")]
        public List<TuyenXe> GetListDSTuyenXe(string diadiemdi, string diadiemden, DateTime ngayKhoiHanh)
        {
            var dstuyenxe = tuyenXeLogic.GetDSTuyenXe(diadiemdi, diadiemden, ngayKhoiHanh);
            return dstuyenxe;
        }

        [HttpGet("dschongoi")]
        public List<ChoNgoiItem> GetDSChoNgoi(long maDieuHanh)
        {
            var dsChoNgoi = tuyenXeLogic.GetDSChoNgoi(maDieuHanh);
            return dsChoNgoi;
        }
    }
}
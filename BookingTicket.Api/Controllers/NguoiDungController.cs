using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingTicket.Domain.DataModels;
using BookingTicket.Entities.Models;
using BookingTicket.Logic;
using CookieJWT.Server.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookingTicket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungLogic nguoiDungLogic;
        private readonly JwtTokenManager tokenManager;

        public NguoiDungController(INguoiDungLogic nguoiDungLogic, IConfiguration configuration)
        {
            this.nguoiDungLogic = nguoiDungLogic;
            tokenManager = new JwtTokenManager(configuration["Secret"]);
        }

        [HttpGet]
        public List<NguoiDung> Get()
        {
            return nguoiDungLogic.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute(Name = "id")] long userId)
        {
            var nguoiDung = nguoiDungLogic.GetById(userId);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return Ok(nguoiDung);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NguoiDung nguoiDung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                nguoiDungLogic.Add(nguoiDung);
                return CreatedAtAction(nameof(Post), nguoiDung.UserID, nguoiDung);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            try
            {
                if (nguoiDungLogic.DeleteById(id))
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("editinfo/{id}")]
        public IActionResult EditThongTinNguoiDung([FromRoute(Name = "id")] long userId, [FromBody] NguoiDung nguoiDung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userId != nguoiDung.UserID)
            {
                return BadRequest();
            }

            try
            {
                var res = nguoiDungLogic.Modify(nguoiDung);
                if (!res)
                {
                    return Ok(new { Edit = "error" });
                }
                return Ok(new { Edit = "success" });
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("register")]
        public IActionResult Register(string sdt, string email, string matkhau)
        {
            if (String.IsNullOrWhiteSpace(sdt)&& String.IsNullOrWhiteSpace(email) && String.IsNullOrWhiteSpace(matkhau))
            {
                return BadRequest();
            }
            if (nguoiDungLogic.CheckTonTai(sdt))
            {
                return Ok(new { ExistPhone = true, Registered = false });
            }
            var success = nguoiDungLogic.Register(sdt,email,matkhau);         
            return Ok(new { ExistPhone = false, Registered = success });
        }


        [HttpGet("signin")]
        public IActionResult LoginWithUsernameAndPassword(string phone, string password)
        {
            var found = nguoiDungLogic.CheckUserNameAndPass(phone,password);
            if (found == null)
            {
                return Ok(new UserLoginDataModel
                {
                    LoginStatus = LoginStatus.InvalidUsernameOrPassword
                });
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.MobilePhone, phone),
                new Claim(ClaimTypes.NameIdentifier, found.UserID.ToString())
            };
            var token = tokenManager.GenerateToken(claims);

            return Ok(new UserLoginDataModel(phone, found.UserID.ToString())
            {
                LoginStatus = LoginStatus.Successfull,
                Token = token
            });
        }

        [HttpGet("validate")]
        public IActionResult LoginWithToken(string phone, string userId, string token)
        {
            bool accept;
            try
            {
                accept = IsvalidToken(phone, userId, token);
            }
            catch (SecurityTokenExpiredException)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.MobilePhone, phone),
                    new Claim(ClaimTypes.NameIdentifier, userId)
                };
                var newToken = tokenManager.GenerateToken(claims);

                return Ok(new UserLoginDataModel
                {
                    LoginStatus = LoginStatus.TokenExpiredWithNewToken,
                    Token = newToken
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (accept)
            {
                return Ok(new UserLoginDataModel(phone, userId)
                {
                    Token = token,
                    LoginStatus = LoginStatus.Successfull
                });
            }

            return Ok(new UserLoginDataModel
            {
                LoginStatus = LoginStatus.InvalidToken
            });
        }

        private bool IsvalidToken(string phone, string userId, string token)
        {
            try
            {
                var principal = tokenManager.GetPrincipal(token, out var securityToken);
                if (principal == null)
                {
                    return false;
                }

                if (principal.Identity is ClaimsIdentity identity)
                {
                    var userClaim = identity.FindFirst(ClaimTypes.MobilePhone);
                    var userIdClainm = identity.FindFirst(ClaimTypes.NameIdentifier);

                    if (userClaim?.Value == phone && userIdClainm?.Value == userId)
                    {
                        if (securityToken != null && securityToken.ValidTo.Date < DateTime.Now.Date)
                        {
                            throw new SecurityTokenExpiredException();
                        }
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("dsve")]
        public List<DSVe> GetDSVe(long userId, int status)
        {
            var dsVe = nguoiDungLogic.GetDSVe(userId, status);
            return dsVe;
        }

        [HttpPost("datve")]
        public IActionResult DatVe([FromBody] DatVe datVe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = nguoiDungLogic.DatVe(datVe);
            if (!check)
            {
                return Ok(new { mesmessage = "fail" , TinhTrang = "Chỗ ngồi đã đặt"});
            }
            return Ok(new { mesmessage = "success" });
        }

        [HttpPost("huyve")]
        public IActionResult HuyVe(long mave)
        {
            if (mave == 0)
            {
                return BadRequest();
            }
            var check = nguoiDungLogic.HuyVe(mave);
            if (!check)
            {
                return Ok(new { mesmessage = "fail", TinhTrang = "Co loi" });
            }
            return Ok(new { mesmessage = "success" });
        }
    }
}
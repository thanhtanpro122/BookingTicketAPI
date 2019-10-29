using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.DataModels
{
    public class UserLoginDataModel
    {
        public UserLoginDataModel()
        {

        }
        public UserLoginDataModel(string phone, string userId)
        {
            Phone = phone;
            UserId = userId;
        }

        public string Phone { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public LoginStatus LoginStatus { get; set; }
    }
    public enum LoginStatus
    {
        Default,
        Successfull,
        InvalidUsernameOrPassword,
        InvalidToken,
        TokenExpired,
        TokenExpiredWithNewToken
    }
}

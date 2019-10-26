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
        public UserLoginDataModel(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
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

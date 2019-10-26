using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicket.Domain.DataModels
{
    public class UserAccount
    {
        public string SDT { get; set; }
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
    }
}

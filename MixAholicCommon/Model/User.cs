using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixAholicCommon.Model
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int UserID { get; set; }
        public string SessionKey { get; set; }
    }
}

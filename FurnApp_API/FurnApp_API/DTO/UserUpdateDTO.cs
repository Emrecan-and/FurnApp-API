using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.DTO
{
    public class UserUpdateDTO
    {
        public string UsersPassword { get; set; }
        public int? UsersAuthorization { get; set; }
        public int? UsersTelNo { get; set; }
        public string UsersAddress { get; set; }

    }
}

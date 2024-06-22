using FurnApp_API.DTO;
using FurnApp_API.Models;
using FurnApp_API.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Queries
{
    public class UserLogInQuery : IRequest<ApiResponse<Token>>
    {
       public UserDTO user;
    }
}

using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Interface.User
{
    public interface IUserService
    {

        Task<ClientResponse> RegisterUser(Register register);
  
    }
}

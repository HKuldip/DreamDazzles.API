using DreamDazzle.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Interface.User
{
    public interface IUsersService
    {
        Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default);
    }
}

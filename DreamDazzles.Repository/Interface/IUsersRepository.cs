using DreamDazzle.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Repository.Interface
{
    public interface IUsersRepository
    {
        Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default);
    }
}

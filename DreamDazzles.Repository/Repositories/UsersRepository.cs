using DreamDazzle.DTO;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Repositories;
using DreamDazzles.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Repository.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainDBContext _context;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(MainDBContext context, ILogger<UsersRepository> logger)
        {
            _context = context;
            _logger = logger;
            
        }

        public async Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductDTO> response = new();
            string mname = "GetProductByIdAsync";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var userId = Guid.NewGuid();
                    var roleId = Guid.NewGuid();


                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{mname}: Error => {ex.Message} | trace: " + traceid);
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }

    }
}

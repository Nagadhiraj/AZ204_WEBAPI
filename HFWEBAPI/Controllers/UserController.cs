using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HFWEBAPI.Models;
using HFWEBAPI.DataAccess;
using Microsoft.Extensions.Configuration;

namespace HFWEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IConfiguration config;

        public UserController(IConfiguration Configuration)
        {
            config = Configuration;
        }

        //[Route("api/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<UserEntity>> Get()
        {
            IUserRepository<UserEntity> Respository = new UserRepository<UserEntity>(config);
            var results = await Respository.GetItemsAsync("User");
            List<UserEntity> users = new List<UserEntity>();
            foreach (var result in results)
            {
                if (result.isActive == true)
                {
                    users.Add(result);
                }
            }
            return users;
        }

        //[Route("api/[controller]/{id:int}")]
        [HttpGet("{Id}")]
        public async Task<UserEntity> GetAsync(string Id)
        {
            IUserRepository<UserEntity> Respository = new UserRepository<UserEntity>(config);
            var users = await Respository.GetItemsAsync(d => d.id == Id && d.isActive == true, "User");
            UserEntity user = new UserEntity();
            foreach (var us in users)
            {
                user = us;
                break;
            }
            return user;
        }

        //[Route("api/[controller]")]
        [HttpPost]
        public async Task<bool> Post([FromBody] UserEntity user)
        {
            try
            {
                IUserRepository<UserEntity> Respository = new UserRepository<UserEntity>(config);
                //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                //var user = JsonConvert.DeserializeObject<UserEntity>(requestBody);
                
                user.id = null;
                await Respository.CreateItemAsync(user, "User");
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        //[Route("api/[controller]/{id:int}")]
        [HttpPut]
        public async Task<bool> Put([FromBody] UserEntity user)
        {
            try
            {
                IUserRepository<UserEntity> Respository = new UserRepository<UserEntity>(config);
                //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                //var user = JsonConvert.DeserializeObject<UserEntity>(requestBody);

                await Respository.UpdateItemAsync(user.id, user, "User");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

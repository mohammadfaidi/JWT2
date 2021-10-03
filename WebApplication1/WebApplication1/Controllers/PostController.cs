using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.models;
using WebApplication1.Repo;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServ _postServ;
        private readonly IMapper _mapper;
        public PostController(IMapper mapper, IPostServ postServ)
        {
            
            _mapper = mapper;
            _postServ = postServ;
        }
        [HttpGet("GetPosts")]

        public List<Posts> GetPosts()
        {
            return _postServ.GetMyPosts();

        }
        [HttpGet("GetPostsWithUser")]

        public List<Posts> GetPostsWithUse()
        {
            return _postServ.GetPostWithUsr();

        }

        [HttpGet]
       
        public async Task<IEnumerable<PostViewModel>> getall()
        {
            var u = _mapper.Map<IEnumerable<PostViewModel>>(await _postServ.Get()).Skip(1).Take(2);
          
            return u;


        }
        [HttpGet("GetPage/{size}/{index}")]
        public Task<IEnumerable<Posts>> GetPage(int size, int index)
        {
            return _postServ.GetPage(size, index);
        }

        [HttpGet("{id}")]
        public async Task<Posts> get(int id)
        {
            Posts u = await _postServ.Get(id);
            return u;
           
        }

        [HttpPost]
        public async Task addUser([FromBody] Posts u)
        {
            await _postServ.Add(u);
        

        }
        /*
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {

            _dbContext.users.Remove(delus);
            _dbContext.SaveChanges();

        }
        */

        }
    }

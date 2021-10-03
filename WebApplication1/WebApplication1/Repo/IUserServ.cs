﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.models;

namespace WebApplication1.Repo
{
  public interface IUserServ :IGenRepo<Users>
    {
        //public Users GetWithPosts();
        public Task<Users> GetWithPosts();
        /*
        Users Get(int id);
        IEnumerable<Users> Get();
        void Add(Users user);
        */


    }

    public class UserServ:GenRepo<Users>, IUserServ
    {
        private readonly AppDBContext _dbcontext;
        public  UserServ(AppDBContext dBContext) : base(dBContext)
        {
            _dbcontext = dBContext;
        }
        //1 Questions 
        public async Task<Users> GetWithPosts()
        {
            //return _dbcontext.users.Include(c=>c.posts)();
            return await _dbcontext.users.Include(c => c.posts).FirstOrDefaultAsync();
        }
/*
        public IEnumerable<Users> Get()
        {
            return _dbcontext.users.ToList();
        }
        public void Add(Users user)
        {

        }

        */
    }
}

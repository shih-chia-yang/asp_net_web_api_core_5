using System.Linq;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using sa_login.Domain;
using System.Linq.Expressions;

namespace sa_login.Repositories
{   
    public interface IUserRepository
    {
        User Find(int id);

        User Find(string name,string password);
    }
    public class UserRepository:IUserRepository
    {
        private List<User> _context {get;set;}
        public UserRepository()
        {
            Init();
        }

        private void Init()
        {
            _context = new List<User>();
            _context.Add(new User(){Id=1,Name="stone",Email="stone@test.com.tw",Password="1234",CanManaged=true});
            _context.Add(new User(){Id=1,Name="john",Email="john@test.com.tw",Password="1234",CanManaged=false});
            _context.Add(new User(){Id=1,Name="eva",Email="eva@test.com.tw",Password="1234",CanManaged=true});
            _context.Add(new User(){Id=1,Name="peter",Email="peter@test.com.tw",Password="1234",CanManaged=false});
        }

        public User Find(int id)
        {
            return _context.Where(x=>x.Id == id).FirstOrDefault();
        }

        public User Find(string name,string password)
        {
            if(!_context.Any(x=>x.Name == name && x.Password == password))
                return null;
            else
            return _context.Where(x=>x.Name==name && x.Password==password).FirstOrDefault();
        }
    }
}

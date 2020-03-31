using FindMydinner.Domain.Model;
using FindMydinner.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyDinner.Domain.Infrastracture.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(FindMyDinnerContext dbcontext) : base(dbcontext) { } 
    }
}

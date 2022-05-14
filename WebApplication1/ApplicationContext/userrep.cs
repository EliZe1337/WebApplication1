using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication1.ApplicationContext
{
    public class userrep : IUserRep
    {
        private readonly ApplicationDBcontext db;
        public userrep(ApplicationDBcontext dd)
        {
            db = dd;    
        }
        public async Task<userscard> GetByName(string name)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Login == name); 
        }
        public async Task<int> Buy(int price, string name)
        {
            try
            {
                var name3 = await GetByName(name);
                name3.Balance -= price;
                db.Update(name3);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            
            return 0;
        }

        public async Task<int> Login(userscard model)
        {
            db.Users.Add(model);
            await db.SaveChangesAsync();
            return 0;
        }

        public async Task<userscard> Register(userscard model)
        {
            db.Users.Add(model);
            await db.SaveChangesAsync();
            return null;
        }

        public async Task<bool> AddBalance(int balance, string name)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login == name);
            user.Balance += balance;
            db.Update(user);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

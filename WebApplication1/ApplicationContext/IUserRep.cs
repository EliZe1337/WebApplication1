using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.ApplicationContext
{
    public interface IUserRep
    {
        Task<userscard> Register(userscard model);
        Task<int> Login(userscard model);
        Task<int> Buy(int price, string name);
        Task<userscard> GetByName(string name);
        Task<bool> AddBalance(int balance, string name);


    }
}

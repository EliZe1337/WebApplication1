using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.ApplicationContext
{
    public interface ICardRepository
    {
        Task<shopcard> GetEntity(int id);   
        Task<shopcard> Create(shopcard model);
        List<shopcard> GetbyId(int id); 
        Task<List<shopcard>> GetAll();    
        Task<shopcard> Edit(shopcard model);
        Task<List<shopcard>> filter(string filter);
    }
}

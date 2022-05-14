using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.ApplicationContext
{
    public class repository : ICardRepository
    {
        public readonly ApplicationDBcontext _db;
        public repository(ApplicationDBcontext db)
        {
            _db = db;
        }
        
        public async Task<shopcard> Create(shopcard model)
        {
            _db.ShopCard.Add(model);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<shopcard> Edit(shopcard model)
        {
            var item = _db.ShopCard.FirstOrDefault(x => x.Id == model.Id);
            item.price = model.price;
            item.Description = model.Description;
            item.Name = model.Name;
            item.type = model.type;
            _db.Update(item);
            await _db.SaveChangesAsync();
            return null;
        }

        public async Task<List<shopcard>> filter(string filter)
        {
            //string filter = "100-500";
            int price1 = 0;
            int price2 = 0;
            string[] sst = filter.Split('-');
            for(int i = 0; i < sst.Length; i++)
            {
                if(i == 0)
                    price1 = Convert.ToInt32(sst[i]);
                if(i == 1)
                    price2 = Convert.ToInt32(sst[i]);
            }
            
            var list = new List<shopcard>();
            var s = await _db.ShopCard.ToListAsync();
            foreach(var item in s)
            {
                if(item.price >= price1 && item.price <= price2)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public async Task<List<shopcard>> GetAll()
        {
            return await _db.ShopCard.ToListAsync();
        }

        public List<shopcard> GetbyId(int id)
        {
            List<shopcard> list = new List<shopcard>();
            var db = _db.ShopCard.ToList();
            foreach(var i in db)
            {
                if(i.type == id)
                {
                    list.Add(i);
                }
            }
            if(list.Count == 0)
            {
                return null;
            }
            return list;
        }

        public async Task<shopcard> GetEntity(int id)
        {
            return await _db.ShopCard.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

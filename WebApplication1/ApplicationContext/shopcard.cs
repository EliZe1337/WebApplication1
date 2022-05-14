using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.ApplicationContext
{
    public class shopcard
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int price { get;set; }
        [Required]
        public int type { get; set; }
        public string Description { get; set; }
        public string src_ph { get; set; }
    }
}

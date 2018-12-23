using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.Models
{
    public class Dal
    {
        private rogueContext bdd;

        public Dal (rogueContext context)
        {
            bdd = context;
        }


    }
}

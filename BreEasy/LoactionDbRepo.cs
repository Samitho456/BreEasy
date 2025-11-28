using BreEasy.EFDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreEasy
{
    public class LoactionDbRepo
    {
        private int _nextId = 0;
        private readonly WindowDbContext _context;

        public LoactionDbRepo(WindowDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


    }
}

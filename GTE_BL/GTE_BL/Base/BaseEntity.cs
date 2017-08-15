using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTE_BL
{
    public abstract class BaseEntity
    {
        public virtual void Save() { }

        public virtual void Fetch() { }
    }
}

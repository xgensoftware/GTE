using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTE_BL
{
    public interface IEntity
    {
        void LoadFrom(DataRow dr);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.State
{
    public interface Istate 
    {
        void ChangeState(WebShop webShop);
    }
}

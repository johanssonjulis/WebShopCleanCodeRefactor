using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.State
{
    //När du är inloggad, ska "log in" vara "log out"
    internal class Loggedin : Istate
    {
        public void ChangeState(WebShop webShop)
        {
            webShop.state = new LoggedOut();
        }

    }
}

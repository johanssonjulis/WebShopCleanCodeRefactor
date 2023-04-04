using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.State
{
    //När du är utloggad, ska "log ut" vara "log in"
    internal class LoggedOut : Istate
    {
        public void ChangeState(WebShop webShop)
        {
            webShop.state = new Loggedin();
        }
    }
}

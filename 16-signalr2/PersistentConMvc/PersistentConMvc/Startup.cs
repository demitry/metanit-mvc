using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using PersistentConMvc.Connection;

[assembly: OwinStartup(typeof(PersistentConMvc.Startup))]

namespace PersistentConMvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.MapSignalR<ChatConnection>("/chat");
            //В данном случае регистрируем маршрут, по которому затем будем обращаться из клиентской части.
        }
    }
}

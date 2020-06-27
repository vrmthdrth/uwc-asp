using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UniversityWebChat.SignalRManagement.Startup))]

namespace UniversityWebChat.SignalRManagement
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
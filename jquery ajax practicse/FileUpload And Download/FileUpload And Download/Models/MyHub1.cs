using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FileUpload_And_Download.Models
{
    [HubName("myHub")]
    public class MyHub1 : Hub
    {
        [HubMethodName("SendMessage")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub1>();
            context.Clients.All.updateMessages();  
        }
    }
}
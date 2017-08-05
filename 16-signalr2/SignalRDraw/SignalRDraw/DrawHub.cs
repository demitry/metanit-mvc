using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRDraw.Models;

namespace SignalRDraw
{
    /*С помощью метода Clients.AllExcept() мы можем исключить из рассылки сообщений того клиента, который собственно и прислал сообщений, и таким образом, избежать ситуации, когда у него будет дублироваться нарисованная линия. А метод addLine собственно и выполняет рыссылку всем подключенным клиентам и задействует на клиенте одноименный метод addLine, который выполняет рисование.*/
    public class DrawHub : Hub
    {
        public void Send(Data data)
        {
            Clients.AllExcept(Context.ConnectionId).addLine(data);
        }
    }
}

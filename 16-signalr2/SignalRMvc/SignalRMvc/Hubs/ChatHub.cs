using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRMvc.Models;

namespace SignalRMvc.Hubs
{

    /*SignalR использует две модели взаимодействия сервера и клиента: Persistent Connection и хабы. В данном случае мы используем хабы. Для этого создаем свой хаб ChatHub, который наследуется от класса Hub.
      Во-первых, мы создаем список, который будет хранить подключенных к чату пользователей.
      Далее у нас определен ряд методов для отправки сообщений, подключения и отключения пользователей.
        Формат вызова методов клиента
        Вызов метода на всех клиентах: Clients.All.addMessage(name, message);
        Вызов метода только на текущем клиенте, который обратился к серверу: Clients.Caller.addMessage(name, message);
        Вызов метода на всех клиентах, кроме того, который обратился к серверу: Clients.Others.addMessage(name, message);
        Вызов метода только у клиента с определенным id: Clients.Client(Context.ConnectionId).addMessage(name, message);
        Вызов метода на всех клиентах, кроме клиента с определенным id: Clients.AllExcept(connectionId).addMessage(name, message);
        Вызов метода на всех клиентах указанной группы: Clients.Group(groupName).addMessage(name, message);
        Вызов метода на всех клиентах указанной группы, за исключением клиента, у которого id - connectionId: Clients.Group(groupName, connectionId).addMessage(name, message);
        Вызов метода на всех клиентах указанной группы, за исключением обратившегося к серверу клиента: Clients.OthersInGroup(groupName).addMessage(name, message);

        //comment (Metanit) :hub - это просто логическая конструкция, своего рода точка, 
        // через которую приложение получает данные от пользователей и может обращаться к подключенным клиентам
        // как скрипт файл узнает к какому именно классу/методу на сервере обращаться и наоборот? - по названию хаба - $.connection.chatHub
        // и по названию функций, которые аналогичны методам хаба

     */
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            //if (Users.Count(x => x.ConnectionId == id) == 0)
            //comment: лучше писать Any, т.к. Count будет бежать по всей коллекции в любом случае, вычисляя количество, 
            // а Any остановится на первом же объекте, удовлетворяющем условии. 
            // Когда мы работает с большими объемами данных, это дает серьезную разницу.

            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
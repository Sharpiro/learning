using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalR.Web.SignalR
{
    public class SignalRWebHub : Hub
    {
        public static Dictionary<string, string> Connections { get; } = new Dictionary<string, string>();

        public override Task OnConnected()
        {
            Connections.Add(Context.ConnectionId, null);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Connections.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public void Send(string message)
        {
            Clients.Caller.Send(message);
        }

        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
            if (!Connections.ContainsKey(Context.ConnectionId)) return;
            Connections[Context.ConnectionId] = groupName;
        }
    }
}
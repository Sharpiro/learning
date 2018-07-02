using Microsoft.AspNet.SignalR;
using SignalR.Web.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SignalR.Web.Api
{
    public class CommandController : ApiController
    {
        private readonly IHubContext _hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRWebHub>();

        public IHttpActionResult RunJob(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    _hubContext.Clients.All.RunJob();
                else
                    _hubContext.Clients.Group(id.ToUpper()).RunJob();
                return Ok(id ?? "");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult UpdatePolling(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    _hubContext.Clients.All.UpdatePolling();
                else
                    _hubContext.Clients.Group(id.ToUpper()).UpdatePolling();
                return Ok(id ?? "");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IEnumerable<object> GetConnections()
        {
            var connectionInfo = SignalRWebHub.Connections.Select(c => new
            {
                ConnectionId = c.Key,
                ApplicationGuid = c.Value
            });
            return connectionInfo;
        }
    }
}
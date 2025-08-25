using Microsoft.AspNetCore.SignalR;

namespace Apibackend.Hubs
{
    /// <summary>
    /// hub class for real-time communication
    /// </summary>
    public class Hubs
    {
        /// <summary>
        /// property hub for chat messages 
        /// </summary>
        public class ProductHub : Hub { }
    }
}

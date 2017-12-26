using Microsoft.AspNetCore.SignalR;
using NSI.BLL.Interfaces;
using NSI.DC.Conversations;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST
{
    
    public class InitialHub : Hub
    {

        private  IConversationsRepository _convRepo;
        private IConversationsManipulation _convManipulation;
        private static List<Tuple<string, List<string>>> usersAndConnections = new List<Tuple<string, List<string>>>();

        public InitialHub(IConversationsRepository conversationRepository, IConversationsManipulation conversationsManipulation)
        {
            this._convRepo = conversationRepository;
            this._convManipulation = conversationsManipulation;
            
           
        }
        
        public Task persistForOnlineStatus(string username)
        {
            if(usersAndConnections.Exists(x => x.Item1 == username))
                usersAndConnections.Find(x => x.Item1 == username).Item2.Add(Context.ConnectionId);                
            else
                usersAndConnections.Add(new Tuple<string, List<string>>(username, new List<string>() { Context.ConnectionId }));

            List<string> onlineUsers = new List<string>();
            foreach (var user in usersAndConnections)
                onlineUsers.Add(user.Item1);

            
            
            return  Clients.All.InvokeAsync("setOnlineUsers", onlineUsers);
            
                      
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connToRemove = Context.ConnectionId;

            int remainingConnections = usersAndConnections.Find(x => x.Item2.Contains(connToRemove)).Item2.Count;
            if (remainingConnections == 1)
                usersAndConnections.Remove(usersAndConnections.Find(x => x.Item2.Contains(connToRemove)));
            else
                usersAndConnections.Find(x => x.Item2.Contains(connToRemove)).Item2.Remove(connToRemove);

            List<string> onlineUsers = new List<string>();
            foreach (var user in usersAndConnections)
                onlineUsers.Add(user.Item1);



            Clients.All.InvokeAsync("setOnlineUsers", onlineUsers);

            return base.OnDisconnectedAsync(exception);
        }

        public bool checkOnlineStatusForUser (string userId)
        {
            return usersAndConnections.Exists(x => x.Item1 == userId);
        }

        public async Task Send(string data,int conversationId, int loggedUserId,int participantId)
        {
            var saveTask = _convRepo.SaveToExistingConversation(conversationId, data, loggedUserId);
            var responseTask = Clients.All.InvokeAsync("Send", data, conversationId, loggedUserId, participantId);
            await Task.WhenAll(saveTask, responseTask);
        }

        public Task whoIsTyping(string username)
        {
            IReadOnlyList<string> toExclude = new List<string>() { Context.ConnectionId };
            return Clients.AllExcept(toExclude).InvokeAsync("whoIsTyping", String.Format("{0} is typing...", username));
            
        }



        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).InvokeAsync("JoinGroup", groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Clients.Group(groupName).InvokeAsync("LeaveGroup", groupName);
            await Groups.RemoveAsync(Context.ConnectionId, groupName);
        }

        public void CreateConversation(int loggedUserId, List<int> usersToParticipants, string conversationName)
        {
            _convManipulation.CreateConversation(loggedUserId, usersToParticipants, conversationName);
            
        }

    }   
}


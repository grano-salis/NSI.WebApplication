using Microsoft.AspNetCore.SignalR;
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

        public InitialHub(IConversationsRepository conversationRepository)
        {
            this._convRepo = conversationRepository;
        }

        public Task Send(string data,int conversationId, int loggedUserId,int participantId)
        {
            _convRepo.SaveToExistingConversation(conversationId, data, loggedUserId);
            return Clients.All.InvokeAsync("Send", data,conversationId,loggedUserId,participantId);
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

    }   
}


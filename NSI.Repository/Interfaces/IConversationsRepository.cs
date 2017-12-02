using IkarusEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IConversationsRepository
    {
        IEnumerable<Conversation> GetAllConversations();
        IEnumerable<Message> GetMessagesFromConversation(int conversationId);
        bool CheckIFConversationExists(int conversationId);
        IEnumerable<Participant> GetConversationParticipants(int conversationId);
    }
}

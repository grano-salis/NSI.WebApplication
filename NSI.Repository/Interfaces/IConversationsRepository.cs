using IkarusEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IConversationsRepository
    {
        IEnumerable<Conversation> GetAllConversations();
        ICollection<Message> GetMessagesFromConversation(int conversationId);
        bool CheckIFConversationExists(int conversationId);
        IEnumerable<Participant> GetConversationParticipants(int conversationId);
        Conversation GetConversationById(int id);
        ICollection<Conversation> GetConversationByUserId(int id);
        void SaveToExistingConversation(int conversationId, string message, int loggedUserId);
    }
}

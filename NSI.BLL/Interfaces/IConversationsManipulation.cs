using IkarusEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IConversationsManipulation
    {
        IEnumerable<Conversation> GetAllConversations();
        IEnumerable<Message> GetMessagesFromConversation(int conversationId);
        bool CheckIfConversationExists(int conversationId);
        IEnumerable<Participant> GetConversationParticipants(int conversationId);
        ICollection<Conversation> GetConversationForUser(int userId);
    }
}

using IkarusEntities;
using NSI.DC.Conversations;
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
        System.Threading.Tasks.Task SaveToExistingConversation(int conversationId, string message, int loggedUserId);
        Participant GetParticipantById(int id);
        UserInfo GetUserByIdForConversations(int id);
        int CreateConversation(Conversation conv);
        int CreateParticipant(Participant p);
        int GetLastConversationId();
        int GetLastParticipantId();
        //void AddParticipantsToConversation(int convId, int partId);
    }
}

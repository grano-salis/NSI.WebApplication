using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using IkarusEntities;
using NSI.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace NSI.BLL
{
    public class ConversationsManipulation : IConversationsManipulation
    {
        private readonly IConversationsRepository repository;
        private readonly ILogger<ConversationsManipulation> logger;

        public ConversationsManipulation(IConversationsRepository repository, ILogger<ConversationsManipulation> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public bool CheckIfConversationExists(int conversationId)
        {
            try
            {
                return repository.CheckIFConversationExists(conversationId);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsBLL:CheckIfConversationExists: " + ex.Message);
                throw new Exception(message: "ConversationsBLL:CheckIfConversationExists error", innerException: ex);
            }
        }

        public IEnumerable<Conversation> GetAllConversations()
        {
            try
            {
                return repository.GetAllConversations();
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsBLL:GetAllConversations: " + ex.Message);
                throw new Exception(message: "ConversationsBLL:GetAllConversations error", innerException: ex);
            }
        }

        public IEnumerable<Participant> GetConversationParticipants(int conversationId)
        {
            try
            {
                return repository.GetConversationParticipants(conversationId);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsBLL:GetConversationParticipant: " + ex.Message);
                throw new Exception(message: "ConversationsBLL:GetConversationParticipant error", innerException: ex);
            }
        }

        public IEnumerable<Message> GetMessagesFromConversation(int conversationId)
        {
            try
            {
                return repository.GetMessagesFromConversation(conversationId);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsBLL:GetMessagesFromConversation: " + ex.Message);
                throw new Exception(message: "ConversationsBLL:GetMessagesFromConversation error", innerException: ex);
            }
        }
        public ICollection<Conversation> GetConversationForUser(int userId)
        {
            try
            {
                return repository.GetConversationByUserId(userId);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsBLL: GetConversationForUser: " + ex.Message);
                throw new Exception(message: "ConversationsBLL: GetConversationForUser error", innerException: ex);
            }
        }

        public Participant GetParticipantForId(int participantId)
        {
            try
            {
                return repository.GetParticipantById(participantId);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsBLL: GetParticipantForId: " + ex.Message);
                throw new Exception(message: "ConversationsBLL: GetParticipantForId error", innerException: ex);
            }
        }
    }
}

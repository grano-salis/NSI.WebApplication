using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using IkarusEntities;
using NSI.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using NSI.DC.Conversations;

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

        public Conversation CreateConversation(int loggedUserId, List<int> usersToParticipants, string conversationName)
        {
            UserInfo createdByUser = repository.GetUserByIdForConversations(loggedUserId);
            Conversation conversation = new Conversation()
            {
                ConversationName = conversationName,
                UserId = loggedUserId,
                User = createdByUser                
            };
            int convId = repository.CreateConversation(conversation);

          
            for (int i = 0; i < usersToParticipants.Count; i++)
            {
                var user = repository.GetUserByIdForConversations(usersToParticipants[i]);

                Participant p = new Participant()
                {                    
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsSnoozed = false,
                    IsDeleted = false,
                    LastSeenTime = DateTime.Now,
                    User = user,
                    ConversationId = convId
                };
                repository.CreateParticipant(p);                
            }

            return conversation;
        }

        public List<UserInfo> GetSystemUsers()
        {
            return repository.getSystemUsers();
        }

        public List<Participant> AddParticipantToExistingConversation(int convId, List<int> userIds)
        {
            List<Participant> newParticipants = new List<Participant>();
            for(int i = 0; i < userIds.Count; i++)
            {
                Participant p = repository.addParticipantToExistingConversation(convId, userIds[i]);
                newParticipants.Add(p);
            }
            return newParticipants;
        }
    }
}

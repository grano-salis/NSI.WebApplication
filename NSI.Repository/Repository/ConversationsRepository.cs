using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using IkarusEntities;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NSI.DC.Conversations;
using NSI.DC.Exceptions;

namespace NSI.Repository.Repository
{
    public class ConversationsRepository : IConversationsRepository
    {
        private readonly IkarusContext context;
        private readonly ILogger<ConversationsRepository> logger;

        public ConversationsRepository(IkarusContext context, ILogger<ConversationsRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public bool CheckIFConversationExists(int conversationId)
        {
            try
            {
                return (context.Conversation.Where(x => x.ConversationId == conversationId).FirstOrDefault() != null);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsRepository:GetAllConversations(): " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository", innerException: ex);
            }
        }

        public IEnumerable<Conversation> GetAllConversations()
        {
            try
            {
                var conversations = context.Conversation//.Include(x => x.Message)
                                           .Include("Participant.User")
                                           //x => x.Participant.)
                                           .ToList();


                //conversations.ForEach(x => x.Message.ToList().Sort((z, y) => z.DateCreated.CompareTo(y.DateCreated)));
                conversations.ForEach(x => x.Message = GetMessagesFromConversation(x.ConversationId));
                return conversations;
            }   
            catch(Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsRepository:GetAllConversations(): " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository", innerException: ex);
            }
        }

        public IEnumerable<Participant> GetConversationParticipants(int conversationId)
        {
            try
            {
                return context.Participant.Include("User").Where(x => x.ConversationId == conversationId).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsRepository:GetConversationParticipants: " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository:GetConversationParticipants", innerException: ex);
            }
        }

        public ICollection<Message> GetMessagesFromConversation(int conversationId)
        {
            try
            {
                return context.Message.Include("CreatedByUser").Where(x => x.ConversationId == conversationId).OrderBy(x => x.DateCreated).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsRepository:GetMessagesFromConversation(): " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository", innerException: ex);
            }
        }

        public Conversation GetConversationById(int id)
        {
            try {

                return context.Conversation.Where(x => x.ConversationId == id).FirstOrDefault();

            }
            catch (Exception ex) {
                logger.LogError("Something went wrong in ConversationsRepository:GetMessagesFromConversation(): " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository", innerException: ex);
            }
        }

        public ICollection<Conversation> GetConversationByUserId(int id)
        {
            try
            {
                var conversations = context.Conversation.Include("Participant.User").Where(x => x.Participant.Any(y => y.User.UserId == id)).ToList();

                conversations.ForEach(x => x.Message = GetMessagesFromConversation(x.ConversationId));
                return conversations;

               

            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsRepository:GetMessagesFromConversation(): " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository", innerException: ex);
            }
        }

        public Participant GetParticipantById(int id)
        {
            try
            {

                return context.Participant.Include("User").Where(x => x.ParticipantId == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsRepository:GetMessagesFromConversation(): " + ex.Message);
                throw new Exception(message: "Error in ConversationsRepository", innerException: ex);
            }
        }

        public async System.Threading.Tasks.Task SaveToExistingConversation(int conversationId, string message, int loggedUserId)
        {
            Message m = new Message();
            m.ConversationId = conversationId;
            m.Message1 = message;
            m.CreatedByUserId = loggedUserId;
            m.DateCreated = DateTime.Now;
           // m.MessageId = context.Message.Last().MessageId;
          //  m.MessageId += 1;

            context.Message.Add(m);
            await context.SaveChangesAsync();
        }

        public UserInfo GetUserByIdForConversations(int id)
        {
            try
            {
                var user = context.UserInfo.Where(x => x.UserId == id).First();
                return user;
            }
            catch (Exception e)
            {
                throw new NSIException("No user found!");
            }
           
        }

        public int CreateConversation(Conversation conv)
        {
            context.Conversation.Add(conv); 
            context.SaveChanges();

            return conv.ConversationId;

        }

        public int CreateParticipant(Participant p)
        {
            context.Participant.Add(p);
            context.SaveChanges();
            return p.ParticipantId;
        }

        public int GetLastConversationId()
        {
            return context.Conversation.Last().ConversationId;
        }

        public int GetLastParticipantId()
        {
            return context.Participant.Last().ParticipantId;
        }

        public List<UserInfo> getSystemUsers()
        {
            return context.UserInfo.ToList();
        }
    }
}

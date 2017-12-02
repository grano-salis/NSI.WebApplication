using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using NSI.DC.Conversations;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/conversations/{conversationId}/messages")]
    public class MessagesController : Controller
    {
        private readonly IConversationsManipulation conversationManipulation;
        private readonly ILogger<MessagesController> logger;
        private readonly IMapper mapper;

        public MessagesController(IConversationsManipulation conversationManipulation, ILogger<MessagesController> logger, IMapper mapper)
        {
            this.conversationManipulation = conversationManipulation;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMessages(int conversationId)
        {
            try
            {
                bool conversationExists = conversationManipulation.CheckIfConversationExists(conversationId);

                if (!conversationExists)
                    return NotFound();

                var conversationMessages = conversationManipulation.GetMessagesFromConversation(conversationId);

                var messagesDto = mapper.Map<IEnumerable<MessageGetDTO>>(conversationMessages);

                return Ok(messagesDto);
            }
            catch (Exception ex)
            {
                logger.LogError("MessageController error: " + ex.Message);
                return StatusCode(500);

            }
        }
    }
}
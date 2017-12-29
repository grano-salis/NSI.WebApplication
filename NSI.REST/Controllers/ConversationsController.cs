using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSI.BLL.Interfaces;
using AutoMapper;
using NSI.DC.Conversations;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/conversations")]
    public class ConversationsController : Controller
    {
        private readonly IConversationsManipulation conversationManipulation;
        private readonly ILogger<ConversationsController> logger;
        private readonly IMapper mapper;

        public ConversationsController(IConversationsManipulation conversationManipulation, 
                                        ILogger<ConversationsController> logger,
                                        IMapper mapper)
        {
            this.conversationManipulation = conversationManipulation;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetConversations()
        {
            try
            {
                var conversations = mapper.Map<IEnumerable<ConversationGetDTO>>(conversationManipulation.GetAllConversations());
                return Ok(conversations);
            }
            catch(Exception ex)
            {
                logger.LogError("Something went wrong in ConversationsController" + ex.Message);
                return StatusCode(500);
            }
        }
    }
}
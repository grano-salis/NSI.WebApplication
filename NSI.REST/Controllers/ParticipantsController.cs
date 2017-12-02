using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.Repository.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSI.DC.Conversations;
using NSI.BLL.Interfaces;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/conversations/{conversationId}/participants")]
    public class ParticipantsController : Controller
    {
        private readonly IConversationsManipulation conversationsManipulation;
        private readonly ILogger<ParticipantsController> logger;
        private readonly IMapper mapper;

        public ParticipantsController(IConversationsManipulation conversationsManipulation, ILogger<ParticipantsController> logger, IMapper mapper)
        {
            this.conversationsManipulation = conversationsManipulation;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetConversationParticipants(int conversationId)
        {
            try
            {
                bool conversationExists = conversationsManipulation.CheckIfConversationExists(conversationId);

                if (!conversationExists)
                    return NotFound();

                var participants = conversationsManipulation.GetConversationParticipants(conversationId);

                return Ok(mapper.Map<IEnumerable<ParticipantGetDTO>>(participants));
            }
            catch (Exception ex)
            {
                logger.LogError("ParticipantController:GetConversationParticipants error: " + ex.Message);
                return StatusCode(500);
            }
        }
    }
}
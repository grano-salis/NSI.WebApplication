using System;
using NSI.BLL;
using NSI.DC.ClientsRepository;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NSI.BLL.Interfaces;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/client")]
    public class ClientsController: Controller
    {
        IClientManipulation _clientManipulation { get; set; }

        public ClientsController (IClientManipulation clientManipulation){

            _clientManipulation = clientManipulation;
        }

        [HttpGet("{id}")]
        public ActionResult GetClientById(int id)
        {
            return Ok(_clientManipulation.GetClientById(id));
        }

        [HttpGet("all")]
        public ActionResult GetAllClients()
        {
            return Ok(_clientManipulation.GetAllClients());
        }

        [HttpGet]
        public ActionResult GetClients()
        {
            return Ok(_clientManipulation.GetClients());
        }

        [HttpPost]
        public ActionResult CreateNewClient(ClientDTO clientDto)
        {
            return Ok(_clientManipulation.CreateClient(clientDto));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int id)
        {
            return Ok(_clientManipulation.DeleteClientById(id));
        }

        [HttpPut]
        public ActionResult EditClient(ClientDTO clientDto)
        {
            return Ok(_clientManipulation.EditClient(clientDto));
        }

        [HttpPost("search")]
        public ActionResult SearchClients([FromBody]ClientSearchDTO clientSearch)
        {
            return Ok(_clientManipulation.SearchClients(clientSearch));
        }
    }
}

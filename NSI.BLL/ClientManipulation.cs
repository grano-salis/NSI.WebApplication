using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.DC.ClientsRepository;

namespace NSI.BLL
{
    public partial class ClientManipulation:IClientManipulation
    {
        private readonly IClientRepository _clientRepository;

        public ClientManipulation(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientDto CreateClient(ClientDto clientDTO)
        {
           return _clientRepository.CreateClient(clientDTO);
        }

        public bool DeleteClientById(int clientId)
        {
            return _clientRepository.DeleteClientById(clientId);
        }

        public bool EditClient(ClientDto clientDTO)
        {
            return _clientRepository.EditClient(clientDTO); 
        }

        public ClientDto GetClientById(int clientId)
        {
            return _clientRepository.GetClientById(clientId);
        }

        public ICollection<ClientDto> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public ICollection<ClientDto> GetClients()
        {
            return _clientRepository.GetClients();    
        }

        public ICollection<ClientDto> SearchClients(ClientSearchDTO searchCriteria)
        {
            return _clientRepository.SearchClients(searchCriteria);
        }
    }
}

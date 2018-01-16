using System;
using System.Collections.Generic;
using NSI.DC.ClientsRepository;

namespace NSI.Repository.Interfaces
{
    public interface IClientRepository
    {
        ClientDto CreateClient(ClientDto clientDTO);
        ClientDto GetClientById(int clientId);
        ICollection<ClientDto> GetAllClients();
        ICollection<ClientDto> GetClients();
        bool DeleteClientById(int clientId);
        bool EditClient(ClientDto clientDTO);
        ICollection<ClientDto> SearchClients(ClientSearchDTO searchClient);
    }
}

using System;
using NSI.DC.ClientsRepository;
using System.Collections.Generic;

namespace NSI.BLL.Interfaces
{
    public interface IClientManipulation
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

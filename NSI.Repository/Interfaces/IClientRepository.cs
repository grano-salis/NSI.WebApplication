using System;
using System.Collections.Generic;
using NSI.DC.ClientsRepository;

namespace NSI.Repository.Interfaces
{
    public interface IClientRepository
    {
        ClientDTO CreateClient(ClientDTO clientDTO);
        ICollection<ClientDTO> SearchClient(ClientDTO searchCriteria);
        ClientDTO GetClientById(int clientId);
        ICollection<ClientDTO> GetClients();
        bool DeleteClientById(int clientId);
        bool EditClient(int clientId, ClientDTO clientDTO);
    }
}

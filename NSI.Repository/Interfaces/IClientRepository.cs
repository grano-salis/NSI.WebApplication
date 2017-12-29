using System;
using System.Collections.Generic;
using NSI.DC.ClientsRepository;

namespace NSI.Repository.Interfaces
{
    public interface IClientRepository
    {
        ClientDTO CreateClient(ClientDTO clientDTO);
        ClientDTO GetClientById(int clientId);
        ICollection<ClientDTO> GetAllClients();
        ICollection<ClientDTO> GetClients();
        bool DeleteClientById(int clientId);
        bool EditClient(ClientDTO clientDTO);
        ICollection<ClientDTO> SearchClients(ClientSearchDTO searchClient);
    }
}

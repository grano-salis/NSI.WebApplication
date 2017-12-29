using System;
using NSI.DC.ClientsRepository;
using System.Collections.Generic;

namespace NSI.BLL.Interfaces
{
    public interface IClientManipulation
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

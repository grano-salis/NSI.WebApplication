using System;
using NSI.DC.ClientsRepository;
using System.Collections.Generic;

namespace NSI.BLL.Interfaces
{
    public interface IClientManipulation
    {
        ClientDTO CreateClient(ClientDTO clientDTO);
        //ICollection<ClientDTO> SearchClient(ClientDTO searchCriteria);
        ClientDTO GetClientById(int clientId);
        ICollection<ClientDTO> GetClients();
        bool DeleteClientById(int clientId);
        bool EditClient(int clientId, ClientDTO clientDTO);
    }
}

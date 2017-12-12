using System;
using System.Linq;
using System.Collections.Generic;
using NSI.DC.ClientsRepository;
using NSI.Repository.Interfaces;
using IkarusEntities;

namespace NSI.Repository.Repository
{
    public partial class ClientRepository:IClientRepository
    {

        private readonly IkarusContext _dbContext;

        public ClientRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClientDTO CreateClient(ClientDTO clientDTO)
        {
            try
            {
                var client = Mappers.ClientRepository.MapToDbEntity(clientDTO);
                client.DateCreated = DateTime.Now;
                client.DateModified = null;
                _dbContext.Add(client);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.ClientRepository.MapToDto(client);
                                    
            }
            catch (Exception ex)
            {
                //log ex
                throw ex.InnerException;
                //throw new Exception("Database error!");
            }
            return null;
        }

        public bool DeleteClientById(int clientId)
        {
            try
            {
                var client = _dbContext.Client.FirstOrDefault(x => x.ClientId == clientId);
                if(client != null){
                    client.IsDeleted = true;
                    _dbContext.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                Console.WriteLine(ex.InnerException);
                throw new Exception("Database error!");
            }
        }

        public bool EditClient(int clientId, ClientDTO clientDTO)
        {
            try
            {
                var client = _dbContext.Client.FirstOrDefault(x => x.ClientId == clientId);
                if (client != null)
                {
                    client.ClientName = clientDTO.ClientName ?? client.ClientName;
                    client.DateModified = clientDTO.DateModified ?? DateTime.Now;
                    client.IsDeleted = clientDTO.IsDeleted ?? client.IsDeleted;
                    client.ClientTypeId = clientDTO.ClientTypeId;
                    client.CustomerId = clientDTO.CustomerId;
                    client.AddressId = clientDTO.AddressId;

                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                Console.WriteLine(ex.InnerException);
                throw new Exception("Database error!");
            }
        }

        public ClientDTO GetClientById(int clientId)
        {
            try
            {
                var client = _dbContext.Client.FirstOrDefault(x=>x.ClientId == clientId);
                if (client != null){
                    return Mappers.ClientRepository.MapToDto(client);
                }
            }
            catch (Exception ex)
            {
                //log ex
                Console.WriteLine(ex.InnerException);
                throw new Exception("Database error!");
            }
            return null;
        }

        public ICollection<ClientDTO> GetClients()
        {
            try
            {
                var client = _dbContext.Client;
                if (client != null){
                    ICollection<ClientDTO> clients = new List<ClientDTO>();
                    foreach (var item in client){
                        clients.Add(Mappers.ClientRepository.MapToDto(item));
                    }
                    return clients;
                }
                    
            }
            catch (Exception ex)
            {
                //log ex
                Console.WriteLine(ex.InnerException);
                throw new Exception("Database error!");
            }
            return null;
        }

        public ICollection<ClientDTO> SearchClient(ClientDTO searchCriteria)
        {
            throw new NotImplementedException();
        }
    }
}

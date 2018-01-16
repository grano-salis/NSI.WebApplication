using System;
using System.Linq;
using System.Collections.Generic;
using NSI.DC.ClientsRepository;
using NSI.Repository.Interfaces;
using IkarusEntities;
using NSI.DC.Exceptions;
using NSI.DC.Exceptions.Enums;

namespace NSI.Repository.Repository
{
    public partial class ClientRepository : IClientRepository
    {

        private readonly IkarusContext _dbContext;

        public ClientRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClientDto CreateClient(ClientDto clientDTO)
        {
            try
            {
                var client = Mappers.ClientRepository.MapToDbEntity(clientDTO);
                client.ClientId = null;
                client.DateCreated = DateTime.Now;
                client.DateModified = null;
                client.IsDeleted = false;
                _dbContext.Add(client);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.ClientRepository.MapToDto(client);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
            return null;
        }

        public bool DeleteClientById(int clientId)
        {
            try
            {
                var client = _dbContext.Client.FirstOrDefault(x => x.ClientId == clientId);
                if (client != null)
                {
                    client.IsDeleted = true;
                    _dbContext.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
        }

        public bool EditClient(ClientDto clientDTO)
        {
            try
            {
                var client = _dbContext.Client.FirstOrDefault(x => x.ClientId == clientDTO.ClientId);
                if (client != null)
                {
                    client.ClientName = clientDTO.ClientName ?? client.ClientName;
                    client.DateModified = DateTime.Now;
                    client.IsDeleted = clientDTO.IsDeleted ?? client.IsDeleted;
                    client.ClientTypeId = clientDTO.ClientTypeId ?? client.ClientTypeId;
                    client.CustomerId = clientDTO.CustomerId ?? client.CustomerId;
                    client.AddressId = clientDTO.AddressId ?? client.AddressId;

                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
        }

        public ClientDto GetClientById(int clientId)
        {
            try
            {
                var client = _dbContext.Client.FirstOrDefault(x => x.ClientId == clientId);
                if (client != null)
                {
                    return Mappers.ClientRepository.MapToDto(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
            return null;
        }

        public ICollection<ClientDto> GetAllClients()
        {
            try
            {
                var client = _dbContext.Client;
                if (client != null)
                {
                    ICollection<ClientDto> clients = new List<ClientDto>();
                    foreach (var item in client)
                    {
                        if (item.IsDeleted == true)
                        {
                            clients.Add(Mappers.ClientRepository.MapToDto(item));
                        }
                    }
                    return clients;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
            return null;
        }

        public ICollection<ClientDto> GetClients()
        {
            try
            {
                var client = _dbContext.Client.Where(x => x.IsDeleted.Equals(false));
                if (client != null)
                {
                    ICollection<ClientDto> clients = new List<ClientDto>();
                    foreach (var item in client)
                    {
                        clients.Add(Mappers.ClientRepository.MapToDto(item));
                    }
                    return clients;
                }

            }
            catch (Exception ex)
            {
                //log ex
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
            return null;
        }

        public ICollection<ClientDto> SearchClients(ClientSearchDTO searchClient)
        {
            try
            {
                var client = _dbContext.Client.Where(x => searchQuery(x, searchClient));
                Console.WriteLine(client);
                if (client != null)
                {
                    ICollection<ClientDto> clientDto = new List<ClientDto>();
                    foreach (var item in client)
                    {
                        clientDto.Add(Mappers.ClientRepository.MapToDto(item));
                    }
                    Console.WriteLine(clientDto);
                    return clientDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new NSIException("Database error!", Level.Error, ErrorType.InvalidParameter);
            }
            return null;
        }

        public bool searchQuery(Client ClientDTO, ClientSearchDTO clientSearchDto)
        {
            Console.WriteLine(clientSearchDto.AddressId);
            Console.WriteLine(clientSearchDto.CustomerId);
            Console.WriteLine(clientSearchDto.ClientTypeId);
            return (ClientDTO.ClientName.Contains(clientSearchDto.ClientName) || String.IsNullOrWhiteSpace(clientSearchDto.ClientName)) &&
                (ClientDTO.DateCreated >= clientSearchDto.FromCreated || clientSearchDto.FromCreated.Equals(null)) &&
                (ClientDTO.DateCreated <= clientSearchDto.ToCreated || clientSearchDto.ToCreated.Equals(null)) &&
                (ClientDTO.DateModified >= clientSearchDto.FromModified || clientSearchDto.FromModified.Equals(null)) &&
                (ClientDTO.DateModified <= clientSearchDto.ToModified || clientSearchDto.FromModified.Equals(null)) &&
                (ClientDTO.AddressId == clientSearchDto.AddressId || clientSearchDto.AddressId.Equals(null)) &&
                (ClientDTO.CustomerId == clientSearchDto.CustomerId || clientSearchDto.CustomerId.Equals(null)) &&
                (ClientDTO.ClientTypeId == clientSearchDto.ClientTypeId || clientSearchDto.ClientTypeId.Equals(null));
        }
    }
}

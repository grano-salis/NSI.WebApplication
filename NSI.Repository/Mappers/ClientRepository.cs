﻿using System;
using IkarusEntities;
using NSI.DC.ClientsRepository;

namespace NSI.Repository.Mappers
{
    public class ClientRepository
    {
        public static Client MapToDbEntity(ClientDto clientDTO){
            return new Client()
            {
                ClientId = clientDTO.ClientId,
                ClientName = clientDTO.ClientName,
                DateCreated = clientDTO.DateCreated,
                DateModified = clientDTO.DateModified,
                IsDeleted = clientDTO.IsDeleted,
                ClientTypeId = clientDTO.ClientTypeId,
                CustomerId = clientDTO.CustomerId,
                AddressId = clientDTO.AddressId,
                CreatedByUserId = clientDTO.CreatedByUserId
            };
        }
        public static ClientDto MapToDto(Client client){
            return new ClientDto()
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                DateCreated = client.DateCreated,
                DateModified = client.DateModified,
                IsDeleted = client.IsDeleted,
                ClientTypeId = client.ClientTypeId,
                CustomerId = client.CustomerId,
                AddressId = client.AddressId,
                CreatedByUserId = client.CreatedByUserId
            };
        }
    }
}

using IkarusEntities;
using NSI.DC.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public partial class UserInfoRepository
    {
        public static UserInfo MapToDbEntity(UserInfoDto userInfoDto)
        {
            return new UserInfo()
            {
                AvatarPath = userInfoDto.AvatarPath,
                CustomerId = userInfoDto.CustomerId,
                DateCreated = userInfoDto.DateCreated,
                DateModified = userInfoDto.DateModified,
                Email = userInfoDto.Email,
                FirstName = userInfoDto.FirstName,
                IsDeleted = userInfoDto.IsDeleted,
                LastName = userInfoDto.LastName,
                UserId = userInfoDto.UserId,
                Username = userInfoDto.Username
            };
        }

        public static UserInfoDto MapToDto(UserInfo userInfo)
        {
            return new UserInfoDto()
            {
                AvatarPath = userInfo.AvatarPath,
                CustomerId = userInfo.CustomerId,
                DateCreated = userInfo.DateCreated,
                DateModified = userInfo.DateModified,
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                IsDeleted = userInfo.IsDeleted,
                LastName = userInfo.LastName,
                UserId = userInfo.UserId,
                Username = userInfo.Username
            };
        }
    }
}

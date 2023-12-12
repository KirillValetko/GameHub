﻿using AutoMapper;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Infrastructure.DbSettings;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;

namespace GameHub.DAL.Repositories
{
    public class UserRepository : 
        BaseRepository<User, UserDataModel, UserFilter>,
        IUserRepository
    {
        public UserRepository(IMapper mapper,
            IOptions<UsersCollectionSettings> settings) : base(mapper, settings)
        {
        }

        protected override IMongoQueryable<User> AddFilterConditions(IMongoQueryable<User> source, UserFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                source = source.Where(u => u.UserName.Contains(filter.UserName));
            }

            if (!string.IsNullOrEmpty(filter.Login))
            {
                source = source.Where(u => u.Login.Equals(filter.Login));
            }

            return source;
        }
    }
}

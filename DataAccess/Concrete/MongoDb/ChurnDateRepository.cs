﻿using System;
using System.Linq;
using Core.DataAccess;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Core.DataAccess.MongoDb.Concrete;
namespace DataAccess.Concrete.MongoDb
{
    public class ChurnDateRepository : MongoDbRepositoryBase<ChurnDate>, IChurnDateRepository
    {
        public ChurnDateRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}
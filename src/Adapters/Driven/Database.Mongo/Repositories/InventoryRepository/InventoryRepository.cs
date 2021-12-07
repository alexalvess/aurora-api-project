using DataBase.Mongo.Abstractions.Repositories;
using DataBase.Mongo.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Repositories.InventoryRepository;

public class InventoryRepository : MongoRepository, IInventoryRepository
{
    public InventoryRepository(IMongoContext mongoContext)
        : base(mongoContext) { }
}
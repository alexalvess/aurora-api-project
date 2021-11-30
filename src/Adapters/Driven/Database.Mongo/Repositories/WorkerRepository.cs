using DataBase.Mongo.Abstractions.Repositories;
using DataBase.Mongo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Mongo.Repositories;

public class WorkerRepository : MongoRepository, IWorkerRepository
{
    public WorkerRepository(IMongoContext mongoContext)
        : base(mongoContext) { }
}

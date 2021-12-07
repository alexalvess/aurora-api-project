using DataBase.Mongo.Abstractions.Repositories;
using DataBase.Mongo.Context;

namespace DataBase.Mongo.Repositories.OperatorRepository;

public class OperatorRepository : MongoRepository, IOperatorRepository
{
    public OperatorRepository(IMongoContext mongoContext)
        : base(mongoContext) { }
}

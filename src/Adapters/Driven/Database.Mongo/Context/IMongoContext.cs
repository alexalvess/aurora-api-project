﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Mongo.Context;

public interface IMongoContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<TCollection> GetCollection<TCollection>();
}
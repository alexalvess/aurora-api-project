using Domain.Abstractions.ValueTypes;
using Domain.ValueTypes;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Serializers
{
    public class StructBsonSerializer<T> : IBsonSerializer
        where T : struct
    {
        public Type ValueType { get => typeof(T); }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            bsonReader.ReadStartDocument();
            var value = bsonReader.ReadString();
            bsonReader.ReadEndDocument();

            IValueType<string> obj = new T() as IValueType<string>;
            obj.Create(value);

            return obj;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var newVaule = value as IValueType<string>;

            if(newVaule is not null)
            {
                context.Writer.WriteStartDocument();
                context.Writer.WriteString(newVaule.GetType().Name.ToLowerInvariant(), newVaule.Value);
                context.Writer.WriteEndDocument();
            }
        }
    }
}

using Domain.Abstractions.ValueTypes;
using Domain.ValueTypes;
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
    public class StructBsonSerializer : IBsonSerializer
    {
        public Type ValueType { get => typeof(Name); }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var newVaule = value as IValueType<string>;

            if(newVaule is not null)
            {
                context.Writer.WriteStartDocument();
                context.Writer.WriteName(newVaule.GetType().Name.ToLowerInvariant());
                BsonSerializer.Serialize(context.Writer, newVaule.Value.GetType(), newVaule.Value);
                context.Writer.WriteEndDocument();
            }
        }
    }
}

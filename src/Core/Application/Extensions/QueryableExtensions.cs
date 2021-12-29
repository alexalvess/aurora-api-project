using Application.Envelop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace Application.Extensions;

public static class QueryableExtensions
{
    public static void Bind(this Envelop.Queryable queryableScoped, Envelop.Queryable queryable)
    {
        queryableScoped.Filter = queryable.Filter;
        queryableScoped.Page = queryable.Page;
        queryableScoped.Fields = queryable.Fields;
        queryableScoped.Sort = queryable.Sort;
        queryableScoped.Search = queryable.Search;
        queryableScoped.Wrap = queryable.Wrap;
    }

    public static object SelectFields(this Envelop.Queryable queryable, object response)
    {
        if (queryable.Fields?.Any() ?? false)
        {
            var jsonResult = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            var jToken = JToken.Parse(jsonResult);

            if (jToken is JObject)
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(jsonResult);

                foreach (var fieldToRemove in queryable.Fields)
                    jObject.Property(fieldToRemove).Remove();

                return jObject;
            }
            else
            {
                var jArray = JsonConvert.DeserializeObject<JArray>(jsonResult);
                var newJArray = new JArray();


                jArray.Select(item => (JObject)item)
                    .ToList()
                    .ForEach(field =>
                    {
                        var newJObject = new JObject();
                        queryable.Fields.ForEach(fieldToAdd =>
                        {
                            var jToken = field.SelectToken(fieldToAdd);

                            if (fieldToAdd.Contains('.'))
                                BuildJOBject(fieldToAdd.Split("."), jToken, newJObject);
                            else
                                newJObject.Add(fieldToAdd, jToken);

                        });
                        newJArray.Add(newJObject);
                    });

                return newJArray;
            }
        }

        return default;
    }

    private static JObject BuildJOBject(string[] fields, JToken jToken, JObject jObject)
    {
        if (fields.Length == 1)
        {
            var newJObject = new JObject();
            newJObject.Add(fields.First(), jToken);
            return newJObject;
        }

        jObject.Add(fields.First(), BuildJOBject(fields.Skip(1).ToArray(), jToken, jObject));

        return jObject;
    }
}

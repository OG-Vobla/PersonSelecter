using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSelecter.Other
{
    public class Buff
    {
        public Buff(string name, string featureName, int value)
        {
            Name = name;
            FeatureName = featureName;
            Value = value;
        }

        [BsonId]
        [BsonIgnoreIfDefault]
        ObjectId _id;
        public string Name { get; set; }
        public string FeatureName { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            
            return $"{FeatureName}+{Value}";
        }

    }
}

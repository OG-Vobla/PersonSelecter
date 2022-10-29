using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonSelecter.Other
{
    public class Criterion : Buff
    {
        public Criterion(string name, string featureName, int value) : base(name, featureName, value)
        {
            Name = name;
            FeatureName = featureName;
            Value = value;
        }

        public override string ToString()
        {
            return $"{FeatureName}={Value}";
        }

    }
}

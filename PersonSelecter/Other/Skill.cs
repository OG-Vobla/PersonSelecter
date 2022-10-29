using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSelecter.Other
{
    public class Skill
    {
        public Skill(string SkillName, List<Buff> buffs, string description)
        {
            this.SkillName = SkillName;
            Buffs = buffs;
            Description = description;
        }
        [BsonId]
        [BsonIgnoreIfDefault]
        ObjectId _id;
        public List<Buff> Buffs { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public string GetDescription()
        {
            string str = $"{(Description == null ? "Описание отсутвует" : Description)} \nБаффы:\n";
            foreach (var i in Buffs)
            {
                str += $"\t{i.FeatureName} + {i.Value}\n";
            }
           
            return str;
        }
    }
}

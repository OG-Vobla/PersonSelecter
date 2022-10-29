﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSelecter.Other
{
    public class Item
    {
        public Item(string itemName, List<Buff> buffs, string type, string description)
        {
            ItemName = itemName;
            Buffs = buffs;
            Type = type;
            Description = description;
        }
        [BsonId]
        [BsonIgnoreIfDefault]
        ObjectId _id;
        public string Type { get; set; }    
        public List<Buff> Buffs { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string GetDescription()
        {
            string str = $"{(Description == null || Description == "" ? "Описание отсутвует" : Description)} \nБаффы:\n";
            foreach (var i in Buffs)
            {
                str += $"\t{i.FeatureName} + {i.Value}\n";
            }
           
            return str;
        }
    }
}

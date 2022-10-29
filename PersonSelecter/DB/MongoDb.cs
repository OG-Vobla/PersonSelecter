
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonSelecter.PersonClass;
using System.Windows.Controls;
using PersonSelecter.Other;

namespace PersonSelecter.DB
{
    public class MongoDb
    {
        static MongoClient client;
        static IMongoDatabase PersonDatabase;
        static IMongoCollection<Person> PersonCollection;
        static IMongoDatabase ItemDatabase;
        static IMongoCollection<Item> ItemCollection;
        static IMongoCollection<Skill> SkillCollection;
        static IMongoCollection<Buff> BuffCollection;
        static IMongoCollection<Criterion> CriterionCollection;
        static MongoDb()
        {
            client = new MongoClient();
            PersonDatabase = client.GetDatabase("PersonDB");
            PersonCollection = PersonDatabase.GetCollection<Person>("Persons");
            ItemDatabase = client.GetDatabase("Items");
            ItemCollection = ItemDatabase.GetCollection<Item>("Items");
            SkillCollection = ItemDatabase.GetCollection<Skill>("Skills");
            BuffCollection = ItemDatabase.GetCollection<Buff>("Buffs");
            CriterionCollection = ItemDatabase.GetCollection<Criterion>("Criteria");
        }
        public static void AddPersonToDB(Person newPerson)
        {
            PersonCollection.InsertOne(newPerson);
        }
        public static void AddItemToDB(Item newItem)
        {
            ItemCollection.InsertOne(newItem);
        }
        public static void AddBuffToDB(Buff buff)
        {
            BuffCollection.InsertOne(buff);
        }
        public static void AddSkillToDB(Skill skill)
        {
            SkillCollection.InsertOne(skill);
        }
        public static void AddCriterionToDB(Criterion criterion)
        {
            CriterionCollection.InsertOne(criterion);
        }
        public static List<Criterion> FindAllCriteria()
        {
            return CriterionCollection.Find(x => true).ToList();

        }
        public static List<Person> FindAllPersons()
        {
            return PersonCollection.Find(x => true).ToList();
            
        }
        public static List<Item> FindAllItems()
        {
            return ItemCollection.Find(x => true).ToList();
        }
        public static List<Buff> FindAllBuffs()
        {
            return BuffCollection.Find(x => true).ToList();

        }
        public static List<Skill> FindAllSkills()
        {
            return SkillCollection.Find(x => true).ToList();
        }

        public static Criterion FindCriterion(string name)
        {
            var one = CriterionCollection.Find(x => x.Name == name).FirstOrDefault();
            return one;
        }
        public static Person FindPerson(string name)
        {
            var one = PersonCollection.Find(x => x.Name == name).FirstOrDefault();
            return one;
        }
        public static Item FindItem(string name)
        {
            var one = ItemCollection.Find(x => x.ItemName == name).FirstOrDefault();
            return one;
        }
        public static Buff FindBuff(string name)
        {
            var one = BuffCollection.Find(x => x.Name == name).FirstOrDefault();
            return one;
        }
        public static Skill FindSkill(string name)
        {
            var one = SkillCollection.Find(x => x.SkillName == name).FirstOrDefault();
            return one;
        }
        public static void ReplaceItem(string name, Item item)
        {
            ItemCollection.ReplaceOne(x => x.ItemName == name, item);
        }
        public static void ReplacePerson(string name, Person person)
        {
            PersonCollection.ReplaceOne(x => x.Name == name, person);
     
        }
        public static void ReplaceSkill(string name, Skill skill)
        {
            SkillCollection.ReplaceOne(x => x.SkillName == name, skill);
        }
    }
}
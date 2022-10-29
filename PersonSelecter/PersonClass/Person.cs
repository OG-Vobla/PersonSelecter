using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonSelecter.Other;
using System.ComponentModel;
using System.Windows.Input;

namespace PersonSelecter.PersonClass
{
    [BsonKnownTypes(typeof(Wizard), typeof(Rogue), typeof(Warrior))]
    abstract public class Person
    {

        private int strenght, dexterity, constitution, intelligence;
        private double at, hp, pdet, matt, mp;
        private int MaxStrenght, MaxDexterity, MaxConstitution, MaxIntelligence;
        private int MinStrenght, MinDexterity, MinConstitution, MinIntelligence;
        private string name;
        private int expirience;
        private int lvl;
        private List<Skill> skills;
        private int charPoints;
        private Item wornHelmet;
        private Item wornBreastplate;
        private Item wornSword;
        private Dictionary<String, double> strenghtCalcValue;
        private Dictionary<String, double> dexterityCalcValue;
        private Dictionary<String, double> constitutionCalcValue;
        private Dictionary<String, double> intelligenceCalcValue;
        private List<ItemCount> inventory; 

        public Person()
        {
            Skills = new List<Skill>();
            Inventory = new List<ItemCount>();
            lvl = 1;
            at = 0;
            hp = 0;
            pdet = 0;
            matt = 0;
            mp = 0;
            StrenghtCalcValue = new Dictionary<string, double>()
            {
                {"At",  0},
                {"Hp", 0}
            };
            DexterityCalcValue = new Dictionary<string, double>()
            {
                {"At",  0},
                {"Pdet", 0}
            };
            ConstitutionCalcValue = new Dictionary<string, double>()
            {
                {"Hp", 0},
            };
            IntelligenceCalcValue = new Dictionary<string, double>()
            {
                {"Mp", 0},
                {"Matt", 0}
            };

        }

        [BsonId]
        [BsonIgnoreIfDefault]
        ObjectId _id;

        #region Prop

        public string Name
        { get => name; set => name = value; }
        public int LVL { get => lvl; set => lvl = value; }
        public int CharPoints { get => charPoints; set => charPoints = value; }

        public int Strenght
        { get => strenght; 
            set 
            {
                strenght = value; 
                CalculationStrenght(); 
            }
        }

        public int Dexterity
        { get => dexterity; set
            {
                dexterity = value;
                CalculationDexterity();
            }    
        }

        public int Constitution
        {
            get => constitution; set
            {
                constitution = value;
                CalculationConstitution();
            }
        }

        public int Intelligence
        {
            get => intelligence; set
            {
                intelligence = value;
                CalculationIntelligence();
            }
        }

        public double At
        { get => at; set => at = value; }

        public double HP
        { get => hp; set => hp = value; }

        public double Pdet
        { get => pdet; set => pdet = value; }

        public double Matt
        { get => matt; set => matt = value; }

        public double MP
        { get => mp; set => mp = value; }

        public List<ItemCount> Inventory 
        { get => inventory; set => inventory = value; }
        public Item WornHelmet { get => wornHelmet; set => wornHelmet = value; }
        public Item WornBreastplate { get => wornBreastplate; 
            set 
            { 
                wornBreastplate = value; 

            } 
        }
        public Item WornSword { get => wornSword; set => wornSword = value; }
        public List<Skill> Skills
        { get => skills; set => skills = value; }

        [BsonIgnore]
        public int maxStrenght
        { get => MaxStrenght; set => MaxStrenght = value; }

        [BsonIgnore]
        public int maxDexterity
        { get => MaxDexterity; set => MaxDexterity = value; }

        [BsonIgnore]
        public int maxConstitution
        { get => MaxConstitution; set => MaxConstitution = value; }

        [BsonIgnore]
        public int maxIntelligence
        { get => MaxIntelligence; set => MaxIntelligence = value; }

        [BsonIgnore]
        public int minStrenght
        { get => MinStrenght; set => MinStrenght = value; }

        [BsonIgnore]
        public int minDexterity
        { get => MinDexterity; set => MinDexterity = value; }

        [BsonIgnore]
        public int minConstitution
        { get => MinConstitution; set => MinConstitution = value; }

        [BsonIgnore]
        public int minIntelligence
        { get => MinIntelligence; set => MinIntelligence = value; }
        public int Expirience { get => expirience; set => expirience = value; }
        [BsonIgnore]
        public Dictionary<string, double> StrenghtCalcValue
        { get => strenghtCalcValue; set => strenghtCalcValue = value; }
        [BsonIgnore]
        public Dictionary<string, double> DexterityCalcValue
        { get => dexterityCalcValue; set => dexterityCalcValue = value; }
        [BsonIgnore]
        public Dictionary<string, double> ConstitutionCalcValue
        { get => constitutionCalcValue; set => constitutionCalcValue = value; }
        [BsonIgnore]
        public Dictionary<string, double> IntelligenceCalcValue
        { get => intelligenceCalcValue; set => intelligenceCalcValue = value; }

        #endregion

        public void CalculationStrenght()
        {
            if (strenghtCalcValue != null)
            {
                at = strenghtCalcValue["At"] * strenght;
                hp = strenghtCalcValue["Hp"] * strenght;
            }
        }
        public void CalculationDexterity()
        {
            if (dexterityCalcValue != null)
            {
                pdet = dexterityCalcValue["Pdet"] * dexterity;
                try { at += (dexterityCalcValue["At"] * dexterity); } catch { }
            }
        }
        public void CalculationConstitution()
        {
            if (constitutionCalcValue != null)
            {
                hp += constitutionCalcValue["Hp"] * constitution;
                try { pdet += constitutionCalcValue["Pdet"] * constitution; } catch { }
            }
        }
        public void CalculationIntelligence() 
        {
            if (intelligenceCalcValue != null )
            {
                mp = intelligenceCalcValue["Mp"] * intelligence;
                matt = intelligenceCalcValue["Matt"] * intelligence;
            }
        }
        public void AddItem(Item newItem)
        {
            for (var i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Item.ItemName == newItem.ItemName)
                {
                    Inventory[i] = new ItemCount(Inventory[i].Count + 1, Inventory[i].Item);
                    return;
                }
                
            }
            Inventory.Add(new ItemCount(1, newItem));
        }
        public void AddSkill(Skill skill)
        {
            for (var i = 0; i < Skills.Count; i++)
            {
                if (Skills[i].SkillName == skill.SkillName)
                {
                    return;
                }

            }
            Skills.Add(skill);
            foreach (var buff in skill.Buffs)
            {
                switch (buff.FeatureName)
                {
                    case "At":
                        At += buff.Value;
                        break;
                    case "HP":
                        HP += buff.Value;
                        break;
                    case "Pdet":
                        Pdet += buff.Value;
                        break;
                    case "MP":
                        MP += buff.Value;
                        break;
                    case "Matt":
                        Matt += buff.Value;
                        break;
                }
            }
        }
        public void DropItem(string ItemName)
        {
            for (var i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Item.ItemName == ItemName)
                {
                    if(Inventory[i].Count - 1 <= 0)
                    {
                        Inventory.Remove(Inventory[i]);
                        return;
                    }
                    Inventory[i] = new ItemCount(Inventory[i].Count - 1, Inventory[i].Item);
                    
                }

            }
        }
        public string PrintInfo()
        {
            
            string info = (Name);
            info += ($"\n Strenght - {Strenght} \n Dexterity - {Dexterity} \n Constitution - {Constitution} \n Intelligence - {Intelligence} \n");
            info += ($"At - {at} \n HP - {hp} \n Pdet - {pdet} \n Matt - {matt} \n MP - {mp}");
            return info;
        }
        public void WornItem(Item item)
        {
            switch (item.Type)
            {
                case "Helmet":
                    wornHelmet = item;
                    break;
                case "Sword":
                    wornSword = item;
                    break;
                case "Breastplate":
                    wornBreastplate = item;
                    break;
            }
            foreach(var buff in item.Buffs)
            {
                switch (buff.FeatureName)
                {
                    case "At":
                        At += buff.Value;
                        break;
                    case "HP":
                        HP += buff.Value;
                        break;
                    case "Pdet":
                        Pdet += buff.Value;
                        break;
                    case "MP":
                        MP += buff.Value;
                        break;
                    case "Matt":
                        Matt += buff.Value;
                        break;
                }
            }
        }
        public void TakeOffItem(string Type)
        {
            Item takeOffItem = null;
            switch (Type)
            {
                case "Helmet":
                    takeOffItem = wornHelmet;
                    wornHelmet = null;
                    break;
                case "Sword":
                    takeOffItem = wornSword;
                    wornSword = null;
                    break;
                case "Breastplate":
                    takeOffItem = wornBreastplate;
                    wornBreastplate = null;
                    break;
            }
            foreach (var buff in takeOffItem.Buffs)
            {
                switch (buff.FeatureName)
                {
                    case "At":
                        At -= buff.Value;
                        break;
                    case "HP":
                        HP -= buff.Value;
                        break;
                    case "Pdet":
                        Pdet -= buff.Value;
                        break;
                    case "MP":
                        MP -= buff.Value;
                        break;
                    case "Matt":
                        Matt -= buff.Value;
                        break;
                }
            }
        }
    }
}

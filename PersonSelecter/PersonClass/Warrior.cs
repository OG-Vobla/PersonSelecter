using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSelecter.PersonClass
{
    internal class Warrior : Person
    {
        public Warrior(string newName)
        {
            Name = newName;
            maxStrenght = 250;
            maxDexterity = 70;
            maxConstitution = 100;
            maxIntelligence = 50;
            minStrenght = 30;
            minDexterity = 15;
            minConstitution = 20;
            minIntelligence = 10;
            StrenghtCalcValue = new Dictionary<string, double>()
            {
                {"At",  5},
                {"Hp", 2}
            };
            DexterityCalcValue = new Dictionary<string, double>()
            {
                {"At",  1},
                {"Pdet", 1}
            };
            ConstitutionCalcValue = new Dictionary<string, double>()
            {
                {"Hp", 10},
                {"Pdet", 2}
            };
            IntelligenceCalcValue = new Dictionary<string, double>()
            {
                {"Mp", 1},
                {"Matt", 1}
            };
        }
 
    }
}

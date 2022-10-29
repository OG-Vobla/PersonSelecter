using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSelecter.PersonClass
{
    internal class Rogue : Person
    {
        public Rogue(string newName)
        {
            Name = newName;
            maxStrenght = 55;
            maxDexterity = 250;
            maxConstitution = 80;
            maxIntelligence = 70;
            minStrenght = 15;
            minDexterity = 30;
            minConstitution = 20;
            minIntelligence = 15;
            StrenghtCalcValue = new Dictionary<string, double>()
            {
                {"At",  2},
                {"Hp", 1}
            };
            DexterityCalcValue = new Dictionary<string, double>()
            {
                {"At",  4},
                {"Pdet", 1.5}
            }; 
            ConstitutionCalcValue = new Dictionary<string, double>()
            {
                {"Hp", 6},
            };
            IntelligenceCalcValue = new Dictionary<string, double>()
            {
                {"Mp", 1.5},
                {"Matt", 2}
            };
            

        }

    }
}

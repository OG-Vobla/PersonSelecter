using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSelecter.PersonClass
{
    internal class Wizard : Person
    {
        public Wizard(string newName)
        {
            Name = newName;
            maxStrenght = 45;
            maxDexterity = 70;
            maxConstitution = 60;
            maxIntelligence = 250;
            minStrenght = 10;
            minDexterity = 20;
            minConstitution = 15;
            minIntelligence = 35;
            StrenghtCalcValue = new Dictionary<string, double>()
            {
                {"At",  3},
                {"Hp", 1}
            };
            DexterityCalcValue = new Dictionary<string, double>()
            {
                {"Pdet", 0.5}
            };
            ConstitutionCalcValue = new Dictionary<string, double>()
            {
                {"Hp", 3},
                {"Pdet", 1}
            };
            IntelligenceCalcValue = new Dictionary<string, double>()
            {
                {"Mp", 2},
                {"Matt", 5}
            };
        }
 
    }
}

using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX_Mod
{
    internal class Pr_Apathy : Property
    {
        public List<Challenge> challenges = new List<Challenge>();

        public Pr_Apathy(Location loc) : base(loc)
        {
            charge = 1;
        }

        public override string getName()
        {
            return "Apathy";
        }

        public override List<Challenge> getChallenges()
        {
            return challenges;
        }

        public override void turnTick()
        {
            base.turnTick();
            influences.Add(new ReasonMsg("Despair", 1));
        }

        public override string getDesc()
        {
            return "People in this location are starting to give up. <b>Security</b> will decrease and <b>Hunger</b> will increase over time as the populace stops caring.";
        }

        public override bool deleteOnZero()
        {
            return true;
        }

        public override bool survivesRuin()
        {
            return false;
        }
    }
}

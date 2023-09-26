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
        public Challenge challenge;

        public Pr_Apathy(Location loc, int charge = 1) : base(loc)
        {
            this.charge = charge;

            challenge = new Ch_CombatApathy(loc);
            challenges.Add(challenge);
            challenge = new Ch_PreachApathy(loc);
            challenges.Add(challenge);
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

            // Remove the Supplicant's influence if they are not present.
            foreach(ReasonMsg msg in influences)
            {
                if(msg.msg.Equals("Supplicant's Gospel"))
                {
                    bool pres = false;
                    foreach(Unit unit in location.units.ToList())
                    {
                        if(unit is UAE_Supplicant)
                        {
                            pres = true;
                        }
                    }
                    if(pres == false)
                    {
                        influences.Remove(msg);
                    }
                }
            }
            if(charge >= 100)
            {
                foreach(Location loc in location.getNeighbours())
                {
                    if(loc.settlement != null && loc.settlement.isHuman)
                    {
                        bool flag = false;
                        foreach(Property prop in loc.properties.ToList())
                        {
                            if(prop is Pr_Apathy)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if(!flag)
                        {
                            loc.properties.Add(new Pr_Apathy(loc, 1));
                        }
                    }
                }
            }
        }

        public override double getProsperityInfluence()
        {
            return 0.0 - charge / 100.0;
        }

        public override int getSecurityChange(SettlementHuman hum)
        {
            return (int)(0.0 - Math.Round(charge / 20));
        }

        public override string getDesc()
        {
            return "People in this location are starting to give up. <b>Security</b> will decrease and <b>Hunger</b> will increase over time as the populace stops caring. Once charge is greater than 100%, <b>Apathy</b> can begin spreading to nearby settlements.";
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

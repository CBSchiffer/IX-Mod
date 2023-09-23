using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IX_Mod
{
    internal class Ch_CombatApathy : Challenge
    {

        double charge;
        
        public Ch_CombatApathy(Location loc) : base(loc)
        {
            charge = 0;
        }
        public override double getMenace()
        {
            foreach(Property prop in base.location.properties)
            {
                if(prop is Pr_Apathy ap)
                { 
                    charge = ap.charge;
                    return (1.0 - base.location.getShadow()) * (ap.charge);
                }
            }
            return 0;
        }

        public override string getName()
        {
            return "Combat Apathy";
        }

        public override string getDesc()
        {
            return "Reduces the Apathy in the current location. Complexity increases with Apathy levels. May increase Unrest if Apathy is reduced by a large ammount.";
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.COMMAND;
        }

        public override double getProfile()
        {
            foreach (Property prop in base.location.properties)
            {
                if (prop is Pr_Apathy ap)
                {
                    charge = ap.charge;
                    return ap.charge / 2.0;
                }
            }
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Command", Math.Max(1, unit.getStatCommand())));
            return Math.Max(1, unit.getStatCommand());
        }
        public override double getComplexity()
        {
            return 25 + Math.Round(charge / 2);
        }
        public override Sprite getSprite()
        {
            return map.world.iconStore.reduceUnrest;
        }
        public override int isGoodTernary()
        {
            return 1;
        }
        public override bool validFor(UA ua)
        {
            return true;
        }
        public override int getSimplificationLevel()
        {
            return 0;
        }

        public override void complete(UA u)
        {
            foreach (Property prop in base.location.properties)
            {
                if (prop is Pr_Apathy ap)
                {
                    ap.charge = ap.charge / 2;
                    charge = ap.charge;
                    if(charge >= 50)
                    {
                        bool found = false;
                        foreach(Property prop2 in base.location.properties)
                        {
                            if(prop2 is Pr_Unrest unr)
                            {
                                Property.addToPropertySingleShot("Drop in Apathy", Property.standardProperties.UNREST, 40.0, base.location);
                                found = true;
                            }
                        }
                        if(!found)
                        {
                            Pr_Unrest unrest = new Pr_Unrest(base.location);
                            unrest.charge = 40;
                            base.location.properties.Add(unrest);
                        }
                    }
                    
                }
            }
            
        }

        public override bool valid()
        {
            Society society = base.location.soc as Society;
            if (society != null && (society.isDarkEmpire || society.isOphanimControlled))
            {
                return false;
            }

            return true;
        }

        public override int[] buildPositiveTags()
        {
            return new int[1] { Tags.COOPERATION };
        }

        public override int[] buildNegativeTags()
        {
            return new int[5] { Tags.DISCORD, Tags.AMBITION, Tags.SHADOW, God_Nine.Observe("Apathy"), God_Nine.Observe("IX") };
        }
    }
}

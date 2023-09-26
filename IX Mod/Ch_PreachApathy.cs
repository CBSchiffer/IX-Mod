using Assets.Code;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IX_Mod
{
    internal class Ch_PreachApathy : Challenge
    {
        double charge;

        public Ch_PreachApathy(Location loc) : base(loc)
        {
            charge = 0;
        }
        public override double getMenace()
        {
            foreach (Property prop in base.location.properties.ToList())
            {
                if (prop is Pr_Apathy ap)
                {
                    charge = ap.charge;
                    return (ap.charge) - 50;
                }
            }
            return 50;
        }

        public override string getName()
        {
            return "Preach Apathy";
        }

        public override string getDesc()
        {
            return "Increases the Apathy of the current location. If there is Unrest present, it will decrease the unrest and increase the Apathy further.";
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.COMMAND;
        }

        public override double getProfile()
        {
            foreach (Property prop in base.location.properties.ToList())
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
            return 15;
        }

        public override int getCompletionMenace()
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
            return 35;
        }
        public override Sprite getSprite()
        {
            return map.world.iconStore.unrest;
        }
        public override int isGoodTernary()
        {
            return -1;
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
            foreach (Property prop in base.location.properties.ToList())
            {
                if (prop is Pr_Apathy ap)
                {
                    ap.charge = ap.charge + 25;
                    charge = ap.charge;
                    foreach (Property prop2 in base.location.properties.ToList())
                    {
                        if (prop2 is Pr_Unrest unr)
                        {
                            Property.addToPropertySingleShot("Drop in Apathy", Property.standardProperties.UNREST, -unr.charge / 2, base.location);
                            ap.charge += unr.charge / 4;
                            charge = ap.charge;
                        }
                        
                    }

                }
            }

        }

        public override bool valid()
        {
            return true;
        }

        public override int[] buildPositiveTags()
        {
            return new int[4] { Tags.DISCORD, Tags.SHADOW, God_Nine.Observe("Apathy"), God_Nine.Observe("IX") };
        }

        public override int[] buildNegativeTags()
        {
            return new int[2] { Tags.COOPERATION, Tags.AMBITION };
        }

        
    }
}

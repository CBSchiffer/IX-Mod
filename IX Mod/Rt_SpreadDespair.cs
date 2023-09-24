using Assets.Code;
using DuloGames.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SortedDictionaryProvider;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace IX_Mod
{
    internal class Rt_SpreadDespair : Ritual
    {
        public UA caster;
        public Location loc;
        public Rt_SpreadDespair(Location location, UA parent) : base(location)
        {
            this.caster = parent;
            this.loc = location;
        }

        public override string getName()
        {
            return "Spread Despair";
        }

        public override string getCastFlavour()
        {
            return "The others must know of what is to come.";
        }

        public override string getDesc()
        {
            return "Spreads Apathy to this location. If it is already present, it greatly strenghthens the Apathy.";
        }

        public override string getRestriction()
        {
            return "";
        }

        public override double getProfile()
        {
            return 10.0;
        }

        public override double getMenace()
        {
            return 0.0;
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.COMMAND;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Lore", Math.Max(1, unit.getStatLore())));
            msgs?.Add(new ReasonMsg("Stat: Command", Math.Max(1, unit.getStatCommand())));
            return Math.Max(1, unit.getStatLore()) + Math.Max(1, unit.getStatCommand());
        }

        public override double getComplexity()
        {
            return 40.0;
        }

        public override int getCompletionMenace()
        {
            return 5;
        }

        public override int getCompletionProfile()
        {
            return 10;
        }

        public override bool validFor(UA ua)
        {
            if(loc != null && loc.settlement != null)
            {
                return loc.settlement.isHuman;
            }
            return false;
        }

        public override Sprite getSprite()
        {
            return map.world.iconStore.unrest;
        }

        public override int isGoodTernary()
        {
            return -1;
        }

        public override double getUtility(UA ua, List<ReasonMsg> msgs)
        {
            double utility = base.getUtility(ua, msgs);
            msgs?.Add(new ReasonMsg("Base Reluctance", -20));
            utility -= 20;
            foreach(Trait t in ua.person.traits)
            {
                if(t is T_ApostleIX)
                {
                    utility += 100;
                    msgs?.Add(new ReasonMsg("Is Apostle of IX", 100));
                    bool flag = false;
                    foreach(Property p in loc.properties)
                    {
                        if(p is Pr_Apathy ap)
                        {
                            flag = true;
                            msgs?.Add(new ReasonMsg("Apathy Present", ap.charge));
                            utility += ap.charge;
                        }
                    }
                    if(!flag)
                    {
                        msgs?.Add(new ReasonMsg("No Apathy Yet", 50));
                        utility += 50;
                    }
                }
            }

            return utility;
        }

        public override void complete(UA cast)
        {
            map.addUnifiedMessage(cast, cast, "Spread Despair", cast.getName() + " has spread the gospel of IX to " + loc.getName() + ", increasing the Apathy present.", "APATHY SPREAD", force: true);
            foreach (Property p in loc.properties)
            {
                if (p is Pr_Apathy ap)
                {
                    ap.charge *= 1.5;
                }
            }
        }

        public override bool valid()
        {
            bool flag = true;
            foreach (Property p in loc.properties)
            {
                if(p is Pr_Apathy ap)
                {
                    return (ap.charge > 0 && ap.charge < 100);
                }
            }
            return loc.settlement.isHuman;
        }

        public override int[] buildPositiveTags()
        {
            return new int[3] { Tags.SHADOW, God_Nine.Observe("IX"), God_Nine.Observe("Apathy") };
        }

        public override int[] buildNegativeTags()
        {
            return new int[2] { Tags.COOPERATION, Tags.AMBITION };
        }
    }
}

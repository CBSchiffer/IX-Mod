using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IX_Mod
{
    internal class Rt_WellShadow : Ritual
    {
        public SettlementHuman hum;

        public Rt_WellShadow(Location loc, SettlementHuman hum)
            : base(loc)
        {
            this.hum = hum;
        }

        public override string getName()
        {
            return "Well of Shadows";
        }

        public override string getDesc()
        {
            return "Starts a Well of Shadows at this location, which spreads <b>shadow</b> to all neighbouring human settlement with lower shadow";
        }

        public override string getCastFlavour()
        {
            return "The darkness flows across the land, like a malign lake, drowning the world in its eerie gloom, blotting out the sun and driving the people slowly into oblivion, their minds collapsing and the souls putrifying.";
        }

        public override string getRestriction()
        {
            return "Requires a location with shadow > 10% and Well of Shadows modifier lower than 100%";
        }

        public override double getProfile()
        {
            return map.param.ch_wellofshadows_aiProfile;
        }

        public override double getMenace()
        {
            if (base.location.settlement == null)
            {
                return map.param.ch_wellofshadows_aiMenace;
            }

            double num = 0.0;
            foreach (Link link in base.location.links)
            {
                if (link.other(base.location).settlement is SettlementHuman)
                {
                    Society society = link.other(base.location).soc as Society;
                    if (society == null || !society.isAlliance || map.opt_allianceState != 1)
                    {
                        num += Math.Max(0.0, base.location.settlement.shadow - link.other(base.location).settlement.shadow);
                    }
                }
            }

            if (num == 0.0)
            {
                return map.param.ch_wellofshadows_parameterValue2;
            }

            return 30.0 * num;
        }

        public override Sprite getSprite()
        {
            return map.world.iconStore.wellOfShadows;
        }

        public override challengeStat getChallengeType()
        {
            return challengeStat.LORE;
        }

        public override bool validFor(UA ua)
        {
            return true;
        }

        public override bool valid()
        {
            if (map.tutorial && map.tutorialState < ManagerTutorial.STATE_VICTORY && map.overmind.god is God_Tutorial1)
            {
                return false;
            }
            foreach(Property prop in location.properties.ToList())
            {
                if(prop is Pr_WellOfShadows shad)
                {
                    return hum.shadow >= 0.1 && shad.charge < 80;
                }
            }
            return hum.shadow >= 0.1;
        }

        public override int isGoodTernary()
        {
            return -1;
        }

        public override int getCompletionProfile()
        {
            return map.param.ch_wellofshadows_parameterValue4;
        }

        public override int getCompletionMenace()
        {
            return map.param.ch_wellOfShadowsMenaceGain;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Lore", Math.Max(1, unit.getStatLore())));
            return Math.Max(1, unit.getStatLore());
        }

        public override double getComplexity()
        {
            if (map.tutorial)
            {
                return map.param.ch_wellofshadows_complexity;
            }

            return map.param.ch_wellOfShadowsComplexity;
        }

        public override int getSimplificationLevel()
        {
            return 0;
        }

        public override double getUtility(UA ua, List<ReasonMsg> msgs)
        {
            double utility = base.getUtility(ua, msgs);
            msgs?.Add(new ReasonMsg("base Reluctance", -50));
            utility -= 50;
            utility += 100.0;
            utility += (1 - location.getShadow()) * 5;
            msgs?.Add(new ReasonMsg("Abandoned Hope", 100));
            msgs?.Add(new ReasonMsg("Lack of Shadow", (1 - location.getShadow()) * 5));
            if(location.settlement.isHuman)
            {
                SettlementHuman hum = location.settlement as SettlementHuman;
                msgs?.Add(new ReasonMsg("Population", hum.population * 1.5));
                utility += hum.population * 1.5;
            }
            

            return utility;
        }

        public override void complete(UA u)
        {
            Property.addToPropertySingleShot("Well of Shadows", Property.standardProperties.WELL_OF_SHADOWS, map.param.ch_wellofshadows_parameterValue6, u.location);
        }

        public override int[] buildPositiveTags()
        {
            return new int[1] { Tags.SHADOW };
        }

        public override int[] buildNegativeTags()
        {
            return new int[0];
        }
    }
}

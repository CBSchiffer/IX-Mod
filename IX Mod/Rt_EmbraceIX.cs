using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IX_Mod
{
    internal class Rt_EmbraceIX : Ritual
    {

        public UA caster;
        public T_ApostleIX apost;
        public Rt_EmbraceIX(Location location, UA parent, T_ApostleIX apost) : base(location)
        {
            this.caster = parent;
            this.apost = apost;
        }

        public override string getName()
        {
            return "Embrace IX";
        }

        public override string getCastFlavour()
        {
            return "With their mind totally consumed, they have no choice but to bow.";
        }

        public override string getDesc()
        {
            return "This unit fully surrenders to IX, losing all shadow but becoming a mere slave to IX's will.";
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
            return challengeStat.LORE;
        }

        public override double getProgressPerTurnInner(UA unit, List<ReasonMsg> msgs)
        {
            msgs?.Add(new ReasonMsg("Stat: Lore", Math.Max(1, unit.getStatLore())));
            return Math.Max(1, unit.getStatLore());
        }

        public override double getComplexity()
        {
            return 30.0;
        }

        public override int getCompletionMenace()
        {
            return 0;
        }

        public override int getCompletionProfile()
        {
            return 0;
        }

        public override bool validFor(UA ua)
        {
            return true;
        }

        public override Sprite getSprite()
        {
            return map.world.iconStore.enshadow;
        }

        public override int isGoodTernary()
        {
            return 1;
        }

        public override double getUtility(UA ua, List<ReasonMsg> msgs)
        {
            double utility = base.getUtility(ua, msgs);
            double num = 120.0 * (ua.person.shadow);
            msgs?.Add(new ReasonMsg("Light of Soul", -100));
            utility -= 100;
            utility += num;
            msgs?.Add(new ReasonMsg("Abandonment of Hope", num));

            return utility;
        }

        public override void complete(UA cast)
        {
            apost.embrace = true;
            map.addUnifiedMessage(cast, cast, "Surrendered Hope", cast.getName() + " has given up on all hope and fully surrendered to IX. They will now begin acting in accordance with IX's goals and level up much faster, but will gain menace at an accelerated rate.", "SURRENDER TO IX", force: true);
        }

        public override bool valid()
        {
            return !apost.embrace;
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

using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX_Mod
{
    internal class P_SelfDeceit : Power
    {
        public P_SelfDeceit(Map map) : base(map)
        {

        }

        public override int getCost()
        {
            return 0;
        }

        public override string getName()
        {
            return "Café Self-Deceit";
        }

        public override string getDesc()
        {
            return "Doubles Apathy in the specified location.";
        }

        public override string getFlavour()
        {
            return "The dreams begin. The somber dance.";
        }

        public override string getRestrictionText()
        {
            return "Must target a settlement with the <b>Apathy</b> modifier.";
        }

        public override bool validTarget(Unit unit)
        {
            return false;
        }

        public override bool validTarget(Location loc)
        {
            if (loc != null && loc.settlement != null)
            {
                if (!loc.settlement.isHuman)
                    return false;
                foreach (Property prop in loc.properties.ToList())
                {
                    if (prop is Pr_Apathy ap)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void cast(Location loc)
        {
            base.cast(loc);

            if (loc.map.overmind.god is God_Nine god)
            {
                foreach (Property prop in loc.properties.ToList())
                {
                    if (prop is Pr_Apathy ap)
                    {
                        ap.charge *= 2;
                    }
                }
            }
        }
    }
}

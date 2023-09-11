using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX_Mod
{
    internal class P_TragicLecture : Power
    {
        public P_TragicLecture(Map map) : base(map)
        {

        }

        public override int getCost()
        {
            return 1;
        }

        public override string getName()
        {
            return "Tragic Lecture";
        }

        public override string getDesc()
        {
            return "Start <b>Apathy</b> in a settlement.";
        }

        public override string getFlavour()
        {
            return "The dreams begin. The somber dance.";
        }

        public override string getRestrictionText()
        {
            return "Must target a settlement with an agent that doesn't already have the <b>Apathy</b> modifier.";
        }

        public override bool validTarget(Unit unit)
        {
            return false;
        }

        public override bool validTarget(Location loc)
        {
            foreach (Property prop in loc.properties)
            {
                if(prop is Pr_Apathy ap)
                {
                    return false;
                }
            }
            foreach (Unit u in loc.units)
            {
                if(u.isCommandable())
                {
                    return true;
                }
            }
            return false; ;
        }

        public override void cast(Location loc)
        {
            base.cast(loc);

            if(loc.map.overmind.god is God_Nine god)
            {
                loc.properties.Add(new Pr_Apathy(loc, 1));
            }
        }
    }
}

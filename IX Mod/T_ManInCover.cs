using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX_Mod
{
    internal class T_ManInCover : Trait
    {
        public override string getDesc()
        {
            return "While this unit is in a location with <b>Apathy</b> > 50%, <b>Profile</b> falls to its minimum value.";
        }

        public override int getMaxLevel()
        {
            return 1;
        }

        public override string getName()
        {
            return "Man in the Cover";
        }

        public override void turnTick(Person p)
        {
            base.turnTick(p);
            Location loc = p.getLocation();
            if (loc != null)
            {
                foreach (Property pr in loc.properties.ToList())
                {
                    if(pr is Pr_Apathy apathy)
                    {
                        if(apathy.charge >= 50)
                        {
                            // Set Profile to minimum
                            UA ua = p.unit as UA;
                            double mp = ua.inner_profileMin;
                            ua.setProfile(mp);
                        }
                        
                    }
                }
            }

        }
    }
}

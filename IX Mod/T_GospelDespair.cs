using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code;

namespace IX_Mod
{
    internal class T_GospelDespair : Trait
    {
        public override string getDesc()
        {
            return "While <b>Apathy</b> is present in the same settlement as this unit, <b>Apathy</b> increases at a faster rate.";
        }

        public override int getMaxLevel()
        {
            return 1;
        }

        public override string getName()
        {
            return "Gospel of Despair";
        }

        public override void turnTick(Person p)
        {
            base.turnTick(p);
            Location loc = p.getLocation();
            if (loc != null)
            {
                foreach (Property pr in loc.properties)
                {
                    if (pr is Pr_Apathy apathy)
                    {
                        if (apathy.charge >= 1)
                        {
                            bool present = false;
                            foreach (ReasonMsg msg in apathy.influences)
                            {
                                if (msg.msg.Equals("Supplicant's Gospel"))
                                    present = true;
                            }
                            if(present == false)
                                apathy.influences.Add(new ReasonMsg("Supplicant's Gospel", 1));
                        }

                    }
                }
            }

        }
    }
}

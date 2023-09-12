using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IX_Mod
{
    internal class T_Ignosticism : Trait
    {

        bool increased;

        public T_Ignosticism()
        {
            increased = false;
        }

        public override string getDesc()
        {
            return "While this unit is in a location with <b>Apathy</b> > 50%, this unit's <b>Might</b>, <b>Lore</b>, <b>Intrigue</b>, and <b>Command</b> increase by 2.";
        }

        public override int getMaxLevel()
        {
            return 1;
        }

        public override string getName()
        {
            return "Ignosticism";
        }

        public override void turnTick(Person p)
        {
            base.turnTick(p);
            Location loc = p.getLocation();
            foreach (Property pr in loc.properties)
            {
                if (pr is Pr_Apathy apathy)
                {
                    if (apathy.charge >= 50)
                    {
                        if (!increased)
                        {
                            increased = true;
                            this.assignedTo.stat_might += 2;
                            this.assignedTo.stat_lore += 2;
                            this.assignedTo.stat_intrigue += 2;
                            this.assignedTo.stat_command += 2;
                        }
                    }
                    else
                    {
                        if (increased)
                        {
                            increased = false;
                            this.assignedTo.stat_might -= 2;
                            this.assignedTo.stat_lore -= 2;
                            this.assignedTo.stat_intrigue -= 2;
                            this.assignedTo.stat_command -= 2;
                        }
                    }

                }
            }
        }

        public override void onMove(Location current, Location dest)
        {
            base.onMove(current, dest);
            foreach (Property pr in dest.properties)
            {
                if (pr is Pr_Apathy apathy)
                {
                    if (apathy.charge >= 50)
                    {
                        if(!increased)
                        {
                            increased = true;
                            this.assignedTo.stat_might += 2;
                            this.assignedTo.stat_lore += 2;
                            this.assignedTo.stat_intrigue += 2;
                            this.assignedTo.stat_command += 2;
                        }
                    } else
                    {
                        if(increased)
                        {
                            increased = false;
                            this.assignedTo.stat_might -= 2;
                            this.assignedTo.stat_lore -= 2;
                            this.assignedTo.stat_intrigue -= 2;
                            this.assignedTo.stat_command -= 2;
                        }
                    }

                }
            }
        }
    }
}

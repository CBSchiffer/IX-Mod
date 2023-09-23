using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IX_Mod
{
    internal class P_ChooseApostle : Power
    {
        bool activated;
        public P_ChooseApostle(Map map) : base(map)
        {
            activated = false;
        }

        public override int getCost()
        {
            return 3;
        }

        public override string getName()
        {
            return "Beginning and End";
        }

        public override string getDesc()
        {
            return "Select a single hero to become your chosen Apostle. They will be slowly corrupted by the powers of the Nihility, making them much stronger than any other hero. You can only choose one Apostle per game, so choose wisely.";
        }

        public override string getFlavour()
        {
            return "They gave glimpsed something they should not have. As the twisting nether slowly corrupts their mind, their body is hardened beyond mortal limits.";
        }

        public override string getRestrictionText()
        {
            return "Must target a hero who is not the chosen one. Can only be used once per game.";
        }
        public override bool validTarget(Unit unit)
        {
            UA uA = unit as UA;
            if (uA != null)
            {
                if (activated)
                {
                    return false;
                }
                else
                {
                    foreach (Trait trait in uA.person.traits)
                    {
                        if (trait is T_ChosenOne)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public override bool validTarget(Location loc)
        {
            return false; ;
        }
        public override void cast(Unit unit)
        {
            base.cast(unit);

            unit.person.receiveTrait(new T_ApostleIX());
            activated = true;
        }
    }
}

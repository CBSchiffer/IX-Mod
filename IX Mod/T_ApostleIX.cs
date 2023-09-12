using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX_Mod
{
    internal class T_ApostleIX : Trait
    {
        public T_ApostleIX() 
        {
            level = 1;
        }
        public override string getDesc()
        {
            return "This unit will continuously gain <b>Shadow</b> and <b>Menace</b>. However, they will gain XP at a highly accelerated rate and they will slowly begin to like IX. <i>There can only be one Apostle of IX</i>.";
        }

        public override int getMaxLevel()
        {
            return 20;
        }

        public override string getName()
        {
            return "Apostle of IX";
        }

        public override void turnTick(Person p)
        {
            base.turnTick(p);
            Location loc = p.getLocation();

            UA ua = p.unit as UA;

            // Increases the victim's shadow by one every turn.
            if (p.shadow < 100)
            {
                p.shadow += 1;
            }
            else
            {
                p.shadow = 100;
            }

            

            /**
             * Ticks a timer every turn. Once the timer reaches 20, it resets to 1, increases menace, and applies an effect.
             * This effect varies depending on the state of the person.
             */

            if (level < 20)
            {
                level++;
            } else
            {
                level = 1;
                ua.setMenace(ua.menace + 5);
            }

        }
    }
}

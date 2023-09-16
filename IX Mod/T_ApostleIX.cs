using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace IX_Mod
{
    internal class T_ApostleIX : Trait
    {
        protected Random rnd;

        public T_ApostleIX() 
        {
            level = 1;
            rnd = new Random();
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
                if(p.shadow >= 100)
                    ua.setMenace(ua.menace + 15);
                int totxp = p.XP + p.XPForNextLevel;
                p.receiveXP((int) Math.Round(totxp * 0.5));
                p.addGold((int) Math.Round(p.getGold() * 0.5));
                int counter = 0;

                loop:
                counter++;
                int choice = rnd.Next(0, 5);
                string tag = "";
                List<int> hts = p.hates;
                List<int> ehts = p.extremeHates;
                List<int> elks = p.extremeLikes;
                List<int> lks = p.likes;
                int index = -1;
                if (counter < 3)
                {
                    switch (choice)
                    {
                        case 0:
                            index = God_Nine.Observe("Ambition");
                            if (hts.Contains(index))
                            {
                                if (ehts.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.extremeHates.Add(index);
                                }
                            }
                            else
                            {
                                p.hates.Add(index);
                            }
                            break;
                        case 1:
                            index = God_Nine.Observe("Co-Operation");
                            if (hts.Contains(index))
                            {
                                if (ehts.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.extremeHates.Add(index);
                                }
                            }
                            else
                            {
                                p.hates.Add(index);
                            }
                            break;
                        case 2:
                            index = God_Nine.Observe("Gold");
                            if (hts.Contains(index))
                            {
                                if (ehts.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.extremeHates.Add(index);
                                }
                            }
                            else
                            {
                                p.hates.Add(index);
                            }
                            break;
                        case 3:
                            index = God_Nine.Observe("Shadow");
                            if (lks.Contains(index))
                            {
                                if (elks.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.extremeLikes.Add(index);
                                }
                            }
                            else
                            {
                                p.likes.Add(index);
                            }
                            break;
                        case 4:
                            index = God_Nine.Observe("IX");
                            if (lks.Contains(index))
                            {
                                if (elks.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.extremeLikes.Add(index);
                                }
                            }
                            else
                            {
                                p.likes.Add(index);
                            }
                            break;
                        default:
                            index = God_Nine.Observe("Apathy");
                            if (lks.Contains(index))
                            {
                                if (elks.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.extremeLikes.Add(index);
                                }
                            }
                            else
                            {
                                p.likes.Add(index);
                            }
                            break;
                    }
                }
                else
                {
                    choice = rnd.Next(0, 4);
                    switch (choice)
                    {
                        case 0:
                            p.receiveTrait(new T_StatIntrigue());
                            break;
                        case 1:
                            p.receiveTrait(new T_StatMight());
                            break;
                        case 2:
                            p.receiveTrait(new T_StatLore());
                            break;
                        default:
                            p.receiveTrait(new T_StatCommand());
                            break;
                    }
                }


            }

        }
    }
}

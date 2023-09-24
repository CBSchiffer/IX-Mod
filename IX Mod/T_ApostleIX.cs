using Assets.Code;
using LivingCharacters;
using ShadowsLib;
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
        public int strength;
        public bool embrace;

        public T_ApostleIX() 
        {
            level = 1;
            strength = 1;
            rnd = new Random();
            embrace = false;
        }

        
        public override string getDesc()
        {
            return "This unit will continuously gain <b>Shadow</b> and <b>Menace</b>. However, they will gain XP at a highly accelerated rate and they will slowly begin to like IX. <i>There can only be one Apostle of IX</i>.";
        }

        public override int getMaxLevel()
        {
            return 1;
        }

        public override string getName()
        {
            return "Apostle of IX (" + strength + ")";
        }


        public override void turnTick(Person p)
        {
            Assets.Code.Map map = this.assignedTo.getLocation().map;
            base.turnTick(p);
            Location loc = p.getLocation();

            UA ua = p.unit as UA;

            bool flag = false;
            bool flag2 = false;
            foreach (Ritual ritual in p.unit.rituals)
            {
                if (ritual is Rt_EmbraceIX && !embrace)
                {
                    flag = true;
                    break;
                }
                if(ritual is Rt_WellShadow)
                {
                    flag2 = true;
                    break;
                }
            }
            
            
            if (!flag)
            {
                ua.rituals.Add(new Rt_EmbraceIX(ua.location, ua, this));
            }
            if(!flag2)
            {
                if(ua.location.settlement.isHuman)
                {
                    ua.rituals.Add(new Rt_WellShadow(ua.location, ua.location.settlement as SettlementHuman));
                }
                ua.rituals.Add(new Rt_SpreadDespair(ua.location, ua));
            }

            // Increases the victim's shadow by one every turn.
            if(embrace)
            {
                assignedTo.shadow = 0;
            } else
            {
                if (assignedTo.shadow < 100)
                {
                    assignedTo.shadow += .01;
                }
                else
                {
                    assignedTo.shadow = 1;
                }
            }
            

            

            /**
             * Ticks a timer every turn. Once the timer reaches 20, it resets to 1, increases menace, and applies an effect.
             * This effect varies depending on the state of the person.
             */
            if (strength < 10)
            {
                strength++;
            } else
            {
                strength = 1;
                if(p.shadow >= 1 || embrace)
                    ua.setMenace(ua.menace + 15);
                int totxp = p.XP + p.XPForNextLevel;
                assignedTo.receiveXP((int) Math.Round(totxp * 0.5 + 25));
                if(embrace)
                    assignedTo.receiveXP((int)Math.Round(totxp * 0.2));
                assignedTo.addGold((int) Math.Round(p.getGold() * 0.5 + 10));
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
                                    p.hates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeLikes.Remove(index);
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a vehement hatred of Ambition.", "IX'S EMPOWERMENT", force: true);
                                }
                            }
                            else
                            {
                                if (ehts.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.hates.Add(index);
                                    p.extremeHates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeLikes.Remove(index);
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a dislike of Ambition.", "IX'S EMPOWERMENT", force: true);
                                }
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
                                    p.hates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeLikes.Remove(index);
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a vehement hatred of Co-Operation.", "IX'S EMPOWERMENT", force: true);
                                }
                            }
                            else
                            {
                                if (ehts.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.hates.Add(index);
                                    p.extremeHates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeLikes.Remove(index);
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a dislike of Co-Operation.", "IX'S EMPOWERMENT", force: true);
                                }
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
                                    p.hates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeLikes.Remove(index);
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a vehement hatred of Gold.", "IX'S EMPOWERMENT", force: true);
                                }
                            }
                            else
                            {
                                if (ehts.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    p.hates.Add(index);
                                    p.extremeHates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeLikes.Remove(index);
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a dislike of Gold.", "IX'S EMPOWERMENT", force: true);
                                }
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
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining an extreme liking of Shadow.", "IX'S EMPOWERMENT", force: true);
                                    p.extremeLikes.Add(index);
                                    p.hates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeHates.Remove(index);
                                }
                            }
                            else
                            {
                                if (elks.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a liking of Shadow.", "IX'S EMPOWERMENT", force: true);
                                    p.likes.Add(index);
                                    p.hates.Remove(index);
                                    p.extremeHates.Remove(index);
                                    p.extremeLikes.Remove(index);
                                }
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
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining an extreme liking of IX.", "IX'S EMPOWERMENT", force: true);
                                    p.extremeLikes.Add(index);
                                    p.hates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeHates.Remove(index);
                                }
                            }
                            else
                            {
                                if (elks.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a liking of IX.", "IX'S EMPOWERMENT", force: true);
                                    p.likes.Add(index);
                                    p.hates.Remove(index);
                                    p.extremeHates.Remove(index);
                                    p.extremeLikes.Remove(index);
                                }
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
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining an extreme liking of Apathy.", "IX'S EMPOWERMENT", force: true);
                                    p.extremeLikes.Add(index);
                                    p.hates.Remove(index);
                                    p.likes.Remove(index);
                                    p.extremeHates.Remove(index);
                                }
                            }
                            else
                            {
                                if (elks.Contains(index))
                                {
                                    // Go next
                                    goto loop;
                                }
                                else
                                {
                                    map.addUnifiedMessage(assignedTo, assignedTo, "Corruption of IX", assignedTo.getName() + " is falling further under IX's influence, gaining a liking of Apathy.", "IX'S EMPOWERMENT", force: true);
                                    p.likes.Add(index);
                                    p.hates.Remove(index);
                                    p.extremeHates.Remove(index);
                                    p.extremeLikes.Remove(index);
                                }
                            }
                            break;
                    }
                }
                else
                {
                    choice = rnd.Next(0, 4);
                    bool found = false;
                    switch (choice)
                    {
                        case 0:
                            map.addUnifiedMessage(assignedTo, assignedTo, "Empowerment of IX", assignedTo.getName() + " is being empowered by IX's influence, permanately increasing their intrigue.", "IX'S EMPOWERMENT", force: true);
                            foreach(Trait t in p.traits)
                            {
                                if(t is T_StatIntrigue si)
                                {
                                    si.level++;
                                    found = true;
                                }
                            }
                            if(!found)
                                p.receiveTrait(new T_StatIntrigue());
                            break;
                        case 1:
                            map.addUnifiedMessage(assignedTo, assignedTo, "Empowerment of IX", assignedTo.getName() + " is being empowered by IX's influence, permanately increasing their might.", "IX'S EMPOWERMENT", force: true);
                            foreach (Trait t in p.traits)
                            {
                                if (t is T_StatMight sm)
                                {
                                    sm.level++;
                                    found = true;
                                }
                            }
                            if (!found)
                                p.receiveTrait(new T_StatMight());
                            break;
                        case 2:
                            map.addUnifiedMessage(assignedTo, assignedTo, "Empowerment of IX", assignedTo.getName() + " is being empowered by IX's influence, permanately increasing their lore.", "IX'S EMPOWERMENT", force: true);
                            foreach (Trait t in p.traits)
                            {
                                if (t is T_StatLore sl)
                                {
                                    sl.level++;
                                    found = true;
                                }
                            }
                            if (!found)
                                p.receiveTrait(new T_StatLore());
                            break;
                        default:
                            map.addUnifiedMessage(assignedTo, assignedTo, "Empowerment of IX", assignedTo.getName() + " is being empowered by IX's influence, permanately increasing their command.", "IX'S EMPOWERMENT", force: true);
                            foreach (Trait t in p.traits)
                            {
                                if (t is T_StatCommand sc)
                                {
                                    sc.level++;
                                    found = true;
                                }
                            }
                            if (!found)
                                p.receiveTrait(new T_StatCommand());
                            break;
                    }
                }


            }

        }
    }
}

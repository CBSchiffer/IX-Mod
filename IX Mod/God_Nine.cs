using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code;
using UnityEngine;

namespace IX_Mod
{
    internal class God_Nine : God
    {
        /**
         * Here is where all the internal constants will go
         * 
         * 
         */
        public const string NAME = "IX - The Nihility";
        public const string DESC_MECHANICS = "Nine can inflict human settlements with <b>Apathy</b>, causing society to crumble as people give up. As it spreads, rulers will eventually stop taking action and eventually disband their armies, crippling their own defences as their people slowly starve.";
        public const string DESC_FLAVOUR = "\"Blindfold your eyes they say, so that the approaching darkness may strike no fear in you. Let it not burden your soul, or numb your stride...\"\nNine is the Aeon of Nihility, he spreads despair and melancholy to all that glimpse him. Perhaps he was once a human who saw too much. Perhaps he was a cosmic entity that witnessed the end of the universe. It matters not. Nothing does.";
        public const double AWAKE_PANIC = 0.4;
        public readonly int[] SEAL_LVLS = new int[] { 10, 20, 40, 60, 80, 150, 220, 300, 399 };
        public readonly int[] AGENT_CAPS = new int[] { 1, 2, 2, 3, 3, 3, 4, 4, 5, 6 };

        public override string getName()
        {
            return NAME;
        }

        public override void setup(Map map)
        {
            base.setup(map);

            /**
             * Here is where the code for adding the new powers will go
             */
        }

        public override void onStart(Map map)
        {
            base.onStart(map);

            /**
             * Here is where the code for changing start conditions will go
             */
        }

        public override string getDescMechanics()
        {
            return DESC_MECHANICS;
        }

        public override string getDescFlavour()
        {
            return DESC_FLAVOUR;
        }

        public override string getDetailedMechanics()
        {
            string msg = "";
            msg += "<b>Apathy</b>";
            msg += "\nApathy can be started by using IX's power <b>Tragic Lecture</b>, causing people to get glimpses of their insignificance. Apathy will cause the civilians to stop taking action, leading to food shortages and decreased security. If Apathy reaches high enough levels, it can cause the entire settlement to collapse.";
            msg += "\n\n<b>Hero Tampering</b>";
            msg += "\nUtilizing powers like <b>Questioning of Purpose</b> and <b>Hell is Other People</b> can manipulate the likes and dislikes of the Heroes on the map, allowing you to more easily enshadow them.";
            msg += "\n\n<b>Doctors of Chaos</b>";
            msg += "\nHeroes that spend time in areas with high levels of Apathy may be inflicted with it themselves, causing them to spread it further. However, sometimes heroes that witness Apathy might become <b>Doctors of Chaos</b>. <b>Doctors of Chaos</b> will gain a fascination with IX and will attempt to prove you wrong by remedying your Apathy. While they will not remove shadow or co-operate with the Chosen One, they cannot become enshadowed themselves and will actively attempt to remove your Apathy, making it more difficult to cripple settlements.";
            return msg;
        }

        public override int[] getSealLevels()
        {
            return SEAL_LVLS;
        }

        public override int[] getAgentCaps()
        {
            return AGENT_CAPS;
        }

        public override Sprite getGodPortrait(World world)
        {
            return EventManager.getImg("nihility.god_portrait.png");
        }

        public override Sprite getGodBackground(World world)
        {
            return EventManager.getImg("nihility.god_background.png");
        }

        public override Sprite getSupplicant()
        {
            return EventManager.getImg("");
        }

        public override double getWorldPanicOnAwake()
        {
            return AWAKE_PANIC;
        }
        public override string getPowerDesc()
        {
            return this.getName() + " grows as infected human populations are brought to the <b>Hives</b> by <b>Drones</b>. These paralysed victims serve as living incubators for the larvae, which add to the iocord fungal hive-mind's power, letting you employ more powers";
        }
        public override string getSealDesc()
        {
            return this.getName() + " grows as infected human populations are brought to the <b>Hives</b> by <b>Drones</b>. These paralysed victims serve as living incubators for the larvae, which add to the Cordyceps fungal hive-mind's power, letting you employ more powers";
        }
        public override string getSealUITextUpper()
        {
            return "Seals Broken: " + map.overmind.sealsBroken + " of " + this.getSealLevels().Length;
        }
        public override string getSealUITextLower()
        {

            int turnsLeft = getSealLevels()[map.overmind.sealsBroken] - map.overmind.sealProgress;
            return "Amount to next Seal " + turnsLeft;
        }

        public override string getAwakenMessage()
        {
            return "The parasites grow inside, and writhe around in the bodies of the victims. The air feels heavy with spores, and the nights are filled with sounds of mutated insect wings buzzing incessantly. Humanity recoils in disgust, horror and terror.";
        }

        public override bool hasSupplicantStartingTraits()
        {
            return true;
        }
        public override List<Trait> getSupplicantStartingTraits()
        {
            List<Trait> traits = new List<Trait>();
            //traits.Add(new T_TheScentOfPrey());
            return traits;
        }
    }
}

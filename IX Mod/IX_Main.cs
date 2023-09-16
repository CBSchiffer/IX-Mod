using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code;

namespace IX_Mod
{
    public class IX_Main : Assets.Code.Modding.ModKernel
    {
        public static int NINE;
        public Map map;
        public bool warningGiven = false;

        public override void onStartGamePresssed(Map map, List<God> gods)
        {
            gods.Add(new God_Nine());
            base.onStartGamePresssed(map, gods);
        }

        public override void beforeMapGen(Map map)
        {
            base.beforeMapGen(map);
            Tags.addTagEnemy("IX");
            Tags.addTagEnemy("Apathy");
        }
    }
}

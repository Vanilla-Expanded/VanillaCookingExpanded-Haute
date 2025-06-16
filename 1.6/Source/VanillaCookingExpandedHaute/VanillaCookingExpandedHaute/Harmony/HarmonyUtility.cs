using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace VanillaCookingExpandedHaute
{
    [StaticConstructorOnStartup]
    public static class HarmonyUtility
    {
        private const string _harmonyId = "VanillaCookingExpandedHaute";

        public static Harmony Instance = new Harmony(_harmonyId);

        static HarmonyUtility()
        {
            Instance.PatchAll();
        }
    }
}

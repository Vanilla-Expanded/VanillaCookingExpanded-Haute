using RimWorld;
using UnityEngine;
using Verse;

namespace VanillaCookingExpandedHaute
{

    public class VanillaCookingExpandedHaute_Mod : Mod
    {
        public VanillaCookingExpandedHaute_Mod(ModContentPack content) : base(content)
        {
            GetSettings<VanillaCookingExpandedHaute_Settings>();
        }

        public override string SettingsCategory()
        {
            return "VCE - Haute";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            VanillaCookingExpandedHaute_Settings.DoWindowContents(inRect);
        }
    }


}

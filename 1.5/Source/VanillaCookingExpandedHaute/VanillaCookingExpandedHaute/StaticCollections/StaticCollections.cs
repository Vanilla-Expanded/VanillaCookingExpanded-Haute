
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;



namespace VanillaCookingExpandedHaute
{
    [StaticConstructorOnStartup]
    public static class StaticCollections
    {

      
        public static HashSet<ThoughtDef> hauteThoughts = new HashSet<ThoughtDef>() { InternalDefOf.VCE_AteHauteMeal, InternalDefOf.VCE_AteHauteMeal_Two
        , InternalDefOf.VCE_AteHauteMeal_Three, InternalDefOf.VCE_AteHauteMeal_Four, InternalDefOf.VCE_AteHauteMeal_Five};


      



    }
}

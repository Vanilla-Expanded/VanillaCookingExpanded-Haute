using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;


namespace VanillaCookingExpandedHaute
{

    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }


        public static ThingCategoryDef AnimalProductRaw;
        public static ThingCategoryDef VCE_Condiments;

        public static ThingDef VCE_MealHaute;
        public static ThingDef VCE_ElectricHauteSection;

        public static ThoughtDef VCE_AteHauteMeal;
        public static ThoughtDef VCE_AteHauteMeal_Two;
        public static ThoughtDef VCE_AteHauteMeal_Three;
        public static ThoughtDef VCE_AteHauteMeal_Four;
        public static ThoughtDef VCE_AteHauteMeal_Five;
        public static ThoughtDef VCE_AteHauteMeal_Social_Author;
        public static ThoughtDef VCE_AteHauteMeal_Social_Sharing;



    }
}
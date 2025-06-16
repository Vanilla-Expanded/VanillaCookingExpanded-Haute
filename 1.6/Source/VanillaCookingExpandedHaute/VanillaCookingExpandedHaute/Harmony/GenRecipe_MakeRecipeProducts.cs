using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;


namespace VanillaCookingExpandedHaute
{

    [HarmonyPatch(typeof(GenRecipe))]
    [HarmonyPatch("MakeRecipeProducts")]

    public static class VanillaCookingExpandedHaute_GenRecipe_MakeRecipeProducts_Patch
    {

        public static IEnumerable<Thing> Postfix(IEnumerable<Thing> values, RecipeDef recipeDef, Pawn worker, IBillGiver billGiver)
        {
            List<Thing> resultingList = values.ToList();
            Building_WorkTable workbench;
            CompAffectedByFacilities comp;
            if ((workbench = billGiver as Building_WorkTable) != null && recipeDef.products != null && recipeDef.products.Select(x => x.thingDef).Contains(InternalDefOf.VCE_MealHaute))
            {
                if ((comp = workbench.TryGetComp<CompAffectedByFacilities>()) != null)
                {

                    if (comp.LinkedFacilitiesListForReading.ContainsAny(x => x.def == InternalDefOf.VCE_ElectricHauteSection) == true)
                    {
                        foreach (Thing thing in resultingList)
                        {
                            int numBerOfStations = comp.LinkedFacilitiesListForReading.Where(x => x.def == InternalDefOf.VCE_ElectricHauteSection).Count();

                            for(int i = 0; i < numBerOfStations; i++)
                            {
                                string labelOld = thing.LabelCap;
                                CompQuality compQuality = thing.TryGetComp<CompQuality>();

                                if (Rand.Chance(0.5f))
                                {
                                    if (compQuality.Quality < QualityCategory.Legendary)
                                    {
                                        compQuality.SetQuality(compQuality.Quality + 1, null);
                                        Messages.Message("VCE_ItemImproved_HauteSection".Translate(labelOld, compQuality.Quality.ToString()), worker, MessageTypeDefOf.PositiveEvent, null, historical: false);
                                    }
                                    
                                }
                            }
                            
                            
                        }
                    }




                }






            }

            return resultingList;




        }
    }
}








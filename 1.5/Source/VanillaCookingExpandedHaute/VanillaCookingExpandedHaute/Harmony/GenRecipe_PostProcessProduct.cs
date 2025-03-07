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
using VanillaCookingExpandedHaute;
using static UnityEngine.UI.GridLayoutGroup;


namespace VanillaCookingExpandedHaute
{


    [HarmonyPatch(typeof(GenRecipe))]
    [HarmonyPatch("PostProcessProduct")]

    public static class VanillaCookingExpandedHaute_GenRecipe_PostProcessProduct_Patch
    {

        public static void Postfix(Thing product,RecipeDef recipeDef, Pawn worker)
        {
            if (product.def == InternalDefOf.VCE_MealHaute) {
                CompFoodArt compArt = product.TryGetComp<CompFoodArt>();
                CompQuality compQuality = product.TryGetComp<CompQuality>();
                if (compArt != null)
                {
                    compArt.JustCreatedBy(worker);
                    if (compQuality != null && (int)compQuality.Quality >= 4)
                    {
                        TaleRecorder.RecordTale(TaleDefOf.CraftedArt, worker, product);
                    }
                }

            }
            



        }

    }





}

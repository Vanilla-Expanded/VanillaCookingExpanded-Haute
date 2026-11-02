using RimWorld;
using System;
using System.Reflection;
using UnityEngine;
using VEF;
using Verse;


namespace VanillaCookingExpandedHaute
{

    public class VanillaCookingExpandedHaute_Settings : ModSettings

    {
        public static bool displayImprovementMessages = true;

        public static float distance = baseDistance;
        public const float baseDistance = 8;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref displayImprovementMessages, "displayImprovementMessages", true);
            Scribe_Values.Look(ref distance, "distance", baseDistance);           
        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();

            ls.Begin(inRect);

            ls.CheckboxLabeled("VCE_DisplayImprovementMessages".Translate(), ref displayImprovementMessages, "VCE_DisplayImprovementMessages_Desc".Translate());

            ls.Gap(30f);

            var distanceLabel = ls.LabelPlusButton("VCE_DistanceForSocial".Translate() + ": " + distance, "VCE_DistanceForSocial_Desc".Translate());
            distance = (float)Math.Round(ls.Slider(distance, 1, 40), 1);

            if (ls.Settings_Button("VCE_Reset".Translate(), new Rect(0f, distanceLabel.position.y + 35, 250f, 29f)))
            {
                distance = baseDistance;
            }

           

            ls.End();
        }





    }










}

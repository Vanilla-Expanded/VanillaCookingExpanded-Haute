﻿
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using static HarmonyLib.Code;
namespace VanillaCookingExpandedHaute
{
    public class IngestionOutcomeDoer_HauteMeal : IngestionOutcomeDoer
    {
      

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            List<ThoughtDef> validThoughts = StaticCollections.hauteThoughts.Where(x => pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(x) == null).ToList();
            if (validThoughts?.Count > 0) {
                ThoughtDef randomThought = validThoughts.RandomElement();
                if (randomThought != null) {
                    Thought_HauteMeal thought_Memory = (Thought_HauteMeal)ThoughtMaker.MakeThought(randomThought);
                    CompFoodArt compArt = ingested.TryGetComp<CompFoodArt>();
                    CompQuality compQuality = ingested.TryGetComp<CompQuality>();

                    if(compArt.Title != "VCE_ManufacturedSlop".Translate())
                    {
                        thought_Memory.mealThoughtLabel = compArt.Title;
                        thought_Memory.quality = compQuality.Quality;
                        pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(thought_Memory);

                        Thought_MemorySocial thought_SocialMemory = (Thought_MemorySocial)ThoughtMaker.MakeThought(InternalDefOf.VCE_AteHauteMeal_Social_Author);
                        if (pawn != compArt.authorPawn && compArt.authorPawn != null)
                        {
                            pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(thought_SocialMemory, compArt.authorPawn);

                        }
                        foreach (Pawn sharingPawn in pawn.Map.mapPawns.FreeColonists)
                        {

                            if (sharingPawn != pawn && sharingPawn.Position.InHorDistOf(pawn.Position, 8))
                            {

                                Thought_MemorySocial thought_SocialMemory_Sharing_Other = (Thought_MemorySocial)ThoughtMaker.MakeThought(InternalDefOf.VCE_AteHauteMeal_Social_Sharing);
                                sharingPawn.needs?.mood?.thoughts?.memories?.TryGainMemory(thought_SocialMemory_Sharing_Other, pawn);
                                Thought_MemorySocial thought_SocialMemory_Sharing = (Thought_MemorySocial)ThoughtMaker.MakeThought(InternalDefOf.VCE_AteHauteMeal_Social_Sharing);
                                pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(thought_SocialMemory_Sharing, sharingPawn);


                            }
                        }
                    }

                   
                    
                   
                }
            }

            
        }

      
    }
}
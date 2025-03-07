
using RimWorld;
using Verse;
using System.Collections.Generic;
using Verse.Grammar;
using UnityEngine;
using System.Linq;

namespace VanillaCookingExpandedHaute
{
    public class CompFoodArt: ThingComp
    {
        private TaggedString authorNameInt = null;

        private TaggedString titleInt = null;

        private TaleReference taleRef;

        public TaggedString AuthorName
        {
            get
            {
                if (authorNameInt.NullOrEmpty())
                {
                    return "UnknownLower".Translate().CapitalizeFirst();
                }
                return authorNameInt.Resolve();
            }
        }

        public string Title
        {
            get
            {
                
                if (titleInt.NullOrEmpty())
                {
                    Log.Error("CompArt got title but it wasn't configured.");
                    titleInt = "Error";
                }
                return titleInt;
            }
        }

        public TaleReference TaleRef => taleRef;

        public bool CanShowArt
        {
            get
            {
                if (Props.mustBeFullGrave)
                {
                    Building_Grave building_Grave = parent as Building_Grave;
                    if (building_Grave == null || !building_Grave.HasCorpse)
                    {
                        return false;
                    }
                }
                if (!parent.TryGetQuality(out var qc))
                {
                    return true;
                }
                return (int)qc >= (int)Props.minQualityForArtistic;
            }
        }

        public bool Active => taleRef != null;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                InitializeArt(ArtGenerationContext.Colony);
            }
        }

        public CompProperties_FoodArt Props => (CompProperties_FoodArt)props;

        public override string TransformLabel(string label)
        {
            if (!titleInt.NullOrEmpty())
            {
                return Title;

            }return label;
        }

        public void InitializeArt(ArtGenerationContext source)
        {
            InitializeArt(null, source);
        }

        public void InitializeArt(Thing relatedThing)
        {
            InitializeArt(relatedThing, ArtGenerationContext.Colony);
        }

        private void InitializeArt(Thing relatedThing, ArtGenerationContext source)
        {
            if (taleRef != null)
            {
                taleRef.ReferenceDestroyed();
                taleRef = null;
            }
            if (CanShowArt)
            {
                if (Current.ProgramState == ProgramState.Playing)
                {
                    if (relatedThing != null)
                    {
                        taleRef = Find.TaleManager.GetRandomTaleReferenceForArtConcerning(relatedThing);
                    }
                    else
                    {
                        taleRef = Find.TaleManager.GetRandomTaleReferenceForArt(source);
                    }
                }
                else
                {
                    taleRef = TaleReference.Taleless;
                }
                titleInt = GenerateTitle(source);
            }
            else
            {
                titleInt = null;
                taleRef = null;
            }
        }

        public void JustCreatedBy(Pawn pawn)
        {
            if (CanShowArt)
            {
                authorNameInt = pawn.NameFullColored;
            }
        }

        public void Clear()
        {
            authorNameInt = null;
            titleInt = null;
            if (taleRef != null)
            {
                taleRef.ReferenceDestroyed();
                taleRef = null;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref authorNameInt, "authorName", null);
            Scribe_Values.Look(ref titleInt, "title", null);
            Scribe_Deep.Look(ref taleRef, "taleRef");
        }

       /* public override string CompInspectStringExtra()
        {
            if (!Active)
            {
                return null;
            }
            return (string)("Author".Translate() + ": " + AuthorName) + ("\n" + "Title".Translate() + ": " + Title);
        }*/

      

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            base.PostDestroy(mode, previousMap);
            if (taleRef != null)
            {
                taleRef.ReferenceDestroyed();
                taleRef = null;
            }
        }

        public override string GetDescriptionPart()
        {
            if (!Active)
            {
                return null;
            }
            return string.Concat(string.Concat("" + Title, "\n\n") + GenerateImageDescription(), "\n\n") + ("Author".Translate() + ": " + AuthorName);
        }

        public override bool AllowStackWith(Thing other)
        {
            if (Active)
            {
                return false;
            }
            return true;
        }

        public TaggedString GenerateImageDescription()
        {
            if (taleRef == null)
            {
                Log.Error("Did CompArt.GenerateImageDescription without initializing art: " + parent);
                InitializeArt(ArtGenerationContext.Outsider);
            }

            CompIngredients compIngredients = this.parent.TryGetComp<CompIngredients>();
            string protein = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(ThingCategoryDefOf.MeatRaw) == true || x.thingCategories?.Contains(InternalDefOf.AnimalProductRaw) == true).First().LabelCap;
            string plant1 = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(ThingCategoryDefOf.PlantFoodRaw) == true).First().LabelCap;
            string plant2 = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(ThingCategoryDefOf.PlantFoodRaw) == true && x.LabelCap != plant1).First().LabelCap;
            string condiment1 = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(InternalDefOf.VCE_Condiments) == true).First().LabelCap;
            string condiment2 = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(InternalDefOf.VCE_Condiments) == true && x.LabelCap != condiment1).First().LabelCap;
            List<Rule> listRules = new List<Rule>();
            listRules.Add(new Rule_String("protein", protein));
            listRules.Add(new Rule_String("vegetable1", plant1));
            listRules.Add(new Rule_String("vegetable2", plant2));
            listRules.Add(new Rule_String("condiment1", condiment1));
            listRules.Add(new Rule_String("condiment2", condiment2));
            return taleRef.GenerateText(TextGenerationPurpose.ArtDescription, Props.descriptionMaker, listRules);
        }

        private string GenerateTitle(ArtGenerationContext context)
        {
            if (taleRef == null)
            {
                Log.Error("Did CompArt.GenerateTitle without initializing art: " + parent);
                InitializeArt(ArtGenerationContext.Outsider);
            }

            CompIngredients compIngredients = this.parent.TryGetComp<CompIngredients>();
            string protein = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(ThingCategoryDefOf.MeatRaw) == true || x.thingCategories?.Contains(InternalDefOf.AnimalProductRaw) == true).First().LabelCap;
            string plant1 = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(ThingCategoryDefOf.PlantFoodRaw) == true).First().LabelCap;
            string plant2 = compIngredients.ingredients.Where(x => x.thingCategories?.Contains(ThingCategoryDefOf.PlantFoodRaw) == true && x.LabelCap!=plant1).First().LabelCap;

            List<Rule> listRules = new List<Rule>();
            listRules.Add(new Rule_String("protein", protein));
            listRules.Add(new Rule_String("vegetable1", plant1));
            listRules.Add(new Rule_String("vegetable2", plant2));
            return GenText.CapitalizeAsTitle(taleRef.GenerateText(TextGenerationPurpose.ArtName, Props.nameMaker, listRules));
        }
    }
}
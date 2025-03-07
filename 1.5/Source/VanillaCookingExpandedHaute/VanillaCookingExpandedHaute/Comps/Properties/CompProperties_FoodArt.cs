
using RimWorld;
using Verse;

namespace VanillaCookingExpandedHaute
{
    public class CompProperties_FoodArt:CompProperties
    {
        public RulePackDef nameMaker;

        public RulePackDef descriptionMaker;

        public QualityCategory minQualityForArtistic;

        public bool mustBeFullGrave;

        public bool canBeEnjoyedAsArt;

        public CompProperties_FoodArt()
        {
            compClass = typeof(CompFoodArt);
        }
    }
}

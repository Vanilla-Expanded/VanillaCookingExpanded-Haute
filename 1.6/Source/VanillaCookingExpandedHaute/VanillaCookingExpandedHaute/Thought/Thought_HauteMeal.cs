
using RimWorld;
using Verse;
namespace VanillaCookingExpandedHaute
{
    public class Thought_HauteMeal : Thought_Memory
    {

        public string mealThoughtLabel;

        public QualityCategory quality;


        public static readonly SimpleCurve OffsetByQuality = new SimpleCurve
        {
            new CurvePoint(0f, 0.5f),
            new CurvePoint(1f, 0.75f),
            new CurvePoint(2f, 1f),
            new CurvePoint(3f, 1.5f),
            new CurvePoint(4f, 2f),
            new CurvePoint(5f, 2.5f),
            new CurvePoint(6f, 3f)
        };

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref quality, "quality");
        }

        public override string LabelCap
        {
            get
            {

                return "VCE_AteHauteMeal".Translate(mealThoughtLabel).CapitalizeFirst();
            }
        }

        public override string Description
        {
            get
            {

                return "VCE_AteHauteMealDesc".Translate(mealThoughtLabel).CapitalizeFirst();
            }


        }

        public override float MoodOffset()
        {
            if (CurStage == null)
            {
                Log.Error("CurStage is null while ShouldDiscard is false on " + def.defName + " for " + pawn);
                return 0f;
            }
            if (ThoughtUtility.ThoughtNullified(pawn, def))
            {
                return 0f;
            }
            float num = BaseMoodOffset;
           
                num *= OffsetByQuality.Evaluate((int)quality);
            
            return num;
        }



    }
}
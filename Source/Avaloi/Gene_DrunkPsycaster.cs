using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

[DefOf]
public static class Avaloi_DefOf
{
    public static HediffDef Avaloi_DrunkPsycasting;

    static Avaloi_DefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(Avaloi_DefOf));
    }
}

namespace Avaloi
{
    public class Gene_DrunkPsycaster : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            pawn.health.AddHediff(Avaloi_DefOf.Avaloi_DrunkPsycasting);
        }

        public override void PostRemove()
        {
            base.PostRemove();
            pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(Avaloi_DefOf.Avaloi_DrunkPsycasting));
        }
    }

    public class HediffDrunkPsycasting : HediffWithComps
    {
        public Hediff cachedDrunk;
        public HediffStage cachedStage;

        public Hediff DrunkHediff
        {
            get
            {
                if (cachedDrunk != null && cachedDrunk.pawn != null)
                {
                    return cachedDrunk;
                }

                cachedDrunk = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.AlcoholHigh);
                return cachedDrunk;
            }
        }

        public override HediffStage CurStage
        {
            get
            {
                if (DrunkHediff == null)
                {
                    return null;
                }

                if (cachedStage == null)
                {
                    cachedStage = new HediffStage();
                    cachedStage.statOffsets = new List<StatModifier>()
                    {
                        new StatModifier() { stat = StatDefOf.PsychicSensitivity, value = 0.5f * DrunkHediff.Severity } 
                    }; 
                }

                cachedStage.statOffsets[0].value = 0.5f * DrunkHediff.Severity;
                return cachedStage;
            }
        }
    } 
}

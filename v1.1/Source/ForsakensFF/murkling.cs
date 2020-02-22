using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace ForsakensFF
{
    internal class murkling : IncidentWorker
    {
        private const float PointsFactor = 1f;

        private const int AnimalsStayDurationMin = 60000;

        private const int AnimalsStayDurationMax = 120000;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            bool flag = !base.CanFireNowSub(parms);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                Map map = (Map)parms.target;
                PawnKindDef fO_Murkling = ForsakensFF_DefOf.FO_Murkling;
                List<Thing> allThings = map.listerThings.AllThings;
                int num = 0;
                foreach (Thing current in allThings)
                {
                    bool flag2 = current.def.race.Humanlike && RottableUtility.IsDessicated(current);
                    if (flag2)
                    {
                        num++;
                    }
                }
                IntVec3 intVec;
                result = (RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null) && num > 10);
            }
            return result;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef fO_Murkling = ForsakensFF_DefOf.FO_Murkling;
            IntVec3 intVec;
            bool flag = !RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                List<Pawn> list = ManhunterPackIncidentUtility.GenerateAnimals(fO_Murkling, map.Tile, parms.points * 1f);
                Rot4 rot = Rot4.FromAngleFlat((map.Center - intVec).AngleFlat);
                for (int i = 0; i < list.Count; i++)
                {
                    Pawn pawn = list[i];
                    IntVec3 intVec2 = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                    GenSpawn.Spawn(pawn, intVec2, map, rot, 0, false);
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, false, false, null, false);
                    pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(60000, 120000);
                }
                Find.LetterStack.ReceiveLetter(Translator.Translate("LetterLabelManhunterPackArrived"), TranslatorFormattedStringExtensions.Translate("ManhunterPackArrived", fO_Murkling.GetLabelPlural(-1)), LetterDefOf.ThreatBig, list[0], null, null);
                Find.TickManager.slower.SignalForceNormalSpeedShort();
                LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
                LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Critical);
                result = true;
            }
            return result;
        }
    }
}

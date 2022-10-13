using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace ForsakensFF
{
    /*This Harmony Postfix multiplies commonality of animals in the biome
    */
    [HarmonyPatch(typeof(BiomeDef))]
    [HarmonyPatch("CommonalityOfAnimal")]
    public static class ForsakensFauna_BiomeDef_CommonalityOfAnimal_Patch
    {
        [HarmonyPostfix]
        public static void MultiplyForsakensFaunaCommonality(PawnKindDef animalDef, ref float __result)

        {

            if (animalDef.defName.Contains("FO_"))
            {
                __result *= ForsakensFauna_Mod.settings.FFSpawnMultiplier;

            }


        }
    }
}

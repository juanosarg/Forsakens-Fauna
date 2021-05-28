using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System;


namespace ForsakensFF
{
    public class ForsakensFauna_Settings : ModSettings

    {
        private static Vector2 scrollPosition = Vector2.zero;
        public Dictionary<string, bool> pawnSpawnStates = new Dictionary<string, bool>();
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref pawnSpawnStates, "pawnSpawnStates", LookMode.Value, LookMode.Value, ref pawnKeys, ref boolValues);
            Scribe_Values.Look(ref FFSpawnMultiplier, "FFSpawnMultiplier", 1, true);

        }
        private List<string> pawnKeys;
        private List<bool> boolValues;

        public const float FFSpawnMultiplierBase = 1;
        public float FFSpawnMultiplier = FFSpawnMultiplierBase;

        public void DoWindowContents(Rect inRect)
        {

            List<string> keys = pawnSpawnStates.Keys.ToList().OrderByDescending(x => x).ToList();
            Listing_Standard ls = new Listing_Standard();
            Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
          
          

            ls.Begin(rect);


            ls.Label("FF_AnimalSpawnMultiplier".Translate() + ": " + FFSpawnMultiplier, -1, "FF_AnimalSpawnMultiplierTooltip".Translate());
            FFSpawnMultiplier = (float)Math.Round(ls.Slider(FFSpawnMultiplier, 0.1f, 2f), 2);
            if (ls.Settings_Button("FF_Reset".Translate(), new Rect(0f, ls.CurHeight, 180f, 29f)))
            {
                FFSpawnMultiplier = FFSpawnMultiplierBase;
            }

            ls.Gap(40f);


            for (int num = keys.Count - 1; num >= 0; num--)
            {

                bool test = pawnSpawnStates[keys[num]];
                ls.CheckboxLabeled("FF_DisableAnimal".Translate(PawnKindDef.Named(keys[num]).LabelCap), ref test);
                pawnSpawnStates[keys[num]] = test;
            }



            ls.End();
          
            base.Write();


        }


    }



}

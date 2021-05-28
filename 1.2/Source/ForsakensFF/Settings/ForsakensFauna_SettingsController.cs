using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace ForsakensFF
{





    public class ForsakensFauna_Mod : Mod
    {

        public static ForsakensFauna_Settings settings;

        public ForsakensFauna_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ForsakensFauna_Settings>();
        }
        public override string SettingsCategory() => "Forsakens: Fauna";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            FFToggleableSpawnDef toggleablespawndef = (from k in DefDatabase<FFToggleableSpawnDef>.AllDefsListForReading
                                                       where k.defName == "FF_ToggleableAnimals"
                                                       select k).RandomElement();


            if (settings.pawnSpawnStates == null) settings.pawnSpawnStates = new Dictionary<string, bool>();
            foreach (string defName in toggleablespawndef.toggleablePawns)
            {
                if (!settings.pawnSpawnStates.ContainsKey(defName))
                {
                    settings.pawnSpawnStates[defName] = false;
                }
            }



            settings.DoWindowContents(inRect);


        }
    }
}

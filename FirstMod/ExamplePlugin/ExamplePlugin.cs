using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BepInEx;
using HarmonyLib;
using IL.RoR2.WwiseUtils;
using R2API;
using R2API.Utils;
using RoR2;

namespace ExamplePlugin
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]
    internal class ExamplePlugin : BaseUnityPlugin
    {
        internal const string PluginGUID = PluginAuthor + "." + PluginName;
        internal const string PluginAuthor = "Yariazen-Nfox18212";
        internal const string PluginName = "ExamplePlugin";
        internal const string PluginVersion = "1.0.0";

        internal string CurrentTrackName { get; set; }

        public ExamplePlugin()
        {
            Harmony harmony = new Harmony(PluginGUID);
            harmony.Patch(
                original: AccessTools.Method(typeof(RoR2.WwiseUtils.SoundbankLoader),
                    nameof(RoR2.WwiseUtils.SoundbankLoader.Start)),
                postfix: new HarmonyMethod(typeof(ExamplePlugin), nameof(GetSoundbankStrings)));
            
            Log.Init(Logger);
            On.RoR2.MusicController.UpdateState += GetTrackName;
        }

        internal void Awake()
        {
            Log.LogInfo($"{nameof(Awake)} done.");
        }

        internal void GetTrackName(On.RoR2.MusicController.orig_UpdateState orig, MusicController self)
        {
            orig(self);

            if (self && self.currentTrack)
            {
                string NewTrackName = self.currentTrack.cachedName;
                if(NewTrackName != CurrentTrackName)
                {
                    CurrentTrackName = NewTrackName;
                    Log.LogInfo($"{CurrentTrackName}");
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"Current Song: {CurrentTrackName}"
                    });
                }
            }
        }

        internal void GetSoundbankStrings(ref RoR2.WwiseUtils.SoundbankLoader __instance)
        {
            var bankStrings = __instance.soundbankStrings;
            if (bankStrings != null)
            {
                List<string> soundbank = bankStrings.ToList();
                foreach (var s in soundbank)
                {
                    Log.LogInfo(s);
                }
            }
            else
            { 
                Log.LogInfo("bankStrings is null");
            }
        }
    }
}

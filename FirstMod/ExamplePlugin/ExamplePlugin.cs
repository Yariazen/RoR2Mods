using BepInEx;
using R2API;
using R2API.Utils;
using HarmonyLib;
using RoR2;

namespace ExamplePlugin
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]
    public class ExamplePlugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Yariazen-NFox18212";
        public const string PluginName = "ExamplePlugin";
        public const string PluginVersion = "1.0.0";

        public ExamplePlugin()
        {
            Log.Init(Logger);
            Harmony harmony = new Harmony(PluginGUID);
            harmony.Patch(
                original: AccessTools.Method(typeof(MusicController), nameof(MusicController.PickCurrentTrack)),
                prefix: new HarmonyMethod(typeof(ExamplePlugin), nameof(ExamplePlugin.Postfix_MusicController_PickCurrentTrack))
            );
        }

        public void Awake()
        {
            Log.LogInfo($"{nameof(Awake)} done.");
        }

        private static void Postfix_MusicController_PickCurrentTrack(ref MusicTrackDef newTrack)
        {
            if (newTrack != null)
            {
                Log.LogInfo($"{newTrack.cachedName}");
            }
        }
    }
}

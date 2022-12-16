using BepInEx;
using R2API;
using R2API.Utils;

namespace ExamplePlugin
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]
    public class ExamplePlugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Yariazen/NFox18212";
        public const string PluginName = "ExamplePlugin";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo(nameof(Awake) + " done.");
        }

        private void Update() { }
    }
}

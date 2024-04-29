using UnityEditor;
using System.Collections.Generic;
using Edgegap.Editor;
using UnityEngine;

namespace Edgegap.Editor
{
    public class BuildScript
    {
        static string[] GetEnabledScenes()
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            List<string> enabledScenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in scenes)
            {
                if (scene.enabled)
                {
                    enabledScenes.Add(scene.path);
                }
            }

            return enabledScenes.ToArray();
        }

        [MenuItem("Build/Windows 64-bit")]
        static void BuildWindows64()
        {
            string projectName = "Terron"; // Set the name of your project here
            string outputPath = $"Builds/Windows64/{projectName}64Windows.exe";

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

            buildPlayerOptions.scenes = GetEnabledScenes();
            buildPlayerOptions.locationPathName = outputPath;
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.subtarget = (int)StandaloneBuildSubtarget.Player;

            // Set the scripting backend to IL2CPP for Windows 64-bit
            //PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
            //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "WINDOWS64");

            // Disable development build options
            EditorUserBuildSettings.development = false;

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        [MenuItem("Build/Edgegap Server")]
        static void BuildEdgegapServer()
        {
            EdgegapWindowV2 edgegapWindowInstance = ScriptableObject.CreateInstance<EdgegapWindowV2>();

            edgegapWindowInstance.setVariablesAndBuildAndPushServer();
        }
    }
}
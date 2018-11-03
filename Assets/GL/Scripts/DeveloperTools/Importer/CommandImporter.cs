using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GL.Battle.Commands.Element;
using GL.Battle.Commands.Element.Blueprints;
using GL.Database;
using GL.Extensions;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using static GL.Battle.Commands.Bundle.Implement.Parameter;

namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandImporter
    {
        [MenuItem("GL/MasterData/Import Command")]
        private static void Import()
        {
            var commandBundleData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - CommandRecord.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var commandElementData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - CommandElement.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var commandElementDictionary = new Dictionary<string, Blueprint>();

            var elementDictionary = new Dictionary<string, List<string>>();
            var commandNameAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/CommandName.asset");
            var commandDescriptionAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/CommandDescription.asset");

            // CommandElementを抽出
            for (var i = 1; i < commandElementData.Length; i++)
            {
                var e = commandElementData[i];
                var splitElementData = e.Split(',');
                GetAssetName(splitElementData.ToList());
                var commandName = splitElementData[0];
                List<string> elements = null;
                if(!elementDictionary.TryGetValue(commandName, out elements))
                {
                    elements = new List<string>();
                    elementDictionary.Add(commandName, elements);
                }

                elements.Add(e);
            }

            // CommandRecordを抽出
            for (var i = 1; i < commandBundleData.Length; i++)
            {
                var split = commandBundleData[i].Split(',');
                var fileName = split[0];
                var commandName = split[1];
                var path = $"Assets/GL/MasterData/Commands/Bundles/";
                var bundle = ImporterUtility.GetOrCreate<CommandRecord>(path, fileName);
                bundle.Parameter.Set(
                    commandNameAsset.CreateOrGetFinder(commandName),
                    commandDescriptionAsset.CreateOrGetFinder(split[2]),
                    (Constants.TargetPartyType)Enum.Parse(typeof(Constants.TargetPartyType), split[3]),
                    (Constants.TargetType)Enum.Parse(typeof(Constants.TargetType), split[4]),
                    (Constants.PostprocessCommand)Enum.Parse(typeof(Constants.PostprocessCommand), split[5]),
                    int.Parse(split[6]),
                    int.Parse(split[7]),
                    GetBlueprintLists(elementDictionary[commandName], commandElementDictionary)
                );
                EditorUtility.SetDirty(bundle);
            }

            AssetDatabase.SaveAssets();
        }

        private static BlueprintList[] GetBlueprintLists(List<string> commandData, Dictionary<string, Blueprint> blueprintDictionary)
        {
            var splitCommandData = commandData.Select(c => c.Split(',').ToList()).ToArray();
            splitCommandData.ForEach(s => s.RemoveAll(m => m == "\r"));
            splitCommandData.Sort(new CommandDataComparer());
            var result = new List<BlueprintList>();
            var index = -1;
            BlueprintList blueprintList = null;
            foreach(var s in splitCommandData)
            {
                var newIndex = int.Parse(s[2]);
                if (index != newIndex)
                {
                    if (blueprintList != null)
                    {
                        result.Add(blueprintList);
                    }
                    index = newIndex;
                    blueprintList = new BlueprintList();
                }
                var elementAssetName = GetAssetName(s);
                blueprintList.Elements.Add(GetOrCreateBlueprint(blueprintDictionary, elementAssetName));
            }

            result.Add(blueprintList);
            return result.ToArray();
        }

        private static Blueprint GetOrCreateBlueprint(Dictionary<string, Blueprint> dictionary, string key)
        {
            Blueprint result = null;
            if(!dictionary.TryGetValue(key, out result))
            {
                var path = $"Assets/GL/MasterData/Commands/Elements/{key}.asset";
                result = AssetDatabase.LoadAssetAtPath<Blueprint>(path);
                if(result == null)
                {
                    result = CreateBlueprint(key);
                    AssetDatabase.CreateAsset(result, path);
                }
                dictionary.Add(key, result);
            }

            return result;
        }

        private static Blueprint CreateBlueprint(string data)
        {
            var type = data.Substring(0, data.IndexOf("_"));
            Blueprint result = null;
            switch(type)
            {
                case "Attack":
                    result = ScriptableObject.CreateInstance<Attack>().SetupFromEditor(data);
                    break;
                case "AddStatusParameterRate":
                    result = ScriptableObject.CreateInstance<AddStatusParameterRate>().SetupFromEditor(data);
                    break;
                case "AddStatusAilment":
                    result = ScriptableObject.CreateInstance<AddStatusAilment>().SetupFromEditor(data);
                    break;
                case "Recovery":
                    result = ScriptableObject.CreateInstance<Recovery>().SetupFromEditor(data);
                    break;
                case "RecoveryFixed":
                    result = ScriptableObject.CreateInstance<RecoveryFixed>().SetupFromEditor(data);
                    break;
            }

            return result;
        }

        private static string GetAssetName(List<string> splitElementData)
        {
            splitElementData.RemoveAll(m => string.IsNullOrEmpty(m) || m == "\r");
            var builder = new StringBuilder();
            for (var i = 3; i < splitElementData.Count; i++)
            {
                var s = splitElementData[i].RemoveNewLine();
                builder.Append(s);
                if(i + 1 < splitElementData.Count)
                {
                    builder.Append("_");
                }
            }

            return builder.ToString();
        }

        public class CommandDataComparer : IComparer<List<string>>
        {
            public int Compare(List<string> x, List<string> y)
            {
                var xIndex = int.Parse(x[2]);
                var yIndex = int.Parse(y[2]);

                return xIndex - yIndex;
            }
        }
    }
}

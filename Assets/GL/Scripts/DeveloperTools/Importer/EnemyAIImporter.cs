using System;
using System.IO;
using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using GL.Extensions;
using System.Collections.Generic;
using GL.Battle.AIControllers;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnemyAIImporter
    {
        [MenuItem("GL/MasterData/Import EnemyAI")]
        public static void Import()
        {
            var commandConditionData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EnemyAICommandCondition.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var invokeCommandData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EnemyAICommandRecord.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var onEndTurnEventConditionData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EnemyAIOnEndTurnEventCondition.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var onEndTurnEventRecordData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EnemyAIOnEndTurnEventRecord.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var commandConditionDictionary = new Dictionary<string, List<string>>();
            var invokeCommandDictionary = new Dictionary<string, List<string>>();
            var onEndTurnEventConditionDictionary = new Dictionary<string, List<string>>();
            var onEndTurnEventRecordDictionary = new Dictionary<string, List<string>>();
            var commandSelectorDictionary = new Dictionary<string, List<CommandSelector>>();
            var onEndTurnEventSelectorDictionary = new Dictionary<string, List<EventSelector>>();

            // 実際のCommandRecordを予め読み込んでおく
            var commandRecords = AssetDatabase.FindAssets("t:CommandRecord", new string[] { "Assets/GL/MasterData/Commands/Bundles" })
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .Select(path => AssetDatabase.LoadAssetAtPath<CommandRecord>(path))
                .ToDictionary(c => c.Parameter.Name.Get);

            // CommandConditionを抽出
            var currentPatternId = -1;
            var currentListId = 0;
            for (var i = 1; i < commandConditionData.Length; i++)
            {
                var splitData = commandConditionData[i].Split(',');
                var enemyId = splitData[0];
                var patternId = int.Parse(splitData[1]);
                if(currentPatternId != patternId)
                {
                    currentPatternId = patternId;
                    currentListId = 0;
                }
                var commandConditionKey = $"{enemyId}_{patternId}_{currentListId}";
                currentListId++;
                List<string> commandConditionList = null;
                if(!commandConditionDictionary.TryGetValue(commandConditionKey, out commandConditionList))
                {
                    commandConditionList = new List<string>();
                    commandConditionDictionary.Add(commandConditionKey, commandConditionList);
                }
                commandConditionList.Add(commandConditionData[i]);
            }

            // InvokeCommandを抽出
            currentPatternId = -1;
            for (var i = 1; i < invokeCommandData.Length; i++)
            {
                var splitData = invokeCommandData[i].Split(',');
                var enemyId = splitData[0];
                var patternId = int.Parse(splitData[1]);
                if(currentPatternId != patternId)
                {
                    currentPatternId = patternId;
                    currentListId = 0;
                }
                var invokeCommandKey = $"{enemyId}_{patternId}_{currentListId}";
                currentListId++;
                List<string> invokeCommandList = null;
                if (!invokeCommandDictionary.TryGetValue(invokeCommandKey, out invokeCommandList))
                {
                    invokeCommandList = new List<string>();
                    invokeCommandDictionary.Add(invokeCommandKey, invokeCommandList);
                }
                invokeCommandList.Add(invokeCommandData[i]);
            }

            // CommandSelectorを作成
            foreach(var commandConditionPair in commandConditionDictionary)
            {
                var fileName = commandConditionPair.Key;
                var enemyId = fileName.Split('_')[0];
                Assert.IsTrue(invokeCommandDictionary.ContainsKey(fileName), $"{fileName}に対応するCommandRecordがありませんでした");
                var commandConditions = commandConditionPair.Value.Select(c =>
                {
                    var splitData = c.Split(',');
                    var conditionName = splitData[2];
                    var result = AssetDatabase.LoadAssetAtPath<Condition>($"Assets/GL/MasterData/AI/Conditions/{conditionName}.asset");
                    Assert.IsNotNull(result, $"{conditionName}のConditionがありませんでした");
                    return result;
                });
                var invokeCommands = invokeCommandDictionary[fileName].Select(c =>
                {
                    var splitData = c.Split(',');
                    var commandName = splitData[2];
                    var targetType = (InvokeCommand.TargetType)Enum.Parse(typeof(InvokeCommand.TargetType), splitData[3]);
                    var command = commandRecords[commandName];
                    Assert.IsNotNull(command, $"{commandName}のコマンドがありませんでした");
                    return new InvokeCommand().Set(command, targetType);
                });
                var path = "Assets/GL/MasterData/AI/CommandSelector/";
                var commandSelector = ImporterUtility.GetOrCreate<CommandSelector>(path, fileName).Set(commandConditions.ToArray(), invokeCommands.ToArray());

                List<CommandSelector> commandSelectors = null;
                if(!commandSelectorDictionary.TryGetValue(enemyId, out commandSelectors))
                {
                    commandSelectors = new List<CommandSelector>();
                    commandSelectorDictionary.Add(enemyId, commandSelectors);
                }
                commandSelectors.Add(commandSelector);
            }

            // OnEndTurnEventConditionを抽出
            currentPatternId = -1;
            currentListId = 0;
            for (var i = 1; i < onEndTurnEventConditionData.Length; i++)
            {
                var splitData = onEndTurnEventConditionData[i].Split(',');
                var enemyId = splitData[0];
                var patternId = int.Parse(splitData[1]);
                if(currentPatternId != patternId)
                {
                    currentPatternId = patternId;
                    currentListId = 0;
                }
                var onEndTurnEventConditionKey = $"{enemyId}_{patternId}_{currentListId}";
                currentListId++;
                List<string> onEndTurnEventConditionList = null;
                if (!onEndTurnEventConditionDictionary.TryGetValue(onEndTurnEventConditionKey, out onEndTurnEventConditionList))
                {
                    onEndTurnEventConditionList = new List<string>();
                    onEndTurnEventConditionDictionary.Add(onEndTurnEventConditionKey, onEndTurnEventConditionList);
                }
                onEndTurnEventConditionList.Add(onEndTurnEventConditionData[i]);
            }

            // OnEndTurnEventRecordを抽出
            currentPatternId = -1;
            for (var i = 1; i < onEndTurnEventRecordData.Length; i++)
            {
                var splitData = onEndTurnEventRecordData[i].Split(',');
                var enemyId = splitData[0];
                var patternId = int.Parse(splitData[1]);
                if(currentPatternId != patternId)
                {
                    currentPatternId = patternId;
                    currentListId = 0;
                }
                var onEndTurnEventRecordKey = $"{enemyId}_{patternId}_{currentListId}";
                currentListId++;
                List<string> onEndTurnEventRecordList = null;
                if(!onEndTurnEventRecordDictionary.TryGetValue(onEndTurnEventRecordKey, out onEndTurnEventRecordList))
                {
                    onEndTurnEventRecordList = new List<string>();
                    onEndTurnEventRecordDictionary.Add(onEndTurnEventRecordKey, onEndTurnEventRecordList);
                }
                onEndTurnEventRecordList.Add(onEndTurnEventRecordData[i]);
            }

            // OnEndTurnEventSelectorを作成
            foreach(var onEndTurnEventConditionPair in onEndTurnEventConditionDictionary)
            {
                var fileName = onEndTurnEventConditionPair.Key;
                var enemyId = fileName.Split('_')[0];
                Assert.IsTrue(onEndTurnEventRecordDictionary.ContainsKey(fileName), $"{fileName}に対応するonEndTurnEventRecordがありませんでした");
                var onEndTurnEventConditions = onEndTurnEventConditionPair.Value
                    .Select(e =>
                    {
                        var splitData = e.Split(',');
                        var conditionName = splitData[2];
                        var result = AssetDatabase.LoadAssetAtPath<Condition>($"Assets/GL/MasterData/AI/Conditions/{conditionName}.asset");
                        Assert.IsNotNull(result, $"{conditionName}のConditionがありませんでした");
                        return result;
                    });
                var onEndTurnEventRecords = onEndTurnEventRecordDictionary[fileName]
                    .Select(e =>
                    {
                        var splitData = e.Split(',');
                        return GetOrCreateEvent(splitData, commandRecords);
                    });
                var path = "Assets/GL/MasterData/AI/EventSelector/";
                var eventSelector = ImporterUtility.GetOrCreate<EventSelector>(path, fileName).Set(onEndTurnEventConditions.ToArray(), onEndTurnEventRecords.ToArray());

                List<EventSelector> onEndTurnEventSelectors = null;
                if(!onEndTurnEventSelectorDictionary.TryGetValue(enemyId, out onEndTurnEventSelectors))
                {
                    onEndTurnEventSelectors = new List<EventSelector>();
                    onEndTurnEventSelectorDictionary.Add(enemyId, onEndTurnEventSelectors);
                }
                onEndTurnEventSelectors.Add(eventSelector);
            }

            // AIを作成
            foreach(var commandSelectorPair in commandSelectorDictionary)
            {
                var enemyId = commandSelectorPair.Key;
                List<CommandSelector> commandSelectors = null;
                commandSelectorDictionary.TryGetValue(enemyId, out commandSelectors);
                List<EventSelector> onEndTurnEventSelectors = null;
                onEndTurnEventSelectorDictionary.TryGetValue(enemyId, out onEndTurnEventSelectors);
                var path = "Assets/GL/MasterData/AI/Controllers/";
                ImporterUtility.GetOrCreate<AI>(path, enemyId).Set(GetCommandSelectorLists(commandSelectors), GetOnEndTurnEventSelectorLists(onEndTurnEventSelectors));
            }
        }

        private static Battle.AIControllers.Event GetOrCreateEvent(string[] splitData, Dictionary<string, CommandRecord> commandRecords)
        {
            var path = "Assets/GL/MasterData/AI/Events/";
            var type = splitData[2];
            switch(type)
            {
                case "Counter":
                    var commandRecord = commandRecords[splitData[3]];
                    var targetType = (Counter.TargetType)Enum.Parse(typeof(Counter.TargetType), splitData[4]);
                    return ImporterUtility.GetOrCreate<Counter>(path, $"{type}_{commandRecord.Id}_{targetType}").Set(commandRecord, targetType);
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
                    return null;
            }
        }

        private static AI.CommandSelectorList[] GetCommandSelectorLists(List<CommandSelector> commandSelectors)
        {
            var result = new List<AI.CommandSelectorList>();
            foreach(var commandSelector in commandSelectors)
            {
                var splitData = commandSelector.name.Split('_');
                var patternId = int.Parse(splitData[1]);
                var imax = result.Count;
                for (var i = patternId; i <= imax; i++)
                {
                    result.Add(new AI.CommandSelectorList());
                }
                result[patternId].CommandSelectors.Add(commandSelector);
            }

            return result.ToArray();
        }

        private static AI.EventSelectorList[] GetOnEndTurnEventSelectorLists(List<EventSelector> eventSelectors)
        {
            var result = new List<AI.EventSelectorList>();
            foreach(var eventSelector in eventSelectors)
            {
                var splitData = eventSelector.name.Split('_');
                var patternId = int.Parse(splitData[1]);
                var imax = result.Count;
                for (var i = patternId; i <= imax; i++)
                {
                    result.Add(new AI.EventSelectorList());
                }
                result[patternId].EventSelectors.Add(eventSelector);
            }

            return result.ToArray();
        }
    }
}
#endif

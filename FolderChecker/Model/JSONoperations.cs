using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace FolderChecker.Model
{
    public static class JSONoperations
    {
        private static void CreateListOfRulesJSON(List<Rule> rules, string path)
        {
            string json = JsonConvert.SerializeObject(rules);
            File.WriteAllText(path+"\\rule.json", json);
        }
        public static void onRuleUpdated(object source, RuleEventArgs ruleEventArgs)
        {
            CreateListOfRulesJSON(ruleEventArgs.rules, ruleEventArgs.jsonPath);
        }
        public static ObservableCollection<Rule> loadRules(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ObservableCollection<Rule>>(json);
        }
        public static string LoadSettings(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);
            return json;
        }
    }
}

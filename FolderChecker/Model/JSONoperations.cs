using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace FolderChecker.Model
{
    public static class JSONoperations
    {
        private static void CreateListOfRulesJSON(List<Rule> rules)
        {
            string json = JsonConvert.SerializeObject(rules);
            File.WriteAllText(@"C:\Users\tomasz.tkocz\Desktop\ghghg\rule.json", json);
        }
        public static void onRuleUpdated(object source, RuleEventArgs ruleEventArgs)
        {
            CreateListOfRulesJSON(ruleEventArgs.rules);
        }
        public static ObservableCollection<Rule> loadRules()
        {
            string json = File.ReadAllText(@"C:\Users\tomasz.tkocz\Desktop\ghghg\rule.json");
            return JsonConvert.DeserializeObject<ObservableCollection<Rule>>(json);
        }

    }
}

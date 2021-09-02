using System;
using System.Collections.Generic;

namespace FolderChecker.Model
{
    public class RuleEventArgs: EventArgs
    {
        public Rule rule { get; set; }
        public List<Rule> rules { get; set; }
    }
}

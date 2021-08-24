using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.Model
{
    public class RuleEventArgs: EventArgs
    {
        public Rule rule { get; set; }
        public List<Rule> rules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework18._06.наследники
{
    public class LegendaryArtifact : Artifact
    {
        public string CurseDescription { get; set; }
        public bool IsCursed { get; set; }

        public override string Serialize() { return $"Legendary: {Name}, Cursed: {IsCursed}"; }
    }
}

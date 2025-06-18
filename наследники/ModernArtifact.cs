using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework18._06.наследники
{
    public class ModernArtifact : Artifact
    {
        public double TechLevel { get; set; }
        public string Manufacturer { get; set; }

        public override string Serialize() { return $"Modern: {Name}, TechLevel: {TechLevel}"; }
    }
}

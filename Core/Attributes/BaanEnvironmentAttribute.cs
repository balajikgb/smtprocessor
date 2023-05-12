using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    public class BaanEnvironmentAttribute : Attribute
    {
        public string environment;
        public BaanEnvironmentAttribute(string environment)
        {
            this.environment = environment;
        }
    }
}

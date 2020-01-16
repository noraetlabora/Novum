using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Access.AccessSystems.ExSI
{
    public class ExSI : AccessSystemBase
    {
        public override string Cancel()
        {
            return this.name;
        }

        public override string CheckMedium(string mediumId)
        {
            throw new NotImplementedException();
        }

        public override string Pay()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            string xxx = base.
        }
    }
}

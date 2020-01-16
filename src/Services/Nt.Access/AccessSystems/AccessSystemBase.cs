using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Access.AccessSystems
{
    public abstract class AccessSystemBase
    {
        protected string name;

        public abstract string CheckMedium(string mediumId);

        public abstract string Pay();

        public abstract string Cancel();
    }
}

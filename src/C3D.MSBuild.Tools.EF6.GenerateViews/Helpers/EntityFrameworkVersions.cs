using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity.Design
{
    class EntityFrameworkVersions
    {
        public readonly static Version Version1;

        public readonly static Version Version2;

        public readonly static Version Version3;

        internal static Version EdmVersion1_1
        {
            get
            {
                return new Version(1, 1, 0, 0);
            }
        }

        static EntityFrameworkVersions()
        {
            EntityFrameworkVersions.Version1 = new Version(1, 0, 0, 0);
            EntityFrameworkVersions.Version2 = new Version(2, 0, 0, 0);
            EntityFrameworkVersions.Version3 = new Version(3, 0, 0, 0);
        }

        internal static Version ConvertToVersion(double runtimeVersion)
        {
            if (runtimeVersion == 1 || runtimeVersion == 0)
            {
                return EntityFrameworkVersions.Version1;
            }
            if (runtimeVersion == 1.1)
            {
                return EntityFrameworkVersions.EdmVersion1_1;
            }
            if (runtimeVersion == 2)
            {
                return EntityFrameworkVersions.Version2;
            }
            return EntityFrameworkVersions.Version3;
        }
    }


}

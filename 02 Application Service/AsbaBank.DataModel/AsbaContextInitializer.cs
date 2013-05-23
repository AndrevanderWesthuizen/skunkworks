using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsbaBank.DataModel
{
    public class AsbaContextInitializer
    {
        public void InitializeDatabase(AsbaContext context)
        {
            context.Database.Delete();
            context.Database.Create();
        }
    }
}

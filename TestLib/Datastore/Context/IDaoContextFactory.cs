using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib.Datastore.Context
{
    public interface IDaoContextFactory
    {
        IDaoContext CreateContext();
    }
}

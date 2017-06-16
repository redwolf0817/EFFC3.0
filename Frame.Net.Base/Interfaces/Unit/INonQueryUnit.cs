﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFFC.Frame.Net.Base.Interfaces.Core;
using EFFC.Frame.Net.Base.Parameter;

namespace EFFC.Frame.Net.Base.Interfaces.Unit
{
    public interface INonQueryUnit<T> where T : ParameterStd
    {
        Func<T, string> NonQuerySQLFunc(string flag);
    }
}
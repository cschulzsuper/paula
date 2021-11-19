﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Paula.Application.Runtime
{
    public interface IConnectionManager
    {
        void Trace(string realm, string account, string proof);

        void Forget(string realm, string account);

        bool Verify(string realm, string account, string proof);
    }
}
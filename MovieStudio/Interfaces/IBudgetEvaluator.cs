﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStudio.Interfaces
{
    public interface IBudgetEvaluator
    {
        bool CanBeProduced(MovieDefinition movieDefinition);
    }
}

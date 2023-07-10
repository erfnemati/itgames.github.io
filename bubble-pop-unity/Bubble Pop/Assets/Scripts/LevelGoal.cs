using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    interface LevelGoal
    {
        abstract public bool IsGoalReached();
        abstract public float GetLevelRate();
    }
}

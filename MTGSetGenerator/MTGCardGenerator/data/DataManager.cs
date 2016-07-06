using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    public static class DataManager
    {
        public static void InitDataClasses()
        {
            CardColors.InitCardColors();
            CardFrames.InitCardFrames();
            CardTypes.InitCardTypes();
            CostIcons.InitCostIcons();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MTGSetGenerator
{
    public interface IAddNewCardPage
    {
        void ResetToDefault();
        void RefreshCardPreview();
        IAddNewCardPage GenerateNextAddNewCardPage(out bool isFinalPage);
    }
}

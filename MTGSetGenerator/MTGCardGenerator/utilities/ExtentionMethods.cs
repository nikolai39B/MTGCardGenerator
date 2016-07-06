using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    static class ExtentionMethods
    {
        //--------//
        // Events //
        //--------//
        public static void SafeInvoke(this EventHandler eventToInvoke, object sender, EventArgs e)
        {
            var ev = eventToInvoke;
            if (ev != null)
            {
                ev.Invoke(sender, e);
            }
        }
    }
}

using System.Collections.Generic;

namespace BarLauncher.EasyHelper
{
    public delegate IEnumerable<BarLauncherResult> ResultGetter(BarLauncherQuery query, int position);
}
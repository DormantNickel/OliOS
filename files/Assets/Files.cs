using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliOS.Assets
{
    public static class Files
    {
        [ManifestResourceStream(ResourceName = "OliOS.Assets.Wallpapers.orgwallpaper1.bmp")] public static byte[] OliOSBackgroundRaw;
        [ManifestResourceStream(ResourceName = "OliOS.Assets.Cursors.orgcursor1.bmp")] public static byte[] OliOSCursorRaw;
    }
}

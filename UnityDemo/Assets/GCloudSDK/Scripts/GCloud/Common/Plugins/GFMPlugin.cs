using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace GCloud
{
    public class GFMPlugin
    {
        public static readonly GFMPlugin Instance = new GFMPlugin();

        public IntPtr GetConnectorFactory()
        {
            return gfm_connector_get_factory_instance();
        }

        #region GFMPlugin Private function
        private GFMPlugin() { }
        #endregion

        #region GFMPlugin DllImport
        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr gfm_connector_get_factory_instance();
        #endregion

    }
}

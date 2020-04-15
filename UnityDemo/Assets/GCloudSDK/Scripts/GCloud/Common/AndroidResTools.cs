using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GCloud
{

    class AndroidResourceType
    {
        public const string ID  = "id";
        public const string Layout = "layout";
        public const string Drawable = "drawable";
        public const string Menu = "menu";
        public const string String = "string";
        public const string Style = "style";

        public static bool Valied(string typeName)
        {
            if ( typeName == ID ||
                typeName == Layout ||
                typeName == Drawable ||
                typeName == Menu ||
                typeName == String ||
                typeName == Style) 
            {
                return true;
            }
            return false;

        }
    }

    public class AndroidResourceTools
    {

        public static int GetResourceID(string resName,string typeName)
        {
#if UNITY_ANDROID
			if (! AndroidResourceType.Valied(typeName)) {
                return 0;
            }
            return gcloud_utils_get_res_id(resName,typeName);
#else 
            return 0;
#endif
        }


    #region Dllimport
    #if UNITY_ANDROID
    [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
    private static extern int gcloud_utils_get_res_id([MarshalAs(UnmanagedType.LPStr)] string resName,[MarshalAs(UnmanagedType.LPStr)] string typeName);
    #endif
    #endregion

    }
}
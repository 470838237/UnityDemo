using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


namespace GCloud.GRobot
{
    public class GRobotEngine
    {
#if UNITY_EDITOR
        public const string LibName = "grobot";
#else
    #if UNITY_IPHONE
            public const string LibName = "__Internal";
    #else
            public const string LibName = "grobot";
    #endif
#endif

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void GRobot_Show(string paramers, int engineType);
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void GRobot_Close();
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void GRobot_SetURLCallback(GRobotURLCallback callback);

        public static void Show(string paramers) {
            GRobot_Show(paramers, 0);
        }

        public static void Close() {
            GRobot_Close();
        }

        public static void SetURLCallback(GRobotURLCallback callback) {
            GRobot_SetURLCallback(callback);
        }

        public delegate void GRobotURLCallback(string text);
    }
}

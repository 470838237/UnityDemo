//------------------------------------------------------------------------------
//
// File: OneSDKTss.h
// Module: TssSDK
// Version: 3.0
// Author: TssSDK Dev Team
//
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GCloud.TssSDK
{
    
    public enum TssSDKEntryType
    {
        EntryIdQQ = 1,     // QQ
        EntryIdMM = 2,     // WeChat
        EntryIdFacebook = 3,
        EntryIdTwitter = 4,
        EntryIdLine = 5,
        EntryIdWhatsapp = 6,
        EntryIdGamecenter = 7,
        EntryIdGoogleplay = 8,
        EntryIdVK = 9,
        EntryIdOthers = 99
    }
    
    
    #region TssSDK definition
    public static class TssSDK
    {
        
        public static void Init(int gameId)
        {
            TssSDKInit(gameId);
        }
        public static void SetUserInfo(int entryId, string openId)
        {
            TssSDKSetUserInfo(entryId, openId);
        }
        public static void OnPause()
        {
            TssSDKOnPause();
        }
        public static void OnResume()
        {
            TssSDKOnResume();
        }
        public static byte[] GetReportData()
        {
            IntPtr addr = TssSDKGetReportData();
            if (addr == IntPtr.Zero)
            {
                return null;
            }
            ushort anti_data_len = (ushort)Marshal.ReadInt16(addr, 0);
            IntPtr anti_data = ReadIntPtr(addr, 2);

            //
            if (anti_data == IntPtr.Zero)
            {
                TssSDKDelReportData(addr);
                return null;
            }
            //
            byte[] data = new byte[anti_data_len];
            Marshal.Copy(anti_data, data, 0, anti_data_len);
            
            //
            TssSDKDelReportData(addr);
            //
            return data;
        }
        public static void OnRecvData(byte[] data)
        {
            TssSDKOnRecvData(data, data.Length);
        }
        
        
        private static Boolean Is64bit()
        {
            return IntPtr.Size == 8;
        }

        private static Boolean Is32bit()
        {
            return IntPtr.Size == 4;
        }
            /**
         * 读取指针
         * 
         * 说明:直接使用Marshal.ReadIntPtr是不可以的，测试的时候，在一些老版本的Unity(4.*)上，在编译成64ios工程后会编译不过
         *         所以需要先按机器位数(32/64)读整形，再转成IntPtr指针
         */
        private static IntPtr ReadIntPtr(IntPtr addr, int off)
        {
            IntPtr ptr = IntPtr.Zero;
            if (Is64bit())
            {
                Int64 v64 = Marshal.ReadInt64(addr, off);
                ptr = new IntPtr(v64);
            }
            else if (Is32bit())
            {
                Int32 v32 = Marshal.ReadInt32(addr, off);
                ptr = new IntPtr(v32);
            }
            return ptr;
        }
   
#if UNITY_IOS
        internal const string LibName = "__Internal";
#else
        internal const string LibName = "tersafe";
#endif
   
        [DllImport(LibName)]
        public static extern void TssSDKInit(int gameID);
        
        
        [DllImport(LibName)]
        public static extern void TssSDKSetUserInfo(int entryId, string openId);
    
        [DllImport(LibName)]
        public static extern void TssSDKOnPause();
    
    
        [DllImport(LibName)]
        public static extern void TssSDKOnResume();
        
        [DllImport(LibName)]
        public static extern IntPtr TssSDKGetReportData();
        
        
        [DllImport(LibName)]
        public static extern void TssSDKDelReportData(IntPtr info);

        [DllImport(LibName)]
        private static extern void TssSDKOnRecvData(byte[] data, int data_len);
    }
    #endregion
}
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

namespace GCloud
{
	public class Tdir : ApolloObject, ITdir
	{
		public event QueryAllHandler QueryAllEvent;
		public event QueryTreeHandler QueryTreeEvent;
		public event QueryLeafHandler QueryLeafEvent;

		static Tdir instance;

		public static Tdir Instance 
		{
			get 
			{
				if (instance == null) 
				{
					instance = new Tdir ();
				}
				return instance;
			}
		}

		public bool Initialize (TdirInitInfo initInfo)
		{
			if (initInfo != null) {
				byte[] buffer;
				if (!initInfo.Encode (out buffer)) {
					ADebug.LogError("Tdir Initialize Encode Error");
					return false;
				}
				return gcloud_tdir_initialize (buffer, buffer.Length);
			}
			return false;
		}

		public new void Update ()
		{
			gcloud_tdir_update ();
		}

		public bool IsConnected()
		{
			return gcloud_tdir_isconnected ();
		}

		public int QueryAll ()
		{
			return gcloud_tdir_queryall ();
		}

		public int QueryTree (Int32 treeId)
		{
			return gcloud_tdir_querytree (treeId);
		}

		public int QueryLeaf (Int32 treeId, Int32 leafId)
		{
			return gcloud_tdir_queryleaf (treeId, leafId);
		}

		#region Callback
		
		void OnQueryAllProc (int error, byte[] data)
		{
			Result result = new Result ();
			result.ErrorCode = (ErrorCode)error;
			ADebug.Log ("OnQueryAllProc error:" + result.ErrorCode);

			TreeCollection trees = null;
			if (result.ErrorCode == ErrorCode.Success) {
				trees = new TreeCollection ();
				if (!trees.Decode (data)) {
					ADebug.LogError ("OnQueryAllProc Decode error!");
				}
			}
			
			if (QueryAllEvent != null) {
				QueryAllEvent (result, trees);
			}
		}
		
		void OnQueryTreeProc (int error, byte[] data)
		{
			Result result = new Result ();
			result.ErrorCode = (ErrorCode)error;
			
			TreeInfo tree = null;
			if (result.ErrorCode == ErrorCode.Success) {
				tree = new TreeInfo ();
				if (!tree.Decode (data)) {
					ADebug.LogError ("OnQueryTreeProc Decode error!");
				}
			}
			
			if (QueryTreeEvent != null) {
				QueryTreeEvent (result, tree);
			}
		}

		void OnQueryLeafProc (int error, byte[] data)
		{
			Result result = new Result ();
			result.ErrorCode = (ErrorCode)error;
			
			NodeWrapper node = null;
			if (result.ErrorCode == ErrorCode.Success) {
				node = new NodeWrapper ();
				if (!node.Decode (data)) {
					ADebug.LogError ("OnQueryLeafProc Decode error!");
				}
			}
			
			if (QueryLeafEvent != null) {
				QueryLeafEvent (result, node);
			}
		}

		#endregion
		
		#region Dll Import
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_tdir_initialize (byte[] data, int len);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_tdir_update ();


		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_tdir_isconnected();

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern int gcloud_tdir_queryall ();
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern int gcloud_tdir_querytree (Int32 treeId);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern int gcloud_tdir_queryleaf (Int32 treeId, Int32 leafId);


		#endregion
	}
}

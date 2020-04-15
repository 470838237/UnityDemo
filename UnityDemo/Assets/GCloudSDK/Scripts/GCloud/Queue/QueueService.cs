using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GCloud
{
	public class QueueService : ApolloObject, IQueueService
	{
		public event QueueStatusHandler QueueStatusEvent;
		public event QueueFinishedHandler QueueFinishedEvent;

		#region API

		static IQueueService instance;

		public static IQueueService Instance 
		{
			get 
			{
				if (instance == null) 
				{
					instance = new QueueService();
				}
				return instance;
			}
		}

		public bool Initialize(QueueInitInfo initInfo)
		{
			if (initInfo != null) {
				byte[] buffer;
				if (!initInfo.Encode (out buffer)) {
					ADebug.LogError("QueueService Initialize Encode Error");
					return false;
				}
				return gcloud_queue_initialize(buffer, buffer.Length);
			}
			return false;

		}

		public bool JoinQueue(string zoneId,string queflag)
		{
			return gcloud_queue_join(zoneId,queflag);
		}

		public bool ExitQueue()
		{
			return gcloud_queue_exit();
		}

		public new void Update()
		{
			gcloud_queue_update();
		}

		#endregion


		#region Callback

		void OnQueueStatusProc(int error, byte[] data)
		{
			Result result = new Result();
			result.ErrorCode = (ErrorCode)error;
			ADebug.Log("OnQueueStatusProc error:" + result.ErrorCode);

			QueueStatusInfo info = new QueueStatusInfo();
			if (!info.Decode(data)) {
				ADebug.LogError("OnQueueStatusProc Decode error!");
			}
			
			if (QueueStatusEvent != null) {
				QueueStatusEvent(result, info);
			}
		}

		void OnQueueFinishedProc(int error, byte[] data)
		{
			Result result = new Result();
			result.ErrorCode = (ErrorCode)error;
			ADebug.Log("OnQueueFinishedProc error:" + result.ErrorCode);

			QueueFinishedInfo info = new QueueFinishedInfo();
			if (!info.Decode(data)) {
				ADebug.LogError("OnQueueFinishedProc Decode error!");
			}
			
			if (QueueFinishedEvent != null) {
				QueueFinishedEvent(result, info);
			}
		}

		#endregion


		#region Dll Import

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_queue_initialize(byte[] data, int len);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_queue_join([MarshalAs(UnmanagedType.LPStr)] string zondId,[MarshalAs(UnmanagedType.LPStr)] string queflag);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_queue_exit();

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_queue_update();

		#endregion

	}

}
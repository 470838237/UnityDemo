// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace GCloud
{
	public class GCloudCommon
	{
		public GCloudCommon()
		{
		}
		
		public static byte MsgVersion = 0x0001;
		
		private static UInt32 msgSeq = 1;
		public static UInt32 MsgSeq
		{
			get
			{
				msgSeq += 2;
				return msgSeq;
			}
		}
		
		static InitializeInfo initializeInfo;
		public static InitializeInfo InitializeInfo
		{
			get
			{
				if (initializeInfo == null)
				{
					throw new Exception("IGCloud.Instance.Initialize must be called before using GCloud!");
				}
				return initializeInfo;
			}
			set
			{
				initializeInfo = value;
			}
		}
		
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_STANDALONE_OSX
		public const string PluginName = "gcloud";
		public const string ABasePluginName = "gcloud";
		#else
		
		#if UNITY_IPHONE || UNITY_XBOX360
		// On iOS and Xbox 360 plugins are statically linked into
		// the executable, so we have to use __Internal as the
		// library name
		public const string PluginName = "__Internal";
		public const string ABasePluginName = "__Internal";
		#else
		// Other platforms load plugins dynamically, so pass the name
		// of the plugin's dynamic library.
		public const string PluginName = "gcloud";
		public const string ABasePluginName = "gcloudcore";
		#endif
		
		#endif
		
	}
}


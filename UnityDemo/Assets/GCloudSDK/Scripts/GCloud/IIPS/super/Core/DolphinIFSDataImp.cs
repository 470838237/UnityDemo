using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GCloud
{
	namespace Dolphin
	{
		public class DolphinIFSDataImp : IDolphinIFSDataInterface
		{
			 
			private IIPSMobile.IIPSMobileData mFactory = null;
			private IIPSMobile.IIPSMobileDataMgrInterface mgr = null;
			private IIPSMobile.IIPSMobileDataReaderInterface mReader = null;
			private IIPSMobile.IIPSMobileDataQueryInterface mFinder = null;

			private DataIFSInitInfo mInitinfo = new DataIFSInitInfo();

			public DolphinIFSDataImp ()
			{
				
			}
			public bool Init(DataIFSInitInfo initinfo)
			{
				
				ADebug.Log(string.Format("[ApolloUpdate] initupdatemgr ifspath:{0},ifsname:{1},ifsFileSize:{2}"
					, initinfo.ifspath, initinfo.ifsname, initinfo.ifsFileSize));

				mFactory = new IIPSMobile.IIPSMobileData ();

				mInitinfo.ifsname = initinfo.ifsname;
				mInitinfo.ifspath = initinfo.ifspath;
				mInitinfo.ifsFileSize = initinfo.ifsFileSize;
				mInitinfo.isHasPwd = initinfo.isHasPwd;
				mInitinfo.password = initinfo.password;

				if (mFactory == null) 
				{
					ADebug.LogError("[DataIFS] new mFactory fail");
					return false;
				}
				string config = getIFSConfig ();
				mgr = mFactory.CreateDataMgr (config);
				if (mgr == null)
				{
					ADebug.LogError("[DataIFS] new data mgr fail,Please check init param format!");
					return false;
				}
				mReader = mgr.GetDataReader ();
				if (mReader == null) 
				{
					ADebug.LogError("[DataIFS]  GetDataReader fail");
					return false;
				}
				mFinder = mgr.GetDataQuery ();
				if(mFinder == null)
				{
					ADebug.LogError("[DataIFS]  GetDataQuery fail");
					return false;
				}
				return true;
			}
			public System.UInt32 GetLastError()
			{
				if (mgr != null) 
				{
					return mgr.MgrGetDataMgrLastError ();
				} 
				else
				{
					return System.UInt32.MaxValue;
				}

			}
			public bool Uninit()
			{
				if (mgr == null) 
				{
					ADebug.LogError("[DataIFS]  mgr is null");
					return false;
				}
				else 
				{
					bool ret = mgr.Uninit ();
					mgr = null;
					return ret;
				}
			}
			public string GetIFSPath(bool bIfsInAPK)
			{
				return DolphinFunction.GetFirstExtractIfsPath(bIfsInAPK, true);
			}

			public bool Read(System.UInt32 fileId, System.UInt64 offsetInFile, byte[] buff, ref System.UInt32 readlength)
			{
				if (mReader == null || mgr == null) 
				{
					ADebug.LogError("[DataIFS]  mReader is null");
					return false;
				} 
				else 
				{
					return mReader.Read (fileId, offsetInFile, buff, ref readlength);
				}
			}

			public string GetFileName(System.UInt32 fileId)
			{
				if (mFinder == null || mgr == null) 
				{
					ADebug.LogError("[DataIFS]  mFinder is null");
					return null;
				} 
				else 
				{
					return mFinder.GetFileName (fileId);
				}
			}


			public System.UInt32 GetFileId(string fileName)
			{
				if (mFinder == null || mgr == null) 
				{
					ADebug.LogError("[DataIFS]  mFinder is null");
					return System.UInt32.MaxValue;
				} 
				else 
				{
					return mFinder.GetFileId (fileName);
				}
			}


			public System.UInt32 GetFileSize(System.UInt32 fileId)
			{
				if (mFinder == null || mgr == null) 
				{
					ADebug.LogError("[DataIFS]  mFinder is null");
					return System.UInt32.MaxValue;
				} 
				else 
				{
					return mFinder.GetFileSize (fileId);
				}
			}


			public bool IsDirectory(System.UInt32 fileId)
			{
				if (mFinder == null || mgr == null) 
				{
					ADebug.LogError("[DataIFS]  mFinder is null");
					return false;
				} 
				else 
				{
					return mFinder.IsDirectory (fileId);
				}
			}


			private string getIFSConfig()
			{
				if (mInitinfo == null) 
				{
					ADebug.LogError("[DataIFS]  mInitinfo is null");
					return null;
				}

				string haspassword = "false";
				if (mInitinfo.isHasPwd) 
				{
					haspassword = "true";
				}

				string config = string.Format(@"
		        {{
		            ""ifs"" : 
		            {{
		                ""filelist"" : 
		                [
		                   {{
		                        ""filemetaurl"" : """",
		                        ""filename"" : ""{1}"",
		                        ""filepath"" : ""{0}"",
		                        ""filesize"" : {2},
		                        ""readonly"" : true,
		                        ""resfilename"" : """",
		                        ""url"" : ""{0}""
		                    }}
		                 ],
		                  ""hasifs"" : true,
		                  ""password"" : 
			              {{
		                     ""haspassword"" : {3},
		                     ""value"" : ""{4}""
		                  }}
		             }}
		         }}", mInitinfo.ifspath, mInitinfo.ifsname,mInitinfo.ifsFileSize,haspassword,mInitinfo.password);
				ADebug.Log (config);
				return config;
				
			}

			public bool IsFileNewestInIFS(string fileName)
			{
				return true;
			}
		}
	}
}


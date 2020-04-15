//------------------------------------------------------------------------------
// 
// File:    DolphinIFSDataInterface
// Module:  Dolphin
// Version: 1.0    
// Author:  samplewang
// 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GCloud
{
	namespace Dolphin
	{
		public class DataIFSInitInfo
		{
			public bool isHasPwd =false;//Whether need a password, no need by default
            public string password=null;
			public System.UInt64 ifsFileSize=0;//ifs file size
			public string ifspath=null; //the path of ifs file
			public string ifsname=null; //the name of ifs file

		}
		public interface IDolphinIFSDataInterface
		{
            //===================Init=======================//		
            /// <summary>
            /// Initially read the handle of ifs.
            /// </summary>
            /// <param name="initinfo">ifs initialization information</param>
            /// <returns>success or failure</returns>
            bool Init(DataIFSInitInfo initinfo);

            /// <summary>
            /// Deinitialize, release resources
            /// </summary>
            /// <returns>success or failure</returns>
			bool Uninit();
			
            /// <summary>
            /// The interface is to be implemented, combined with the download update, to determine whether a file is up to date within ifs
            /// </summary>
            /// <param name="fileName">file name</param>
            /// <returns>yes or no</returns>
			bool IsFileNewestInIFS(string fileName);

            /// <summary>
            /// Obtain an error code for a particular phase when all interfaces fail.
            /// </summary>
            /// <returns>error code</returns>
			System.UInt32 GetLastError();

            /// <summary>
            /// Get the location of the ifs, here is the location of the first package, if you want to read ifs in other locations, you can customize it During initialization 
            /// </summary>
            /// <param name="bIfsInAPK">ifs in obb or in apk</param>
            /// <returns>the path of ifs file</returns>
			string GetIFSPath(bool bIfsInAPK);

            //===================Query=======================//
            /// <summary>
            /// Get the file name based on the file ID.
            /// </summary>
            /// <param name="fileId">the file ID of the file</param>
            /// <returns>file name</returns>
			string GetFileName(System.UInt32 fileId);

            /// <summary>
            /// Get the file ID based on the file name
            /// </summary>
            /// <param name="fileName">the file name of the file</param>
            /// <returns>file ID</returns>
			System.UInt32 GetFileId(string fileName);

            /// <summary>
            /// Get the file length based on the file ID.
            /// </summary>
            /// <param name="fileId">the file ID of the file</param>
            /// <returns> the file length</returns>
			System.UInt32 GetFileSize(System.UInt32 fileId);

            /// <summary>
            /// Determines whether the current file is a directory based on the file ID.
            /// </summary>
            /// <param name="fileId">the file ID of the file</param>
            /// <returns>Whether a directory</returns>
			bool IsDirectory(System.UInt32 fileId);

            //===================Reader=======================//
            /// <summary>
            /// Read data from the IFS to the buffer.
            /// </summary>
            /// <param name="fileId"> file ID</param>
            /// <param name="offsetInFile"> The offset of the content that needs to be read in the file</param>
            /// <param name="buff"> Buffer for receiving data</param>
            /// <param name="readlength"> the length of data need to get</param>
            /// <returns> Whether read success</returns>
			bool Read(System.UInt32 fileId, System.UInt64 offsetInFile, byte[] buff, ref System.UInt32 readlength);


		}


		//--------------------------------  DolphinIFSDataFactory  --------------------------------//
		public class DolphinIFSDataFactory
		{
			private IDolphinIFSDataInterface mDolphinDataIfsMgr = null;

			public IDolphinIFSDataInterface CreateDolphinDataMgr()
			{
				mDolphinDataIfsMgr = new DolphinIFSDataImp();

				return mDolphinDataIfsMgr;
			}

		}

	}
}


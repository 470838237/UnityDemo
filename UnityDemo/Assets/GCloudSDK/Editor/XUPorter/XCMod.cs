using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

namespace GCloudSDKEditor.XCodeEditor 
{
	public class XCMod 
	{
		private Hashtable _datastore = new Hashtable();
		private ArrayList _libs = null;
		
		public string name { get; private set; }
		public string path { get; private set; }
		
		public string group {
			get {
				if (_datastore != null && _datastore.Contains("group"))
					return (string)_datastore["group"];
				return string.Empty;
			}
		}
		
		public ArrayList patches {
			get {
                return GetArrayConfig("patches");
				//return (ArrayList)_datastore["patches"];
			}
		}
		
		public ArrayList libs {
			get {
                if( _libs == null) {
                    var libArray = GetArrayConfig("libs");
                    _libs = new ArrayList( libArray.Count );
                    foreach( string fileRef in libArray ) 
                    {
                        Debug.Log("Adding to Libs: " + fileRef);
                        _libs.Add(new XCModFile(fileRef));
					}
				}
				return _libs;
			}
		}

        internal ArrayList GetArrayConfig(string key)
        {
            if (!string.IsNullOrEmpty(key) && _datastore.ContainsKey(key))
            {
                return (ArrayList)_datastore[key];
            }
            return new ArrayList();
        }

        internal Hashtable GetTableConfig(string key)
        {
            if (!string.IsNullOrEmpty(key) && _datastore.ContainsKey(key))
            {
                return (Hashtable)_datastore[key];
            }
            return new Hashtable();
        }

        public ArrayList frameworks {
			get {
                return GetArrayConfig("frameworks");
			}
		}
		
		public ArrayList headerpaths {
			get {
                return GetArrayConfig("headerpaths");
                //return (ArrayList)_datastore["headerpaths"];
			}
		}
		
		public ArrayList files {
			get {
                return GetArrayConfig("files");
                //return (ArrayList)_datastore["files"];
			}
		}
		
		public ArrayList folders {
			get {
                return GetArrayConfig("folders");
                //return (ArrayList)_datastore["folders"];
			}
		}
		
		public ArrayList excludes {
			get {
                return GetArrayConfig("excludes");
                //return (ArrayList)_datastore["excludes"];
			}
		}

//		public ArrayList compiler_flags {
//			get {
//				return (ArrayList)_datastore["compiler_flags"];
//			}
//		}

		public Hashtable build_settigs {
			get {
                return GetTableConfig("build_settings");
				//return (Hashtable)_datastore["build_settings"];
			}
		}
//		public object bit_code{
//			get {
//				return (object)_datastore["bit_code"];
//			}
//		}
		public ArrayList embed_binaries {
			get {
                return GetArrayConfig("embed_binaries");
                //return (ArrayList)_datastore["embed_binaries"];
			}
		}

		public Hashtable info_plist {
			get {
                return GetTableConfig("Info.plist");
                //return (Hashtable)_datastore["Info.plist"];
			}
		}
		public Hashtable system_capabilities{

			get {
                return GetTableConfig("system_capabilities");
                //return (Hashtable)_datastore["system_capabilities"];
			}
		}
		
		public XCMod( string filename )
		{	
			FileInfo projectFileInfo = new FileInfo( filename );
			if( !projectFileInfo.Exists ) {
				Debug.LogWarning( "File does not exist." );
			}
			
			name = System.IO.Path.GetFileNameWithoutExtension( filename );
			path = System.IO.Path.GetDirectoryName( filename );
			
			string contents = projectFileInfo.OpenText().ReadToEnd();
			Debug.Log (contents);
			_datastore = (Hashtable)XUPorterJSON.MiniJSON.jsonDecode( contents );
			if (_datastore == null || _datastore.Count == 0) {
				Debug.Log (contents);
				throw new UnityException("Parse error in file " + System.IO.Path.GetFileName(filename) + "! Check for typos such as unbalanced quotation marks, etc.");
			}
		}
	}

	public class XCModFile
	{
		public string filePath { get; private set; }
		public bool isWeak { get; private set; }
		
		public XCModFile( string inputString )
		{
			isWeak = false;
			
			if( inputString.Contains( ":" ) ) {
				string[] parts = inputString.Split( ':' );
				filePath = parts[0];
				isWeak = ( parts[1].CompareTo( "weak" ) == 0 );	
			}
			else {
				filePath = inputString;
			}
		}
	}
}

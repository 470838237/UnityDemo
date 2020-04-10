using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GCloudSDKEditor.XCodeEditor
{
	public class XCPlist
	{
		string plistPath;
		bool plistModified;

		public XCPlist (string plistPath)
		{
			this.plistPath = plistPath;
		}

		public void Process (Hashtable plist)
		{
			Dictionary<string, object> dict = (Dictionary<string, object>)PlistCS.Plist.readPlist (plistPath);
			foreach (DictionaryEntry entry in plist) {
				this.AddPlistItems ((string)entry.Key, entry.Value, dict);
			}
			if (plistModified) {
				PlistCS.Plist.writeXml (dict, plistPath);
			}
		}

		public void AddPlistItems (string key, object value, Dictionary<string, object> dict)
		{
			Debug.Log ("AddPlistItems: key=" + key);
			if (dict.ContainsKey(key)) {
				object old_value = dict[key];
				Debug.Log ("old_value typeof=" + old_value.GetType());
				Debug.Log ("value typeof=" + value.GetType());
				if( old_value.GetType()==typeof(Dictionary<string, object>) && value.GetType()==typeof(Hashtable) ) {
					Debug.Log ("merge dict" + value + old_value);
					Dictionary<string, object> old_value_dict = (Dictionary<string, object>)old_value;
					Hashtable value_dict = (Hashtable)value;
					foreach(KeyValuePair<string, object>kvp in old_value_dict) {
						value_dict[kvp.Key] = kvp.Value;
					}
					dict [key] = value_dict;
				} else if( old_value.GetType()==typeof(List<object>) && typeof(ArrayList).IsInstanceOfType(value)  ) {
					Debug.Log ("merge array" + value + old_value);
					ArrayList value_list = value as ArrayList;
					List<object> old_value_list = (List<object>)old_value;
					foreach (object old_v in old_value_list)
						value_list.Add(old_v);
					dict [key] = value_list;
				} else {
					Debug.Log ("replace value" + value + old_value);
					dict [key] = value;
				}
			} else {
				dict [key] = value;
			}
			plistModified = true;
		}
	}
}

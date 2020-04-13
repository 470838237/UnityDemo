using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace GCloud
{
    public interface IRemoteConfig
    {
        Int32 GetInt(String key, Int32 defaultValue);
        Int64 GetLong(String key, Int64 defaultValue);
        String GetString(String key, String defaultValue);
        Boolean GetBool(String key, Boolean defaultValue);
        Double GetDouble(String key, Double defaultValue);
    }
}
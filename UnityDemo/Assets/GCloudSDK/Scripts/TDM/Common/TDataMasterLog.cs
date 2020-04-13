using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TDM
{
    public enum TLogPriority
    {
        Debug,      //Reserved 
        Info,
        Warning,
        Error,
        None,
    };

    public class TLog
    {
        private static TLogPriority tLevel = TLogPriority.Debug;
        public static TLogPriority Level 
        {
            get
            {
                return tLevel;
            }
            set
            {
                tLevel = value;
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static public void TDebug(object message)
        {
            if (tLevel > TLogPriority.Debug)
            {
                return;
            }
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static public void TInfo(object message)
        {
            if (tLevel > TLogPriority.Info)
            {
                return;
            }
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static public void TWarning(object message)
        {
            if (tLevel > TLogPriority.Warning)
            {
                return;
            }
            Debug.LogWarning(message);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static public void TError(object message)
        {
            if (tLevel > TLogPriority.Error)
            {
                return;
            }
            Debug.LogError(message);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static public void TException(Exception exception)
        {
            if (tLevel > TLogPriority.Error)
            {
                return;
            }
            Debug.LogException (exception);
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TDM
{
    public class BasicClassTypeUtil
    {
        public static object CreateObject<T>()
        {
            return CreateObject(typeof(T));
        }

        public static object CreateObject(Type type)
        {
            try
            {
                // String类型没有缺省构造函数，
                if (type.ToString() == "System.String")
                {
                    return "";
                }
                return Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object CreateListItem(Type typeList)
        {
            Type[] itemType = typeList.GetGenericArguments();
            if (itemType == null || itemType.Length == 0)
            {
                return null;
            }
            return CreateObject(itemType[0]);
        }
        
    }

}

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace GCloud
{
	public class ApolloBufferReader
	{
		public ApolloBufferReader()
		{
		}
		
		
		public ApolloBufferReader(byte[] bs)
		{
			buffer = bs;
		}
		
		public void Reset()
		{
			buffer = null;
			position = 0;
		}

        public void ResetPosition()
        {
            position = 0;
        }
		
		public bool Read(ref bool b)
		{
			//ADebug.Log ("Read bool stream.Position:" + stream.Position);
			byte v = (b ? (byte)1 : (byte)0);
			b = Read(ref v) != 0?true:false;
			return b;
		}
		
		public byte Read(ref byte c)
		{
			//ADebug.Log ("Read byte stream.Position:" + stream.Position);
			
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			c = buffer [position];
			position += sizeof(byte);
			return c;
		}
		
		public byte[] Read(ref byte[] buf)
		{
			if (buffer == null || position >= buffer.Length) {
				return null;
			}
			int len = 0;
			//ADebug.Log ("Read buf stream.Position:" + stream.Position);
			Read(ref len);
			if (len > 0)
			{
				buf = new byte[len];
				
				Array.Copy(buffer, position, buf, 0, len);
				position += len;
				
				return buf;
			}
			return null;
        }

        public int ReadArray(byte[] buf)
        {
            if (buffer == null || buf.Length == 0)
            {
                return 0;
            }
            int len = 0;
            Read(ref len);
            if (len > 0 && len < buf.Length)
            {
                Array.Copy(buffer, position, buf, 0, len);
                position += len;

                return len;
            }
            return 0;
        }

        public Int16 Read(ref Int16 v)
		{
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			v = BitConverter.ToInt16 (buffer, position);
			position += sizeof(Int16);
			v = ByteConverter.ReverseEndian (v);
			
			//ADebug.Log ("after stream.Position:" + stream.Position + " v:" + v);
			return v;
		}
		
		public UInt16 Read(ref UInt16 v)
		{
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			//ADebug.Log ("stream.Position:" + stream.Position);
			//v = ByteConverter.ReverseEndian(reader.ReadUInt16());
			
			v = BitConverter.ToUInt16 (buffer, position);
			position += sizeof(UInt16);
			v = ByteConverter.ReverseEndian (v);
			//ADebug.Log ("after Read U16 stream.Position:" + stream.Position + " v:" + v);
			return v;
		}
		
		public Int32 Read(ref Int32 v)
		{
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			//ADebug.Log ("Int stream.Position:" + position);
			//v = ByteConverter.ReverseEndian(reader.ReadInt32());
			
			v = BitConverter.ToInt32 (buffer, position);
			position += sizeof(Int32);
			v = ByteConverter.ReverseEndian (v);
			//ADebug.Log ("after Read 32 stream.Position:" + position + " v:" + v);
			return v;
		}
		
		public UInt32 Read(ref UInt32 v)
		{
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			//ADebug.Log ("UInt stream.Position:" + position);
			//v = ByteConverter.ReverseEndian(reader.ReadUInt32());
			
			v = BitConverter.ToUInt32 (buffer, position);
			position += sizeof(UInt32);
			v = ByteConverter.ReverseEndian (v);
			//ADebug.Log ("after Read U32 stream.Position:" + position + " v:" + v);
			return v;
		}
		
		public Int64 Read(ref Int64 v)
		{
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			//ADebug.Log ("stream.Position:" + position);
			//v = ByteConverter.ReverseEndian(reader.ReadInt64());
			
			v = BitConverter.ToInt64 (buffer, position);
			position += sizeof(Int64);
			v = ByteConverter.ReverseEndian (v);
			//ADebug.Log ("after Read 64 stream.Position:" + position + " v:" + v);
			return v;
		}
		
		public UInt64 Read(ref UInt64 v)
		{
			if (buffer == null || position >= buffer.Length) {
				return 0;
			}
			//ADebug.Log ("stream.Position:" + position);
			//v = ByteConverter.ReverseEndian(reader.ReadUInt64());
			
			v = BitConverter.ToUInt64 (buffer, position);
			position += sizeof(UInt64);
			v = ByteConverter.ReverseEndian (v);
			//ADebug.Log ("after Read U64 stream.Position:" + position);
			return v;
		}
		
		public string Read(ref string s)
		{
			if (this.buffer == null || position >= this.buffer.Length) {
				return null;
			}
			//int len = 0;
			//ADebug.Log ("Read S stream.Position:" + stream.Position);
			//Read(ref len);
			//ADebug.Log ("after Read S len stream.Position:" + stream.Position + " len:" + len);
			//if (len > 0)
			{
				//ADebug.Log("Read s len:" + len);
				//ADebug.Log ("stream.Position:" + stream.Position);
				byte[] buffer = null;
				
				buffer = Read(ref buffer);
				
				//ADebug.Log ("after stream.Position:" + stream.Position);
				if(buffer != null)
				{
					//ADebug.Log("Read s buf size:" + buffer.Length);
					
					s = System.Text.Encoding.UTF8.GetString(buffer);
					
					//ADebug.Log("Read s:" + s);
					//s = ByteConverter.Bytes2String(buffer);
					return s;
				}
				else
				{
					//ADebug.LogError("Read string Error");
				}
			}
			return null;
		}
		
		public IList<T> Read<T>(ref IList<T> v)
		{
			return Read(ref v) as IList<T>;
		}

		public IList ReadList<T>(ref T l)
		{
			int count = 0;
			Read(ref count);
			
			IList list = l as IList;//BasicClassTypeUtil.CreateObject(l.GetType()) as IList;
			if (list == null)
			{
				ADebug.LogError("ReadList list == null");
				return null;
			}
			
			list.Clear ();
			
			
			for (int i = 0; i < count; i++)
			{
				object objItem = BasicClassTypeUtil.CreateListItem(list.GetType());
				Read(ref objItem);
				list.Add(objItem);
			}
			
			return list;
		}

        public int ReadList(IList list)
        {
            if(list == null)
            {
                return 0;
            }

            int count = 0;
            Read(ref count);
			int i = 0;
            for (; i < count && i < list.Count; i++)
            {
                object objItem = list[i];
                Read(ref objItem);
            }
            return i;
        }
        public int ReadList(IList list, int count)
        {
            if (list == null)
            {
                return 0;
            }

            int i = 0;
            for (; i < count && i < list.Count; i++)
            {
                object objItem = list[i];
                Read(ref objItem);
            }
            return i;
        }	
		public IDictionary<K, V> Read<K, V>(ref IDictionary<K, V> map)
		{
			return ReadMap(ref map) as IDictionary<K, V>;
		}
		public IDictionary ReadMap<T>(ref T map)
		{
			IDictionary m = BasicClassTypeUtil.CreateObject(map.GetType()) as IDictionary;
			if (m == null)
			{
				return null;
			}
			
			m.Clear();
			
			int count = 0;
			Read(ref count);
			if(count > 0)
			{
				
				Type type = m.GetType();
				Type[] argsType = type.GetGenericArguments();
				if (argsType == null || argsType.Length < 2)
				{
					return null;
				}
				
				for (int i = 0; i < count; i++)
				{
					var mk = BasicClassTypeUtil.CreateObject(argsType[0]);
					var mv = BasicClassTypeUtil.CreateObject(argsType[1]);
					
					mk = Read(ref mk);
					mv = Read(ref mv);
					
					m.Add(mk, mv);
				}
				map = (T)m;
				return m;
			}
			return null;
		}
		
		public ApolloBufferBase Read(ref ApolloBufferBase ab)
		{
			if (ab != null)
			{
				ab.Decode(this);
			}
			return ab;
		}

        public object Read<T>(ref T o)
        {
            if (o == null)
            {
                o = (T)BasicClassTypeUtil.CreateObject<T>();
            }

            if (o is Byte || o is Char)
            {
                byte b = 0;

                o = (T)(object)(Read(ref b));
            }
            else if (o is char)
            {
                byte c = 0;
                o = (T)(object)(Read(ref c));
            }
            else if (o is Boolean)
            {
                bool b = false;
                o = (T)(object)(Read(ref b));
            }
            else if (o is short)
            {
                short s = 0;
                o = (T)(object)(Read(ref s));
            }
            else if (o is ushort)
            {
                ushort us = 0;
                o = (T)(object)(Read(ref us));
            }
            else if (o is int)
            {
                int i = 0;
                o = (T)(object)Read(ref i);
            }
            else if (o is uint)
            {
                uint ui = 0;
                o = (T)(object)Read(ref ui);
                return o;
            }
            else if (o is long)
            {
                long l = 0;
                o = (T)(object)Read(ref l);
                return o;
            }
            else if (o is Enum)
            {
                int remp = 0;
                o = (T)(object)Read(ref remp);
                return o;
            }
            else if (o is ulong)
            {
                ulong ul = 0;
                object oo = (Read(ref ul));
                o = (T)oo;
                return oo;
            }/*
            else if (o is float)
            {
                return (Read());
            }
            else if (o is Double)
            {
                return (Read());
            }*/
            else if (o is string)
            {
                string s = "";
                o = (T)(object)Read(ref s);
            }
            else if (o is ApolloBufferBase)
            {
                ApolloBufferBase oo = o as ApolloBufferBase;
                o = (T)(object)Read(ref oo);
            }
            else if (o != null && o.GetType().IsArray)
            {
                /*
                if (o is byte[] || o is Byte[])
                {
                    return Read((byte[])null);
                }
                else if (o is bool[])
                {
                    return Read((bool[])null);
                }
                else if (o is short[])
                {
                    return Read((short[])null);
                }
                else if (o is int[])
                {
                    return Read((int[])null);
                }
                else if (o is long[])
                {
                    return Read((long[])null);
                }
                else if (o is float[])
                {
                    return Read((float[])null);
                }
                else if (o is double[])
                {
                    return Read((double[])null);
                }
                else
                {
                    object oo = o;
                    return readArray((Object[])oo);
                }
                 */
            }
            else if (o is IList)
            {
                return ReadList<T>(ref o);
            }
            else if (o is IDictionary)
            {
                return ReadMap<T>(ref o);
            }
            else
            {
                throw new Exception("read object error: unsupport type:" + o.GetType() + " value:" + o.ToString());
            }

            return o;
        }
	    public bool IsEof()
        {
            return position >= this.buffer.Length;
        }
		
		private byte[] buffer;
		private int position;
		//private MemoryStream stream;
		//private BinaryReader reader;
	};
}

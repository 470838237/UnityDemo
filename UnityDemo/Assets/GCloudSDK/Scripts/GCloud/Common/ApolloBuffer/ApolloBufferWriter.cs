
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace GCloud
{
	public class ApolloBufferWriter
	{
		
		public ApolloBufferWriter()
		{
			stream = new MemoryStream(128);
			writer = new BinaryWriter(stream, Encoding.BigEndianUnicode);
		}
		
		public ApolloBufferWriter(int capacity)
		{
			stream = new MemoryStream(capacity);
			writer = new BinaryWriter(stream, Encoding.BigEndianUnicode);
		}
		
		public ApolloBufferWriter(MemoryStream ms)
		{
			stream = ms;
			writer = new BinaryWriter(stream, Encoding.BigEndianUnicode);
		}
		
		public void Write(bool b)
		{
			Write((byte)(b ? 1 : 0));
		}
		
		public void Write(byte c)
		{
			Reserve(1);
			writer.Write(c);
		}
		
		public void Write(Int16 s)
		{
			Reserve(2);
			writer.Write(ByteConverter.ReverseEndian(s));
		}
		
		public void Write(UInt16 s)
		{
			Reserve(2);
			writer.Write(ByteConverter.ReverseEndian((Int16)s));
		}
		
		public void Write(Int32 i)
		{
			Reserve(4);
			writer.Write(ByteConverter.ReverseEndian(i));
		}
		
		public void Write(UInt32 i)
		{
			Reserve(4);
			writer.Write(ByteConverter.ReverseEndian(i));
		}
		
		public void Write(Int64 l)
		{
			Reserve(8);
			writer.Write(ByteConverter.ReverseEndian(l));
		}
		
		public void Write(UInt64 l)
		{
			Reserve(8);
			writer.Write(ByteConverter.ReverseEndian(l));
        }

        public void Write(byte[] buffer)
        {
            Write(buffer, -1);
        }

        public void Write(byte[] buffer, int len)
        {
            if (buffer != null)
            {
                if (len == -1)
                {
                    len = buffer.Length;
                }
                Write(len);
                writer.Write(buffer, 0, len);
            }
            else
            {
                Write((int)0);
            }
        }

        public void Write(string s)
		{
			byte[] buffer = ByteConverter.String2Bytes(s);
			if (buffer == null)
			{
				buffer = new byte[0];
			}
			
			int len = buffer.Length;
			Reserve(len + 4);
			
			Write(len);
			
			if (buffer.Length > 0)
			{
				writer.Write(buffer);
			}
		}
		
		public void Write<T>(List<T> v)
		{
			int size = v != null ? v.Count : 0;
			Write(size);
			
			if(v != null)
			{
				for (int i = 0; i < v.Count; i++)
				{
					Write(v[i]);
				}
			}
		}

        public void Write<T>(List<T> v, int count)
        {
            int size = v != null ? v.Count : 0;
            size = size < count ? size : count;
            Write(count);

            if (v != null)
            {
                for (int i = 0; i < count; i++)
                {
                    Write(v[i]);
                }
            }
        }
		
		public void Write<K,V>(Dictionary<K,V> d)
		{
			if (d != null)
			{
				int size = d.Count;
				Write(size);
				
				foreach (KeyValuePair<K, V> pair in d)
				{
					Write(pair.Key);
					Write(pair.Value);
				}
			}
			else
			{
				Write((int)0);
			}
		}
		
		public void Write(ApolloBufferBase ab)
		{
			if (ab != null)
			{
				ab.Encode(this);
			}
		}
		
		public byte[] GetBufferData()
		{
			byte[] buffer = new byte[stream.Position];
			Array.Copy(stream.GetBuffer(), 0, buffer, 0, stream.Position);
			return buffer;
		}
		
		
		public void Write(object o)
		{
			if (o is byte)
			{
				Write(((byte)o));
			}
			else if (o is Boolean)
			{
				Write((bool)o);
			}
			else if (o is short)
			{
				Write(((short)o));
			}
			else if (o is ushort)
			{
				Write(((int)(ushort)o));
			}
			else if (o is int)
			{
				Write(((int)o));
			}
			else if (o is uint)
			{
				Write((long)(uint)o);
			}
			else if (o is long)
			{
				Write(((long)o));
			}
			else if (o is ulong)
			{
				Write(((long)(ulong)o));
			}
			else if (o is float)
			{
				Write(((float)o));
			}
			else if (o is double)
			{
				Write(((double)o));
			}
			else if (o is string)
			{
				string strObj = o as string;
				Write(strObj);
			}
			else if (o is ApolloBufferBase)
			{
				Write((ApolloBufferBase)o);
			}
			else if (o is byte[])
			{
				Write((byte[])o);
			}
			else if (o is bool[])
			{
				Write((bool[])o);
			}
			else if (o is short[])
			{
				Write((short[])o);
			}
			else if (o is int[])
			{
				Write((int[])o);
			}
			else if (o is long[])
			{
				Write((long[])o);
			}
			else if (o is float[])
			{
				Write((float[])o);
			}
			else if (o is double[])
			{
				Write((double[])o);
			}
			else if (o.GetType().IsArray)
			{
				Write((object[])o);
			}
			else if (o is IList)
			{
				Write((IList)o);
			}
			else if (o is IDictionary)
			{
				Write((IDictionary)o);
			}
			else if (o is Enum)
			{
				Write((int)o);
			}
			else
			{
				throw new Exception(
					"write object error: unsupport type. " + o.ToString() + "\n");
			}
		}
		
		private void Reserve(int len)
		{
			int remain = stream.Capacity - (int)stream.Length;
			if (remain < len)
			{
				stream.Capacity = (stream.Capacity + len) << 1;
			}
		}
		
		private MemoryStream stream;
		private BinaryWriter writer;
	}
}

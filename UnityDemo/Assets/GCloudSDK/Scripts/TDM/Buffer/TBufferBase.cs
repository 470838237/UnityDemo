using System;

namespace TDM
{
	public abstract class TBufferBase
	{
		protected TBufferBase()
		{
		}
		
		public bool Encode(out byte[] buffer)
		{
			try
			{
				TBufferWriter writer = new TBufferWriter();
				BeforeEncode(writer);
				WriteTo(writer);
				
				buffer = writer.GetBufferData();
				return true;
			} 
			catch (Exception ex)
			{
				buffer = null;
				TLog.TError(ex);
				return false;
			}
		}
		
		public bool Encode(TBufferWriter writer)
		{
			if(writer == null)
			{
				return false;
			}
			try
			{
				BeforeEncode(writer);
				WriteTo(writer);

				return true;
			} 
            catch (Exception ex)
			{
                TLog.TError(ex);
				return false;
			}
		}
		
		public bool Decode(byte[] data)
		{
			if (data != null)
			{
				try
				{
					TBufferReader reader = new TBufferReader(data);
					BeforeDecode(reader);
					ReadFrom(reader);
					return true;
				} 
				catch (Exception ex)
				{
                    TLog.TError(ex);
					return false;
				}
			}
			return false;
		}
		
		public bool Decode(TBufferReader reader)
		{
			if (reader != null)
			{
				try
				{
					BeforeDecode(reader);
					ReadFrom(reader);
					return true;
				} 
				catch (Exception ex)
				{
                    TLog.TError(ex);
					return false;
				}
			}
			return false;
		}
		
		public abstract void WriteTo(TBufferWriter writer);
		
		public abstract void ReadFrom(TBufferReader reader);
		
		
		protected virtual void BeforeEncode(TBufferWriter writer)
		{
		}
		
		protected virtual void BeforeDecode(TBufferReader reader)
		{
		}
	};
}

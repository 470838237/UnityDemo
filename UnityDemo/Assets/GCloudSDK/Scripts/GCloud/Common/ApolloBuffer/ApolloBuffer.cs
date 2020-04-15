using System;

namespace GCloud
{
	public abstract class ApolloBufferBase
	{
		protected ApolloBufferBase()
		{
		}
		
		public bool Encode(out byte[] buffer)
		{
			try
			{
				ApolloBufferWriter writer = new ApolloBufferWriter();
				BeforeEncode(writer);
				WriteTo(writer);
				
				buffer = writer.GetBufferData();
				return true;
			} catch (Exception ex)
			{
				buffer = null;
				ADebug.LogException(ex);
				return false;
			}
		}
		
		public bool Encode(ApolloBufferWriter writer)
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
			} catch (Exception ex)
			{
				ADebug.LogException(ex);
				return false;
			}
		}
		
		public bool Decode(byte[] data)
		{
			if (data != null)
			{
				try
				{
					ApolloBufferReader reader = new ApolloBufferReader(data);
					BeforeDecode(reader);
					ReadFrom(reader);
					return true;
				} 
				catch (Exception ex)
				{
					ADebug.LogException(ex);
					return false;
				}
			}
			return false;
		}
		
		public bool Decode(ApolloBufferReader reader)
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
					ADebug.LogException(ex);
					return false;
				}
			}
			return false;
		}
		
		public abstract void WriteTo(ApolloBufferWriter writer);
		
		public abstract void ReadFrom(ApolloBufferReader reader);
		
		
		protected virtual void BeforeEncode(ApolloBufferWriter writer)
		{
		}
		
		protected virtual void BeforeDecode(ApolloBufferReader reader)
		{
		}
	};
	
	
	public abstract class ActionBufferBase : ApolloBufferBase
	{
		private int action;
		public int Action
		{
			get
			{
				return action;
			}
			
			protected set
			{
				action = value;
			}
		}
		
		protected ActionBufferBase()
		{
		}
		
		protected ActionBufferBase(int action)
		{
			this.action = action;
		}
		
		protected override void BeforeEncode(ApolloBufferWriter writer)
		{
			writer.Write(Action);
		}
		
		protected override void BeforeDecode(ApolloBufferReader reader)
		{
			reader.Read(ref action);
		}
	};
	
	
	public class Action : ActionBufferBase
	{
		public Action()
			:base(0)
		{
		}
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			
		}
		
	};

}

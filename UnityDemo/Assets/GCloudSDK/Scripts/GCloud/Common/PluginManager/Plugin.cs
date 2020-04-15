
using System;
namespace GCloud
{
    public abstract class PluginBase : ApolloObject
    {
        protected PluginBase()
        {
            PluginManager.Instance.Add(this);
        }

        public abstract bool Install();
        
        public abstract string GetPluginName();

//        public virtual IServiceBase GetService(ServiceType serviceType)
//        {
//            return null;
//		}
		
		public virtual ApolloBufferBase CreateResponseInfo(int action)
		{
			return null;
		}
		
		public virtual ApolloBufferBase CreatePayResponseInfo(int action)
		{
			return null;
		}
		
		public virtual ActionBufferBase CreatePayResponseAction(int action)
		{
			return null;
		}
    }
	
	
	public class ServiceAction
	{
		//AccountBase 100
		public const int AccountInitialize = 101;
		//PayBase 200
		public const int PayInitialize = 201;
	}
}


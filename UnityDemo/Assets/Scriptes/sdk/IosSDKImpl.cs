using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace honorsdk
{
    class IosSDKImpl : HonorSDK
    {
 

        public IosSDKImpl() {

           
        }

        public override void Login(OnFinish<UserInfo> loginListener)
        {
            base.Login(loginListener);
            login();              
        }


        [DllImport("__Internal")]
        private static extern void login();

    }

    

}

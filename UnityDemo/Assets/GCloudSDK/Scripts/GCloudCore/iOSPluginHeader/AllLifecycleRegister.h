#ifndef AllLifecycleRegister_h
#define AllLifecycleRegister_h


#import <GCloudCore/GCloudAppLifecycleObserver.h>

#import "PluginReportLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(PluginReportLifecycle);

#import "PluginMSDKLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(PluginMSDKLifecycle);


#endif
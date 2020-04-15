#ifndef AllLifecycleRegister_h
#define AllLifecycleRegister_h


#import <GCloudCore/GCloudAppLifecycleObserver.h>

#import "PluginReportLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(PluginReportLifecycle);

#import "APMLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(APMLifecycle);

#import "GCloudAppLifecycleListener.h"
REGISTER_LIFECYCLE_OBSERVER(GCloudAppLifecycleListener);

#import "PluginGVoiceLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(PluginGVoiceLifecycle);

#import "GRobotPluginLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(GRobotPluginLifecycle);

#import "HttpDnsPluginLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(HttpDnsPluginLifecycle);

#import "PluginMSDKLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(PluginMSDKLifecycle);

#import "TGPAPluginLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(TGPAPluginLifecycle);

#import "GPMLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(GPMLifecycle);

#import "PluginTssSDKLifecycle.h"
REGISTER_LIFECYCLE_OBSERVER(PluginTssSDKLifecycle);


#endif
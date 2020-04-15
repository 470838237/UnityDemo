//
//  MyObserver.cpp
//  APM
//
//  Created by 雍鹏亮 on 2019/10/30.
//  Copyright © 2019 xianglin. All rights reserved.
//

#include "MyObserver.h"
#if defined(__cplusplus)
extern "C"{
#endif
    
    void gpm_log(const char * log);
    
    void MySetGPMObserver () {
        MyObserver *observer = MyObserver::GetInstance();
        void gpm_setObserver(GPMObserver *observer);
        gpm_setObserver(observer);
    }
    
#if defined(__cplusplus)
}
#endif

#define UnityReceiverObject  "UnityGPMCallBackGameObejct"

MyObserver* MyObserver::m_pInst = NULL;
MyObserver* MyObserver::GetInstance()
{
    if(!m_pInst){
        m_pInst = new MyObserver();
    }
    return m_pInst;
}

void MyObserver::GPMOnMarkLevelLoad(const char *sceneId){
    if (sceneId != NULL)
    {
        
        gpm_log("gpm_log callee in MyObserver::GPMOnMarkLevelLoad");
        UnitySendMessage(UnityReceiverObject, "GPMOnMarkLevelLoad", sceneId);
    }
}

void MyObserver::GPMOnSetQulaity(const char *quality)
{
    if (quality != NULL)
    {
        gpm_log("gpm_log callee in MyObserver::GPMOnSetQulaity ");
        UnitySendMessage(UnityReceiverObject, "GPMOnSetQuality", quality);
    }
}

void MyObserver::GPMOnLog(const char *log){
    if (log != NULL)
    {
        gpm_log("gpm_log callee in MyObserver::GPMOnLog");
        UnitySendMessage(UnityReceiverObject, "GPMOnLog", log);
    }
}

void MyObserver::GPMOnFpsNotify(const char *fpsString){
    if (fpsString != NULL)
    {
        gpm_log("gpm_log callee in MyObserver::GPMOnFpsNotify");
        UnitySendMessage(UnityReceiverObject, "GPMOnFpsNotify", fpsString);
    }
}

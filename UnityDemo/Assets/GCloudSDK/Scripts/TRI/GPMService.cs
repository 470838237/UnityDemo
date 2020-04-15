
using System;
using System.Collections.Generic;

namespace GCloud.GPM
{
   
    public class GPMService : IGPMService
    {
       
        string IGPMService.GetSDKVersion()
        {
            return GPMAgent.GetSDKVersion();
        }

        int IGPMService.InitContext(string appId, bool debug)
        {
            return GPMAgent.InitContext(appId, debug);
        }

        void IGPMService.SetOpenId(string openId)
        {
            GPMAgent.SetOpenId(openId);
        }

        void IGPMService.SetResourceVersion(string version)
        {
            GPMAgent.SetResourceVersion(version);
        }

        void IGPMService.MarkLevelLoad(string sceneName)
        {
            GPMAgent.MarkLevelLoad(sceneName);
        }

        void IGPMService.MarkLevelLoadCompleted()
        {
            GPMAgent.MarkLevelLoadCompleted();
        }

        void IGPMService.MarkLevelFin()
        {
            GPMAgent.MarkLevelFin();
        }

        void IGPMService.SetServerInfo(string zoneId, string roomIp)
        {
            GPMAgent.SetServerInfo(zoneId, roomIp);
        }

        void IGPMService.BeginTag(string tagName)
        {
            GPMAgent.BeginTag(tagName);
        }

        void IGPMService.EndTag()
        {
            GPMAgent.EndTag();
        }

        void IGPMService.PostEvent(int key, string value)
        {
            GPMAgent.PostEvent(key, value);
        }

        void IGPMService.ReportEvent(string eventName, Dictionary<string, string> eventParams)
        {
            GPMAgent.ReportEvent(eventName, eventParams);
        }

        void IGPMService.CheckDeviceClass(string domainName)
        {
            GPMAgent.CheckDeviceClass(domainName);
        }

        void IGPMService.CheckDeviceClassAsync(string domainName, GPMDeviceLevelEventHandle handler)
        {
            GPMAgent.CheckDeviceClassAsync(domainName, handler);
        }

        void IGPMService.BeginExclude()
        {
            GPMAgent.BeginExclude();
        }

        void IGPMService.EndExclude()
        {
            GPMAgent.EndExclude();
        }

        void IGPMService.SetSceneQuality(int sceneQuality)
        {
            GPMAgent.SetSceneQuality(sceneQuality);
        }

        void IGPMService.SetDeviceLevel(int deviceLevel)
        {
            GPMAgent.SetDeviceLevel(deviceLevel);
        }

        void IGPMService.DetectInTimeout()
        {
            GPMAgent.DetectInTimeout();
        }

        void IGPMService.PostNetworkLatency(int mills)
        {
            GPMAgent.PostNetworkLatency(mills);
        }

       
        void IGPMService.BeginTupleWrap(string tupleName)
        {
            GPMAgent.BeginTupleWrap(tupleName);
        }

        void IGPMService.EndTupleWrap()
        {
            GPMAgent.EndTupleWrap();
        }

        void IGPMService.PostValueF(string category, string key, float a)
        {
            GPMAgent.PostValueF(category, key,  a);
        }

        void IGPMService.PostValueF(string category, string key, float a, float b)
        {
            GPMAgent.PostValueF(category, key,  a, b);
        }

        void IGPMService.PostValueF(string category, string key, float a, float b, float c)
        {
            GPMAgent.PostValueF(category, key,  a, b, c);
        }

        void IGPMService.PostValueI(string category, string key, int a)
        {
            GPMAgent.PostValueI(category, key, a);
        }

        void IGPMService.PostValueI(string category, string key, int a, int b)
        {
            GPMAgent.PostValueI(category, key, a, b);
        }

        void IGPMService.PostValueI(string category, string key, int a, int b, int c)
        {
            GPMAgent.PostValueI(category, key, a, b, c);
        }

        void IGPMService.PostValueS(string category, string key, string value)
        {
            GPMAgent.PostValueS(category, key, value);
        }

        void IGPMService.PostStepEvent(string category, int stepId, int status, int code, string msg, string extraKey, bool authorize, bool finish)
        {
            GPMAgent.PostStepEvent(category, stepId, status, code, msg, extraKey, authorize, finish);
        }


        void IGPMService.LinkLastStepEventSession(string eventName)
        {
            GPMAgent.LinkLastStepEventSession(eventName);
        }

        void IGPMService.InitStepEventContext()
        {
            GPMAgent.InitStepEventContext();
        }

        void IGPMService.ReleaseStepEventContext()
        {
            GPMAgent.ReleaseStepEventContext();
        }

        //void IGPMService.SetPayEvent(int id, int tag, bool status, string msg)
        //{
        //    GPMAgent.SetPayEvent(id, tag, status, msg);
        //}

        //void IGPMService.RegisterDeviceCallback(IDeviceCallback callback)
        //{
        //}

        //void IGPMService.UpdateGameInfo(GameDataKey key, string value)
        //{
        //}

        //void IGPMService.UpdateGameInfo(GameDataKey key, int value)
        //{
        //}

        //void IGPMService.UpdateGameInfo(Dictionary<GameDataKey, string> dict)
        //{
        //}

        //void IGPMService.UpdateGameInfo(string key, string value)
        //{
        //}

        //void IGPMService.UpdateGameInfo(string key, Dictionary<string, string> dict)
        //{
        //    throw new NotImplementedException();
        //}

        //string IGPMService.GetDataFromTGPA(string key, string value)
        //{
        //    throw new NotImplementedException();
        //}

    }
};

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;


namespace GCloud.APM
{
    public interface ApmCallBackInterface
    {
        /// <summary> 初始化接口回调
        ///
        /// </summary>
        /// <param name="succcess">状态，true为成功， false为失败</param>
        /// <param name="msg">如果为false，错误信息</param>
        /// <returns></returns>
        void OnInitContext(bool succcess, string msg);

        /// <summary> 场景开始
        ///
        /// </summary>
        /// <param name="succcess">状态，true为成功， false为失败</param>
        /// <param name="msg">如果为false，错误信息</param>
        /// <param name="sceneName">场景名息</param>
        /// <returns></returns>
        void OnMarkLevelLoad(bool succcess, string msg, string sceneName);

        /// <summary> 场景记载结束回调
        ///
        /// </summary>
        /// <param name="succcess">状态，true为成功， false为失败</param>
        /// <param name="msg">如果为false，错误信息</param>
        /// <param name="sceneName">场景名息</param>
        /// <param name="loadedTime">加载时间mills</param>
        /// <returns></returns>
        void OnMarkLevelLoadCompleted(bool succcess, string msg, string sceneName, int loadedTime);

        /// <summary> 场景结束回调
        ///
        /// </summary>
        /// <param name="succcess">状态，true为成功， false为失败</param>
        /// <param name="msg">如果为false，错误信息</param>
        /// <param name="sceneName">场景名息</param>
        /// <param name="totalSceneTime">总时间</param>
        /// <param name="totalFrames">总帧数</param>
        /// <param name="maxPss">最大PSS</param>
        /// <returns></returns>
        void OnMarkLevelFin(bool succcess, string msg, string sceneName, int totalSceneTime, int totalFrames, int maxPss);
    }
}

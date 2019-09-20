using honorsdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("HonorSDK:BaseScene.Update GetKeyDown=KeyCode.Escape");
            if (HonorSDK.GetInstance().HasExitDialog())
            {
                Scene3.Exit();
            }
            else
            {
                //创建游戏退出框
                ExitDialog.Inst.show(delegate ()
                {
                    Scene3.Exit();
                });
            }
        }
    }

}

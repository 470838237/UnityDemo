using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTest : MonoBehaviour {

    private static LoadTest instance;

    public static LoadTest Instance
    {
        get
        {
            return instance;
        }
    }

    private Transform root;

    public honorsdk.HonorSDKGameObject sdkObj;

    private BaseScene curScene;

    Dictionary<int, System.Type> sceneDic = new Dictionary<int, System.Type>() {
        { 1,typeof(Scene1)},
        { 2,typeof(Scene2)},
        { 3,typeof(Scene3)},
    };
    Dictionary<int, string> scenePathDic = new Dictionary<int, string>() {
        { 1,"Prefabs/scene1"},
        { 2,"Prefabs/scene2"},
        { 3,"Prefabs/scene3"},
    };

    Dictionary<int, BaseScene> loadedScene = new Dictionary<int, BaseScene>();

    void Awake()
    {
        instance = this;
        root = transform.Find("root");
    }

    void Start () {
		if(curScene == null)
        {
            ChangeScene(1);
        }
        
    }

    public void ChangeScene(int type)
    {
        System.Type sceneType = sceneDic[type];
        BaseScene scene;
        if (curScene != null && curScene.GetType() == sceneType)
        {
            
        }
        else
        {
            if (loadedScene.ContainsKey(type))
            {
                scene = loadedScene[type];
            }
            else
            {
                scene = Load(scenePathDic[type], root).GetComponent<BaseScene>();
                loadedScene.Add(type,scene);
            }
            if(curScene != null)
                curScene.gameObject.SetActive(false);
            curScene = scene;
            curScene.gameObject.SetActive(true);

        }
 
    }

    private GameObject Load(string path,Transform parent)
    {
        GameObject go = GameObject.Instantiate(Resources.Load(path) as GameObject);  
        go.transform.SetParent(parent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;  
        return go;
    }
}

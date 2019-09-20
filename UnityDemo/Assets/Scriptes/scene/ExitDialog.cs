using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

class ExitDialog: MonoBehaviour
{
    private static UnityAction sureAction;
    private static ExitDialog inst;
    public static ExitDialog Inst
    {
        get
        {
            return inst;
        }
    }
    private GameObject content;
    Button cancelButton;
    Button sureButton;
    void Awake()
    {

        inst = this;
        content = transform.Find("content").gameObject;
        cancelButton = transform.Find("content/Cancel").GetComponent<Button>();
        sureButton = transform.Find("content/Sure").GetComponent<Button>();
    }

    void Start()
    {
        sureButton.onClick.AddListener(delegate()
        {
            content.SetActive(false);
            if (sureAction != null)
            {
                sureAction();
                sureAction = null;
            }
        });
        cancelButton.onClick.AddListener(delegate ()
        {
            content.SetActive(false);
        });
    }



    public void show(UnityAction pSureAction)
    {
        content.SetActive(true);
        sureAction = pSureAction;
    }
}
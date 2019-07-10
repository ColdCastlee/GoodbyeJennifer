using System;
using Game.Const;
using Game.Global;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        gameObject.AddComponent<MainLoop>();
        gameObject.AddComponent<AudioMgr>();
        GlobalVar.InitGlovalVar();
        AudioMgr.Instance.PlayAuBg("Music_game");
        RegisterUI();
    }

    private void Start()
    {
        //这里加载第一个Panel，以后的panel转换都在panel里写
        //UIManager.Instance.PushPanel(字符串Panel名，一般用PanelName类成员);
        UIManager.Instance.PushPanel(UIName.start);
    }

    void RegisterUI()
    {
        //这里注册你写的UI类
        AbstractPanel.RegisterPanel<StartPanel>(UIName.start);
        AbstractPanel.RegisterPanel<AfterP1Panel>(UIName.afterP1);
        AbstractPanel.RegisterPanel<AfterP2Pane2>(UIName.afterP2);
        AbstractPanel.RegisterPanel<Player1Panel>(UIName.Player1Win);
        AbstractPanel.RegisterPanel<Player2Panel>(UIName.Player2Win);
        AbstractPanel.RegisterPanel<Pause1Panel>(UIName.Player1pause);
        AbstractPanel.RegisterPanel<Pause2Panel>(UIName.Player2pause);
    }
}

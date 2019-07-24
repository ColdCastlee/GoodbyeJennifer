using System;
using System.Runtime.InteropServices;
using Game.Common;
using Game.Const;
using Game.Global;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;

public class GameMgr : MonoSingleton<GameMgr>
{

    private AudioMgr audioMgr;
    public string nextUIPanelName;


    protected override void Awake()
    {
        base.Awake();
        //MessageBox("GameMgr_Awake");
        
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<MainLoop>();

        audioMgr = gameObject.AddComponent<AudioMgr>();
        
        GlobalVar.InitGlovalVar();

        RegisterUI();
    }

    private void Start()
    {
        //MessageBox("GameMgr_Start");
        
        
        //这里加载第一个Panel，以后的panel转换都在panel里写
        //UIManager.Instance.PushPanel(字符串anel名，一般用PanelName类成员);
        UIManager.Instance.PushPanel(UIName.start);
        audioMgr.PlayAuBg("Music_game");
    }

    void RegisterUI()
    {
        AbstractPanel.RegisterPanel<StartPanel>(UIName.start);
        AbstractPanel.RegisterPanel<AfterP1Panel>(UIName.afterP1);
        AbstractPanel.RegisterPanel<AfterP2Pane2>(UIName.afterP2);
        AbstractPanel.RegisterPanel<Player1Panel>(UIName.Player1Win);
        AbstractPanel.RegisterPanel<Player2Panel>(UIName.Player2Win);
        AbstractPanel.RegisterPanel<Pause1Panel>(UIName.Player1pause);
        AbstractPanel.RegisterPanel<Pause2Panel>(UIName.Player2pause);
    }
    
    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr handle, String message, String title="Title", int type=0);

    public static void MessageBox(string msg, string titile = "Title")
    {
        MessageBox(IntPtr.Zero, msg, titile, 0);
    }
}

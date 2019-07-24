using System;
using Game.Const;
using Game.Global;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;


    public class SceneMgr : MonoBehaviour
    {
        public bool workAsGameMgr;
        public bool initSceneMark;

        private void Awake()
        {

            if (workAsGameMgr)
            {
              DontDestroyOnLoad(gameObject);
          
              gameObject.AddComponent<MainLoop>();
              gameObject.AddComponent<AudioMgr>();
              
              GlobalVar.InitGlovalVar();
              
              AudioMgr.Instance.PlayAuBg("Music_game");
              RegisterUI();              
            }
            else
            {
                GlobalVar.InitGlovalVar();
            }
            
            if(initSceneMark)
                GlobalVar.InitSceneMark();

        }

        private void Start()
        {
            if (GameMgr.Instance && !string.IsNullOrEmpty(GameMgr.Instance.nextUIPanelName))
            {
                UIManager.Instance.PushPanel(GameMgr.Instance.nextUIPanelName);
                GameMgr.Instance.nextUIPanelName = null;
            }
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
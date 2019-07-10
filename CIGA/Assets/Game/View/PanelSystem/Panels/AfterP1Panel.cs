using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Const;
using Game.Script;
using UnityEngine.SceneManagement;

namespace Game.View.PanelSystem
{

    public class AfterP1Panel : AbstractPanel
    {
        protected override void Load()
        {
            Create(DirPath.PanelDir + UIName.afterP1);
            var p2 = m_TransFrom.Find("Image_P2");
            var backMain = m_TransFrom.Find("Image_Exit");

     
            var choseP2 = p2.gameObject.AddComponent<UIInputer>();
            var choseBackMain = backMain.gameObject.AddComponent<UIInputer>();

            choseP2.eventOnPointerClick += ChoseP2;
            choseBackMain.eventOnPointerClick += ChoseBackMain;

            void ChoseP2(UnityEngine.EventSystems.PointerEventData data)
            {



                SceneManager.LoadScene("Player2");

            }

            void ChoseBackMain(UnityEngine.EventSystems.PointerEventData data)
            {

                UIManager.Instance.PushPanel(UIName.start);
            }
        }
    }

}
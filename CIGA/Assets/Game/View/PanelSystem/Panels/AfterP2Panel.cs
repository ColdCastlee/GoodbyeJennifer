using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Const;
using Game.Script;
using UnityEngine.SceneManagement;

namespace Game.View.PanelSystem
{

    public class AfterP2Pane2 : AbstractPanel
    {
        protected override void Load()
        {
            Create(DirPath.PanelDir + UIName.afterP2);
            var p1 = m_TransFrom.Find("Image_P1");
            var backMain = m_TransFrom.Find("Image_Exit");


            var choseP1 = p1.gameObject.AddComponent<UIInputer>();
            var choseBackMain = backMain.gameObject.AddComponent<UIInputer>();

            choseP1.eventOnPointerClick += ChoseP1;
            choseBackMain.eventOnPointerClick += ChoseBackMain;

            void ChoseP1(UnityEngine.EventSystems.PointerEventData data)
            {
                SceneManager.LoadScene("Player1");
            }

            void ChoseBackMain(UnityEngine.EventSystems.PointerEventData data)
            {

                UIManager.Instance.PushPanel(UIName.start);
            }
        }
    }

}
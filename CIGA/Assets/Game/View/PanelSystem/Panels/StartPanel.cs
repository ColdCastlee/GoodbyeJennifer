using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Const;
using Game.Script;
using UnityEngine.SceneManagement;

namespace Game.View.PanelSystem
{
    public class StartPanel : AbstractPanel
    {
        protected override void Load()
        {
            Create(DirPath.PanelDir + UIName.start);

            var p1 = m_TransFrom.Find("Image_P1");
            var p2 = m_TransFrom.Find("Image_P2");
            var oldPlayer = m_TransFrom.Find("Image_OldPlayer");

            var choseP1 = p1.gameObject.AddComponent<UIInputer>();
            var choseP2 = p2.gameObject.AddComponent<UIInputer>();
            var choseOldPlayer = oldPlayer.gameObject.AddComponent<UIInputer>();


            choseP1.eventOnPointerClick += ChoseP1;
            choseP2.eventOnPointerClick += ChoseP2;
            choseOldPlayer.eventOnPointerClick += ChoseOldPlayer;


            void ChoseP1(UnityEngine.EventSystems.PointerEventData data)
            {
                UIManager.Instance.PushPanel(UIName.afterP1);
                //SceneManager.LoadScene("Player1");
            }

            void ChoseP2(UnityEngine.EventSystems.PointerEventData data)
            {
                UIManager.Instance.PushPanel(UIName.afterP2);
                //SceneManager.LoadScene("Player2");
            }

            void ChoseOldPlayer(UnityEngine.EventSystems.PointerEventData data)
            {

                SceneManager.LoadScene("MainScene");
            }

        }
    }
}




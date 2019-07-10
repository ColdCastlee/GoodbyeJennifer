using System.Collections;
using System.Collections.Generic;
using Game.Const;
using Game.Script;
using UnityEngine.SceneManagement;

namespace Game.View.PanelSystem
{

    public class Pause1Panel : AbstractPanel
    {
        protected override void Load()
        {
            Create(DirPath.PanelDir + UIName.Player1pause);
            var backMain = m_TransFrom.Find("Image_Exit");


            var choseBackMain = backMain.gameObject.AddComponent<UIInputer>();

            choseBackMain.eventOnPointerClick += ChoseBackMain;

            void ChoseBackMain(UnityEngine.EventSystems.PointerEventData data)
            {

                UIManager.Instance.PushPanel(UIName.start);
            }
        }
    }
}

using Game.Const;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;


    public class Player1Life : MonoBehaviour
    {

        public GameObject playerOneUI;
        public void TakeDamage()
        {
            AudioMgr.Instance.PlayAuEffect("Hit");
            int count = 0;
                     
            for (int i = 0; i < playerOneUI.transform.childCount; i++)
            {
                         
                if (playerOneUI.transform.GetChild(i).gameObject.activeSelf)
                    count++;
                         
            }
            if (count <= 2)
            {
                UIManager.Instance.PushPanel(UIName.Player2Win);
            }
            else
            {
                playerOneUI.transform.GetChild(count-1).gameObject.SetActive(false);
            }
        }
    }
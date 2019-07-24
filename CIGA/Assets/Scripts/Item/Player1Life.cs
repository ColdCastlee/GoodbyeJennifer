using Game.Const;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;
using UnityEngine.SceneManagement;


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
                Debug.Log("Player2Win");
                GameMgr.Instance.nextUIPanelName = UIName.Player2Win;
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                playerOneUI.transform.GetChild(count-1).gameObject.SetActive(false);
            }
        }
    }
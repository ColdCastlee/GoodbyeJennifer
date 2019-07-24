using System;
using Game.Const;
using Game.Script;
using Game.View.PanelSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attacked : MonoBehaviour
{
    public Slider bloodSlider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //print("调用函数");
        //print(other.name);
        if (other.name=="Player_1")
        {
            //print("碰到玩家");
            if(MainCharacter.Instance.isDashing==false)
                return;
            var damage = bloodSlider.maxValue/3+1;
            if ((bloodSlider.maxValue-bloodSlider.value)<damage)
            {
                bloodSlider.value = bloodSlider.maxValue;
                MainLoop.Instance.ExecuteLater(() =>
                {
                    Debug.Log("Player1Win");
                    GameMgr.Instance.nextUIPanelName = UIName.Player1Win;
                    SceneManager.LoadScene("WinScene");
                }, 1);
            }
            else
            {
                
                bloodSlider.value += damage;
                print("blood:"+ bloodSlider.value);
            }
        }
    }
}
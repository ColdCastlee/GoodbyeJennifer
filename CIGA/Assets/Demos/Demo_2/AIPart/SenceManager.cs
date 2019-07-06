using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceManager : MonoBehaviour
{
    public GameObject[] AIs;
    // Start is called before the first frame update
    void Start()
    {
        AIs = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //Debug.Log(AIs[0]);
            //第一个ai被控制
            var AIhandScript = AIs[0].GetComponent<HandAiTest>();
            AIhandScript.enabled = false;
            var HumanHnadScript = AIs[0].GetComponent<AIswitchToHuman>();
            HumanHnadScript.enabled = true;
            //第二个ai不被控制
            var AIhandScript2 = AIs[1].GetComponent<HandAiTest>();
            AIhandScript2.enabled = true;
            var HumanHnadScript2 = AIs[1].GetComponent<AIswitchToHuman>();
            HumanHnadScript2.enabled = false;
        }
      

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            
          
            //第二个ai被控制
            var AIhandScript2 = AIs[1].GetComponent<HandAiTest>();
            AIhandScript2.enabled = false ;
            var HumanHnadScript2 = AIs[1].GetComponent<AIswitchToHuman>();
            HumanHnadScript2.enabled = true;
            Debug.Log(AIs[1]);
            //第一个ai不被控制
            var AIhandScript = AIs[0].GetComponent<HandAiTest>();
            AIhandScript.enabled = true;
            var HumanHnadScript = AIs[0].GetComponent<AIswitchToHuman>();
            HumanHnadScript.enabled = false;
        }

    }
}

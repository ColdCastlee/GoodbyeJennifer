using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestory : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DD( ));
    }

    IEnumerator DD() {
        var WaitTimeFromParent = transform.parent.gameObject.GetComponent<HandAiTest>();
        var WaitTime = WaitTimeFromParent.WaitTime;
        yield return new WaitForSeconds(WaitTime);
        Destroy(gameObject);
    }
}

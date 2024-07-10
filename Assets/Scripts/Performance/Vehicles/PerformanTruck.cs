using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanTruck : MonoBehaviour
{
    public GameObject truckDesGO;

    private Vector3 curSpeed = Vector3.zero;
    private float curDur = 0;
    private float dur = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curSpeed.magnitude!=0)
        {
            curDur += Time.deltaTime;
            transform.position += curSpeed * Time.deltaTime;

            if (curDur >= dur)
            {
                curSpeed=Vector3.zero; curDur=0;
            }
        }

    }

    public void StartToMoveToDes(float dur)
    {
        curSpeed = truckDesGO.transform.position - transform.position;
        curDur += 0;
    }
}

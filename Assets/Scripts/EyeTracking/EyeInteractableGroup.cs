using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbstrackTask;

public class EyeInteractableGroup : MonoBehaviour
{
    public int groupIndex;
    public int sizeX;
    public int sizeY;
    public int unitSizeX;
    public int unitSizeY;
    public bool isVisible;

    public string unitPath = "Prefabs/EyeTracking/EIPanel";

    public Transform centerTransform;

    // Start is called before the first frame update
    void Start()
    {
        Transform tFirstUnit=this.transform;
        Transform tLastUnit=this.transform;
        for(int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                GameObject unitGO= Instantiate(Resources.Load(unitPath, typeof(GameObject)), this.transform) as GameObject;
                Vector3 localPosition = new Vector3((i+0.5f) * unitSizeX, 0, (j+0.5f) * unitSizeY);
                Vector3 scale = new Vector3(unitSizeX, unitSizeY,1 );
                unitGO.transform.localPosition = localPosition;
                unitGO.transform.localScale = scale;
                EyeInteractable eyeInteractable = unitGO.gameObject.GetComponent<EyeInteractable>();
                if (eyeInteractable != null)
                {
                    eyeInteractable.groupIndex = groupIndex;
                    eyeInteractable.itemIndex0 = i;
                    eyeInteractable.itemIndex1 = j;
                }

                if(isVisible)
                {
                    unitGO.layer = LayerMask.NameToLayer("Visible_EyeInteractable");
                }
                else
                {
                    unitGO.layer = LayerMask.NameToLayer("Invisible_EyeInteractable");
                }

                if(i == 0 && j == 0)
                {
                    tFirstUnit = unitGO.transform;
                }
                if(i==sizeX-1 && j == sizeY-1)
                {
                    tLastUnit = unitGO.transform;
                }
            }
        }

        if(centerTransform!=null)
        {
            Vector3 centerPos = (tFirstUnit.position + tLastUnit.position) / 2;

            this.transform.position += centerTransform.position - centerPos;
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

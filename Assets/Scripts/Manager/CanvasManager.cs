using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour, IManager
{

    void Awake()
    {
        PreInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateLoadingUI(float progress)
    {
        Debug.Log($"CanvasManager: update loading ui, progress = {progress}");
    }

    public void LockAll()
    {
        Debug.Log($"CanvasManager: Lock All");
    }

    public void UnlockAll()
    {
        Debug.Log($"CanvasManager: Unlock All");
    }

    public void Init()
    {

    }

    public void PreInit()
    {

    }

}

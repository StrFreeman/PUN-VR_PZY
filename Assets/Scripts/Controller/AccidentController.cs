using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;
using static AYellowpaper.SerializedCollections.SerializedDictionarySample;
using static AYellowpaper.SerializedCollections.SerializedDictionarySampleTwo;
using UnityEngine.Events;
using Photon;
using Photon.Pun;

public class AccidentController : MonoBehaviour
{
    [SerializedDictionary("Key Code", "RpcTarget and AddTaskEvent")]
    public SerializedDictionary<KeyCode, UnityEvent> keyCodeToAddTask;

    [SerializedDictionary("Key Code", "Add Task Action")]
    public Dictionary<string, float> dict;

    [SerializedDictionary("KeyCode", "AddTaskAction")]
    public Dictionary<KeyCode, float> Key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode keyCode in keyCodeToAddTask.Keys)
        {
            if (Input.GetKeyDown(keyCode))
            {
                keyCodeToAddTask[keyCode].Invoke();
            }
        }
    }
}

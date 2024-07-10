using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using static AbstrackTask;

public class TaskManagerV2 : MonoBehaviour, IManager
{
    [SerializeField] private TowerCrane _target;
    public TowerCrane Target { get { return _target; } private set { _target = value; } }

    [SerializeField] private PerformanceManager _performanceManager;
    private PerformanceManager PerformanceManager { get { return _performanceManager; } }



    private List<AbstrackTask> _tasks;
    private List<AbstrackTask> Tasks
    {
        get { return _tasks; } 
        set { _tasks = value; } 
    }

    #region Fail and Finish
    [SerializeField] private float _thrErrorWeight;
    private float _curErrorWeight;
    [SerializeField] private float _thrFinishWeight;
    private float _curFinishWeight;

    
    public float ThrErrorWeight
    {
        get { return _thrErrorWeight; }
        private set { _thrErrorWeight = value; }
    }

    public float CurErrorWeight
    {
        get { return _curErrorWeight; }
        private set { _curErrorWeight = value; }
    }

    public float ThrFinishWeight
    {
        get { return _thrFinishWeight; }
        private set { _thrFinishWeight = value;}
    }

    public float CurFinishWeight
    {
        get { return _curFinishWeight; }
        private set { _curFinishWeight = value; }
    }


    #endregion

    void CheckOut()
    {

    }

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
        for(int i = 0; i<Tasks.Count; i++)
        {
            var task = Tasks[i];

            if (task.IsFailed())
            {
                List<PerformanceData> failPD = task.GetPerformanceData();
                PerformanceManager.Perform(failPD);

                CurErrorWeight += task.errorWeight;

                Tasks.RemoveAt(i);
                i--;

                continue;
            }

            if (task.IsFinished())
            {
                List<PerformanceData> finishPD = task.GetPerformanceData();
                PerformanceManager.Perform(finishPD);

                CurFinishWeight += task.finishWeight;

                Tasks.RemoveAt(i);
                i--;

                continue;
            }

            List<PerformanceData> performanceData = task.GetPerformanceData();
            if (performanceData != null && performanceData.Count != 0)
            {
                PerformanceManager.Perform(performanceData);
            }

            if (CurErrorWeight >= ThrErrorWeight || CurFinishWeight >= ThrFinishWeight)
            {
                CheckOut();
            }
        }
    }

    public void AddTask1()
    {
        PerformanceData startPD = new PerformanceData(
            new Message(Message.Source.Signaller, "A Truck Is Going to Enter the Cross Zone"),
            PerformanceManager.TruckEnterCrossZone
            );
        PerformanceData endPD = new PerformanceData(
            new Message(Message.Source.Signaller, "The Truck Has Left the Cross Zone"),
            PerformanceManager.TruckExitCrossZone
            );
        PerformanceData failPD = new PerformanceData(
            new Message(Message.Source.Signaller, "Task1 failed"),
            PerformanceManager.T1Failed
            );

        BaseTask sub0_timerTask = MakeTimerTask(5, 0, 0);
        ConditionalTask sub1_noMoveTask = new ConditionalTask(MakeTimerTask(5, 0, 0), MakeAnyMoveTask(new int[] {1,2,3}), 0, 0);

        SerialTask task1 = new SerialTask(new List<AbstrackTask>() { sub0_timerTask, sub1_noMoveTask }, 0, 0, startPD, endPD, failPD);
        Tasks.Add(task1);
    }

    public void AddTask2()
    {
        PerformanceData startPD = new PerformanceData(
            new Message(Message.Source.Signaller, "A Boom Pump Is Going to *** in the Cross Zone"),
            PerformanceManager.BoomPumpEnterCrossZone
            );
        PerformanceData endPD = new PerformanceData(
            new Message(Message.Source.Signaller, "The Boom Pump in the Cross Zone Has ***"),
            PerformanceManager.BoomPumpEnterCrossZone
            );
        PerformanceData failPD = new PerformanceData(
            new Message(Message.Source.Signaller, "Task2 failed"),
            PerformanceManager.T2Failed
            );

        BaseTask sub0_timerTask = MakeTimerTask(5, 0, 0);
        ConditionalTask sub1_noMoveTask = new ConditionalTask(MakeTimerTask(5, 0, 0), MakeAnyMoveTask(new int[] { 1, 2, 3 }), 0, 0);

        SerialTask task2 = new SerialTask(new List<AbstrackTask>() { sub0_timerTask, sub1_noMoveTask }, 0, 0, startPD, endPD, failPD);
        Tasks.Add(task2);
    }

    public void AddTask3()
    {
        PerformanceData startPD = new PerformanceData(
           new Message(Message.Source.Signaller, "des something"),
           PerformanceManager.AddObstacleToDes
           );

        PerformanceManager.Perform(startPD);
    }

    public void AddTask4()
    {
        PerformanceData startPD = new PerformanceData(
            new Message(Message.Source.Signaller, "There was a tremendous amount of wind."),
            PerformanceManager.StrongRainStart
            );
        PerformanceData endPD = new PerformanceData(
            new Message(Message.Source.Signaller, "The winds have stopped."),
            PerformanceManager.StrongRainEnd
            );
        PerformanceData failPD = new PerformanceData(
            new Message(Message.Source.Signaller, "Task4 failed"),
            PerformanceManager.T4Failed
            );

        BaseTask sub0_timerTask = MakeTimerTask(5, 0, 0);
        BaseTask sub1_lockTask = new BaseTask(Target.walkietalkie.GetStatus_throwAway, 0, true, false, false, 0, 0);
        ConditionalTask sub1_noMoveTask = new ConditionalTask(MakeTimerTask(5, 0, 0), MakeAnyMoveTask(new int[] { 1, 2, 3 }), 0, 0);

        SerialTask task4 = new SerialTask(new List<AbstrackTask>() { sub0_timerTask, sub1_noMoveTask }, 0, 0, startPD, endPD);
        Tasks.Add(task4);
    }

    public void AddTask5()
    {
        PerformanceData startPD = new PerformanceData(
            new Message(Message.Source.Signaller, "There was a rain."),
            PerformanceManager.StrongWindStart
            );
        PerformanceData endPD = new PerformanceData(
            new Message(Message.Source.Signaller, "The rain has stopped."),
            PerformanceManager.StrongWindEnd
            );
        PerformanceData failPD = new PerformanceData(
            new Message(Message.Source.Signaller, "Task5 failed"),
            PerformanceManager.T5Failed
            );

        BaseTask sub0_timerTask = MakeTimerTask(5, 0, 0);

        BaseTask sub1_0_lockTask = new BaseTask(Target.walkietalkie.GetStatus_throwAway, 0, true, false, false, 0, 0);
        BaseTask sub1_1_timerTask = MakeTimerTask(0.5f, 0, 0);
        ConditionalTask sub1_checkLockTask = new ConditionalTask(sub1_0_lockTask, sub1_1_timerTask, 0, 0, startPD, endPD);
        
        ConditionalTask sub2_noMoveTask =  new ConditionalTask(MakeTimerTask(5, 0, 0), MakeAnyMoveTask(new int[] { 1, 2, 3 }), 0, 0);

        SerialTask task5 = new SerialTask(new List<AbstrackTask>() { sub0_timerTask, sub1_checkLockTask, sub2_noMoveTask }, 0, 0);
        Tasks.Add(task5);
    }



    private BaseTask MakeAnyMoveTask(int[] compnentIndices, float finishWeight = 0, float errorWeight = 0)
    {


        Func<float> getParaMethod = () =>
        {
            float totalChange = 0;
            foreach (int compnentIndex in compnentIndices)
            {
                AbstractCompnent compnent = Target.compnents[compnentIndex];
                foreach (Pair<float, float> pair in compnent.behavRecordDict["UpdateStatus"])
                {
                    totalChange += Math.Abs(pair.Item2 * 10000);
                }
            }
            return totalChange;
        };

        BaseTask task = new BaseTask(getParaMethod, 0, true, false, false, finishWeight, errorWeight);
        return task;
    }

    private BaseTask MakeTimerTask(float duration, float finishWeight = 0, float errorWeight = 0, PerformanceData startPD = null, PerformanceData finishPD = null, PerformanceData failPD = null, AbstrackTask.ExitCode failExitCode = AbstrackTask.ExitCode.none)
    {
        BaseTask timerTask = new BaseTask(null, duration, true, true, false, finishWeight, errorWeight, startPD, finishPD, failPD);
        timerTask.getParaMethod = timerTask.GetDuration;


        return timerTask;
    }

    public void Init()
    {
        Tasks = new List<AbstrackTask>();

        BaseTask makeCollisionTask = new BaseTask(Target.IsLoadCollidedF, 0.5f, true, true, false, 0, 0);
        ConditionalTask avoidCollisionTask = new ConditionalTask(MakeTimerTask(5, 0, 0), MakeAnyMoveTask(new int[] { 1, 2, 3 }), 0, 0);


        PerformanceData unlockStartButton = new PerformanceData(
            PerformanceManager.EnableStartButton
            );
        BaseTask arriveDesTask = new BaseTask(Target.IsLoadArrivedF, 0.5f, true, true, false, 0, 0, unlockStartButton);


    }

    public void PreInit()
    {
        Tasks = new List<AbstrackTask>();

        switch (MonoApplication.Instance.CurPlayerRole)
        {
            case MonoApplication.PlayerRole.Player1:
                {
                    GameObject tc1GO = GameObject.Find("TC_player_1");
                    Target = tc1GO.GetComponent<TowerCrane>();
                    


                    break;
                }
            case MonoApplication.PlayerRole.Player2:
                {
                    GameObject tc2GO = GameObject.Find("TC_player_2");
                    Target = tc2GO.GetComponent<TowerCrane>();


                    break;
                }
            default:
                {
                    Debug.Log($"LevelManager: unknown PlayerRole = {MonoApplication.Instance.CurPlayerRole}");
                    break;
                }

        }

    }
}

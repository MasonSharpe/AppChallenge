using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour {


    public static List<Timer> timers = new();

    public static int timerCount = 0;


    public class Timer {

        public string name;
        public float startingTime;
        public float time;
        public float uneasedTime;
        public bool goesDown;
        public float endingTime;
        public Action action;
        public bool doActionEveryFrame;

    }

    public static Timer CreateTimer(float time, Action action = null, string name = "", bool actionEveryFrame = false) {

        Timer timer = new() {
            name = name == "" ? "NA" + timerCount.ToString() : name, time = time, startingTime = time, action = action, uneasedTime = time, doActionEveryFrame = actionEveryFrame
        };
        timers.Add(timer);
        timerCount++;
        return timer;

    }

    public static Timer GetTimer(string name) {
        return timers.Find(t => t.name == name);
    }

    public static void DestroyTimer(string name) {
        timers.Remove(GetTimer(name));
    }

    private void Update() {
 
        for (int i = 0; i < timers.Count; i++) {

            Timer timer = timers[i];
            timer.time -= Time.deltaTime;
            if (timer.time <= 0) {

                timer.time = timer.endingTime;
                timer.action();
                DestroyTimer(timer.name);
                i--;

            }else if (timer.doActionEveryFrame) {
                timer.action();
            }
        }
    }

    

}
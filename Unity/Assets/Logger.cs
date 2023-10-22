using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Logger : MonoBehaviour 
{

    private class Counter {
        public uint count = 0;
    }

    string log_filename;
    string log_path;

    Button button_view_1;
    Button button_view_2;
    Button button_view_3;
    Button button_mirror;
    Button button_stop;
    Button button_play;
    Button button_prev;
    Button button_next;

    Slider slider_speed;
    Slider slider_elapsed;

    Counter counter_button_view_1 = new Counter();
    Counter counter_button_view_2 = new Counter();
    Counter counter_button_view_3 = new Counter();
    Counter counter_button_mirror = new Counter();
    Counter counter_button_stop = new Counter();
    Counter counter_button_play = new Counter();
    Counter counter_button_next = new Counter();
    Counter counter_button_prev = new Counter();

    Counter counter_slider_speed = new Counter();
    Counter counter_slider_elapsed = new Counter();

    void LogName(string name, Counter counter) {
        counter.count = counter.count + 1;
        string to_write = "\nCLICK " + name + " " + getTimeShort();
        writeToFile(to_write);
    }

    void Start()
    {   
        log_filename = "log_" + getUnixTime().ToString() + ".txt";
        log_path = Path.Combine(Application.dataPath + "/Logs/", log_filename);

        writeToFile("START " + DateTime.UtcNow);

        button_view_1 = GameObject.Find("Button_PerspectiveDefault").GetComponent<Button>();
        button_view_2 = GameObject.Find("Button_PerspectiveFace").GetComponent<Button>();
        button_view_3 = GameObject.Find("Button_PerspectiveHands").GetComponent<Button>();
        button_mirror = GameObject.Find("Button_Mirror").GetComponent<Button>();
        button_stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        button_play = GameObject.Find("Button_PlayPause").GetComponent<Button>();
        button_next = GameObject.Find("Button_Next").GetComponent<Button>();
        button_prev = GameObject.Find("Button_Prev").GetComponent<Button>();

        slider_elapsed = GameObject.Find("Slider_AnimationElapsed").GetComponent<Slider>();
        slider_speed = GameObject.Find("Slider_AnimationSpeed").GetComponent<Slider>();

        button_view_1.onClick.AddListener(delegate {LogName(button_view_1.name, counter_button_view_1);});
        button_view_2.onClick.AddListener(delegate {LogName(button_view_2.name, counter_button_view_2);});
        button_view_3.onClick.AddListener(delegate {LogName(button_view_3.name, counter_button_view_3);});
        button_mirror.onClick.AddListener(delegate {LogName(button_mirror.name, counter_button_mirror);});
        button_stop.onClick.AddListener(delegate {LogName(button_stop.name, counter_button_stop);});
        button_play.onClick.AddListener(delegate {LogName(button_play.name, counter_button_play);});
        button_next.onClick.AddListener(delegate {LogName(button_next.name, counter_button_next);});
        button_prev.onClick.AddListener(delegate {LogName(button_prev.name, counter_button_prev);});

        slider_elapsed.onValueChanged.AddListener(delegate {LogName(slider_elapsed.name, counter_slider_elapsed);});
        slider_speed.onValueChanged.AddListener(delegate {LogName(slider_speed.name, counter_slider_speed);});
    }

    void Update() {

    }

    long getUnixTime() {
        return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }

    string getTimeShort() {
        DateTime currentTime = DateTime.Now;
        int hours = currentTime.Hour;
        int minutes = currentTime.Minute;
        int seconds = currentTime.Second;
        int milliseconds = currentTime.Millisecond;
        return string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);
    }

    void writeToFile(string to_write) {
        try {  
            File.AppendAllText(log_path, to_write);
        } catch (Exception e) {
            Debug.LogError("Error writing to file: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        string to_write = string.Format(
            "\nClicks:" + 
            "\n\tButton_PerspectiveDefault: {0}" + 
            "\n\tButton_PerspectiveFace: {1}" +
            "\n\tButton_PerspectiveHands: {2}" +
            "\n\tButton_Mirror: {3}" +
            "\n\tButton_Stop: {4}" +
            "\n\tButton_Play: {5}" + 
            "\n\tButton_Prev: {6}" + 
            "\n\tButton_Next: {7}",
            counter_button_view_1.count,
            counter_button_view_2.count,
            counter_button_view_3.count,
            counter_button_mirror.count,
            counter_button_stop.count,
            counter_button_play.count,
            counter_button_prev.count,
            counter_button_next.count
        );
        /*
        string to_write = string.Format(
            "\nClicks:" + 
            "\n\tButton_PerspectiveDefault: {0}" + 
            "\n\tButton_PerspectiveFace: {1}" +
            "\n\tButton_PerspectiveHands: {2}" +
            "\n\tButton_Mirror: {3}" +
            "\n\tButton_Stop: {4}" +
            "\n\tButton_Play: {5}" + 
            "\n\tButton_Prev: {6}" + 
            "\n\tButton_Next: {7}" +
            "\n\tSlider_Elapsed: {8}" +
            "\n\tSlider_Speed: {9}",
            counter_button_view_1.count,
            counter_button_view_2.count,
            counter_button_view_3.count,
            counter_button_mirror.count,
            counter_button_stop.count,
            counter_button_play.count,
            counter_button_prev.count,
            counter_button_next.count,
            counter_slider_elapsed.count,
            counter_slider_speed.count
        );
        */

        writeToFile(to_write);
    }

}

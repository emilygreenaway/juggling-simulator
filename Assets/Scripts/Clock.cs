﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float currentTime = 0F;
    public bool shouldTime = false;
    public GameObject currentTimeTextGameObject;
    public GameObject currentRecordTextGameObject;
    public Dictionary<int, float> records = new Dictionary<int, float>()
    {
        { 1, 0F },
        { 2, 0F },
        { 3, 0F },
        { 4, 0F },
        { 5, 0F },
        { 6, 0F },
        { 7, 0F }
    };

    private VRTK.VRTK_ObjectTooltip currentTimeText;
    private VRTK.VRTK_ObjectTooltip currentRecordText;
    private Customisation customisation;

    private void Start()
    {
        currentTimeText = currentTimeTextGameObject.GetComponent<VRTK.VRTK_ObjectTooltip>();
        currentRecordText = currentRecordTextGameObject.GetComponent<VRTK.VRTK_ObjectTooltip>();
        customisation = this.gameObject.GetComponent<Customisation>();

        SetCurrentTimeText();
        SetCurrentRecordText();
    }

    void Update()
    {
        if (shouldTime)
        {
            currentTime += Time.deltaTime;
            SetCurrentTimeText();
        }
    }

    private float GetCurrentRecord()
    {
        var numberOfBalls = customisation.GetNumberOfBalls();
        /*
        Debug.Log("numberOfBalls" + numberOfBalls.ToString());
        Debug.Log("records[numberOfBalls]" + records.ToString());

        foreach (var record in records)
        {
            Debug.Log(string.Format("Employee with key {0}: value={1}", record.Key, record.Value));
        }
        */

        // todo: throw error if not underfined
        return records[numberOfBalls];
    }

    private void SetCurrentRecord(float newRecord)
    {
        var numberOfBalls = customisation.GetNumberOfBalls();
        records[numberOfBalls] = newRecord;
    }

    private void SetCurrentTimeText()
    {
        currentTimeText.UpdateText(currentTime.ToString());
    }

    private void SetCurrentRecordText()
    {
        currentRecordText.UpdateText(GetCurrentRecord().ToString());
    }


    public void StartTimer()
    {
        shouldTime = true;
        currentTime = 0F;
        SetCurrentTimeText();
        SetCurrentRecordText();
    }

    public void StopTimer()
    {
        if (shouldTime) // a previous ball stopped the time
        {
            if (GetCurrentRecord() <= currentTime)
            {
                SetCurrentRecord(currentTime);
                SetCurrentRecordText();
                SetCurrentTimeText();
            }

            shouldTime = false;
        }
    }

    public void UpdateScoreBoard()
    {
        SetCurrentTimeText();
        SetCurrentRecordText();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KmCounterScript : MonoBehaviour
{
    private PlayerBusScript bus;
    [SerializeField] private TMP_Text KMText;
    private void Awake()
    {
        bus = FindObjectOfType<PlayerBusScript>();
    }
    private void OnEnable()
    {
        Events.StartToMoveBusEvent.AddListener(RefreshKM);
    }


    private void OnDisable()
    {
        Events.StartToMoveBusEvent.RemoveListener(RefreshKM);
    }
    //private void CheckEndFase(Transform obj)
    //{
    //    if (!bus.currentRoad.GetComponent<RoadScript>().busStop
    //        && bus.currentKMTraveled >= bus.maxKMCapacity)
    //    {
    //        Debug.LogWarning("Se não tem parada e acabou o KM, deve finalizar a fase");
    //    }
    //}

    private void RefreshKM(int km)
    {
        bus.currentKMTraveled += km;
        if (bus.currentKMTraveled > bus.maxKMCapacity) { bus.currentKMTraveled = bus.maxKMCapacity; }
        KMText.text = bus.currentKMTraveled.ToString() +"/" + bus.maxKMCapacity.ToString();
        Debug.LogWarning("Chamou?");
    }

}

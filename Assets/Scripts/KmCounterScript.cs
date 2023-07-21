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
        Events.atDestinationEvent.AddListener(CheckEndFase);
    }


    private void OnDisable()
    {
        Events.StartToMoveBusEvent.RemoveListener(RefreshKM);
        Events.atDestinationEvent.RemoveListener(CheckEndFase);
    }
    private void CheckEndFase(Transform obj)
    {
        if (!bus.currentRoad.GetComponent<RoadScript>().busStop
            && bus.currentKMTraveled >= bus.maxKMCapacity)
        {
            Debug.LogWarning("Se não tem parada e acabou o KM, deve finalizar a fase");
        }
    }

    private void RefreshKM(int km)
    {
        bus.currentKMTraveled += km;
        KMText.text = bus.currentKMTraveled.ToString() +"/" + bus.maxKMCapacity.ToString();
        Debug.LogWarning("Chamou?");
    }

}

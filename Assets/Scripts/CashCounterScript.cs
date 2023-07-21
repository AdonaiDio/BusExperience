using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashCounterScript : MonoBehaviour
{
    private PlayerBusScript bus;
    [SerializeField] private TMP_Text CashText;
    private void Awake()
    {
        bus = FindObjectOfType<PlayerBusScript>();
    }
    private void Start()
    {
        RefreshCash();    
    }

    private void OnEnable()
    {
        Events.CashInEvent.AddListener(RefreshCash);
    }
    private void OnDisable()
    {
        Events.CashInEvent.RemoveListener(RefreshCash);
    }

    private void RefreshCash()
    {
       CashText.text = bus.money.ToString();
    }
}

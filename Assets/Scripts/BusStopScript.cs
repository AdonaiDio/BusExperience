using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopScript : MonoBehaviour
{
    public enum BusStopType { type1, type2, type3, type4};
    [SerializeField] private List<Passenger> passengers;
    [SerializeField] private BusStopType busStopType;

    private EmbarckWindowManagerScript EWMScript;
    //>Onibus chega na parada
    //>Enquanto tiver espaço para todos, todos embarcam
    //>Onibus chega na parada
    //>Se tiver passageiro que desce ele desce automaticamente
    //>Se não tiver espaço para todos os passageiros entrarem, então o jogador deve escolher quem sobe.
    private void Awake()
    {
        EWMScript = FindObjectOfType<EmbarckWindowManagerScript>();
    }
    private void OnEnable()
    {
        Events.OnEmbarkButtonEvent.AddListener(RefreshPassangersList);
    }

    private void OnDisable()
    {
        Events.OnEmbarkButtonEvent.RemoveListener(RefreshPassangersList);
    }

    private void Start()
    {

    }
    private void RefreshPassangersList(List<Passenger> pEmbarkList, List<Passenger> pWaitList)
    {
        passengers.Clear();
        foreach (Passenger p in pWaitList)
        {
            passengers.Add(p);
        }
    }

    public void CheckForPassengers(Transform playerBus)
    {
        PlayerBusScript bus = playerBus.GetComponent<PlayerBusScript>();
        List<Passenger> auxBusPassengersList = new List<Passenger>();

        foreach (Passenger pas in bus.passengers)
        { 
            auxBusPassengersList.Add(pas);
        }
        // se tem passageiro no ONIBUS que vai descer, DECE todos
        foreach (Passenger passenger in auxBusPassengersList) {
            if (passenger.destiny == busStopType) {
                // remove o passageiro do onibus e somar o dinheiro
                Events.RemovePassangerFromBusEvent.Invoke(passenger);
            };
        }

        // se tem espaço e passageiro na parada que vai subir, SOBE todos
        if (passengers.Count <= (bus.maxPassengerCapacity
                                 - bus.passengers.Count))
        { //cabe
            foreach (Passenger passenger in passengers)
            {
                // adiciona passageiro ao onibus
                Events.AddPassangerFromBusEvent.Invoke(passenger);
            }
            // remove todos os passageiros da parada de onibus
            passengers.Clear();
        }
        else if (passengers.Count > (bus.maxPassengerCapacity
                                     - bus.passengers.Count)
                 && bus.maxPassengerCapacity - bus.passengers.Count != 0)
        {
            // não há espaço no onibus. Escolher quem sobe.
            Debug.Log("Opa! Falta espaço. Escolha quem vai subir.");
            //abri janela de gerenciamento de passageiros
            int _availableSlots = bus.maxPassengerCapacity - bus.passengers.Count;
            EWMScript.StartPassengersInfo(passengers, _availableSlots);
            //desativar controle até a janela fechar

        }
    }
}

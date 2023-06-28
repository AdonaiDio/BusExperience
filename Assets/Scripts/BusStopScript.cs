using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopScript : MonoBehaviour
{
    public enum BusStopType { type1, type2, type3};
    [SerializeField] private List<Passenger> passengers;
    [SerializeField] private BusStopType busStopType;

    //>Onibus chega na parada
    //>Enquanto tiver espaço para todos, todos embarcam
    //>Onibus chega na parada
    //>Se tiver passageiro que desce ele desce automaticamente
    //>Se não tiver espaço para todos os passageiros entrarem, então o jogador deve escolher quem sobe.
    public void CheckForPassengers(Transform playerBus)
    {
        List<Passenger> busPassengers = playerBus.GetComponent<PlayerBusScript>().passengers;

        // se tem passageiro no ONIBUS que vai descer, DECE todos
        foreach (Passenger passenger in busPassengers)
        {
            if (passenger.destiny == busStopType)
            {
                // remove o passageiro do onibus e somar o dinheiro
                Debug.Log("Dece");
                busPassengers.Remove(passenger);
                playerBus.GetComponent<PlayerBusScript>().money += passenger.Cash;
            };
        }

        // se tem espaço e passageiro na parada que vai subir, SOBE todos
        if (passengers.Count <= (playerBus.GetComponent<PlayerBusScript>().maxPassengerCapacity 
                                 - playerBus.GetComponent<PlayerBusScript>().passengers.Count))
        {
            foreach (Passenger passenger in passengers)
            {
                // adiciona passageiro ao onibus
                busPassengers[busPassengers.Count] = passenger;
                Debug.Log("Sobe");
            }
            // remove todos os passageiros da parada de onibus
            passengers.Clear();
        }
        else
        {
            // não há espaço no onibus. Escolher quem sobe.
            Debug.Log("Opa! Falta espaço. Escolha quem vai sobir.");
        }
    }
}

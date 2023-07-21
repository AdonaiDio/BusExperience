using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassangersCountersScript : MonoBehaviour
{
    private int maxCapacity;
    private int passengersOnBoard;

    [SerializeField] private TMP_Text passengersCounterText;

    [SerializeField] private List<Passenger> passengersType1;
    private GameObject passengersType1UI;

    [SerializeField] private List<Passenger> passengersType2;
    private GameObject passengersType2UI;

    [SerializeField] private List<Passenger> passengersType3;
    private GameObject passengersType3UI;

    [SerializeField] private List<Passenger> passengersType4;
    private GameObject passengersType4UI;

    private PlayerBusScript playerBus;

    //Animation
    [SerializeField] private float slideDuration = .2f;


    private void Awake()
    {
        playerBus = FindObjectOfType<PlayerBusScript>();
        passengersType1UI = transform.Find("ListOfPassagersCounters/P_Counter_type1").gameObject;
        passengersType2UI = transform.Find("ListOfPassagersCounters/P_Counter_type2").gameObject;
        passengersType3UI = transform.Find("ListOfPassagersCounters/P_Counter_type3").gameObject;
        passengersType4UI = transform.Find("ListOfPassagersCounters/P_Counter_type4").gameObject;
    }
    private void OnEnable()
    {
        Events.RemovePassangerFromBusEvent.AddListener(RemovePassanger);
        Events.AddPassangerFromBusEvent.AddListener(AddPassanger);
        Events.OnEmbarkButtonEvent.AddListener(AddPassangersList);
    }
    private void OnDisable()
    {
        Events.RemovePassangerFromBusEvent.RemoveListener(RemovePassanger);
        Events.AddPassangerFromBusEvent.RemoveListener(AddPassanger);
        Events.OnEmbarkButtonEvent.RemoveListener(AddPassangersList);
    }
    void Start()
    {
        maxCapacity = playerBus.maxPassengerCapacity;
        passengersOnBoard = playerBus.passengers.Count;
        passengersCounterText.text = passengersOnBoard + "/" + maxCapacity;

        void ChosenType(List<Passenger> passengerList, GameObject passengerUI)
        {
            if (passengerList != null && passengerList.Count > 0)
            {
                passengerUI.SetActive(true);
                passengerUI.transform.Find("circleCounter/Text")
                    .GetComponent<TMP_Text>().text = passengerList.Count.ToString();
                int _totalValue = 0;
                foreach (Passenger p in passengerList)
                {
                    _totalValue += p.Cash;
                }
                passengerUI.transform.Find("mask/pop-upTotalCash/Text")
                    .GetComponent<TMP_Text>().text = "$" + _totalValue.ToString();
            }
        }

        foreach (Passenger passenger in playerBus.passengers)
        {
            switch (passenger.destiny)
            {
                case BusStopScript.BusStopType.type1:
                    passengersType1.Add(passenger);
                    break;
                case BusStopScript.BusStopType.type2:
                    passengersType2.Add(passenger);
                    break;
                case BusStopScript.BusStopType.type3:
                    passengersType3.Add(passenger);
                    break;
                case BusStopScript.BusStopType.type4:
                    passengersType4.Add(passenger);
                    break;
                default:
                    Debug.LogWarning("BusStopType not predicted.");
                    break;
            }
        }
        
        ChosenType(passengersType1,passengersType1UI);
        ChosenType(passengersType2,passengersType2UI);
        ChosenType(passengersType3,passengersType3UI);
        ChosenType(passengersType4,passengersType4UI);
    }

    private void RemovePassanger(Passenger passenger)
    {
        passengersOnBoard -= 1;
        passengersCounterText.text = passengersOnBoard + "/" + maxCapacity;
        void ChosenType(List<Passenger> passengerList, GameObject passengerUI)
        {
            passengerList.Remove(passenger);
            if (passengerList != null && passengerList.Count > 0)
            {
                passengerUI.SetActive(true);
                passengerUI.transform.Find("circleCounter/Text")
                    .GetComponent<TMP_Text>().text = passengerList.Count.ToString();
                int _totalValue = 0;
                foreach (Passenger p in passengerList)
                {
                    _totalValue += p.Cash;
                }
                passengerUI.transform.Find("mask/pop-upTotalCash/Text")
                    .GetComponent<TMP_Text>().text = "$" + _totalValue.ToString();
            }
            else passengerUI.SetActive(false);
        }
        switch (passenger.destiny)
        {
            case BusStopScript.BusStopType.type1:
                ChosenType(passengersType1, passengersType1UI);
                break;

            case BusStopScript.BusStopType.type2:
                ChosenType(passengersType2, passengersType2UI);
                break;

            case BusStopScript.BusStopType.type3:
                ChosenType(passengersType3, passengersType3UI);
                break;

            case BusStopScript.BusStopType.type4:
                ChosenType(passengersType4, passengersType4UI);
                break;

            default:
                Debug.LogWarning("BusStopType not predicted.");
                break;
        }
    }
    private void AddPassanger(Passenger passenger)
    {
        passengersOnBoard += 1;
        passengersCounterText.text = passengersOnBoard + "/" + maxCapacity;
        void ChosenType(List<Passenger> passengerList, GameObject passengerUI)
        {
            passengerList.Add(passenger);
            if (passengerList != null && passengerList.Count > 0)
            {
                passengerUI.SetActive(true);
                passengerUI.transform.Find("circleCounter/Text")
                    .GetComponent<TMP_Text>().text = passengerList.Count.ToString();
                int _totalValue = 0;
                foreach (Passenger p in passengerList)
                {
                    _totalValue += p.Cash;
                }
                passengerUI.transform.Find("mask/pop-upTotalCash/Text")
                    .GetComponent<TMP_Text>().text = "$" + _totalValue.ToString();
            }
            else passengerUI.SetActive(false);
        }
        switch (passenger.destiny)
        {
            case BusStopScript.BusStopType.type1:
                ChosenType(passengersType1, passengersType1UI);
                break;

            case BusStopScript.BusStopType.type2:
                ChosenType(passengersType2, passengersType2UI);
                break;

            case BusStopScript.BusStopType.type3:
                ChosenType(passengersType3, passengersType3UI);
                break;

            case BusStopScript.BusStopType.type4:
                ChosenType(passengersType4, passengersType4UI);
                break;

            default:
                Debug.LogWarning("BusStopType not predicted.");
                break;
        }
    }

    public void CheckForPriceTagActive(GameObject priceTagUI)
    {
        if (priceTagUI.activeSelf) { 
            priceTagUI.transform.LeanMoveLocalX(-140f, slideDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(_SetFalse);
        }
        else
        {
            priceTagUI.SetActive(true);
            priceTagUI.transform.LeanMoveLocalX(-40f, slideDuration).setEase(LeanTweenType.easeOutQuad);
        }
        void _SetFalse() { priceTagUI.SetActive(false); }
    }

    private void AddPassangersList(List<Passenger> pEmbarkList, List<Passenger> pWaitList)
    {
        foreach (Passenger p in pEmbarkList)
        {
            AddPassanger(p);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmbarckWindowManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text counterUIText;
    private int passengersEmbarking = 0;
    private int availablesSlots;
    private int currentSlotsAvailable;

    [SerializeField] private GameObject embarkWindow;
    [SerializeField] private Button embarkButton;

    //waiting
    [SerializeField] private List<Passenger> passengersWaitingType1;
    private GameObject passengersWaitingType1UI;

    [SerializeField] private List<Passenger> passengersWaitingType2;
    private GameObject passengersWaitingType2UI;

    [SerializeField] private List<Passenger> passengersWaitingType3;
    private GameObject passengersWaitingType3UI;

    [SerializeField] private List<Passenger> passengersWaitingType4;
    private GameObject passengersWaitingType4UI;
    
    //embarking
    [SerializeField] private List<Passenger> passengersEmbarkingType1;
    private GameObject passengersEmbarkingType1UI;

    [SerializeField] private List<Passenger> passengersEmbarkingType2;
    private GameObject passengersEmbarkingType2UI;

    [SerializeField] private List<Passenger> passengersEmbarkingType3;
    private GameObject passengersEmbarkingType3UI;

    [SerializeField] private List<Passenger> passengersEmbarkingType4;
    private GameObject passengersEmbarkingType4UI;

    private InputManager inputManager;
    private void Awake()
    {
        passengersWaitingType1UI = transform.Find(
            "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusStopList/ClickablePassangerGroup_1").gameObject;
        passengersWaitingType2UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusStopList/ClickablePassangerGroup_2").gameObject;
        passengersWaitingType3UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusStopList/ClickablePassangerGroup_3").gameObject;
        passengersWaitingType4UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusStopList/ClickablePassangerGroup_4").gameObject;
        passengersEmbarkingType1UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusList/ClickablePassangerGroup_1").gameObject;
        passengersEmbarkingType2UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusList/ClickablePassangerGroup_2").gameObject;
        passengersEmbarkingType3UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusList/ClickablePassangerGroup_3").gameObject;
        passengersEmbarkingType4UI = transform.Find(
                "EmbarckWindow/LayoutWindow/PassangerTransferencePanel/BusList/ClickablePassangerGroup_4").gameObject;

        inputManager = FindObjectOfType<InputManager>();
    }


    public void StartPassengersInfo(List<Passenger> _busStopPassengers, int _availablesSlots){
        embarkWindow.SetActive(true);
        passengersEmbarking = 0;
        availablesSlots = _availablesSlots;

        currentSlotsAvailable = availablesSlots - passengersEmbarking;

        //atualizar texto counter
        counterUIText.text = passengersEmbarking + "/" + _availablesSlots;

        //separa e ativa icones de passageiros
        foreach (Passenger passenger in _busStopPassengers)
        {
            switch (passenger.destiny)
            {
                case BusStopScript.BusStopType.type1:
                    passengersWaitingType1.Add(passenger);
                    break;
                case BusStopScript.BusStopType.type2:
                    passengersWaitingType2.Add(passenger);
                    break;
                case BusStopScript.BusStopType.type3:
                    passengersWaitingType3.Add(passenger);
                    break;
                case BusStopScript.BusStopType.type4:
                    passengersWaitingType4.Add(passenger);
                    break;
                default:
                    Debug.LogWarning("BusStopType not predicted.");
                    break;
            }
        }
        ChosenType(passengersWaitingType1, passengersWaitingType1UI);
        ChosenType(passengersWaitingType2, passengersWaitingType2UI);
        ChosenType(passengersWaitingType3, passengersWaitingType3UI);
        ChosenType(passengersWaitingType4, passengersWaitingType4UI);
        ChosenType(passengersEmbarkingType1, passengersEmbarkingType1UI);
        ChosenType(passengersEmbarkingType2, passengersEmbarkingType2UI);
        ChosenType(passengersEmbarkingType3, passengersEmbarkingType3UI);
        ChosenType(passengersEmbarkingType4, passengersEmbarkingType4UI);

    }
    void ChosenType(List<Passenger> passengerList, GameObject passengerUI)
    {
        if (passengerList != null && passengerList.Count > 0)
        {
            passengerUI.SetActive(true);
            passengerUI.transform.Find("circleCounter/Text")
                .GetComponent<TMP_Text>().text = passengerList.Count.ToString();
            //int _totalValue = 0;
            //foreach (Passenger p in passengerList)
            //{
            //    _totalValue += p.Cash;
            //}
            passengerUI.transform.Find("cash_unText")
                .GetComponent<TMP_Text>().text = "$" + passengerList[0].Cash;
        }
        else
            passengerUI.SetActive(false);
    }

    public void FromWaitingToEmbarcking(int type) {
        currentSlotsAvailable = availablesSlots - passengersEmbarking;
        void ThisTypeCase(List<Passenger> passengerWaitList, List<Passenger> passengerEmbarkList, 
                            GameObject passengerWaitUI, GameObject passengerEmbarkUI)
        {
            if (passengerWaitList.Count <= currentSlotsAvailable)
            {// menos/igual passageiro esperando que slot disponivel
                foreach (Passenger p in passengerWaitList)
                {
                    passengerEmbarkList.Add(p);
                    passengersEmbarking += 1;
                }
                //passengerEmbarkList = passengerWaitList;
                passengerWaitList.Clear();
                currentSlotsAvailable = availablesSlots - passengersEmbarking;
            }
            else if (currentSlotsAvailable >= 1)
            {// mais passageiro esperando que slot disponivel
             // e tem pelo menos 1 slot disponível
             //passa 1 passageiro para o embarque o numero de vezes que tem de espaço disponível atualmente.
                
                for (int i = 0; i < currentSlotsAvailable; i++)
                {
                    passengerEmbarkList.Add(passengerWaitList[passengerWaitList.Count - 1]);
                    passengerWaitList.Remove(passengerWaitList[passengerWaitList.Count - 1]);
                    passengersEmbarking += 1;
                    currentSlotsAvailable = availablesSlots - passengersEmbarking;
                }
            }
            ChosenType(passengerWaitList, passengerWaitUI);
            ChosenType(passengerEmbarkList, passengerEmbarkUI);

            //atualizar texto counter
            counterUIText.text = passengersEmbarking + "/" + availablesSlots;
        }
        switch (type)
        {
            case 1:
                ThisTypeCase(passengersWaitingType1, passengersEmbarkingType1, 
                             passengersWaitingType1UI, passengersEmbarkingType1UI);
                break;
            case 2:
                ThisTypeCase(passengersWaitingType2, passengersEmbarkingType2,
                             passengersWaitingType2UI, passengersEmbarkingType2UI);
                break;
            case 3:
                ThisTypeCase(passengersWaitingType3, passengersEmbarkingType3,
                             passengersWaitingType3UI, passengersEmbarkingType3UI);
                break;
            case 4:
                ThisTypeCase(passengersWaitingType4, passengersEmbarkingType4,
                             passengersWaitingType4UI, passengersEmbarkingType4UI);
                break;
            default:
                Debug.LogWarning("Numero invalido! Insira um número de 1 a 4");
                break;
        }
        if (currentSlotsAvailable == 0)
        {
            embarkButton.interactable = true;
        }
        else
        {
            embarkButton.interactable = false;
        }
    }
    public void FromEmbarckingToWaiting(int type)
    {
        //clicou no icone do passageiro embarcando
        //volta o grupo todo para a parada de onibus
        void ThisTypeCase(List<Passenger> passengerWaitList, List<Passenger> passengerEmbarkList,
                            GameObject passengerWaitUI, GameObject passengerEmbarkUI)
        {
            //passa todos os passageiros para a espera
            foreach (Passenger p in passengerEmbarkList)
            {
                passengerWaitList.Add(p);
                passengersEmbarking -= 1;
                currentSlotsAvailable = availablesSlots - passengersEmbarking;
            }
            passengerEmbarkList.Clear();
            //atualizar UI
            ChosenType(passengerWaitList, passengerWaitUI);
            ChosenType(passengerEmbarkList, passengerEmbarkUI);
            //atualizar texto counter
            counterUIText.text = passengersEmbarking + "/" + availablesSlots;
        }

        switch (type)
        {
            case 1:
                ThisTypeCase(passengersWaitingType1, passengersEmbarkingType1,
                             passengersWaitingType1UI, passengersEmbarkingType1UI);
                break;
            case 2:
                ThisTypeCase(passengersWaitingType2, passengersEmbarkingType2,
                             passengersWaitingType2UI, passengersEmbarkingType2UI);
                break;
            case 3:
                ThisTypeCase(passengersWaitingType3, passengersEmbarkingType3,
                             passengersWaitingType3UI, passengersEmbarkingType3UI);
                break;
            case 4:
                ThisTypeCase(passengersWaitingType4, passengersEmbarkingType4,
                             passengersWaitingType4UI, passengersEmbarkingType4UI);
                break;
            default:
                Debug.LogWarning("Numero invalido! Insira um número de 1 a 4");
                break;
        }
        if (currentSlotsAvailable > 0)
        {
            embarkButton.interactable = false;
        }
    }

    public void EmbarkButton()
    {
        //1º transfere tudo para lista unica que vai ser enviada via evento com o numero de passageiros
        List<Passenger> AllPassengerEmbarkList = new List<Passenger>();
        foreach (Passenger p in passengersEmbarkingType1)
        {
            AllPassengerEmbarkList.Add(p);
        }
        foreach (Passenger p in passengersEmbarkingType2)
        {
            AllPassengerEmbarkList.Add(p);
        }
        foreach (Passenger p in passengersEmbarkingType3)
        {
            AllPassengerEmbarkList.Add(p);
        }
        foreach (Passenger p in passengersEmbarkingType4)
        {
            AllPassengerEmbarkList.Add(p);
        }

        List<Passenger> AllPassengerWaitList = new List<Passenger>();
        foreach (Passenger p in passengersWaitingType1)
        {
            AllPassengerWaitList.Add(p);
        }
        foreach (Passenger p in passengersWaitingType2)
        {
            AllPassengerWaitList.Add(p);
        }
        foreach (Passenger p in passengersWaitingType3)
        {
            AllPassengerWaitList.Add(p);
        }
        foreach (Passenger p in passengersWaitingType4)
        {
            AllPassengerWaitList.Add(p);
        }

        //depois limpa as listas de embark
        passengersEmbarkingType1.Clear();
        passengersEmbarkingType2.Clear();
        passengersEmbarkingType3.Clear();
        passengersEmbarkingType4.Clear();

        //Disparar evento com a lista a ser adicionada
        Events.OnEmbarkButtonEvent.Invoke(AllPassengerEmbarkList, AllPassengerWaitList);
        //Enable controls
        inputManager.SwitchActionMapUIGaming("Gaming");
    }

}

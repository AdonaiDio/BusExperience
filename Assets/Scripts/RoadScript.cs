using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class RoadScript : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput; //PlayerInput do InputManager que cuida do InputSystem

    private InputAction pressAction;
    private InputAction positionAction;

    private Material roadMaterial;
    [SerializeField] private Transform movePoint;
    private bool isMoving = false;

    private Transform playerBus;

    [SerializeField] private int kilometers;
    [SerializeField] public Transform busStop;
    [SerializeField] private Transform[] connectedRoads;
    //[HideInInspector] public bool canEmbarkDisembark;

    [SerializeField] private KmPopupScript KM_Popup;

    private enum roadStates { none, selected };
    private roadStates currentRoadState;

    private void Awake()
    {
        //playerInput = FindObjectOfType<PlayerInput>();
        pressAction = playerInput.actions["Touch0Contact"];
        positionAction = playerInput.actions["Touch0Position"];
        roadMaterial = GetComponent<Renderer>().material;
        playerBus = FindObjectOfType<PlayerBusScript>().transform;
        KM_Popup = FindObjectOfType<KmPopupScript>();
    }

    private void Start()
    {
        isMoving = false;
        currentRoadState = roadStates.none;
        //canEmbarkDisembark = busStop; //se tiver parada de onibus, então ele pode fazer embarque e desembarque
        roadMaterial.SetColor("_EmissionColor", Color.black);
        /*movePoint.position = new Vector3(transform.position.x+0, 
                                         transform.position.y+1, 
                                         transform.position.z+0);*/

    }
    private void OnEnable()
    {
        pressAction.performed += ClickOnRoad;
        Events.atDestinationEvent.AddListener(AtDestination);
    }
    private void OnDisable()
    {
        pressAction.performed -= ClickOnRoad;
        Events.atDestinationEvent.RemoveListener(AtDestination);
    }
    private void HighlightRoad_ON()
    {
        roadMaterial.SetColor("_EmissionColor", Color.yellow);
    }
    private void HighlightRoad_OFF()
    {
        roadMaterial.SetColor("_EmissionColor", Color.black);
    }
    private void ShowKM_ON()
    {
        KM_Popup.gameObject.SetActive(true);
        KM_Popup.OnStartPopup(transform, kilometers);
    }
    private void ShowKM_OFF()
    {
        KM_Popup.OnEndPopup();
    }
    private void AtDestination(Transform currentRoad)
    {
        //para reduzir quantas ruas respondem o evento
        if (currentRoad == transform) {
            //checar se tem parada e executar tudo que for preciso antes de poder voltar a mudar de rua.
            if (currentRoad.GetComponent<RoadScript>().busStop) {
                Debug.Log("canEmbarkDisembark");
                //chamar o metodo de embarque e desembarque
                busStop.GetComponent<BusStopScript>().CheckForPassengers(playerBus);
            }
            //permitir que possa clicar nas ruas
            isMoving = false;
        }
    }

    private void ClickOnRoad(InputAction.CallbackContext context)
    {
        ///Se a rua não estiver selecionada e clicar nela, ela fica "selected".
        ///No estado "selected" ativa os feedbacks de seleção e pop-up com o kilometro.
        ///Se clicar em qualquer lugar, desativa o feedback desta rua e volta ao estado "none".
        ///Mas se o click for na rua no estado "selected" e a rua for adjacente, então além disso,
        ///vai ser o proximo destino do onibus.

        //esperar o bus chegar no destino anterior //OK
        if(!isMoving) {
            GameObject clickedObject;
            //checar que objeto eu cliquei //OK
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(positionAction.ReadValue<Vector2>().x,
                                                            positionAction.ReadValue<Vector2>().y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickedObject = hit.transform.gameObject;
                //Se o player já estiver na rua não pode ter qualquer feedback. //OK
                if (currentRoadState == roadStates.none
                    && this.gameObject == clickedObject) { //se for 'roadStates.none' e clicar nesta rua //OK
                    if ((playerBus.position - transform.position).magnitude >= 0.7f) { //se não estiver nesta rua
                        currentRoadState = roadStates.selected;
                        //feedback de seleção + pop-up //OK
                        HighlightRoad_ON();
                        ShowKM_ON();
                    }
                }
                else if (currentRoadState == roadStates.selected) {  //se for selected
                    currentRoadState = roadStates.none;
                    //feedback removido
                    HighlightRoad_OFF();
                    ShowKM_OFF();
                    PlayerBusScript _bus = playerBus.GetComponent<PlayerBusScript>();
                    RoadScript _busRoad = _bus.currentRoad.GetComponent<RoadScript>();
                    foreach (Transform road in _busRoad.connectedRoads)
                    {
                        Debug.Log("entrou no " + road.name);
                        //se clicou nesta rua e ela é uma das conectadas
                        if (road == clickedObject.transform
                            && this.gameObject == clickedObject){
                            movePoint.position = transform.position;
                            isMoving = true;
                            //contar o km andado
                            Debug.LogWarning("AAAAA km: "+_busRoad.kilometers);
                            Events.StartToMoveBusEvent.Invoke(_busRoad.kilometers);
                        }
                    }
                }
            }
        }
    }
}

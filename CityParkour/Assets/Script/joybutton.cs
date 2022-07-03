using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class joybutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler{

    public bool Pressed;
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        
    }

    [HideInInspector]
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

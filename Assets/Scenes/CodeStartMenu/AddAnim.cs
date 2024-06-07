using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddAnim : MonoBehaviour
{
    public Animator animator;
    private bool isMouseOver;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("onMouse", true);
    }
    public void OnPointExit(PointerEventData eventData)
    {
        animator.SetBool("OnMouse", false);
    }
}

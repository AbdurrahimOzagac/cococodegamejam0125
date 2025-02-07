using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    private float openSpeed = 2f;
    private float openedAngle = 90f;
    private bool isDoorOpen = false;
    private bool isDoorReadyToOpen = true; //þimdilik test için true kalsýn kodu en son halinde bütün canavarlar bitince yap bunu


    private Quaternion openedRotation;
    private Quaternion closedRotation;

    private Coroutine currentCoroutine;


    void Start()
    {
        closedRotation = transform.rotation;
        openedRotation = Quaternion.Euler(0, openedAngle, 0);
    }

    void Update()
    {
        InteractWithDoor();
    }

    private void InteractWithDoor()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDoorReadyToOpen)
        {
            HandleDoorCoroutines();
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isDoorOpen ? closedRotation : openedRotation;
        isDoorOpen = !isDoorOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private void HandleDoorCoroutines()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        StartCoroutine(ToggleDoor());
    }
}

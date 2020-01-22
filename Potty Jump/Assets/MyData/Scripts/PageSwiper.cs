﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 5;
    public int currentPage = 0;
    public float buttonSize;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
        foreach(Transform child in transform)
        {
            if (child.name.StartsWith("Panel"))
            {
                buttonSize =Mathf.Abs(child.GetComponent<RectTransform>().rect.x);
                break;
            }
        }

        /*if (GameManager.instance.startTransition)
        {
            GameManager.instance.startTransition = false;
            StartCoroutine()
        }*/
        currentPage = GameManager.instance.currentLevel+1;

        

        if (currentPage > 1 )
        {
            Vector3 newLocation = panelLocation + new Vector3(-Screen.width * (currentPage - 1), 0, 0);
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }

         /*if (GameManager.instance.goToNewSlide)
        {
            currentPage = GameManager.instance.currentLevel + 1;
            GameManager.instance.currentLevel = currentPage;
            Vector3 newLocation = panelLocation + new Vector3(-Screen.width * (currentPage ), 0, 0);
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
            GameManager.instance.goToNewSlide = false;
        }*/
    }



    public void OnDrag(PointerEventData data)
    {
        Debug.Log(buttonSize);
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data)
    {

        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
            //GameManager.instance.CurrentPanelLocation = newLocation;
            Debug.Log(newLocation);
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}

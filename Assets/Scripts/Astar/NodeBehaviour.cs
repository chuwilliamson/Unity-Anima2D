using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeBehaviour : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public int f, g, h;
    public ScriptableNode Node;
    public IGridBehaviour gridBehaviour;
    public float scaleFactor = 1.25f;
    private Vector3 scale;
    public bool Block;
    private void Start()
    {
        scale = transform.localScale;
        if(Block)
            Node.Walkable = false;
        if(!Node.Walkable)
            gridBehaviour.SetColor(Node, Color.red);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            eventData.selectedObject = gameObject;
            gridBehaviour.SetGoal(Node);
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            gridBehaviour.SetStart(Node);
        }
        eventData.Use();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Input.GetMouseButton(2))
        {
            Node.Walkable = true;
            gridBehaviour.SetColor(Node, Color.white);
            StartCoroutine("TweenScale");
        }
        if(Input.GetMouseButton(1))
        {
            Node.Walkable = false;
            gridBehaviour.SetColor(Node, Color.red);
            StartCoroutine("TweenScale");
        }

        if(Input.GetMouseButton(0))
        {
            gridBehaviour.SetGoal(Node);
        }
        eventData.Use();
    }
    public void Tween()
    {
        StopCoroutine("TweenScale");
        StartCoroutine("TweenScale");
    }
    private IEnumerator TweenScale()
    {
        float timer = 0;
        Vector3 oldScale = scale;

        while(timer < .5f)
        {
            transform.localScale = Vector3.Lerp(oldScale, oldScale * scaleFactor, timer / 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = oldScale;
        yield return null;
    }


}












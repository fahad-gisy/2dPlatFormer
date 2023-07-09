using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackGround : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed;
    [SerializeField] private Renderer backGroundRenderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        backGroundRenderer.material.mainTextureOffset += new Vector2(scrollingSpeed * Time.deltaTime,0);   
    }
}

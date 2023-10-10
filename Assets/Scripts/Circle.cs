using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.Timeline;

public class Circle : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.position = position;
    }

    public void DrawCircle(Vector2 thePosition, float theRadius, Color theColor) {
        transform.position = thePosition;
        transform.localScale = Vector3.one * theRadius;
        var circleRenderer = GetComponent<Renderer>();
        circleRenderer.material.SetColor("_Color", theColor);
    }
}

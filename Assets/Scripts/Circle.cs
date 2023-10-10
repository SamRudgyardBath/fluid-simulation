using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.Timeline;

public class Circle : MonoBehaviour
{
    public GameObject circlePrefab;
    public float gravity;
    public float particleSize;
    public int noOfParticles;
    public float particleSpacing;
    public Vector2 boundSize;
    [Range(0,1)]
    public float dampingFactor;
    GameObject circle;
    Vector2[] positions;
    Vector2[] velocities;

    // Start is called before the first frame update
    void Start() {
        circle = Instantiate(circlePrefab); 

        positions = new Vector2[noOfParticles];
        velocities = new Vector2[noOfParticles];

        int particlesPerRow = (int) Mathf.Sqrt(noOfParticles);
        int particlesPerColumn = (noOfParticles - 1) / particlesPerRow + 1;
        float spacing = particleSize * 2 + particleSpacing;

        for (int i = 0; i < noOfParticles; i++) {
            float x = (i % particlesPerRow - particlesPerRow/2f + 0.5f) * spacing;
            float y = (i / particlesPerRow - particlesPerColumn/2f + 0.5f) * spacing;
            positions[i] = new Vector2(x,y);
        }

        particleSize = 1f;
        boundSize = new Vector2(10, 10);
        dampingFactor = 1f;
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < positions.Length; i++) {
            velocities[i] += Vector2.down * gravity * Time.deltaTime;
            positions[i] += velocities[i] * Time.deltaTime;
            ResolveCollisions(ref positions[i], ref velocities[i]);
            DrawCircle(positions[i], particleSize, Color.blue);
        }
    }

    void DrawCircle(Vector2 thePosition, float theRadius, Color theColor) {
        circle.transform.position = thePosition;
        circle.transform.localScale = Vector3.one * theRadius;
        var circleRenderer = circle.GetComponent<Renderer>();
        circleRenderer.material.SetColor("_Color", theColor);
    }

    void ResolveCollisions(ref Vector2 thePosition, ref Vector2 theVelocity) {
        Vector2 halfBoundSize = boundSize / 2 - Vector2.one * particleSize;
        if (Mathf.Abs(thePosition.x) > halfBoundSize.x) {
            thePosition.x = halfBoundSize.x * Mathf.Sign(thePosition.x);
            theVelocity.x *= -1 * dampingFactor;
        }
        if (Mathf.Abs(thePosition.y) > halfBoundSize.y) {
            thePosition.y = halfBoundSize.y * Mathf.Sign(thePosition.y);
            theVelocity.y *= -1 * dampingFactor;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.Timeline;

public class Simulation : MonoBehaviour
{
    #region Public Properties
    public float gravity;
    public float particleSize = 0.1f;
    public int noOfParticles = 100;
    public float particleSpacing;
    public Vector2 boundSize = new Vector2(10f, 10f);
    [Range(0,1)]
    public float dampingFactor;
    public GameObject particlePrefab;
    #endregion

    #region Private Properties
    Particle[] particles;
    #endregion

    // Start is called before the first frame update
    void Start() {
        particles = new Particle[noOfParticles];

        int particlesPerRow = (int) Mathf.Sqrt(noOfParticles);
        int particlesPerColumn = (noOfParticles - 1) / particlesPerRow + 1;
        float spacing = particleSize * 2 + particleSpacing;

        for (int i = 0; i < noOfParticles; i++) {
            GameObject particleGO = Instantiate(particlePrefab);
            Particle currentParticle = particleGO.GetComponent<Particle>();
            // Arrange particles
            float x = (i % particlesPerRow - particlesPerRow/2f + 0.5f) * spacing;
            float y = (i / particlesPerRow - particlesPerColumn/2f + 0.5f) * spacing;
            particles[i] = currentParticle;
            currentParticle.position = new Vector2(x,y);

            // Set particle size
            currentParticle.transform.localScale = Vector3.one * particleSize;
        }

        boundSize = new Vector2(10, 10);
        dampingFactor = 1f;
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < particles.Length; i++) {
            Particle currentParticle = particles[i];
            currentParticle.velocity += Vector2.down * gravity * Time.deltaTime;
            currentParticle.position += currentParticle.velocity * Time.deltaTime;
            ResolveCollisions(ref particles[i].position, ref particles[i].velocity);
            currentParticle.DrawCircle(particles[i].position, particleSize, Color.blue);
        }
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

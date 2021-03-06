﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    // Singleton
    private static Earth _instance;
    public static Earth Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Earth>();
            }

            return _instance;
        }

    }

    public Sprite[] EarthSprites;
    private SpriteRenderer sr;
    private LineRenderer lineRenderer;
    private float fullRoationTime = 720f;
    private int state = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RotateEarth();
    }

    private void RotateEarth()
    {
        transform.Rotate(0f, 0f, Time.deltaTime * 360f / fullRoationTime);
    }

    public static void ShootLaser(Vector3 position, float time)
    {
        position.z = 1f;
        Instance.StartCoroutine(Instance.IEshootLaser(position, time));
    }

    private IEnumerator IEshootLaser(Vector2 position, float time)
    {
        Instance.lineRenderer.gameObject.SetActive(true);
        Instance.lineRenderer.SetPosition(1, position);
        yield return new WaitForSeconds(time);
        Instance.lineRenderer.gameObject.SetActive(false);
    }

    public static int GetState()
    {
        return Instance.state;
    }

    public static void UpdateSprite()
    {
        int health = Health.GetHealth();
        int spritesCount = Instance.EarthSprites.Length;
        float div = 100f / (spritesCount - 1);

        bool isAlive = false;
        for (int i = 0; i < spritesCount - 1; i++)
        {
            if (health > div * (spritesCount - i - 2))
            {
                Instance.sr.sprite = Instance.EarthSprites[i];
                isAlive = true;
                Instance.state = i;
                break;
            }
        }

        if (!isAlive)
            Instance.sr.sprite = Instance.EarthSprites[spritesCount - 1];
    }
}

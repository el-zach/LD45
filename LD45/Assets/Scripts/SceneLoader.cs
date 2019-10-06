﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(gameObject);
    }

    GameObject[] savedObjects;
    Vector3 exitPos;
    Vector3 entryPos;

    private void Start()
    {
        SceneLoaded();
    }

    void SceneLoaded()
    {
        //FindObjectOfType<Exit>();
        entryPos = FindObjectOfType<Entry>().transform.position;
    }

    public void TransportGameObjects(GameObject[] toSave, Vector3 exitPosition)
    {
        savedObjects = toSave;
        exitPos = exitPosition;
        foreach(var go in toSave)
        {
            go.transform.SetParent(transform, true);
        }
    }

    public void UnpackGameObjects(Vector3 spawnLocation)
    {
        foreach(var go in savedObjects)
        {
            go.transform.SetParent(null, true);
            go.transform.position = go.transform.position + spawnLocation - exitPos;
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        SceneLoaded();
        UnpackGameObjects(entryPos);
    }



}

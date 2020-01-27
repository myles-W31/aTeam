﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelLoader : MonoBehaviour
{

    List<string> chunkList;
    List<string> loadedChunkList;
    string baseAdd;
    int cChunknum;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //reads file and loads chunk according to file contents
    void LoadChunk(int i)
    {
        string loadingChunk = "";
        if (loadedChunkList[i] == "")
        {
            string path = baseAdd + chunkList[i];
            //Load file to get chunk info
            
            using (StreamReader sr = File.OpenText(path))
            {
                string temp;
                while ((temp = sr.ReadLine()) != null)
                {
                    loadingChunk += temp;
                }
            }

            loadedChunkList[i] = loadingChunk;
            //loadingChunk will have the text data of the chunk
        }
        else
        {
            loadingChunk = loadedChunkList[i];
        }



    }

    void UnloadChunk()
    {

    }


}

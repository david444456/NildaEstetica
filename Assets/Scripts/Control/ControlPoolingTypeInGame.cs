using System;
using System.Collections;
using System.Collections.Generic;
using Est.AI;
using UnityEngine;

public class ControlPoolingTypeInGame : MonoBehaviour
{
    public static ControlPoolingTypeInGame controlPoolingType;

    [SerializeField] ObjectPooling[] objectPoolings;
    [SerializeField] ControlDirectionSpawn[] controlDirectionSpawns;

    private void Start()
    {
        if (controlPoolingType == null)
            controlPoolingType = this;
        else {
            Destroy(controlPoolingType);
            controlPoolingType = this;
        }
    }

    public ObjectPooling GetTypePooling(TypeObjectPooling typeObjPool)
    {
        SearchNewObjectsPooling();
        for (int i = 0; i < objectPoolings.Length; i++)
        {
            if (objectPoolings[i].typeObjectPooling == typeObjPool)
            {
                return objectPoolings[i];
            }
        }
        return null;
    }


    public ControlDirectionSpawn GetTypeControlDirectionSpawn(TypeObjectPooling typeObjControlDir)
    {
        SearchNewControlDirectionSpawn();

        for (int i = 0; i < controlDirectionSpawns.Length; i++)
        {
            if (controlDirectionSpawns[i].typeObjectPooling == typeObjControlDir)
            {
                return controlDirectionSpawns[i];
            }
        }
        return null;
    }

    private void SearchNewObjectsPooling()
    {
        if (objectPoolings[0] == null)
        {
            var objLoop = FindObjectsOfType<ObjectPooling>();
            for (int i = 0; i < objLoop.Length; i++)
            {
                objectPoolings[i] = objLoop[i];
            }
        }
    }


    private void SearchNewControlDirectionSpawn()
    {
        if (controlDirectionSpawns[0] == null)
        {
            var objCtrolDirSpawn = FindObjectsOfType<ControlDirectionSpawn>();
            for (int i = 0; i < objCtrolDirSpawn.Length; i++)
            {
                controlDirectionSpawns[i] = objCtrolDirSpawn[i];
            }
        }
    }

}

﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{

    Block[,,] blocks;
    public static int chunkSize = 16;
    public bool update = true;

    MeshFilter filter;
    MeshCollider coll;

    // Use this for initialization
    void Start()
    {

        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();

        //past here is just to set up an example chunk
        blocks = new Block[chunkSize, chunkSize, chunkSize];

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    blocks[x, y, z] = new BlockAir();
                }
            }
        }

        blocks[3, 5, 2] = new Block();

        UpdateChunk();
    }

    //Update is called once per frame
    void Update()
    {

    }

    public Block GetBlock(int x, int y, int z)
    {
        return blocks[x, y, z];
    }

    // Updates the chunk based on its contents
    void UpdateChunk()
    {
        MeshData meshData = new MeshData();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }

        RenderMesh(meshData);
    }

    // Sends the calculated mesh information
    // to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
    }

}
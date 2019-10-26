using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour
{
    public GameObject block;
    public Vector2 Size;
    public List<GameObject> blocks;
    int blockid = 0;


    public void CallDestroyBlock(int id)
    {
        DestroyBlock(id);
        photonView.RPC("DestroyBlock", PhotonTargets.OthersBuffered, id);
    }

    [PunRPC]
    public void DestroyBlock(int id)
    {
        int i = 0;
        foreach (GameObject block in blocks)
        {
                if (block.GetComponent<blockscript>().blockid == id)
                {

                block.SetActive(false);
                block.GetComponent<Renderer>().material.color = Color.black;

                }
            i++;
        }

        if (photonView.isMine)
        {

        }

    }


    void SpawnChunks(int sizex, int sizey)
    {
        GameObject allchunks = new GameObject();
        allchunks.name = "AllChunks";
        int i = 0;
        int j = 0;

        for (i = 0; i < sizex; i++)
        {
            for (j = 0; j < sizey; j++)
            {
                SpawnChunk(new Vector3(i * 10, 0, j * 10), allchunks);
            }

        }
    }

    void SpawnChunk(Vector3 chunkoffset, GameObject allchunks)
    {
        int i = 0;
        int j = 0;

        GameObject c = new GameObject();
        c.name = "Chunk(" + chunkoffset.x + "," + chunkoffset.y + "," + chunkoffset.z + ")";
        c.transform.SetParent(allchunks.transform);


        for(i = 0; i < 10; i++)
        {
            for(j = 0; j < 10; j++)
            {


                Vector3 blockpos = new Vector3();
                blockpos.x = i + chunkoffset.x;
                blockpos.z = j + chunkoffset.z;

                int scale = 6;

                  blockpos.y = 0 + chunkoffset.y + (int)(scale * (Mathf.PerlinNoise((blockpos.x / 10), (blockpos.z / 10))));




                GameObject b = Instantiate(block, blockpos, Quaternion.identity, c.transform);
                b.name = "block(" + blockpos.x + ", " + blockpos.y + ", " + blockpos.z + ")" +  " - " + c.name;
                b.GetComponent<blockscript>().blockid = blockid;
                blocks.Add(b);
                blockid++;
            }

        }


    }

    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<GameObject>();


        SpawnChunks((int)Size.x, (int)Size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    [Header("SFX Click")]
    public GameObject[] clickSFX;

    

    public void Create(Vector3 position)
    {
        // Click Effect
        GameObject clickInst = Instantiate(gameObject, position, Quaternion.identity, gm.effectCanvas.transform);
        Object.Destroy(clickInst, 1.0f);

        // Click SFX
        int ran = Random.Range(0, clickSFX.Length);
        if (clickSFX.Length > 0)
        {
            GameObject sfxInst = Instantiate(clickSFX[ran], position, Quaternion.identity, gm.effectCanvas.transform);
            sfxInst.GetComponent<AudioSource>().PlayDelayed(0);
            Object.Destroy(sfxInst, 1.0f);
        }
    }
}

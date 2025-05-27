using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallonController : MonoBehaviour
{
    // Start is called before the first frame update
    public BallonData ballonData;
    public float speed;
    MeshRenderer meshRenderer;
    private Rigidbody rb;
    [SerializeField]
    GameObject popEffect;
    [SerializeField]
    GameObject popIndicator;

    void Start()
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.up * speed;
    }


    void FixedUpdate()
    {
        rb.velocity = Vector3.up * speed;
    }

    public void SetBallonData(BallonData ballonData)
    {
        this.ballonData = ballonData;
        MeshRenderer meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        meshRenderer.material.color = ballonData.color;
        meshRenderer.enabled = true;
    }

    public void Pop(bool Finish = false)
    {
        if (Finish)
        {
            if (ballonData.deltaScore > 0)
            {
                GameManager.instance.SetScore(-ballonData.deltaScore);
                PopIndicator(false);
            }
        }
        else
        {
            ballonData.hit++;
            GameManager.instance.SetScore(ballonData.deltaScore);
            PopIndicator(true);
        }

        SoundManager.instance.PlayPop();
        GameObject popObj = Instantiate(popEffect, transform.position, Quaternion.identity);
        popObj.GetComponent<ParticleSystem>().GetComponent<Renderer>().material.color = ballonData.color;
        Destroy(popObj, 5);
        Destroy(gameObject);

    }
    public static void DestroyAllBallons()
    {
        GameObject[] ballons = GameObject.FindGameObjectsWithTag("Ballon");
        foreach (var ballon in ballons)
        {
            Destroy(ballon);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Pop(true);
        }
    }

    private void PopIndicator(bool correct)
    {
        Debug.Log("PopIndicator");
        GameObject indicator = Instantiate(popIndicator, transform.position, Quaternion.identity);
        TextMeshProUGUI text = indicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = ballonData.deltaScore.ToString();
        if (correct && ballonData.deltaScore > 0)
        {
            text.color = Color.green;
            if (ballonData.deltaScore > 0)
            {
                text.text = "+" + text.text;
            }
        }
        else
        {
            text.color = Color.red;
            if (ballonData.deltaScore > 0)
            {
                text.text = "-" + text.text;
            }
        }
        Destroy(indicator, 1);


    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashZone : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI eboueurTeamScore;
    [SerializeField] TextMeshProUGUI raccoonTeamScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == gameObject.tag && collision.gameObject.GetComponent<Trash>())
        {
            Debug.Log("Trash get");
            collision.gameObject.SetActive(false);
        }
        else
        {

        }
    }

    void UpdateTeamScore(TextMeshProUGUI teamScore)
    {
        teamScore.text += 1;
    }
    void UpdatePlayerScore(TextMeshProUGUI playerScore, int amount)
    {
        playerScore.text += amount;
    }
}

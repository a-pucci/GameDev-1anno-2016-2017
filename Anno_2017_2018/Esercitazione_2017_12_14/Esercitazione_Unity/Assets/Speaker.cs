using System.Collections;
using UnityEngine;

public class Speaker : MonoBehaviour 
{
    public TextMesh dialogueText;
    public Transform player;
    public Transform ciccio;
    public Transform carmelo;
    public Transform salvatore;
    public float textWait;
    public Color defaultTextColor = Color.white;

    private Vector3 textOffset = new Vector3(0, 4, 0);

	// Use this for initialization
	void Start () 
	{
        dialogueText.gameObject.SetActive(false);
	}

    public void SetText(string text)
    {
        dialogueText.color = defaultTextColor;
        dialogueText.gameObject.SetActive(true);

        dialogueText.text = "PLAYER: " + text;
        dialogueText.gameObject.transform.position = player.position + textOffset;

        StartCoroutine(WaitForText(text));
    }

    public void SetText(string speaker, string text)
    {
        dialogueText.color = defaultTextColor;
        dialogueText.gameObject.SetActive(true);

        if (speaker == "Ciccio")
        {
            dialogueText.gameObject.transform.position = ciccio.position + textOffset;
        }
        else if (speaker == "Carmelo")
        {
            dialogueText.gameObject.transform.position = carmelo.position + textOffset;
        }
        else if (speaker == "Salvatore")
        {
            dialogueText.gameObject.transform.position = salvatore.position + textOffset;
        }

        dialogueText.text = speaker.ToUpper() + ": " + text;

        StartCoroutine(WaitForText(text));
    }

    public void SetText(Color color, string speaker, string text)
    {
        dialogueText.gameObject.SetActive(true);

        if(speaker == "Player")
        {
            dialogueText.gameObject.transform.position = player.position + textOffset;
        }
        else if (speaker == "Ciccio")
        {
            dialogueText.gameObject.transform.position = ciccio.position + textOffset;
        }
        else if (speaker == "Carmelo")
        {
            dialogueText.gameObject.transform.position = carmelo.position + textOffset;
        }
        else if (speaker == "Salvatore")
        {
            dialogueText.gameObject.transform.position = salvatore.position + textOffset;
        }

        dialogueText.color = color;
        dialogueText.text = speaker.ToUpper() + ": " + text;

        StartCoroutine(WaitForText(text));
    }

    IEnumerator Wait(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dialogueText.gameObject.SetActive(false);
    }

    IEnumerator WaitForText(string text)
    {
        float waitTime = text.Length * textWait;
        yield return new WaitForSeconds(waitTime);
        dialogueText.gameObject.SetActive(false);
    }
}

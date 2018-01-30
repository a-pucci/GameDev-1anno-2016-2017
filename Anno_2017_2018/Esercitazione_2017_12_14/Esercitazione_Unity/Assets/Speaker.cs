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
    private int currentPoseIndex = 0;
    private int currentExpressionIndex = 0;
    private int spriteIndex;
    private Sprite speakerSprite;
    private Sprite[] sprites;

    // Use this for initialization
    void Start () 
	{
        sprites = Resources.LoadAll<Sprite>("sprite");
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

    public void SetExpression(string speaker, string expression)
    {
        switch (expression)
        {
            case "normal":
                currentExpressionIndex = 0;
                break;
            case "happy":
                currentExpressionIndex = 1;
                break;
            case "sad":
                currentExpressionIndex = 2;
                break;
            case "angry":
                currentExpressionIndex = 3;
                break;
            case "suffering":
                currentExpressionIndex = 4;
                break;
            case "terrified":
                currentExpressionIndex = 5;
                break;
            case "sarcastic":
                currentExpressionIndex = 6;
                break;
            case "perplexed":
                currentExpressionIndex = 7;
                break;
            case "scared":
                currentExpressionIndex = 8;
                break;
        }
        Debug.Log("Set Expression" + " | Speaker: " + speaker + " | Expression: " + expression + " | Index: " + currentExpressionIndex);

        spriteIndex = calculateSpriteIndex();
        UpdateSprite();

        switch (speaker)
        {
            case "Player":
                player.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
            case "Ciccio":
                ciccio.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
            case "Carmelo":
                carmelo.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
            case "Salvatore":
                salvatore.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
        }
    }

    public void SetPose (string speaker, string pose)
    {
        switch (pose)
        {
            case "u":
                currentPoseIndex = 0;
                break;
            case "ur":
                currentPoseIndex = 1;
                break;
            case "r":
                currentPoseIndex = 2;
                break;
            case "dr":
                currentPoseIndex = 3;
                break;
            case "d":
                currentPoseIndex = 4;
                break;
            case "dl":
                currentPoseIndex = 5;
                break;
            case "l":
                currentPoseIndex = 6;
                break;
            case "ul":
                currentPoseIndex = 7;
                break;
        }

        Debug.Log("Set Pose" + " | Speaker: " + speaker + " | Pose: " + pose + " | Index: " + currentPoseIndex);
        spriteIndex = calculateSpriteIndex();
        UpdateSprite();

        switch (speaker)
        {
            case "Player":
                player.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
            case "Ciccio":
                ciccio.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
            case "Carmelo":
                carmelo.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
            case "Salvatore":
                salvatore.gameObject.GetComponent<SpriteRenderer>().sprite = speakerSprite;
                break;
        }
    }

    private void UpdateSprite()
    {

        speakerSprite = sprites[spriteIndex];
        Debug.Log("Speaker Sprite: " + speakerSprite);
        currentExpressionIndex = 0;
        currentPoseIndex = 0;
    }

    private int calculateSpriteIndex()
    {

        spriteIndex = (currentExpressionIndex) + (currentPoseIndex * 9);
        Debug.Log("Speaker Sprite Index: " + spriteIndex);
        return spriteIndex;
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

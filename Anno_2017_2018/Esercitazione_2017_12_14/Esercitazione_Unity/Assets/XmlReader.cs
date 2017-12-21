
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlReader : MonoBehaviour
{
    public Speaker speakerScript;

    List<string> hotspotNames = new List<string>();
    XmlNode hotspots;
    XmlDocument xDoc;

    bool isFirstCycle = true;
    float waitTime = 0.0f;
    float textWait;

    void Start()
    {
        textWait = speakerScript.textWait;
        xDoc = new XmlDocument();
        xDoc.Load("Hotspots.xml");
        hotspots = xDoc.SelectSingleNode("root/hotspots");

        foreach (XmlNode childNode in hotspots.ChildNodes)
        {
            hotspotNames.Add(childNode.Attributes["name"].Value);
        }
    }

    void Update()
    {
        if (isFirstCycle)
        {
            StartCoroutine(CommandCoroutine());
            isFirstCycle = false;
        }
    }

    IEnumerator CommandCoroutine()
    {
        foreach (XmlNode childNode in hotspots.ChildNodes)
        {
            if (childNode.SelectSingleNode("look") != null)
            {
                XmlNode nodeLook = childNode.SelectSingleNode("look");
                foreach (XmlNode eventNode in nodeLook.ChildNodes)
                {
                    ExecuteCommand(eventNode.InnerText, hotspotNames);
                    Debug.Log("waitTime: " + waitTime);
                    yield return new WaitForSeconds(waitTime);
                }
            }
        }
    }

    void ExecuteCommand(string textToSplit, List<string> hotspotNames)
    {
        string[] splittedText = textToSplit.Split(' ');
        string command = splittedText[0];

        if (command == "Wait")
        {
            int waitTimeOut;
            if (int.TryParse(splittedText[1], out waitTimeOut))
            {
                waitTime = waitTimeOut;
                waitTime = int.Parse(splittedText[1]);
            }
            Debug.Log("Waiting for response...");
        }
        else if (command == "Text")
        {
            string firstArgument = splittedText[1];
            string secondArgument = splittedText[2];
            string[] arguments = textToSplit.Split('"');

            if (firstArgument[0] == '-' && firstArgument[1] == 'c')
            {
                string color = firstArgument.Split('=')[1];
                Color textColor = StringToColor(color);
                string speaker = arguments[1];

                if (firstArgument[0] == '-' && secondArgument[0] != '-')
                {
                    speaker = "Player";
                }

                if (SpeakerExist(speaker, hotspotNames))
                {
                    string declaration = arguments[arguments.Length - 2];
                    speakerScript.SetText(textColor, speaker, declaration);
                    waitTime = declaration.Length * textWait;
                }
                else
                {
                    Debug.Log("ERRORE: manca la destinazione " + speaker);
                }

            }
            else if (firstArgument[0] == '-' && firstArgument[1] != 'c')
            {
                string speaker = arguments[1];

                if (SpeakerExist(speaker, hotspotNames))
                {
                    string declaration = arguments[arguments.Length - 2];
                    speakerScript.SetText(speaker, declaration);
                    waitTime = declaration.Length * textWait;
                }
                else
                {
                    Debug.Log("ERRORE: manca la destinazione " + speaker);
                }
            }
            else if (firstArgument[0] != '-')
            {
                string declaration = arguments[arguments.Length - 2];
                speakerScript.SetText(declaration);
                waitTime = declaration.Length * textWait;
            }
        }
    }

    bool SpeakerExist(string speaker, List<string> hotspotNames)
    {
        bool speakerExist = false;
        for (int i = 0; i < hotspotNames.Count; i++)
        {
            if (speaker == hotspotNames[i])
            {
                speakerExist = true;
            }
        }
        return speakerExist;
    }

    Color StringToColor(string color)
    {
        Color newColor = new Color();

        switch (color)
        {
            case "cyan":
                newColor = Color.cyan;
                break;
            case "green":
                newColor = Color.green;
                break;
            case "red":
                newColor = Color.red;
                break;
            case "black":
                newColor = Color.black;
                break;
            case "yellow":
                newColor = Color.yellow;
                break;
            case "blue":
                newColor = Color.blue;
                break;
            case "magenta":
                newColor = Color.magenta;
                break;
            case "white":
                newColor = Color.white;
                break;
            case "grey":
                newColor = Color.grey;
                break;
        }
        return newColor;
    }
}



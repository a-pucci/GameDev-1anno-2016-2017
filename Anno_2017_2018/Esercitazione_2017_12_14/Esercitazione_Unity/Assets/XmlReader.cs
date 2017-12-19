
using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class XmlReader : MonoBehaviour
{
    void Start()
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Hotspots.xml");

        XmlNode hotspots = xDoc.SelectSingleNode("root/hotspots");

        List<string> hotspotNames = new List<string>();
        foreach (XmlNode childNode in hotspots.ChildNodes)
        {
            hotspotNames.Add(childNode.Attributes["name"].Value);
        }

        foreach (XmlNode childNode in hotspots.ChildNodes)
        {
            if (childNode.SelectSingleNode("look") != null)
            {
                XmlNode nodeLook = childNode.SelectSingleNode("look");
                foreach (XmlNode eventNode in nodeLook.ChildNodes)
                {
                    ExecuteCommand(eventNode.InnerText, hotspotNames);
                }
            }
        }
        Console.ReadLine();
    }

    static void ExecuteCommand(string textToSplit, List<string> hotspotNames)
    {
        string[] splittedText = textToSplit.Split(' ');
        string command = splittedText[0];

        if (command == "Wait")
        {
            int waitTime = 0;
            if (int.TryParse(splittedText[1], out waitTime))
            {
                waitTime = int.Parse(splittedText[1]) * 1000;
            }
            Debug.Log("Waiting for response...");
            System.Threading.Thread.Sleep(waitTime);
        }
        if (command == "Text")
        {
            string firstArgument = splittedText[1];
            if (firstArgument[0] == '-')
            {
                string[] arguments = textToSplit.Split('"');
                string speaker = arguments[1];

                bool SpeakerExist = false;
                for (int i = 0; i < hotspotNames.Count; i++)
                {
                    if (speaker == hotspotNames[i])
                    {
                        SpeakerExist = true;
                    }
                }

                if (SpeakerExist)
                {
                    Debug.Log(speaker.ToUpper() + ": " + arguments[arguments.Length - 2]);
                }
                else
                {
                    Debug.Log("ERRORE: manca la destinazione -" + speaker);
                }
            }
            else
            {
                string[] arguments = textToSplit.Split('"');
                string declaration = arguments[arguments.Length - 2];

                Debug.Log("PLAYER: " + declaration);
            }
        }        
    }   

    IEnumerator Wait(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}

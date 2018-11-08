using System.Collections.Generic;
using UnityEditor;
using System.Xml;
using System.IO;
using System.Linq;

public static class XMLReader
{
    [MenuItem("Create/Audio List")]
    public static void CreateFIleTxt()
    {
        string[] xmlFiles = Directory.GetFiles("Assets/XML/eng", "*.xml");

        using (StreamWriter streamWriter = new StreamWriter("Assets/AudioList.txt"))
        {

        }

        for (int i = 0; i < xmlFiles.Length; i++)
        {
            List<string> audioNames = new List<string>();

            string locationName = Path.GetFileNameWithoutExtension(xmlFiles[i]);

            XmlDocument xData = new XmlDocument();
            xData.Load(xmlFiles[i]);

            XmlNodeList sceneNodes = xData.SelectNodes("location/scene");

            foreach (XmlNode sceneNode in sceneNodes)
            {
                if(sceneNode.Attributes["sound"] != null)
                {
                    audioNames.Add(sceneNode.Attributes["sound"].Value);
                }
            }

            XmlNodeList commandNodes = xData.SelectNodes("//command");
            foreach (XmlNode commandNode in commandNodes)
            {
                string[] commandTokens = commandNode.InnerText.Split(' ');
                if (commandTokens.Length > 1 && commandTokens[1] == "sound")
                {
                    string[] audioTokens = commandNode.InnerText.Split('"');
                    audioNames.Add(audioTokens[audioTokens.Length-2]);
                }
            }

            audioNames.Sort();
            audioNames = audioNames.Distinct().ToList();

            using (StreamWriter streamWriter = new StreamWriter("Assets/AudioList.txt", true))
            {
                streamWriter.WriteLine("### " + locationName.ToUpper() + " ###\r\n");

                for (int j = 0; j < audioNames.Count; j++)
                {
                    streamWriter.WriteLine(audioNames[j]);
                }
                streamWriter.WriteLine("\r\n");
            }
        }
    }
}

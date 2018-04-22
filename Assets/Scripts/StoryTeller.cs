using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryTeller : MonoBehaviour
{
    public int capacity = 3;
    public Text text;

    List<string> lines;

    void Start()
    {
        lines = new List<string>();
    }

    public void Tell(string sthg)
    {
        Comment("\"" + sthg + "\"");
    }

    public void Comment(string sthg)
    {
        lines.Add(sthg);
        int startIndex = Math.Max(0, lines.Count - capacity);
        int count = Math.Min(lines.Count, capacity);
        List<string> lastLines = lines.GetRange(startIndex, count);
        string trimmed = "";
        foreach(string line in lastLines)
            trimmed += line+"\n";
        text.text = trimmed;
    }

    public void Comment(List<string> sthg, float delaySec=1)
    {
        StartCoroutine(MultiComment(sthg, delaySec));
    }

    IEnumerator MultiComment(List<string> speeches, float delaySec = 1)
    {
        foreach (string speech in speeches)
        {
            Game.teller.Comment(speech);
            yield return new WaitForSeconds(delaySec);
        }
    }
}

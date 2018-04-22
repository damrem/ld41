using System;
using UnityEngine;
using UnityEngine.UI;
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
        int startIndex = Math.Max(0, lines.Count - capacity - 1);
        int count = Math.Min(lines.Count, capacity);
        text.text = lines.GetRange(startIndex, count).ToArray().Join("\n");
    }
}

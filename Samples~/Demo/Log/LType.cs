using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congroo.Core
{
    public enum LType
    {
        Normal,
        HotUpdate,
        Congroo,
        CongrooEditor,
        Diagnost,
        Temp,
        UI,

        WebSocket,

    }

    public class LogCustomData
    {
        public bool IsFilter = false;
        public LType Type;
        public string Tag = "Temp";
        public Color Color = Color.white;

        public LogCustomData(bool bIsFilter, LType rType, string rTag, Color rColor)
        {
            IsFilter = bIsFilter;
            Type = rType;
            Tag = rTag;
            Color = rColor;
        }

        public override string ToString()
        {
#if UNITY_EDITOR
            return $"<color=#{ColorUtility.ToHtmlStringRGB(Color)}>[{Tag}]</color>";
#else
            return $"[{Tag}]";
#endif
        }
    }
}


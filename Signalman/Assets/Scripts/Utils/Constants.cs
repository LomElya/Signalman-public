
using UnityEngine;

public sealed class Constants
{
    public sealed class Scenes
    {
        public const string MAIN_MENU = "MainMenuScene";
        public const string GAMESCENE = "GameScene";
        public const string TESTGAMESCEHE = "TestScene";
    }

    public sealed class Buttons
    {
        public const string JUMP = "Jump";
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        public const string INTERACT = "Interact";
        public const string PAUSE = "Cancel";
    }

    public sealed class MainColor
    {
        public static Color NormalColor = new Color(1f, 1f, 1f, 1f);
        public static Color HighlightColor = new Color(0.6156863f, 0.6156863f, 0.6156863f, 1f);
        public static Color PressedColor = new Color(0.1921569f, 0.1921569f, 0.1921569f, 1f);
        public static Color Orange = new Color(1f, 0.5215686f, 0.06274508f, 1f);
        public static Color Red = new Color(1f, 0.08235291f, 0.06274508f, 1f);
        public static Color Green = new Color(0f, 1f, 0.409091f, 1f);
    }
}

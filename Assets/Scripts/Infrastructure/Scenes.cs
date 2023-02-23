using System.ComponentModel;

namespace Infrastructure
{
    public enum Scenes
    {
        [Description("Initial")] Initial = 0,
        [Description("Main")] Main = 1,
        [Description("Level_1")] Level_1,
        [Description("Level_2")] Level_2,
        [Description("Level_3")] Level_3,
    }
}
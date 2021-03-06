namespace Dashboard.Dashboards.Dashboard_Game.Elements.Icons
{
    public class Icons
    {
        public string this[IconType a]
        {
            get
            {
                switch (a)
                {
                    case IconType.Home:
                        return "\xE80F";
                    case IconType.Players:
                        return "\xEBDA";
                    case IconType.Leaderboards:
                        return "\xECA7";
                    case IconType.Achievements:
                        return "\xEB95";
                    case IconType.Store:
                        return "\xE719";
                    case IconType.Content:
                        return "\xF571";
                    case IconType.KeyValue:
                        return "\xF003";
                    case IconType.Setting:
                        return "\xE713";
                    case IconType.Add:
                        return "\xE710";
                    case IconType.Download:
                        return "\xE896";
                    case IconType.Upload:
                        return "\xE898";
                    case IconType.Export:
                        return "\xEDE1";
                    case IconType.Import:
                        return "\xE8B5";
                    case IconType.Left:
                        return "\xE96F";
                    case IconType.Right:
                        return "\xE970";
                    case IconType.GridView:
                        return "\xF0E2";
                    case IconType.ListView:
                        return "\xF168";

                    default:
                        {
                            return "\xE82F";
                        }
                }

            }
        }


    }
    public enum IconType
    {
        Home,
        Players,
        Leaderboards,
        Achievements,
        Store,
        Content,
        KeyValue,
        Setting,
        Add,
        Download,
        Upload,
        Export,
        Import,
        Left,
        Right,
        GridView,
        ListView

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        {
                            return "\xE80F";
                        }
                    case IconType.Players:
                        {
                            return "\xEBDA";
                        }
                    case IconType.Leaderboards:
                        {
                            return "\xECA7";
                        }
                    case IconType.Achievements:
                        {
                            return "\xEB95";
                        }
                    case IconType.Store:
                        {
                            return "\xE719";
                        }
                    case IconType.Content:
                        {
                            return "\xF571";
                        }
                    case IconType.KeyValue:
                        {
                            return "\xF003";
                        }
                    case IconType.Setting:
                        {
                            return "\xE713";
                        }
                       
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
        Setting

    }
}

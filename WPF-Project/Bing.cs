using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project
{
    public class Bing
    {
        public List<Images> images { get; set; }
        public Tooltips tooltips { get; set; }
    }

    public class Images
    {
        public string startdate { get; set; }
        public string fullstartdate { get; set; }
        public string enddate { get; set; }
        public string url { get; set; }
        public string urlbase { get; set; }
        public string copyright { get; set; }
        public string copyrightlink { get; set; }
        public bool wp { get; set; }
        public string hsh { get; set; }
        public int drk { get; set; }
        public int top { get; set; }
        public int bot { get; set; }
        public List<object> hs { get; set; }
        public List<object> msg { get; set; }
    }

    public class Tooltips
    {
        public string loading { get; set; }
        public string previous { get; set; }
        public string next { get; set; }
        public string walle { get; set; }
        public string walls { get; set; }
    }

    
}


//{
//"images": [
//            {
//            "startdate": "20160531",
//            "fullstartdate": "201605310700",
//            "enddate": "20160601",
//            "url": "/az/hprichbg/rb/ToSuaOceanTrench_EN-US12205365533_1920x1080.jpg",
//            "urlbase": "/az/hprichbg/rb/ToSuaOceanTrench_EN-US12205365533",
//            "copyright": "To Sua Ocean Trench in Lotofaga, Upolu, Samoa (© Danita Delimont/Offset)",
//            "copyrightlink": "http://www.bing.com/search?q=Lotofaga&form=hpcapt&filters=HpDate:%2220160531_0700%22",
//            "wp": true,
//            "hsh": "20de2783c3fd6099c94e459d6671794b",
//            "drk": 1,
//            "top": 1,
//            "bot": 1,
//            "hs": [],
//            "msg": []
//            }
//        ],
//"tooltips": {
//            "loading": "Trwa ładowanie...",
//            "previous": "Poprzedni obraz",
//            "next": "Następny obraz",
//            "walle": "Tego obrazu nie można pobrać jako tapety.",
//            "walls": "Pobierz ten obraz. Ten obraz może być używany tylko jako tapeta."
//            }
//}
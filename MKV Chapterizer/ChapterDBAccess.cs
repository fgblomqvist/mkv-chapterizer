using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

namespace MKV_Chapterizer
{
    public class ChapterDBAccess
    {
        private string dbUrl = "http://chapterdb.org";

        public class Chapter
        {
            public string Name;
            public TimeSpan Time;
        }

        public class ChapterSet : IEnumerable, IEnumerator
        {
            public ChapterSet()
            {
                Chapters = new List<Chapter>();
            }

            public string Name;
            public List<Chapter> Chapters;
            private int position = -1;

            public IEnumerator GetEnumerator()
            {
                return (IEnumerator)this;
            }

            public bool MoveNext()
            {
                if (position < Chapters.Count - 1)
                {
                    ++position;
                    return true;
                }
                else
                {
                    Reset();
                    return false;
                }
            }

            public void Reset()
            {
                position = -1;
            }

            public object Current
            {
                get
                {
                    return Chapters[position];
                }
            } 

        }

        public ArrayList GrabChapters(string searchString)
        {
            ArrayList Results = new ArrayList();

            string url = string.Format("{0}/chapters/search?title={1}&chapterCount=0", dbUrl, searchString);
            string xml = GetXml(url);

            XmlDocument xmlResults = new XmlDocument();
            xmlResults.LoadXml(xml);

            XmlNodeList chapterInfos = xmlResults.GetElementsByTagName("chapterInfo");

            //if chapterInfos.Count is 0 then there was no results

            foreach (XmlNode chapterInfo in chapterInfos)
            {

                ChapterSet chapterSet = new ChapterSet();

                XmlDocument xdox = new XmlDocument();
                xdox.LoadXml(chapterInfo.OuterXml);

                XmlNodeList nameNode = xdox.GetElementsByTagName("title");
                XmlNodeList chapterNodes = xdox.GetElementsByTagName("chapter");

                chapterSet.Name = nameNode[0].InnerText;

                foreach (XmlNode chapter in chapterNodes)
                {
                    Chapter pChapter = new Chapter();
                    pChapter.Name = chapter.Attributes["name"].Value;
                    pChapter.Time = TimeSpan.Parse(chapter.Attributes["time"].Value);

                    chapterSet.Chapters.Add(pChapter);
                }

                Results.Add(chapterSet);

            }

            return Results;
        }

        private string GetXml(string url)
        {
            string xml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = Application.ProductName + " " + Application.ProductVersion;
                client.Headers["ApiKey"] = "a784c7d08e5fe192ca247d1a2dd5c27f";
                client.Headers["UserName"] = "MKV Chapterizer";
                xml = client.DownloadString(url);
            }
            return xml;
        }
    }
}

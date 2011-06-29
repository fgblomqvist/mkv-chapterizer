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

        #region customTypes

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
            public int Quality; //Ranging from 0-5

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
        #endregion

        #region customExceptions
        [Serializable()]
        public class NoResultsException : System.Exception
        {
            public NoResultsException() : base() { }
            public NoResultsException(string message) : base(message) { }
            public NoResultsException(string message, System.Exception inner) : base(message, inner) { }

            // A constructor is needed for serialization when an
            // exception propagates from a remoting server to the client. 
            protected NoResultsException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) { }
        }
        #endregion

        public List<ChapterSet> GrabChapters(string searchString)
        {
            List<ChapterSet> Results = new List<ChapterSet>();

            string url = string.Format("{0}/chapters/search?title={1}&chapterCount=0", dbUrl, searchString);
            string xml = GetXml(url);

            XmlDocument xmlResults = new XmlDocument();
            xmlResults.LoadXml(xml);

            XmlNodeList chapterInfos = xmlResults.GetElementsByTagName("chapterInfo");

            //if chapterInfos.Count is 0 then there was no results
            if (chapterInfos.Count == 0)
            {
                throw new NoResultsException();
            }
            else
            {

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
        }

        private string GetXml(string url)
        {
            string xml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = Application.ProductName + " " + Application.ProductVersion;
                client.Headers["ApiKey"] = "a784c7d08e5fe192ca247d1a2dd5c27f";
                client.Headers["UserName"] = "MKV Chapterizer";

                try
                {
                    xml = client.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return xml;
        }
    }
}

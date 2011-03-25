using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKV_Chapterizer
{
    class TheChapterizer
    {
        private int pProgress;

        public int Progress
        {
            get
            {
                return pProgress;
            }

            set
            {
                pProgress = value;
            }
        }
    }
}

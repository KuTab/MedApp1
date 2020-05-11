using System;
using Foundation;

namespace MedApp1
{

    [Preserve]
    [Serializable]
    public class Medicine
    {
        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime TillDate { get; set; }

        public Medicine(string name,DateTime fdate, DateTime tdate)
        {
            Name = name;
            FromDate = fdate;
            TillDate = tdate;
        }

        public Medicine() { }
    }
}

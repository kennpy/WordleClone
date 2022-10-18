using System;
using System.Collections.Generic;
  
namespace WordleClone{
    public class WordResponseArgs : EventArgs
    {
        public Dictionary<string, dynamic> WordDetails;

        public WordResponseArgs()
        {
            this.WordDetails = new Dictionary<string, dynamic>();
        }

        public Dictionary<string, dynamic> GetWordDetails()
        {
            return this.WordDetails;
        }
        public void SetWordDetails(string key, dynamic value)
        {
            this.WordDetails.Add(key, value);
        }

    }
}

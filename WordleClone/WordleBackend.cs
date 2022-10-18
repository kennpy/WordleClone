using System;


namespace WordleClone
{
    public class WordleBackend
    {

        // declare response event and handler
        public static event EventHandler<WordResponseArgs> ResponseWord;

        protected virtual void OnResponseWord(WordResponseArgs e)
        {
            EventHandler<WordResponseArgs> handler = ResponseWord;
            if(handler != null)
            {
                handler(this, e);
            }
        } 

        public WordleBackend()
        {
            Form1.SubmitButtonEvent += SubmitWordHandler;
        }
        public void SubmitWordHandler(object sender, EventArgs e)
        {
            Console.Write("SubmitWordHandler has been called with : ", e.ToString())
;
            // see if word is in word list


            // if so 

            // Raise SubmitWordResponse event, sending back dictionary of stuff
        }

    }
}


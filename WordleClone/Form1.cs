using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordleClone
{
    public partial class Form1 : Form
    {

        Button[,] WordleSlots = new Button[6, 5];
        String wordToSubmit = "";

        // DELETE THIS 
        WordleBackend backend = new WordleBackend();

        // declare SubmitButtonEvent delegate + event
       //  public delegate void SubmitButtonEventHandler(object sender, EventArgs e);
        public static event EventHandler<SubmitButtonEventArgs> SubmitButtonEvent;
        protected virtual void OnSubmitEvent() {
            // create our event args and store the word
            SubmitButtonEventArgs submitArgs = new SubmitButtonEventArgs();
            submitArgs.Word = wordToSubmit;

            // create a handler so we can raise the event, passing in the word
            EventHandler<SubmitButtonEventArgs> handler = SubmitButtonEvent;
            if (handler != null)
            {
                handler(this, submitArgs);
            }
        }

        public void GotResponse(object sender, WordResponseArgs e)
        {

        }


        // Catches the SubmitWord event triggered in WordleBackend
        public void InitializeListeners()
        {
            WordleBackend.ResponseWord += GotResponse;
        }

        public Form1()
        {

            InitializeComponent();
            InitializeButtons();
            // initialize textbox 
        }

        /// <summary>
        /// Initializes 2d Button array setting
        /// default size, background color, text.
        /// Adds 
        /// </summary>
        public void InitializeButtons()
        {
            Size btnSize = new Size(50, 50);
            for (int row = 0; row < WordleSlots.GetLength(0); row++)
            {
                for (int col = 0; col < WordleSlots.GetLength(1); col++)
                {
                    WordleSlots[row, col] = new Button();
                    WordleSlots[row, col].Size = btnSize;
                    WordleSlots[row, col].Location = new Point(col * btnSize.Width, row * btnSize.Height);
                    this.Controls.Add(WordleSlots[row, col]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            ((Button)sender).BackColor = Color.Green;
            
            // trigger the OnSubmitEvent
            OnSubmitEvent();
        }

        /// <summary>
        /// Fills a wordle row with a word
        /// </summary>
        /// <param name="row">The row to be updated</param>
        /// <param name="word">The word to add to the row</param>
        private void FillWordleSlots(int rowToFind, string word)
        {
            char[] wordCharArr = word.ToCharArray();

            for(int row = 0; row < WordleSlots.GetLength(0); row++)
            {
                for(int col = 0; col < WordleSlots.GetLength(1); col++)
                {
                    if(row == rowToFind)
                    {
                        // update the slot if we are still in range
                        if(col < wordCharArr.GetLength(0))
                        {
                            WordleSlots[row, col].Text = wordCharArr[col].ToString();
                        }
                        // else we fill the rest of slots with spaces for this row
                        else
                        {
                            WordleSlots[row, col].Text = "";
                        }
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool charUpdated = false;
            string text = ((TextBox)sender).Text;
            wordToSubmit = text;
            int textLength = ((TextBox)sender).Text.Length;

            int row = 0;
            // check if box has space
            while(row < WordleSlots.GetLength(0) && charUpdated == false)
            {
                int col = 0;
                while((col < WordleSlots.GetLength(1)) && charUpdated == false)
                {

                    // if a slot has not been updated and we have not updated any slots
                   if( WordleSlots[row, col].Text == "")
                    {
                        // if row is 0 then we can skip checks
                        if(row == 0)
                        {
                            FillWordleSlots(row, text);
                            charUpdated = true;
                        }
                        // else check if col is 0 and the state of the text we are adding
                        else
                        {
                            // if col is 0 check state of text
                            if (col == 0)
                            {
                                // if text is not max then we know we are updating previous
                                // if text is max we know we updated previous, so do nothing
                                if(textLength <= 5 && row == 0)
                                {
                                    FillWordleSlots(row, text);
                                    charUpdated = true;
                                }
                                else
                                {
                                    FillWordleSlots(row - 1, text);
                                    charUpdated = true;
                                }
                            }
                            else
                            {
                                FillWordleSlots(row, text);
                                charUpdated = true;
                            }
                        } 
                    }
                    col += 1;
                } // end while
                row += 1;
            }

            // if all slots are filled then show the guess button
            button1.Visible = (textLength == 5) ? true : false;
        }  
    }
} 

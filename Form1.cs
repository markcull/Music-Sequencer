using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media; //A required import for the soundplayers.
using System.IO;

namespace VisualSequencerMarkCull
{
    public partial class Form1 : Form
    {
        //Initializing the sound players as the form is loaded for improved efficiency.
        SoundPlayer sndplayer1 = new SoundPlayer(VisualSequencerMarkCull.Resource1.kick1);
        SoundPlayer sndplayer2 = new SoundPlayer(VisualSequencerMarkCull.Resource1.snare);
        SoundPlayer sndplayer3 = new SoundPlayer(VisualSequencerMarkCull.Resource1.open_hh);
        SoundPlayer sndplayer4 = new SoundPlayer(VisualSequencerMarkCull.Resource1.handclap);
        SoundPlayer sndplayer5 = new SoundPlayer(VisualSequencerMarkCull.Resource1.hi_conga);
        SoundPlayer sndplayer6 = new SoundPlayer(VisualSequencerMarkCull.Resource1.cowbell);
        SoundPlayer sndplayer7 = new SoundPlayer(VisualSequencerMarkCull.Resource1.maracas);
        SoundPlayer sndplayer8 = new SoundPlayer(VisualSequencerMarkCull.Resource1.tom1);

        //Class Variables
        //Initialize the count variable
        UInt16 count = 0; //Keeps track of the notes that are being played.
        int bpm = 0; //To hold the tempo value entered by the user via the trackbar
        int interval = 0; //To hold the interval value for the timer based on the users entered tempo
        double Temp = 0; // Variable used to do the BPM calculation.

        String saveFilename;  // A variable in which stores the file name entered by the user into the file dialog indicating the file to store the patch data in.
        String openFilename;  // Holds the name of the patch file entered by the user

        public Form1()
        {
            InitializeComponent();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true; // Enable the timer when the form loads.
            updateTempo(); //sets the tempo at the default trackbar position.

            //Setting all the pictureboxes to the 'off' LED when form loads
            BeatLED1.Image = Properties.Resources.BLUELEDOFF;
            BeatLED2.Image = Properties.Resources.BLUELEDOFF;
            BeatLED3.Image = Properties.Resources.BLUELEDOFF;
            BeatLED4.Image = Properties.Resources.BLUELEDOFF;
            BeatLED5.Image = Properties.Resources.BLUELEDOFF;
            BeatLED6.Image = Properties.Resources.BLUELEDOFF;
            BeatLED7.Image = Properties.Resources.BLUELEDOFF;
            BeatLED8.Image = Properties.Resources.BLUELEDOFF;
            BeatLED9.Image = Properties.Resources.BLUELEDOFF;
            BeatLED10.Image = Properties.Resources.BLUELEDOFF;
            BeatLED11.Image = Properties.Resources.BLUELEDOFF;
            BeatLED12.Image = Properties.Resources.BLUELEDOFF;
            BeatLED13.Image = Properties.Resources.BLUELEDOFF;
            BeatLED14.Image = Properties.Resources.BLUELEDOFF;
            BeatLED15.Image = Properties.Resources.BLUELEDOFF;
            BeatLED16.Image = Properties.Resources.BLUELEDOFF;

            BeatLED1.Refresh();
            BeatLED2.Refresh();
            BeatLED3.Refresh();
            BeatLED4.Refresh();
            BeatLED5.Refresh();
            BeatLED6.Refresh();
            BeatLED7.Refresh();
            BeatLED8.Refresh();
            BeatLED9.Refresh();
            BeatLED10.Refresh();
            BeatLED11.Refresh();
            BeatLED12.Refresh();
            BeatLED13.Refresh();
            BeatLED14.Refresh();
            BeatLED15.Refresh();
            BeatLED16.Refresh();

            //Setting all checkboxes with an inital 'rest' value to avoid errors when saving patch files 
            //with notes that have no check boxes checked.
            resetNotes();
        }

        private void trialButton1_Click(object sender, EventArgs e)
        {
            sndplayer1.Play();
        }

        private void trialButton2_Click(object sender, EventArgs e)
        {
            sndplayer2.Play();
        }

        private void trialButton3_Click(object sender, EventArgs e)
        {
            sndplayer3.Play();
        }

        private void trialButton4_Click(object sender, EventArgs e)
        {
            sndplayer4.Play();
        }

        private void trialButton5_Click(object sender, EventArgs e)
        {
            sndplayer5.Play();
        }

        private void trialButton6_Click(object sender, EventArgs e)
        {
            sndplayer6.Play();
        }

        private void trialButton7_Click(object sender, EventArgs e)
        {
            sndplayer7.Play();
        }

        private void trialButton8_Click(object sender, EventArgs e)
        {
            sndplayer8.Play();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Do any saving of data here if needed
            Application.Exit();    //Forces a clean exit from the application

        }

        // Timer event handler
        //**************************************************************************************************
        //This event handler method gets called automatically everytime the timer control ticks.
        //The timer controls tick rate is determined by its interval property in milliseconds
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            count++;          //increment the seconds 

            if (count == 1) //first note
            {
                //Updates the LED pictures
                BeatLED16.Image = Properties.Resources.BLUELEDOFF;
                BeatLED1.Image = Properties.Resources.BLUELEDON;
                BeatLED16.Refresh();
                BeatLED1.Refresh();

                //Checks to see what box is checked then plays the corresponding sound.
                if (N1S1.Checked) sndplayer1.Play();
                else if (N1S2.Checked) sndplayer2.Play();
                else if (N1S3.Checked) sndplayer3.Play();
                else if (N1S4.Checked) sndplayer4.Play();
                else if (N1S5.Checked) sndplayer5.Play();
                else if (N1S6.Checked) sndplayer6.Play();
                else if (N1S7.Checked) sndplayer7.Play();
                else if (N1S8.Checked) sndplayer8.Play();
                else if (N1REST.Checked) { /*Do nothing on a rest*/ }

                //this code repeats for the rest of the notes.
            }
            else if (count == 2) //Second Note
            {
                BeatLED1.Image = Properties.Resources.BLUELEDOFF;
                BeatLED2.Image = Properties.Resources.BLUELEDON;
                BeatLED1.Refresh();
                BeatLED2.Refresh();

                if (N2S1.Checked) sndplayer1.Play();
                else if (N2S2.Checked) sndplayer2.Play();
                else if (N2S3.Checked) sndplayer3.Play();
                else if (N2S4.Checked) sndplayer4.Play();
                else if (N2S5.Checked) sndplayer5.Play();
                else if (N2S6.Checked) sndplayer6.Play();
                else if (N2S7.Checked) sndplayer7.Play();
                else if (N2S8.Checked) sndplayer8.Play();
                else if (N2REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 3) //Third Note
            {
                BeatLED2.Image = Properties.Resources.BLUELEDOFF;
                BeatLED3.Image = Properties.Resources.BLUELEDON;
                BeatLED2.Refresh();
                BeatLED3.Refresh();

                if (N3S1.Checked) sndplayer1.Play();
                else if (N3S2.Checked) sndplayer2.Play();
                else if (N3S3.Checked) sndplayer3.Play();
                else if (N3S4.Checked) sndplayer4.Play();
                else if (N3S5.Checked) sndplayer5.Play();
                else if (N3S6.Checked) sndplayer6.Play();
                else if (N3S7.Checked) sndplayer7.Play();
                else if (N3S8.Checked) sndplayer8.Play();
                else if (N3REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 4) //Fourth Note
            {
                BeatLED3.Image = Properties.Resources.BLUELEDOFF;
                BeatLED4.Image = Properties.Resources.BLUELEDON;
                BeatLED3.Refresh();
                BeatLED4.Refresh();

                if (N4S1.Checked) sndplayer1.Play();
                else if (N4S2.Checked) sndplayer2.Play();
                else if (N4S3.Checked) sndplayer3.Play();
                else if (N4S4.Checked) sndplayer4.Play();
                else if (N4S5.Checked) sndplayer5.Play();
                else if (N4S6.Checked) sndplayer6.Play();
                else if (N4S7.Checked) sndplayer7.Play();
                else if (N4S8.Checked) sndplayer8.Play();
                else if (N4REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 5) //Fifth Note
            {
                BeatLED4.Image = Properties.Resources.BLUELEDOFF;
                BeatLED5.Image = Properties.Resources.BLUELEDON;
                BeatLED4.Refresh();
                BeatLED5.Refresh();

                if (N5S1.Checked) sndplayer1.Play();
                else if (N5S2.Checked) sndplayer2.Play();
                else if (N5S3.Checked) sndplayer3.Play();
                else if (N5S4.Checked) sndplayer4.Play();
                else if (N5S5.Checked) sndplayer5.Play();
                else if (N5S6.Checked) sndplayer6.Play();
                else if (N5S7.Checked) sndplayer7.Play();
                else if (N5S8.Checked) sndplayer8.Play();
                else if (N5REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 6) //sixth Note
            {
                BeatLED5.Image = Properties.Resources.BLUELEDOFF;
                BeatLED6.Image = Properties.Resources.BLUELEDON;
                BeatLED5.Refresh();
                BeatLED6.Refresh();

                if (N6S1.Checked) sndplayer1.Play();
                else if (N6S2.Checked) sndplayer2.Play();
                else if (N6S3.Checked) sndplayer3.Play();
                else if (N6S4.Checked) sndplayer4.Play();
                else if (N6S5.Checked) sndplayer5.Play();
                else if (N6S6.Checked) sndplayer6.Play();
                else if (N6S7.Checked) sndplayer7.Play();
                else if (N6S8.Checked) sndplayer8.Play();
                else if (N6REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 7) //seventh Note
            {
                BeatLED6.Image = Properties.Resources.BLUELEDOFF;
                BeatLED7.Image = Properties.Resources.BLUELEDON;
                BeatLED6.Refresh();
                BeatLED7.Refresh();

                if (N7S1.Checked) sndplayer1.Play();
                else if (N7S2.Checked) sndplayer2.Play();
                else if (N7S3.Checked) sndplayer3.Play();
                else if (N7S4.Checked) sndplayer4.Play();
                else if (N7S5.Checked) sndplayer5.Play();
                else if (N7S6.Checked) sndplayer6.Play();
                else if (N7S7.Checked) sndplayer7.Play();
                else if (N7S8.Checked) sndplayer8.Play();
                else if (N7REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 8) //eighth Note
            {
                BeatLED7.Image = Properties.Resources.BLUELEDOFF;
                BeatLED8.Image = Properties.Resources.BLUELEDON;
                BeatLED7.Refresh();
                BeatLED8.Refresh();

                if (N8S1.Checked) sndplayer1.Play();
                else if (N8S2.Checked) sndplayer2.Play();
                else if (N8S3.Checked) sndplayer3.Play();
                else if (N8S4.Checked) sndplayer4.Play();
                else if (N8S5.Checked) sndplayer5.Play();
                else if (N8S6.Checked) sndplayer6.Play();
                else if (N8S7.Checked) sndplayer7.Play();
                else if (N8S8.Checked) sndplayer8.Play();
                else if (N8REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 9) //ninth Note
            {
                BeatLED8.Image = Properties.Resources.BLUELEDOFF;
                BeatLED9.Image = Properties.Resources.BLUELEDON;
                BeatLED8.Refresh();
                BeatLED9.Refresh();

                if (N9S1.Checked) sndplayer1.Play();
                else if (N9S2.Checked) sndplayer2.Play();
                else if (N9S3.Checked) sndplayer3.Play();
                else if (N9S4.Checked) sndplayer4.Play();
                else if (N9S5.Checked) sndplayer5.Play();
                else if (N9S6.Checked) sndplayer6.Play();
                else if (N9S7.Checked) sndplayer7.Play();
                else if (N9S8.Checked) sndplayer8.Play();
                else if (N9REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 10) //tenth Note
            {
                BeatLED9.Image = Properties.Resources.BLUELEDOFF;
                BeatLED10.Image = Properties.Resources.BLUELEDON;
                BeatLED9.Refresh();
                BeatLED10.Refresh();

                if (N10S1.Checked) sndplayer1.Play();
                else if (N10S2.Checked) sndplayer2.Play();
                else if (N10S3.Checked) sndplayer3.Play();
                else if (N10S4.Checked) sndplayer4.Play();
                else if (N10S5.Checked) sndplayer5.Play();
                else if (N10S6.Checked) sndplayer6.Play();
                else if (N10S7.Checked) sndplayer7.Play();
                else if (N10S8.Checked) sndplayer8.Play();
                else if (N10REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 11) //eleventh Note
            {
                BeatLED10.Image = Properties.Resources.BLUELEDOFF;
                BeatLED11.Image = Properties.Resources.BLUELEDON;
                BeatLED10.Refresh();
                BeatLED11.Refresh();

                if (N11S1.Checked) sndplayer1.Play();
                else if (N11S2.Checked) sndplayer2.Play();
                else if (N11S3.Checked) sndplayer3.Play();
                else if (N11S4.Checked) sndplayer4.Play();
                else if (N11S5.Checked) sndplayer5.Play();
                else if (N11S6.Checked) sndplayer6.Play();
                else if (N11S7.Checked) sndplayer7.Play();
                else if (N11S8.Checked) sndplayer8.Play();
                else if (N11REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 12) //twelfth Note
            {
                BeatLED11.Image = Properties.Resources.BLUELEDOFF;
                BeatLED12.Image = Properties.Resources.BLUELEDON;
                BeatLED11.Refresh();
                BeatLED12.Refresh();

                if (N12S1.Checked) sndplayer1.Play();
                else if (N12S2.Checked) sndplayer2.Play();
                else if (N12S3.Checked) sndplayer3.Play();
                else if (N12S4.Checked) sndplayer4.Play();
                else if (N12S5.Checked) sndplayer5.Play();
                else if (N12S6.Checked) sndplayer6.Play();
                else if (N12S7.Checked) sndplayer7.Play();
                else if (N12S8.Checked) sndplayer8.Play();
                else if (N12REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 13) //thirteenth Note
            {
                BeatLED12.Image = Properties.Resources.BLUELEDOFF;
                BeatLED13.Image = Properties.Resources.BLUELEDON;
                BeatLED12.Refresh();
                BeatLED13.Refresh();

                if (N13S1.Checked) sndplayer1.Play();
                else if (N13S2.Checked) sndplayer2.Play();
                else if (N13S3.Checked) sndplayer3.Play();
                else if (N13S4.Checked) sndplayer4.Play();
                else if (N13S5.Checked) sndplayer5.Play();
                else if (N13S6.Checked) sndplayer6.Play();
                else if (N13S7.Checked) sndplayer7.Play();
                else if (N13S8.Checked) sndplayer8.Play();
                else if (N13REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 14) //fourteenth Note
            {
                BeatLED13.Image = Properties.Resources.BLUELEDOFF;
                BeatLED14.Image = Properties.Resources.BLUELEDON;
                BeatLED13.Refresh();
                BeatLED14.Refresh();

                if (N14S1.Checked) sndplayer1.Play();
                else if (N14S2.Checked) sndplayer2.Play();
                else if (N14S3.Checked) sndplayer3.Play();
                else if (N14S4.Checked) sndplayer4.Play();
                else if (N14S5.Checked) sndplayer5.Play();
                else if (N14S6.Checked) sndplayer6.Play();
                else if (N14S7.Checked) sndplayer7.Play();
                else if (N14S8.Checked) sndplayer8.Play();
                else if (N14REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 15) //fifteenth Note
            {
                BeatLED14.Image = Properties.Resources.BLUELEDOFF;
                BeatLED15.Image = Properties.Resources.BLUELEDON;
                BeatLED14.Refresh();
                BeatLED15.Refresh();

                if (N15S1.Checked) sndplayer1.Play();
                else if (N15S2.Checked) sndplayer2.Play();
                else if (N15S3.Checked) sndplayer3.Play();
                else if (N15S4.Checked) sndplayer4.Play();
                else if (N15S5.Checked) sndplayer5.Play();
                else if (N15S6.Checked) sndplayer6.Play();
                else if (N15S7.Checked) sndplayer7.Play();
                else if (N15S8.Checked) sndplayer8.Play();
                else if (N15REST.Checked) { /*Do nothing on a rest*/ }
            }
            else if (count == 16) //fifteenth Note
            {
                BeatLED15.Image = Properties.Resources.BLUELEDOFF;
                BeatLED16.Image = Properties.Resources.BLUELEDON;
                BeatLED15.Refresh();
                BeatLED16.Refresh();

                if (N16S1.Checked) sndplayer1.Play();
                else if (N16S2.Checked) sndplayer2.Play();
                else if (N16S3.Checked) sndplayer3.Play();
                else if (N16S4.Checked) sndplayer4.Play();
                else if (N16S5.Checked) sndplayer5.Play();
                else if (N16S6.Checked) sndplayer6.Play();
                else if (N16S7.Checked) sndplayer7.Play();
                else if (N16S8.Checked) sndplayer8.Play();
                else if (N16REST.Checked) { /*Do nothing on a rest*/ }

                count = 0;  //resets barcount after last bar
            }
        }


        //updateTempo holds the trackbar code in a separate method as it is used again on initialization, the trackbar update and when opening a patch file.
        private void updateTempo()
        {
            bpm = trackBar1.Value; // sets the bpm variable to the value of the track bar.
            labelBPM.Text = bpm + " bpm"; //updates the GUI for the correct BPM number.
            Temp = ((double)60 / (double)bpm) * 1000; //casted as a double for interval calculation.
            interval = (int)Temp; //interval variable is then updated as an int after the calculation
            timer1.Interval = interval; //interval of the timer1 control is then updated with the new interval number.
        }


        //Controlling the BPM whenever the trackbar scroll event changes.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            updateTempo();
        }


        //******************************************SAVING A PATCH FILE*****************************************************************
        private void saveFile()
        {
            //Method to display the windows file save dialog.
            saveFileDialog1.ShowDialog();
            // Gets the file name entered by the user and stores it in the variable.
            saveFilename = saveFileDialog1.FileName;

            //Calls the CreateText method of the File class and passes it the filename then assigns this as an output stream. 
            StreamWriter outputStream = File.CreateText(saveFilename);

            outputStream.WriteLine("SeqPatch File");  //Writes a string to the first line of the text file so that when the file opens we can test if it is a sequencer patch file

            //This line of the file stores the current tempo information.
            outputStream.WriteLine("TEMPO=" + Convert.ToString(bpm));


            //Each note determines which radio button is active and writes a corresponding string into the patch file.
            // check note 1
            if (N1S1.Checked) outputStream.WriteLine("N1=S1");
            else if (N1S2.Checked) outputStream.WriteLine("N1=S2");
            else if (N1S3.Checked) outputStream.WriteLine("N1=S3");
            else if (N1S4.Checked) outputStream.WriteLine("N1=S4");
            else if (N1S5.Checked) outputStream.WriteLine("N1=S5");
            else if (N1S6.Checked) outputStream.WriteLine("N1=S6");
            else if (N1S7.Checked) outputStream.WriteLine("N1=S7");
            else if (N1S8.Checked) outputStream.WriteLine("N1=S8");
            else if (N1REST.Checked) outputStream.WriteLine("N1=REST");
            //check note 2
            if (N2S1.Checked) outputStream.WriteLine("N2=S1");
            else if (N2S2.Checked) outputStream.WriteLine("N2=S2");
            else if (N2S3.Checked) outputStream.WriteLine("N2=S3");
            else if (N2S4.Checked) outputStream.WriteLine("N2=S4");
            else if (N2S5.Checked) outputStream.WriteLine("N2=S5");
            else if (N2S6.Checked) outputStream.WriteLine("N2=S6");
            else if (N2S7.Checked) outputStream.WriteLine("N2=S7");
            else if (N2S8.Checked) outputStream.WriteLine("N2=S8");
            else if (N2REST.Checked) outputStream.WriteLine("N2=REST");
            //check note 3
            if (N3S1.Checked) outputStream.WriteLine("N3=S1");
            else if (N3S2.Checked) outputStream.WriteLine("N3=S2");
            else if (N3S3.Checked) outputStream.WriteLine("N3=S3");
            else if (N3S4.Checked) outputStream.WriteLine("N3=S4");
            else if (N3S5.Checked) outputStream.WriteLine("N3=S5");
            else if (N3S6.Checked) outputStream.WriteLine("N3=S6");
            else if (N3S7.Checked) outputStream.WriteLine("N3=S7");
            else if (N3S8.Checked) outputStream.WriteLine("N3=S8");
            else if (N3REST.Checked) outputStream.WriteLine("N3=REST");
            //check note 4
            if (N4S1.Checked) outputStream.WriteLine("N4=S1");
            else if (N4S2.Checked) outputStream.WriteLine("N4=S2");
            else if (N4S3.Checked) outputStream.WriteLine("N4=S3");
            else if (N4S4.Checked) outputStream.WriteLine("N4=S4");
            else if (N4S5.Checked) outputStream.WriteLine("N4=S5");
            else if (N4S6.Checked) outputStream.WriteLine("N4=S6");
            else if (N4S7.Checked) outputStream.WriteLine("N4=S7");
            else if (N4S8.Checked) outputStream.WriteLine("N4=S8");
            else if (N4REST.Checked) outputStream.WriteLine("N4=REST");
            //check note 5
            if (N5S1.Checked) outputStream.WriteLine("N5=S1");
            else if (N5S2.Checked) outputStream.WriteLine("N5=S2");
            else if (N5S3.Checked) outputStream.WriteLine("N5=S3");
            else if (N5S4.Checked) outputStream.WriteLine("N5=S4");
            else if (N5S5.Checked) outputStream.WriteLine("N5=S5");
            else if (N5S6.Checked) outputStream.WriteLine("N5=S6");
            else if (N5S7.Checked) outputStream.WriteLine("N5=S7");
            else if (N5S8.Checked) outputStream.WriteLine("N5=S8");
            else if (N5REST.Checked) outputStream.WriteLine("N5=REST");
            //check note 6
            if (N6S1.Checked) outputStream.WriteLine("N6=S1");
            else if (N6S2.Checked) outputStream.WriteLine("N6=S2");
            else if (N6S3.Checked) outputStream.WriteLine("N6=S3");
            else if (N6S4.Checked) outputStream.WriteLine("N6=S4");
            else if (N6S5.Checked) outputStream.WriteLine("N6=S5");
            else if (N6S6.Checked) outputStream.WriteLine("N6=S6");
            else if (N6S7.Checked) outputStream.WriteLine("N6=S7");
            else if (N6S8.Checked) outputStream.WriteLine("N6=S8");
            else if (N6REST.Checked) outputStream.WriteLine("N6=REST");
            //check note 7
            if (N7S1.Checked) outputStream.WriteLine("N7=S1");
            else if (N7S2.Checked) outputStream.WriteLine("N7=S2");
            else if (N7S3.Checked) outputStream.WriteLine("N7=S3");
            else if (N7S4.Checked) outputStream.WriteLine("N7=S4");
            else if (N7S5.Checked) outputStream.WriteLine("N7=S5");
            else if (N7S6.Checked) outputStream.WriteLine("N7=S6");
            else if (N7S7.Checked) outputStream.WriteLine("N7=S7");
            else if (N7S8.Checked) outputStream.WriteLine("N7=S8");
            else if (N7REST.Checked) outputStream.WriteLine("N7=REST");
            //check note 8
            if (N8S1.Checked) outputStream.WriteLine("N8=S1");
            else if (N8S2.Checked) outputStream.WriteLine("N8=S2");
            else if (N8S3.Checked) outputStream.WriteLine("N8=S3");
            else if (N8S4.Checked) outputStream.WriteLine("N8=S4");
            else if (N8S5.Checked) outputStream.WriteLine("N8=S5");
            else if (N8S6.Checked) outputStream.WriteLine("N8=S6");
            else if (N8S7.Checked) outputStream.WriteLine("N8=S7");
            else if (N8S8.Checked) outputStream.WriteLine("N8=S8");
            else if (N8REST.Checked) outputStream.WriteLine("N8=REST");
            //check note 9
            if (N9S1.Checked) outputStream.WriteLine("N9=S1");
            else if (N9S2.Checked) outputStream.WriteLine("N9=S2");
            else if (N9S3.Checked) outputStream.WriteLine("N9=S3");
            else if (N9S4.Checked) outputStream.WriteLine("N9=S4");
            else if (N9S5.Checked) outputStream.WriteLine("N9=S5");
            else if (N9S6.Checked) outputStream.WriteLine("N9=S6");
            else if (N9S7.Checked) outputStream.WriteLine("N9=S7");
            else if (N9S8.Checked) outputStream.WriteLine("N9=S8");
            else if (N9REST.Checked) outputStream.WriteLine("N9=REST");
            //check note 10
            if (N10S1.Checked) outputStream.WriteLine("N10=S1");
            else if (N10S2.Checked) outputStream.WriteLine("N10=S2");
            else if (N10S3.Checked) outputStream.WriteLine("N10=S3");
            else if (N10S4.Checked) outputStream.WriteLine("N10=S4");
            else if (N10S5.Checked) outputStream.WriteLine("N10=S5");
            else if (N10S6.Checked) outputStream.WriteLine("N10=S6");
            else if (N10S7.Checked) outputStream.WriteLine("N10=S7");
            else if (N10S8.Checked) outputStream.WriteLine("N10=S8");
            else if (N10REST.Checked) outputStream.WriteLine("N10=REST");
            //check note 11
            if (N11S1.Checked) outputStream.WriteLine("N11=S1");
            else if (N11S2.Checked) outputStream.WriteLine("N11=S2");
            else if (N11S3.Checked) outputStream.WriteLine("N11=S3");
            else if (N11S4.Checked) outputStream.WriteLine("N11=S4");
            else if (N11S5.Checked) outputStream.WriteLine("N11=S5");
            else if (N11S6.Checked) outputStream.WriteLine("N11=S6");
            else if (N11S7.Checked) outputStream.WriteLine("N11=S7");
            else if (N11S8.Checked) outputStream.WriteLine("N11=S8");
            else if (N11REST.Checked) outputStream.WriteLine("N11=REST");
            //check note 12
            if (N12S1.Checked) outputStream.WriteLine("N12=S1");
            else if (N12S2.Checked) outputStream.WriteLine("N12=S2");
            else if (N12S3.Checked) outputStream.WriteLine("N12=S3");
            else if (N12S4.Checked) outputStream.WriteLine("N12=S4");
            else if (N12S5.Checked) outputStream.WriteLine("N12=S5");
            else if (N12S6.Checked) outputStream.WriteLine("N12=S6");
            else if (N12S7.Checked) outputStream.WriteLine("N12=S7");
            else if (N12S8.Checked) outputStream.WriteLine("N12=S8");
            else if (N12REST.Checked) outputStream.WriteLine("N12=REST");
            //check note 13
            if (N13S1.Checked) outputStream.WriteLine("N13=S1");
            else if (N13S2.Checked) outputStream.WriteLine("N13=S2");
            else if (N13S3.Checked) outputStream.WriteLine("N13=S3");
            else if (N13S4.Checked) outputStream.WriteLine("N13=S4");
            else if (N13S5.Checked) outputStream.WriteLine("N13=S5");
            else if (N13S6.Checked) outputStream.WriteLine("N13=S6");
            else if (N13S7.Checked) outputStream.WriteLine("N13=S7");
            else if (N13S8.Checked) outputStream.WriteLine("N13=S8");
            else if (N13REST.Checked) outputStream.WriteLine("N13=REST");
            //check note 14
            if (N14S1.Checked) outputStream.WriteLine("N14=S1");
            else if (N14S2.Checked) outputStream.WriteLine("N14=S2");
            else if (N14S3.Checked) outputStream.WriteLine("N14=S3");
            else if (N14S4.Checked) outputStream.WriteLine("N14=S4");
            else if (N14S5.Checked) outputStream.WriteLine("N14=S5");
            else if (N14S6.Checked) outputStream.WriteLine("N14=S6");
            else if (N14S7.Checked) outputStream.WriteLine("N14=S7");
            else if (N14S8.Checked) outputStream.WriteLine("N14=S8");
            else if (N14REST.Checked) outputStream.WriteLine("N14=REST");
            //check note 15
            if (N15S1.Checked) outputStream.WriteLine("N15=S1");
            else if (N15S2.Checked) outputStream.WriteLine("N15=S2");
            else if (N15S3.Checked) outputStream.WriteLine("N15=S3");
            else if (N15S4.Checked) outputStream.WriteLine("N15=S4");
            else if (N15S5.Checked) outputStream.WriteLine("N15=S5");
            else if (N15S6.Checked) outputStream.WriteLine("N15=S6");
            else if (N15S7.Checked) outputStream.WriteLine("N15=S7");
            else if (N15S8.Checked) outputStream.WriteLine("N15=S8");
            else if (N15REST.Checked) outputStream.WriteLine("N15=REST");
            //check note 16
            if (N16S1.Checked) outputStream.WriteLine("N16=S1");
            else if (N16S2.Checked) outputStream.WriteLine("N16=S2");
            else if (N16S3.Checked) outputStream.WriteLine("N16=S3");
            else if (N16S4.Checked) outputStream.WriteLine("N16=S4");
            else if (N16S5.Checked) outputStream.WriteLine("N16=S5");
            else if (N16S6.Checked) outputStream.WriteLine("N16=S6");
            else if (N16S7.Checked) outputStream.WriteLine("N16=S7");
            else if (N16S8.Checked) outputStream.WriteLine("N16=S8");
            else if (N16REST.Checked) outputStream.WriteLine("N16=REST");

            //Once all the data has been stored in the patch file it closes the file which saves all the data to disk.
            outputStream.Close();
        }

        //Event handler for the save as event in the Menu Strip.
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //runs the saving file code when the 'Save as...' option is clicked in the menu.
            saveFile(); // Stored in a separate method so I can call the save method again when exiting the application.
        }

        //***************************************OPENING A PATCH FILE*****************************************************************
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
                      
    
            openFileDialog1.ShowDialog();   // Calls the method to display the standard windows file open dialog

            openFilename = openFileDialog1.FileName; //Stores the filename entered by the user into the variable
            openFile(openFilename);//runs the opening patch file code and passes through the filename chosen by the user.
        }

        private void openFile(String filename)
        {
            //Calls the File classes OpenText method on the selected file
            //and assigns it to an input stream object
            StreamReader inputStream = File.OpenText(filename);

            String lineFromFile;                          //This variable will hold each line of text read from the file                 

            lineFromFile = inputStream.ReadLine();             //Reads the first line of text from the file.

            if (lineFromFile.Equals("SeqPatch File"))          //Checks its a valid patch file
            {
                //If it is a valid patch file continue on otherwise...
            }
            else
            {
                MessageBox.Show("This is not a valid sequencer patch file.");   //Tells the user its is not valid 
                Application.Exit();                                             //then exits the program
            }

            String tempoStr;                              //Declares a variable to hold the tempo value as a string
            lineFromFile = inputStream.ReadLine();        //Reads the second line from the file
            tempoStr = lineFromFile.Remove(0, 6);         //From the read text removes the first 6 characters to 
                                                          //leave just the number part i.e. the 120 from TEMPO=120


            timer1.Interval = Convert.ToUInt16(tempoStr);    //Converts the numeric part of the tempo string to an interger
            trackBar1.Value = Convert.ToUInt16(tempoStr);    //then assigns it to the timer's interval property
                                                             //and updates the trackbar value accordingly
            updateTempo(); //this method is run to update the BPM label.

            //Gets the patch data from the file for the sequencer note states
            //Declares 16 strings one for each note
            String N1str; String N2str; String N3str; String N4str;
            String N5str; String N6str; String N7str; String N8str;
            String N9str; String N10str; String N11str; String N12str;
            String N13str; String N14str; String N15str; String N16str;

            //Reads the note data from each line of the file and store it in the corresponding string variable
            N1str = inputStream.ReadLine();
            N2str = inputStream.ReadLine();
            N3str = inputStream.ReadLine();
            N4str = inputStream.ReadLine();
            N5str = inputStream.ReadLine();
            N6str = inputStream.ReadLine();
            N7str = inputStream.ReadLine();
            N8str = inputStream.ReadLine();
            N9str = inputStream.ReadLine();
            N10str = inputStream.ReadLine();
            N11str = inputStream.ReadLine();
            N12str = inputStream.ReadLine();
            N13str = inputStream.ReadLine();
            N14str = inputStream.ReadLine();
            N15str = inputStream.ReadLine();
            N16str = inputStream.ReadLine();

            //Reads the string variables and checks the relevent check boxes.
            //N1
            if (N1str.Equals("N1=S1")) N1S1.Checked = true;
            else if (N1str.Equals("N1=S2")) N1S2.Checked = true;
            else if (N1str.Equals("N1=S3")) N1S3.Checked = true;
            else if (N1str.Equals("N1=S4")) N1S4.Checked = true;
            else if (N1str.Equals("N1=S5")) N1S5.Checked = true;
            else if (N1str.Equals("N1=S6")) N1S6.Checked = true;
            else if (N1str.Equals("N1=S7")) N1S7.Checked = true;
            else if (N1str.Equals("N1=S8")) N1S8.Checked = true;
            else if (N1str.Equals("N1=REST")) N1REST.Checked = true;

            //N2
            if (N2str.Equals("N2=S1")) N2S1.Checked = true;
            else if (N2str.Equals("N2=S2")) N2S2.Checked = true;
            else if (N2str.Equals("N2=S3")) N2S3.Checked = true;
            else if (N2str.Equals("N2=S4")) N2S4.Checked = true;
            else if (N2str.Equals("N2=S5")) N2S5.Checked = true;
            else if (N2str.Equals("N2=S6")) N2S6.Checked = true;
            else if (N2str.Equals("N2=S7")) N2S7.Checked = true;
            else if (N2str.Equals("N2=S8")) N2S8.Checked = true;
            else if (N2str.Equals("N2=REST")) N2REST.Checked = true;

            //N3
            if (N3str.Equals("N3=S1")) N3S1.Checked = true;
            else if (N3str.Equals("N3=S2")) N3S2.Checked = true;
            else if (N3str.Equals("N3=S3")) N3S3.Checked = true;
            else if (N3str.Equals("N3=S4")) N3S4.Checked = true;
            else if (N3str.Equals("N3=S5")) N3S5.Checked = true;
            else if (N3str.Equals("N3=S6")) N3S6.Checked = true;
            else if (N3str.Equals("N3=S7")) N3S7.Checked = true;
            else if (N3str.Equals("N3=S8")) N3S8.Checked = true;
            else if (N3str.Equals("N3=REST")) N3REST.Checked = true;

            //N4
            if (N4str.Equals("N4=S1")) N4S1.Checked = true;
            else if (N4str.Equals("N4=S2")) N4S2.Checked = true;
            else if (N4str.Equals("N4=S3")) N4S3.Checked = true;
            else if (N4str.Equals("N4=S4")) N4S4.Checked = true;
            else if (N4str.Equals("N4=S5")) N4S5.Checked = true;
            else if (N4str.Equals("N4=S6")) N4S6.Checked = true;
            else if (N4str.Equals("N4=S7")) N4S7.Checked = true;
            else if (N4str.Equals("N4=S8")) N4S8.Checked = true;
            else if (N4str.Equals("N4=REST")) N4REST.Checked = true;

            //N5
            if (N5str.Equals("N5=S1")) N5S1.Checked = true;
            else if (N5str.Equals("N5=S2")) N5S2.Checked = true;
            else if (N5str.Equals("N5=S3")) N5S3.Checked = true;
            else if (N5str.Equals("N5=S4")) N5S4.Checked = true;
            else if (N5str.Equals("N5=S5")) N5S5.Checked = true;
            else if (N5str.Equals("N5=S6")) N5S6.Checked = true;
            else if (N5str.Equals("N5=S7")) N5S7.Checked = true;
            else if (N5str.Equals("N5=S8")) N5S8.Checked = true;
            else if (N5str.Equals("N5=REST")) N5REST.Checked = true;

            //N6
            if (N6str.Equals("N6=S1")) N6S1.Checked = true;
            else if (N6str.Equals("N6=S2")) N6S2.Checked = true;
            else if (N6str.Equals("N6=S3")) N6S3.Checked = true;
            else if (N6str.Equals("N6=S4")) N6S4.Checked = true;
            else if (N6str.Equals("N6=S5")) N6S5.Checked = true;
            else if (N6str.Equals("N6=S6")) N6S6.Checked = true;
            else if (N6str.Equals("N6=S7")) N6S7.Checked = true;
            else if (N6str.Equals("N6=S8")) N6S8.Checked = true;
            else if (N6str.Equals("N6=REST")) N6REST.Checked = true;

            //N7
            if (N7str.Equals("N7=S1")) N7S1.Checked = true;
            else if (N7str.Equals("N7=S2")) N7S2.Checked = true;
            else if (N7str.Equals("N7=S3")) N7S3.Checked = true;
            else if (N7str.Equals("N7=S4")) N7S4.Checked = true;
            else if (N7str.Equals("N7=S5")) N7S5.Checked = true;
            else if (N7str.Equals("N7=S6")) N7S6.Checked = true;
            else if (N7str.Equals("N7=S7")) N7S7.Checked = true;
            else if (N7str.Equals("N7=S8")) N7S8.Checked = true;
            else if (N7str.Equals("N7=REST")) N7REST.Checked = true;

            //N8
            if (N8str.Equals("N8=S1")) N8S1.Checked = true;
            else if (N8str.Equals("N8=S2")) N8S2.Checked = true;
            else if (N8str.Equals("N8=S3")) N8S3.Checked = true;
            else if (N8str.Equals("N8=S4")) N8S4.Checked = true;
            else if (N8str.Equals("N8=S5")) N8S5.Checked = true;
            else if (N8str.Equals("N8=S6")) N8S6.Checked = true;
            else if (N8str.Equals("N8=S7")) N8S7.Checked = true;
            else if (N8str.Equals("N8=S8")) N8S8.Checked = true;
            else if (N8str.Equals("N8=REST")) N8REST.Checked = true;

            //N9
            if (N9str.Equals("N9=S1")) N9S1.Checked = true;
            else if (N9str.Equals("N9=S2")) N9S2.Checked = true;
            else if (N9str.Equals("N9=S3")) N9S3.Checked = true;
            else if (N9str.Equals("N9=S4")) N9S4.Checked = true;
            else if (N9str.Equals("N9=S5")) N9S5.Checked = true;
            else if (N9str.Equals("N9=S6")) N9S6.Checked = true;
            else if (N9str.Equals("N9=S7")) N9S7.Checked = true;
            else if (N9str.Equals("N9=S8")) N9S8.Checked = true;
            else if (N9str.Equals("N9=REST")) N9REST.Checked = true;

            //N10
            if (N10str.Equals("N10=S1")) N10S1.Checked = true;
            else if (N10str.Equals("N10=S2")) N10S2.Checked = true;
            else if (N10str.Equals("N10=S3")) N10S3.Checked = true;
            else if (N10str.Equals("N10=S4")) N10S4.Checked = true;
            else if (N10str.Equals("N10=S5")) N10S5.Checked = true;
            else if (N10str.Equals("N10=S6")) N10S6.Checked = true;
            else if (N10str.Equals("N10=S7")) N10S7.Checked = true;
            else if (N10str.Equals("N10=S8")) N10S8.Checked = true;
            else if (N10str.Equals("N10=REST")) N10REST.Checked = true;

            //N11
            if (N11str.Equals("N11=S1")) N11S1.Checked = true;
            else if (N11str.Equals("N11=S2")) N11S2.Checked = true;
            else if (N11str.Equals("N11=S3")) N11S3.Checked = true;
            else if (N11str.Equals("N11=S4")) N11S4.Checked = true;
            else if (N11str.Equals("N11=S5")) N11S5.Checked = true;
            else if (N11str.Equals("N11=S6")) N11S6.Checked = true;
            else if (N11str.Equals("N11=S7")) N11S7.Checked = true;
            else if (N11str.Equals("N11=S8")) N11S8.Checked = true;
            else if (N11str.Equals("N11=REST")) N11REST.Checked = true;

            //N12
            if (N12str.Equals("N12=S1")) N12S1.Checked = true;
            else if (N12str.Equals("N12=S2")) N12S2.Checked = true;
            else if (N12str.Equals("N12=S3")) N12S3.Checked = true;
            else if (N12str.Equals("N12=S4")) N12S4.Checked = true;
            else if (N12str.Equals("N12=S5")) N12S5.Checked = true;
            else if (N12str.Equals("N12=S6")) N12S6.Checked = true;
            else if (N12str.Equals("N12=S7")) N12S7.Checked = true;
            else if (N12str.Equals("N12=S8")) N12S8.Checked = true;
            else if (N12str.Equals("N12=REST")) N12REST.Checked = true;

            //N13
            if (N13str.Equals("N13=S1")) N13S1.Checked = true;
            else if (N13str.Equals("N13=S2")) N13S2.Checked = true;
            else if (N13str.Equals("N13=S3")) N13S3.Checked = true;
            else if (N13str.Equals("N13=S4")) N13S4.Checked = true;
            else if (N13str.Equals("N13=S5")) N13S5.Checked = true;
            else if (N13str.Equals("N13=S6")) N13S6.Checked = true;
            else if (N13str.Equals("N13=S7")) N13S7.Checked = true;
            else if (N13str.Equals("N13=S8")) N13S8.Checked = true;
            else if (N13str.Equals("N13=REST")) N13REST.Checked = true;

            //N14
            if (N14str.Equals("N14=S1")) N14S1.Checked = true;
            else if (N14str.Equals("N14=S2")) N14S2.Checked = true;
            else if (N14str.Equals("N14=S3")) N14S3.Checked = true;
            else if (N14str.Equals("N14=S4")) N14S4.Checked = true;
            else if (N14str.Equals("N14=S5")) N14S5.Checked = true;
            else if (N14str.Equals("N14=S6")) N14S6.Checked = true;
            else if (N14str.Equals("N14=S7")) N14S7.Checked = true;
            else if (N14str.Equals("N14=S8")) N14S8.Checked = true;
            else if (N14str.Equals("N14=REST")) N14REST.Checked = true;

            //N15
            if (N15str.Equals("N15=S1")) N15S1.Checked = true;
            else if (N15str.Equals("N15=S2")) N15S2.Checked = true;
            else if (N15str.Equals("N15=S3")) N15S3.Checked = true;
            else if (N15str.Equals("N15=S4")) N15S4.Checked = true;
            else if (N15str.Equals("N15=S5")) N15S5.Checked = true;
            else if (N15str.Equals("N15=S6")) N15S6.Checked = true;
            else if (N15str.Equals("N15=S7")) N15S7.Checked = true;
            else if (N15str.Equals("N15=S8")) N15S8.Checked = true;
            else if (N15str.Equals("N15=REST")) N15REST.Checked = true;

            //N16
            if (N16str.Equals("N16=S1")) N16S1.Checked = true;
            else if (N16str.Equals("N16=S2")) N16S2.Checked = true;
            else if (N16str.Equals("N16=S3")) N16S3.Checked = true;
            else if (N16str.Equals("N16=S4")) N16S4.Checked = true;
            else if (N16str.Equals("N16=S5")) N16S5.Checked = true;
            else if (N16str.Equals("N16=S6")) N16S6.Checked = true;
            else if (N16str.Equals("N16=S7")) N16S7.Checked = true;
            else if (N16str.Equals("N16=S8")) N16S8.Checked = true;
            else if (N16str.Equals("N16=REST")) N16REST.Checked = true;

            inputStream.Close();  //Once we have got all the data from the patch file closes it
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Asks the user do they want to save before exiting the application.
            DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                saveFile(); //if yes the save file method will run again.
                Application.Exit(); //then the application closes.
            }
            else if (result == DialogResult.No)
            {
                Application.Exit(); // If no the application will exit without saving.
            }
            else
            {
                //when the cancel button is pressed it defaults closes the message box and returns to the application.
            }

            
        }
        //When this method is called all the notes will reset to a default rest value.
        private void resetNotes()
        {
            N1REST.Checked = true;
            N2REST.Checked = true;
            N3REST.Checked = true;
            N4REST.Checked = true;
            N5REST.Checked = true;
            N6REST.Checked = true;
            N7REST.Checked = true;
            N8REST.Checked = true;
            N9REST.Checked = true;
            N10REST.Checked = true;
            N11REST.Checked = true;
            N12REST.Checked = true;
            N13REST.Checked = true;
            N14REST.Checked = true;
            N15REST.Checked = true;
            N16REST.Checked = true;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            //Asks the user do they want to save before exiting the application.
            DialogResult result = MessageBox.Show("This will reset all the notes.\nDo you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                saveFile(); //if yes the save file method will run again.
                resetNotes(); // After the save the notes will then be reset
            }
            else if (result == DialogResult.No)
            {
                // If no the application will reset the notes without saving.
                resetNotes();
            }
            else
            {
                //when the cancel button is pressed it defaults closes the message box and returns to the application.
            }
        }

        private void btnPreset1_Click(object sender, EventArgs e)
        {
            //Generates a Random Number between 0-9 but not including 9.
            Random random = new Random();
            int randomNumber = random.Next(0, 9);

            //sets the random number to whatever check box it corresponds to for the first note.
            if (randomNumber == 0) N1S1.Checked = true;
            else if (randomNumber == 1) N1S2.Checked = true;
            else if (randomNumber == 2) N1S3.Checked = true;
            else if (randomNumber == 3) N1S4.Checked = true;
            else if (randomNumber == 4) N1S5.Checked = true; 
            else if (randomNumber == 5) N1S6.Checked = true;
            else if (randomNumber == 6) N1S7.Checked = true;
            else if (randomNumber == 7) N1S8.Checked = true;
            else if (randomNumber == 8) N1REST.Checked = true;
            //Second Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N2S1.Checked = true;
            else if (randomNumber == 1) N2S2.Checked = true;
            else if (randomNumber == 2) N2S3.Checked = true;
            else if (randomNumber == 3) N2S4.Checked = true;
            else if (randomNumber == 4) N2S5.Checked = true;
            else if (randomNumber == 5) N2S6.Checked = true;
            else if (randomNumber == 6) N2S7.Checked = true;
            else if (randomNumber == 7) N2S8.Checked = true;
            else if (randomNumber == 8) N2REST.Checked = true;
            //3rd Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N3S1.Checked = true;
            else if (randomNumber == 1) N3S2.Checked = true;
            else if (randomNumber == 2) N3S3.Checked = true;
            else if (randomNumber == 3) N3S4.Checked = true;
            else if (randomNumber == 4) N3S5.Checked = true;
            else if (randomNumber == 5) N3S6.Checked = true;
            else if (randomNumber == 6) N3S7.Checked = true;
            else if (randomNumber == 7) N3S8.Checked = true;
            else if (randomNumber == 8) N3REST.Checked = true;
            //4th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N4S1.Checked = true;
            else if (randomNumber == 1) N4S2.Checked = true;
            else if (randomNumber == 2) N4S3.Checked = true;
            else if (randomNumber == 3) N4S4.Checked = true;
            else if (randomNumber == 4) N4S5.Checked = true;
            else if (randomNumber == 5) N4S6.Checked = true;
            else if (randomNumber == 6) N4S7.Checked = true;
            else if (randomNumber == 7) N4S8.Checked = true;
            else if (randomNumber == 8) N4REST.Checked = true;
            //5th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N5S1.Checked = true;
            else if (randomNumber == 1) N5S2.Checked = true;
            else if (randomNumber == 2) N5S3.Checked = true;
            else if (randomNumber == 3) N5S4.Checked = true;
            else if (randomNumber == 4) N5S5.Checked = true;
            else if (randomNumber == 5) N5S6.Checked = true;
            else if (randomNumber == 6) N5S7.Checked = true;
            else if (randomNumber == 7) N5S8.Checked = true;
            else if (randomNumber == 8) N5REST.Checked = true;
            //6th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N6S1.Checked = true;
            else if (randomNumber == 1) N6S2.Checked = true;
            else if (randomNumber == 2) N6S3.Checked = true;
            else if (randomNumber == 3) N6S4.Checked = true;
            else if (randomNumber == 4) N6S5.Checked = true;
            else if (randomNumber == 5) N6S6.Checked = true;
            else if (randomNumber == 6) N6S7.Checked = true;
            else if (randomNumber == 7) N6S8.Checked = true;
            else if (randomNumber == 8) N6REST.Checked = true;
            //7th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N7S1.Checked = true;
            else if (randomNumber == 1) N7S2.Checked = true;
            else if (randomNumber == 2) N7S3.Checked = true;
            else if (randomNumber == 3) N7S4.Checked = true;
            else if (randomNumber == 4) N7S5.Checked = true;
            else if (randomNumber == 5) N7S6.Checked = true;
            else if (randomNumber == 6) N7S7.Checked = true;
            else if (randomNumber == 7) N7S8.Checked = true;
            else if (randomNumber == 8) N7REST.Checked = true;
            //8th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N8S1.Checked = true;
            else if (randomNumber == 1) N8S2.Checked = true;
            else if (randomNumber == 2) N8S3.Checked = true;
            else if (randomNumber == 3) N8S4.Checked = true;
            else if (randomNumber == 4) N8S5.Checked = true;
            else if (randomNumber == 5) N8S6.Checked = true;
            else if (randomNumber == 6) N8S7.Checked = true;
            else if (randomNumber == 7) N8S8.Checked = true;
            else if (randomNumber == 8) N8REST.Checked = true;
            //9th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N9S1.Checked = true;
            else if (randomNumber == 1) N9S2.Checked = true;
            else if (randomNumber == 2) N9S3.Checked = true;
            else if (randomNumber == 3) N9S4.Checked = true;
            else if (randomNumber == 4) N9S5.Checked = true;
            else if (randomNumber == 5) N9S6.Checked = true;
            else if (randomNumber == 6) N9S7.Checked = true;
            else if (randomNumber == 7) N9S8.Checked = true;
            else if (randomNumber == 8) N9REST.Checked = true;
            //10th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N10S1.Checked = true;
            else if (randomNumber == 1) N10S2.Checked = true;
            else if (randomNumber == 2) N10S3.Checked = true;
            else if (randomNumber == 3) N10S4.Checked = true;
            else if (randomNumber == 4) N10S5.Checked = true;
            else if (randomNumber == 5) N10S6.Checked = true;
            else if (randomNumber == 6) N10S7.Checked = true;
            else if (randomNumber == 7) N10S8.Checked = true;
            else if (randomNumber == 8) N10REST.Checked = true;
            //11th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N11S1.Checked = true;
            else if (randomNumber == 1) N11S2.Checked = true;
            else if (randomNumber == 2) N11S3.Checked = true;
            else if (randomNumber == 3) N11S4.Checked = true;
            else if (randomNumber == 4) N11S5.Checked = true;
            else if (randomNumber == 5) N11S6.Checked = true;
            else if (randomNumber == 6) N11S7.Checked = true;
            else if (randomNumber == 7) N11S8.Checked = true;
            else if (randomNumber == 8) N11REST.Checked = true;
            //12th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N12S1.Checked = true;
            else if (randomNumber == 1) N12S2.Checked = true;
            else if (randomNumber == 2) N12S3.Checked = true;
            else if (randomNumber == 3) N12S4.Checked = true;
            else if (randomNumber == 4) N12S5.Checked = true;
            else if (randomNumber == 5) N12S6.Checked = true;
            else if (randomNumber == 6) N12S7.Checked = true;
            else if (randomNumber == 7) N12S8.Checked = true;
            else if (randomNumber == 8) N12REST.Checked = true;
            //13th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N13S1.Checked = true;
            else if (randomNumber == 1) N13S2.Checked = true;
            else if (randomNumber == 2) N13S3.Checked = true;
            else if (randomNumber == 3) N13S4.Checked = true;
            else if (randomNumber == 4) N13S5.Checked = true;
            else if (randomNumber == 5) N13S6.Checked = true;
            else if (randomNumber == 6) N13S7.Checked = true;
            else if (randomNumber == 7) N13S8.Checked = true;
            else if (randomNumber == 8) N13REST.Checked = true;
            //14th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N14S1.Checked = true;
            else if (randomNumber == 1) N14S2.Checked = true;
            else if (randomNumber == 2) N14S3.Checked = true;
            else if (randomNumber == 3) N14S4.Checked = true;
            else if (randomNumber == 4) N14S5.Checked = true;
            else if (randomNumber == 5) N14S6.Checked = true;
            else if (randomNumber == 6) N14S7.Checked = true;
            else if (randomNumber == 7) N14S8.Checked = true;
            else if (randomNumber == 8) N14REST.Checked = true;
            //15th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N15S1.Checked = true;
            else if (randomNumber == 1) N15S2.Checked = true;
            else if (randomNumber == 2) N15S3.Checked = true;
            else if (randomNumber == 3) N15S4.Checked = true;
            else if (randomNumber == 4) N15S5.Checked = true;
            else if (randomNumber == 5) N15S6.Checked = true;
            else if (randomNumber == 6) N15S7.Checked = true;
            else if (randomNumber == 7) N15S8.Checked = true;
            else if (randomNumber == 8) N15REST.Checked = true;
            //16th Random Note
            randomNumber = random.Next(0, 9);
            if (randomNumber == 0) N16S1.Checked = true;
            else if (randomNumber == 1) N16S2.Checked = true;
            else if (randomNumber == 2) N16S3.Checked = true;
            else if (randomNumber == 3) N16S4.Checked = true;
            else if (randomNumber == 4) N16S5.Checked = true;
            else if (randomNumber == 5) N16S6.Checked = true;
            else if (randomNumber == 6) N16S7.Checked = true;
            else if (randomNumber == 7) N16S8.Checked = true;
            else if (randomNumber == 8) N16REST.Checked = true;
        }
    }
    }

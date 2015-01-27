using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace wcSilverlight
{
    public partial class MainPage : UserControl
    {

        // global device variables
        public Lapse lapse;
        public List<Control> lockdown;

        public MainPage()
        {
            InitializeComponent();

            lapse = new Lapse();

            // prepare a list of all available devices on the user's system
            lapse.initDevices();

            foreach (Webcam wc in lapse.getWebcams()())
            {
                lbDevices.Items.Add(wc.toString());
            }

            // maintain a list of controls that should be locked while lapse runs
            lockdown = new List<Control>();
            lockdown.Add(tbHours);
            lockdown.Add(tbSeconds);
            lockdown.Add(tbFPS);
            lockdown.Add(tbDelay);
            lockdown.Add(tbDelayStart);
            lockdown.Add(rbIndefinite);
            lockdown.Add(rbTimed);
            lockdown.Add(cbReinit);
            lockdown.Add(tbReinit);
            lockdown.Add(tbImgName);
            lockdown.Add(tbImgNum);

        }

        private void btnStartLapse_Click(object sender, RoutedEventArgs e)
        {

            // create a reference to the button (sender)
            Button snd = (Button)sender;

            // switch on the button's text
            switch (snd.Content.ToString())
            {
                case "Start":

                    lapse.start(Int32.Parse(tbDelayStart.Text), rbTimed.IsChecked, Int32.Parse(tbHours.Text), Int32.Parse(tbDelay.Text), tbImgName.Text, Int32.Parse(tbImgNum.Text), () => tbImgNum);

                    foreach (Control c in lockdown)
                    {
                        c.IsEnabled = false;
                    }

                    // switch the button text for the next time it is pressed
                    snd.Content = "Stop";

                    break;

                case "Stop":

                    // stop the lapse if requested to
                    lapse.stop();

                    // unlock the controls
                    foreach (Control c in lockdown)
                    {
                        c.IsEnabled = true;
                    }

                    // reset the button text
                    btnStartLapse.Content = "Start";

                    break;
            }

        }

        private void allowOnlyNumbers(object sender, int def)
        {

            // get a reference to the sender textbox
            TextBox tbSender = (TextBox)sender;

            // temporarily store the output of TryParse on the textbox text
            int temp;

            if (!(Int32.TryParse(tbSender.Text, out temp) && tbSender.Text != ""))
            {

                // if the text is numeric, go ahead and allow it
                tbSender.Text = def.ToString();

            }

        }

        private void calculatorOptionsTextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {

                // required frames = desired frames per second (fps) * desired output length
                int fps = Int32.Parse(tbFPS.Text);
                int finalDuration = Int32.Parse(tbSeconds.Text);
                int requiredFrames = fps * finalDuration;

                // total length of lapse in seconds = hours * 60 * 60
                int captureDuration = Int32.Parse(tbHours.Text);
                int totalLength = captureDuration * 60 * 60;

                // delay between frames = length of lapse / required number of frames
                int frameDelay = totalLength / requiredFrames;

                // store the caluclated delay
                tbDelay.Text = frameDelay.ToString();

                // using the suggested reinit, camera will be reset each time a half second of "final duration" has been recorded
                int suggestedReinit = (frameDelay * (fps / 5)) / 60;
                tbReinit.Text = suggestedReinit.ToString();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

        // textbox input handling from here on out

        private void cbIndefinate_Click(object sender, RoutedEventArgs e)
        {

            RadioButton sndr = (RadioButton)sender;
            sndr.IsChecked = true;

            if (rbTimed.IsChecked == true)
            {

                // toggle the use of lapse calculator (vs. running indefinitely)
                tbHours.IsEnabled   = true;
                tbSeconds.IsEnabled = true;
                tbFPS.IsEnabled     = true;

                // toggle the interval input
                tbDelay.IsEnabled = false;

            }
            else
            {

                // toggle the use of lapse calculator (vs. running indefinitely)
                tbHours.IsEnabled   = false;
                tbSeconds.IsEnabled = false;
                tbFPS.IsEnabled     = false;

                // toggle the interval input
                tbDelay.IsEnabled = true;

            }

        }

        private void cbReinit_Click(object sender, RoutedEventArgs e)
        {
            tbReinit.IsEnabled = !tbReinit.IsEnabled;
        }

        private void only_numeric(object sender, KeyEventArgs e)
        {
            // prevent key presses that aren't numeric
            if ((e.Key < Key.D0 && e.Key > Key.D9) || (e.Key < Key.NumPad0 && e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        private void lbDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lapse.displayPreview(lbDevices.SelectedItem.ToString(), videoPanel);
            }
            catch (Exception e0)
            {
                System.Diagnostics.Debug.WriteLine(e0);
            }
        }

    }
}

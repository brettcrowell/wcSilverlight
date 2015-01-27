using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;

namespace wcSilverlight
{
    public class Lapse
    {

        private List<Webcam> webcams;
        public VideoBrush videoBrush;
        private DispatcherTimer frameTimer;
        private DispatcherTimer lapseTimer;
        private DispatcherTimer reinitTimer;
        private DispatcherTimer delayStart;
        private DateTime lastImageTime;
        private List<Func<DispatcherTimer>> timers;

        public Lapse()
        {
            // maintain a list of timers.  since they must be passed by reference, we'll use a simple lambda function
            timers = new List<Func<DispatcherTimer>>();
            timers.Add(() => frameTimer);
            timers.Add(() => lapseTimer);
            timers.Add(() => reinitTimer);
            timers.Add(() => delayStart);

            webcams = new List<Webcam>();
        }

        public void addWebcam(Webcam wc)
        {
            webcams.Add(wc);
        }

        public void initDevices()
        {
            foreach (VideoCaptureDevice c in CaptureDeviceConfiguration.GetAvailableVideoCaptureDevices())
            {
                webcams.Add(new Webcam(c.FriendlyName, c));
            }
        }

        public Func<List<Webcam>> getWebcams()
        {
            return () => webcams;
        }

        public void displayPreview(String camName, Rectangle videoPanel)
        {

            // create a new VideoBrush
            videoBrush = null;
            videoBrush = new VideoBrush();
            videoBrush.Stretch = Stretch.Uniform;

            // connect the new VideoBrush to the new device
            foreach (Webcam wc in webcams)
            {
                if (wc.toString().Equals(camName))
                {
                    videoBrush.SetSource(wc.getSrc()());
                    break;
                }
            }

            // connect the VideoPanel to the new VideoBrush
            videoPanel.Fill = videoBrush;

        }

        private int captureImages(String imgName, int imgNum)
        {

            if (webcams.Count > 0)
            {

                try
                {
                    foreach (Webcam wc in webcams)
                    {
                        wc.captureImage(imgName, imgNum);
                    }

                    if (frameTimer != null)
                    {
                        if (frameTimer.IsEnabled == true)
                        {
                            // restart the timer, if necessary
                            frameTimer.Start();
                        }
                    }

                    // increment the counters
                    return imgNum++;

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

            }

            return imgNum;

        }

        private void stopLapse(object sender, EventArgs e)
        {
            // stop the timer(s).  remember that to pass them by reference, we had to use a lambda function
            // so now we need to call that function to get them back again.
            foreach (Func<DispatcherTimer> t in timers)
            {
                if (t() != null)
                {
                    t().Stop();
                }
            }



        }

        private void startLapse(bool? timed, int hours, int reinit, int interval, String imgName, int imgNum, Func<TextBox> tbImgNum)
        {

            if (delayStart != null)
            {
                // stop the delay timer (if it was used)
                delayStart.Stop();
            }

            if (timed == true)
            {
                // if we aren't running forever, use the lapse timer to end after specified number of hours
                lapseTimer = new DispatcherTimer();
                lapseTimer.Interval = new TimeSpan(hours, 0, 0);
                lapseTimer.Tick += new EventHandler(stopLapse);
                lapseTimer.Start();
            }


            if (reinit > 0)
            {
            // if a reinit timer is required, initialize it here
            reinitTimer = new DispatcherTimer();
            reinitTimer.Interval = new TimeSpan(0, reinit, 0);
            reinitTimer.Tick += new EventHandler(delegate(object s0, EventArgs e0)
            {

            foreach (Webcam wc in webcams)
            {
                if (!wc.cameraIsActive(reinit))
                {

                }
            }

            });
            reinitTimer.Start();
            }

            // the frameTimer triggers an image capture at the appropreate number of seconds
            frameTimer = new DispatcherTimer();
            frameTimer.Interval = new TimeSpan(0, 0, 0, interval);
            frameTimer.Tick += new EventHandler(delegate(object s0, EventArgs e0)
            {
                tbImgNum().Text = captureImages(imgName, imgNum).ToString();
            });
            frameTimer.Start();

        }

        public void start(int delay, bool? timed, int hours, int interval, String imgName, int imgNum, Func<TextBox> tbImgNum)
        {

            // the frameTimer triggers an image capture at the appropreate number of seconds
            delayStart = new DispatcherTimer();
            delayStart.Interval = new TimeSpan(0, delay, 0);
            delayStart.Tick += new EventHandler(delegate(object s0, EventArgs e0)
            {
                startLapse(timed, hours, delay, interval, imgName, imgNum, tbImgNum);
            });

            delayStart.Start();

        }

        public void stop()
        {

        }

    }
}

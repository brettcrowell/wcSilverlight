using System;
using System.Linq;
using System.Windows.Media;
using System.IO;
using ImageTools.IO.Jpeg;
using ImageTools;
using System.ComponentModel;

namespace wcSilverlight
{
    public class Webcam
    {

        private VideoCaptureDevice camera;
        private CaptureSource src;
        private String nextImgName;
        private int nextImgNum;
        private DateTime lastImageTaken;

        public Webcam(String name, VideoCaptureDevice cam)
        {
            this.camera = cam;
            this.src = new CaptureSource();
            this.lastImageTaken = DateTime.Now;
            this.nextImgName = "";
            this.nextImgNum = 0;

            try
            {

                // if we found a camera and successfully connected
                if (camera != null)
                {

                    // set the camera to its maximum resolution (allow choice in future?)
                    camera.DesiredFormat = (from VideoFormat f in camera.SupportedFormats
                                       orderby f.PixelWidth * f.PixelHeight descending
                                       select f).FirstOrDefault<VideoFormat>();

                    // reset the preview source
                    CaptureSource source = new CaptureSource();

                    // set the global CaptureSource to the selected camera
                    source.VideoCaptureDevice = camera;
                    source.AudioCaptureDevice = null;

                    // set the callback function for this camera's async capture
                    source.CaptureImageCompleted += new EventHandler<CaptureImageCompletedEventArgs>(_src_srcImageCompleted);

                    this.src = source;
                    src.Start();
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw new Exception("Prepare source failed: " + ex.ToString());
            }

        }

        public String toString()
        {
            return this.camera.FriendlyName;
        }

        public Func<CaptureSource> getSrc()
        {
            return () => src;
        }

        public void start()
        {
            src.Start();
        }

        public void stop()
        {
            src.Stop();
        }

        public bool cameraIsActive(int delay){

            // if the current time is later than the last image plus 2 delays, we need to reconnect
            DateTime nextShotExpected = lastImageTaken.AddSeconds(delay * 2);
            return (DateTime.Now <= nextShotExpected);

        }

        void saveImage(ImageTools.Image image, String filename, int num)
        {

            // get a path to save pictures
            String myPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // create a new directory at that path
            DirectoryInfo lapseDir = Directory.CreateDirectory(myPictures + "\\" + filename + "\\" + camera.FriendlyName);

            // save the picture
            using (Stream stream = File.Create(lapseDir.FullName + "\\" + filename + num + ".jpg"))
            {

                // Declare jpeg encoder
                var encoder = new JpegEncoder();

                // Set the image quality
                encoder.Quality = 90;

                // Finally encode raw bitmap and save it as a jpg image
                encoder.Encode(image, stream);

                // Close the stream
                stream.Close();
            }

        }

        // callback function for _src.CaptureImageAsync() which saves the captured image
        void _src_srcImageCompleted(object sender, CaptureImageCompletedEventArgs e)
        {

            // create a BackgroundWorker to asyncronously handle the saving of this image
            BackgroundWorker b1 = new BackgroundWorker();

            // Convert raw captured bitmap to the image that Image Tools understand with the extension method
            ImageTools.Image bmp = e.Result.ToImage();

            // assign the BackgroundWorker to SaveImage and use a delegate to pass arguments
            b1.DoWork += delegate(object s, DoWorkEventArgs ev)
            {
                saveImage(bmp, nextImgName, nextImgNum);
            };

            // when the worker completes, update the webcam with the current time
            b1.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs r)
            {
                lastImageTaken = DateTime.Now;
            };

            // run background worker
            b1.RunWorkerAsync();

        }

        public void captureImage(String imgName, int imgNum){

            this.nextImgName = imgName;
            this.nextImgNum = imgNum;

            src.CaptureImageAsync();

        }

        public void kill()
        {
            camera = null;
            try
            {
                src.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            src = null;
        }

    }
}

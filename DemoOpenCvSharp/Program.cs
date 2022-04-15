using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenCvSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //read image
            OpenCvSharp.Mat imgSrc = new OpenCvSharp.Mat();
            string srcPath = @"C:\Users\thanh\Desktop\Image\dong_xu.jpg";
            if(!System.IO.File.Exists(srcPath))
            {
                Console.WriteLine($"{srcPath} is not exist");
                return;
            }
            imgSrc = OpenCvSharp.Cv2.ImRead(srcPath, OpenCvSharp.ImreadModes.Color);
            if(imgSrc == null)
            {
                Console.WriteLine($"Can't read image from  file {srcPath}");
                return;
            }
            OpenCvSharp.Mat imgDraw = imgSrc.Clone();

            //convert to gray
            OpenCvSharp.Mat imgGray = new OpenCvSharp.Mat();
            OpenCvSharp.Cv2.CvtColor(imgSrc, imgGray, OpenCvSharp.ColorConversionCodes.BGR2GRAY);
            string grayPath = @"C:\Users\thanh\Desktop\Image\dong_xu_gray.jpg";
            if (System.IO.File.Exists(grayPath))
            {
                System.IO.File.Delete(grayPath);
            }
            OpenCvSharp.Cv2.ImWrite(grayPath, imgGray);

            //threshold
            OpenCvSharp.Mat imgThreshold = new OpenCvSharp.Mat();
            double dThresh = 200;
            OpenCvSharp.Cv2.Threshold(imgGray, imgThreshold, dThresh, 255, OpenCvSharp.ThresholdTypes.Binary);
            string thresholdPath = @"C:\Users\thanh\Desktop\Image\dong_xu_threshold.jpg";
            if (System.IO.File.Exists(thresholdPath))
            {
                System.IO.File.Delete(thresholdPath);
            }
            OpenCvSharp.Cv2.ImWrite(thresholdPath, imgThreshold);

            //find countour

            OpenCvSharp.Cv2.FindContours(imgThreshold, out OpenCvSharp.Point[][] contours, out OpenCvSharp.HierarchyIndex[] hierarchy, OpenCvSharp.RetrievalModes.CComp,
                OpenCvSharp.ContourApproximationModes.ApproxSimple); ;

            OpenCvSharp.Scalar color = new OpenCvSharp.Scalar(0, 0, 255);//(b,g,r)
            int thickness = 3;
            foreach (var item in contours)
            {
                var rect = OpenCvSharp.Cv2.BoundingRect(item);
                if(rect.Width > 10 && rect.Width < 80) //loc kich thuoc mong muon
                {
                    OpenCvSharp.Cv2.Rectangle(imgDraw, rect, color, thickness);
                }
            }

            //save image drawing

            string drawingPath = @"C:\Users\thanh\Desktop\Image\dong_xu_drawing.jpg";
            if(System.IO.File.Exists(drawingPath))
            {
                System.IO.File.Delete(drawingPath);
            }
            OpenCvSharp.Cv2.ImWrite(drawingPath, imgDraw);
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}

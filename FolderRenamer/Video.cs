using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderRenamer
{
 

    public class VideoModel
    {
        public string FilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public VideoModel VideoInfo(string fileName)
        {
            try
            {
                VideoModel videoModel = new VideoModel();
                MediaFile mediaFile = new MediaFile { Filename = fileName };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(mediaFile);
                }
                try
                {
                    if (mediaFile.Metadata != null && mediaFile.Metadata.VideoData != null && mediaFile.Metadata.VideoData.FrameSize != null)
                    {
                        string[] dimensions = mediaFile.Metadata.VideoData.FrameSize.Split('x');
                        videoModel.FilePath = fileName;
                        videoModel.Width = Int32.Parse(dimensions[0]);
                        videoModel.Height = Int32.Parse(dimensions[1]);

                        return videoModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch { return null; }
            }
            catch (Exception)
            {
                return null;            
            }
        }
    }
}

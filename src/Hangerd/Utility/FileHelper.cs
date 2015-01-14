namespace Hangerd.Utility
{
	using Hangerd.Components;
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Imaging;
	using System.IO;

	public class FileHelper
	{
		public static void Upload(Stream stream, string physicalPath, string fileName)
		{
			const int bufferSize = 256;

			var buffer = new byte[bufferSize];

			if (!Directory.Exists(physicalPath))
			{
				Directory.CreateDirectory(physicalPath);
			}

			var fileStream = File.Create(Path.Combine(physicalPath, fileName), bufferSize);

			try
			{
				var bytesRead = 0;

				do
				{
					bytesRead = stream.Read(buffer, 0, bufferSize);

					if (bytesRead > 0)
					{
						fileStream.Write(buffer, 0, bytesRead);
					}
				} while (bytesRead > 0);
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception(ex);
			}
			finally
			{
				stream.Close();
				fileStream.Close();
			}
		}

		public static void MakeThumbnail(string originalPath, string thumbnailPath, int width, int height, ThumbnailMode mode)
		{
			var originalImage = Image.FromFile(originalPath);

			var towidth = width;
			var toheight = height;

			var ow = originalImage.Width;
			var oh = originalImage.Height;
			var x = 0;
			var y = 0;

			switch (mode)
			{
				case ThumbnailMode.HeightWidth:
					break;
				case ThumbnailMode.Width:
					if (originalImage.Width > width)
					{
						toheight = originalImage.Height * width / originalImage.Width;
					}
					else
					{
						towidth = originalImage.Width;
						toheight = originalImage.Height;
					}
					break;
				case ThumbnailMode.Height:
					if (originalImage.Height > height)
					{
						towidth = originalImage.Width * height / originalImage.Height;
					}
					else
					{
						towidth = originalImage.Width;
						toheight = originalImage.Height;
					}
					break;
				case ThumbnailMode.Cut:
					if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
					{
						oh = originalImage.Height;
						ow = originalImage.Height * towidth / toheight;
						y = 0;
						x = (originalImage.Width - ow) / 2;
					}
					else
					{
						ow = originalImage.Width;
						oh = originalImage.Width * height / towidth;
						x = 0;
						y = (originalImage.Height - oh) / 2;
					}
					break;
			}

			var bitmap = new Bitmap(towidth, toheight);
			var graphics = Graphics.FromImage(bitmap);

			graphics.InterpolationMode = InterpolationMode.High;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(Color.Transparent);

			graphics.DrawImage(
				originalImage,
				new Rectangle(0, 0, towidth, toheight),
				new Rectangle(x, y, ow, oh),
				GraphicsUnit.Pixel);

			try
			{
				bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception(ex);
			}
			finally
			{
				originalImage.Dispose();
				bitmap.Dispose();
				graphics.Dispose();
			}
		}
	}

	public enum ThumbnailMode
	{
		HeightWidth = 1,

		Width = 2,

		Height = 3,

		Cut = 4
	}
}

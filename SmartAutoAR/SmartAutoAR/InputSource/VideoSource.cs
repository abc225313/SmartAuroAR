using System.Drawing;

namespace SmartAutoAR.InputSource
{
	/// <summary>
	/// 處理影像來源為「影片」的類別
	/// </summary>
	public class VideoSource : IInputSource
	{
		public Bitmap GetInputFrame()
		{
			return new Bitmap(0,0);
		}
	}
}

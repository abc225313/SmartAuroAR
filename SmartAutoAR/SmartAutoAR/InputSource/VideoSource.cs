using System.Drawing;

namespace SmartAutoAR.InputSource
{
	// 處理影像來源為 影片 的類別
	public class VideoSource : IInputSource
	{
		// 取得輸入影像
		public Bitmap GetInputFrame()
		{
			return new Bitmap(0,0);
		}
	}
}

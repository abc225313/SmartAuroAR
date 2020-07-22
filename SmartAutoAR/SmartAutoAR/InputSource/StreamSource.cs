using System.Drawing;

namespace SmartAutoAR.InputSource
{
	// 處理影像來源為 串流 的類別
	public class StreamSource : IInputSource
	{
		// 取得輸入影像
		public Bitmap GetInputFrame()
		{
			return new Bitmap(0, 0);
		}
	}
}

using System.Drawing;

namespace SmartAutoAR.InputSource
{
	// 此介面定義了影像來源
	public interface IInputSource
	{
		// 透過此函數可以取得當下應該處理的影像
		public Bitmap GetInputFrame();
	}
}

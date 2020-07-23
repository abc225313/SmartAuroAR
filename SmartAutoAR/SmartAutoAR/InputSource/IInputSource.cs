using System.Drawing;

namespace SmartAutoAR.InputSource
{
	/// <summary>
	/// 此介面定義了影像來源的行為
	/// </summary>
	public interface IInputSource
	{
		public Bitmap GetInputFrame();
	}
}

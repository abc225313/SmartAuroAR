using System.Drawing;

namespace SmartAutoAR.InputSource
{
	// 處理影像來源為 圖片 的類別
	public class ImageSource : IInputSource
	{
		// 儲存圖片的屬性
		public Bitmap Image { get; set; }
		
		// 直接以 bitmap 初始化
		public ImageSource(Bitmap bitmap)
		{
			this.Image = bitmap;
		}

		// 以檔案初始化
		public ImageSource(string file)
		{
			this.Image = new Bitmap(file);
		}

		// 取得輸入影像
		public Bitmap GetInputFrame()
		{
			return this.Image;
		}
	}
}

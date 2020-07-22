using Assimp;
using SmartAutoAR.InputSource;
using System.Drawing;

namespace SmartAutoAR
{
	/// <summary>
	/// 用於存放製作一組AR影像時需要的資料
	/// </summary>
	public class ArParam
	{
		public IInputSource InputSource { get; set; }
		public Bitmap Marker { get; set; }
	}
}

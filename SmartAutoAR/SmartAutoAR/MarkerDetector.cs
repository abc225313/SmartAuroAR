using OpenTK;
using Bitmap = System.Drawing.Bitmap;

namespace SmartAutoAR
{
	/// <summary>
	/// 用於偵測與分析 Marker 的類別
	/// </summary>
	public class MarkerDetector
	{
		public bool Validity { get; protected set; }
		public Matrix4 ViewMatrix { get; protected set; }
		public Vector3 CameraPosition { get; protected set; }
		public Bitmap DetectedMarker { get; protected set; }

		public bool Detecte(Bitmap frame, Bitmap marker)
		{
			bool result = true;
			this.Validity = result;
			return result;
		}
	}
}

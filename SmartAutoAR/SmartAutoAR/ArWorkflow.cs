using System.Drawing;
using SmartAutoAR.InputSource;
using System.Collections.Generic;

namespace SmartAutoAR
{
	/// <summary>
	/// 整合各種功能，能夠快速製作AR的類別
	/// </summary>
	public class ArWorkflow
	{
		public IInputSource InputSource { get; set; }
		public Dictionary<Bitmap, IScene> MarkerPairs { get; set; }
		public MarkerDetector MarkerDetector { get; protected set; }

		public ArWorkflow(IInputSource inputSource)
		{
			this.InputSource = inputSource;
			this.MarkerPairs = new Dictionary<Bitmap, IScene>();
			this.MarkerDetector = new MarkerDetector();
		}

		public void DoWork()
		{
			Bitmap frame = InputSource.GetInputFrame();
			foreach (Bitmap marker in MarkerPairs.Keys)
			{
				if (MarkerDetector.Detecte(frame, marker))
				{
					// 偵測到 marker
					MarkerPairs[marker].SetViewMatrix(MarkerDetector.ViewMatrix);
					MarkerPairs[marker].Render();
				}
				else
				{
					// 沒有偵測到 marker
				}
			}
		}
	}
}

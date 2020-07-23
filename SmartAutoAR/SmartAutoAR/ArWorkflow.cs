using System.Drawing;
using SmartAutoAR.InputSource;
using System.Collections.Generic;
using SmartAutoAR.VirtualObject;

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
		public double AspectRatio { get => background.AspectRatio; }


		protected Background background;

		public ArWorkflow(IInputSource inputSource)
		{
			InputSource = inputSource;
			MarkerPairs = new Dictionary<Bitmap, IScene>();
			MarkerDetector = new MarkerDetector();
			background = new Background();
		}

		public void DoWork()
		{
			Bitmap frame = InputSource.GetInputFrame();
			background.SetImage(frame);
			background.Render();
			foreach (Bitmap marker in MarkerPairs.Keys)
			{
				if (MarkerDetector.Detecte(frame, marker))
				{
					// 偵測到 marker
					MarkerPairs[marker].Camera.Update(MarkerDetector.ViewMatrix, MarkerDetector.CameraPosition);
					MarkerPairs[marker].Render();
				}
			}
		}
	}
}

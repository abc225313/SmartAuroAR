using OpenTK;
using OpenTK.Graphics;
using SmartAutoAR;
using SmartAutoAR.InputSource;
using System;
using Bitmap = System.Drawing.Bitmap;

namespace Debug
{
	public partial class ArForm : GameWindow
	{
		ImageSource inputSource;
		ArWorkflow workflow;
		Bitmap marker;
		Scene scene;

		public ArForm(int width, int height, string title) :
			base(width, height,
				GraphicsMode.Default,
				title,
				GameWindowFlags.Default,
				DisplayDevice.Default,
				4, 5,
				GraphicsContextFlags.ForwardCompatible)
		{ }

		protected override void OnLoad(EventArgs e)
		{
			// 設定影像輸入
			inputSource = new ImageSource("圖片.jpg");

			// 建立 workflow 物件
			workflow = new ArWorkflow(inputSource);

			// 導入 marker圖像
			marker = new Bitmap("marker.jpg");

			// 設定場景
			scene = new Scene();
			// ...

			// 設定 marker 對應的 scene
			workflow.MarkerPairs[marker] = scene;

			// 啟用需要的擬真方法
			// LightSourceSimulation = true;
			// ColorTransfer = true;

			base.OnLoad(e);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			workflow.DoWork();
			SwapBuffers();

			base.OnRenderFrame(e);
		}

		protected override void OnResize(EventArgs e)
		{
			scene.Resize(Width, Height);

			base.OnResize(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			scene.Dispose();

			base.OnUnload(e);
		}
	}
}

using OpenTK;
using OpenTK.Graphics;
using SmartAutoAR;
using SmartAutoAR.InputSource;
using SmartAutoAR.VirtualObject;
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
			inputSource = new ImageSource(@"background.jpg");

			// 建立 workflow 物件
			workflow = new ArWorkflow(inputSource);

			// 導入 marker圖像
			marker = new Bitmap("marker.jpg");

			// 設定場景
			scene = new Scene();

			// 載入模型
			Model coin_model = Model.LoadModel(@"..\..\..\models\ChineseCoin\chinese_coin.obj");
			coin_model.Move(z: -40);
			coin_model.Resize(0.5f);
			scene.Models.Add(coin_model);

			// 設定 marker 對應的 scene
			workflow.MarkerPairs[marker] = scene;

			// 啟用需要的擬真方法
			// LightSourceSimulation = true;
			// ColorTransfer = true;

			base.OnLoad(e);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			// 對下一幀做處理，包含偵測、渲染、擬真
			workflow.DoWork();

			// 根據輸入影像長寬比改變視窗大小
			Height = (int)(Width * workflow.AspectRatio);

			// 針對視窗本身做繪製
			SwapBuffers();

			base.OnRenderFrame(e);
		}

		protected override void OnResize(EventArgs e)
		{
			// 改變攝影機的長寬比
			workflow.SetWindowSize(Width, Height);

			base.OnResize(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			scene.Dispose();

			base.OnUnload(e);
		}
	}
}

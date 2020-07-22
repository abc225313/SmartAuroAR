using OpenTK;
using System;

namespace SmartAutoAR
{
	/// <summary>
	/// 定義場景功能的介面
	/// </summary>
	public interface IScene :IDisposable
	{
		public void SetViewMatrix(Matrix4 viewMatrix);
		public void Render();
		public void Resize(int width, int height);
	}
}

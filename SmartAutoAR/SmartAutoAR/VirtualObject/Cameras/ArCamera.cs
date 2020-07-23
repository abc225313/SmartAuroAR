using System;
using OpenTK;

namespace SmartAutoAR.VirtualObject.Cameras
{
	class ArCamera : ICamera
	{
		public float AspectRatio { get; set; }
		public Matrix4 ViewMatrix { get; set; }
		public Matrix4 ProjectionMatrix { 
			get { return Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.01f, 10000f); } 
		}
		public Vector3 Position { get; protected set; }

		public void Update(Matrix4 matrix, Vector3 position)
		{
			ViewMatrix = matrix;
			Position = position;
		}
	}
}

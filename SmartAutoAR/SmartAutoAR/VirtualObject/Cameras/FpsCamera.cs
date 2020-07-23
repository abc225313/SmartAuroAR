using System;
using Assimp;
using OpenTK;

namespace SmartAutoAR.VirtualObject.Cameras
{
    public class FpsCamera : ICamera
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;

        public FpsCamera(float aspectRatio)
        {
            Position = Vector3.UnitZ * 3;
            AspectRatio = aspectRatio;
        }

        public Vector3 Position { get; set; }

        public float AspectRatio { get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Matrix4 ViewMatrix { get { return Matrix4.LookAt(Position, Position + _front, _up); } }
        public Matrix4 ProjectionMatrix { get { return Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.01f, 10000f); }}

        //public Vector3 Right => _right;

        // We convert from degrees to radians as soon as the property is set to improve performance
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
                // of weird "bugs" when you are using euler angles for rotation.
                // If you want to read more about this you can try researching a topic called gimbal lock
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        // We convert from degrees to radians as soon as the property is set to improve performance
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

		public void Update(Matrix4 matrix, Vector3 position)
		{

		}

		// This function is going to update the direction vertices using some of the math learned in the web tutorials
		private void UpdateVectors()
        {
            // First the front matrix is calculated using some basic trigonometry
            _front.X = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            _front.Y = (float)Math.Sin(_pitch);
            _front.Z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            // We need to make sure the vectors are all normalized, as otherwise we would get some funky results
            _front = Vector3.Normalize(_front);
        }


    }
}

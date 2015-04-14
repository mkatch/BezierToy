using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierToy
{
    public struct Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static implicit operator PointF(Vector2D v)
        {
            return new PointF((float) v.X, (float) v.Y);
        }

        public static implicit operator Vector2D(PointF p)
        {
            return new Vector2D(p.X, p.Y);
        }

        public double LengthSq()
        {
            return X * X + Y * Y;
        }

        public Vector2D(double x, double y) : this()
        {
            X = x; Y = y;
        }

        public static Vector2D operator +(Vector2D u, Vector2D v)
        {
            return new Vector2D(u.X + v.X, u.Y + v.Y);
        }

        public static Vector2D operator -(Vector2D u, Vector2D v)
        {
            return new Vector2D(u.X - v.X, u.Y - v.Y);
        }

        public static Vector2D operator *(double s, Vector2D v)
        {
            return new Vector2D(s * v.X, s * v.Y);
        }

        public static Vector2D operator *(Vector2D v, double s)
        {
            return new Vector2D(v.X * s, v.Y * s);
        }

        public static Vector2D operator /(Vector2D v, double s)
        {
            return new Vector2D(v.X / s, v.Y / s);
        }

        public static bool operator ==(Vector2D u, Vector2D v)
        {
            return u.X == v.X && u.Y == v.Y;
        }

        public static bool operator !=(Vector2D u, Vector2D v)
        {
            return u.X != v.X || u.Y != v.Y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2D)) return false;
            return this == (Vector2D) obj;
        }

        public override int GetHashCode()
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(X), 0)
                ^ BitConverter.ToInt32(BitConverter.GetBytes(Y), 0);
        }

        public override string ToString()
        {
            return string.Format("[ {0}, {1} ]", X, Y);
        }
    }
}
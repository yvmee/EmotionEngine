using System;
using Unity.Mathematics;
using UnityEngine;

namespace EmotionEngine
{
    public class Vector6
    {
        public double x;
        public double y;
        public double z;
        public double u;
        public double v;
        public double w;

        public static readonly Vector6 Zero = new Vector6(0, 0, 0, 0, 0, 0);

        public Vector6(double x, double y, double z, double u, double v, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.u = u;
            this.v = v;
            this.w = w;
        }
        
        public Vector6()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this.u = 0;
            this.v = 0;
            this.w = 0;
        }

        public Vector6 Multiply(double scalar)
        {
            this.x *= scalar;
            this.y *= scalar;
            this.z *= scalar;
            this.u *= scalar;
            this.v *= scalar;
            this.w *= scalar;
            return this;
        }

        public Vector6 Add(Vector6 vector6)
        {
            this.x += vector6.x;
            this.y += vector6.y;
            this.z += vector6.z;
            this.u += vector6.u;
            this.v += vector6.v;
            this.w += vector6.w;
            return this;
        }
        
        public Vector6 Subtract(Vector6 vector6)
        {
            this.x -= vector6.x;
            this.y -= vector6.y;
            this.z -= vector6.z;
            this.u -= vector6.u;
            this.v -= vector6.v;
            this.w -= vector6.w;
            return this;
        }

        public Vector6 Copy()
        {
            return new Vector6(x, y, z, u, v, w);
        }

        public void Normalize()
        {
            double length = Magnitude();
            x /= Magnitude();
            y /= Magnitude();
            z /= Magnitude();
            u /= Magnitude();
            v /= Magnitude();
            w /= Magnitude();
        }

        public double Magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z + 
                                     u * u + v * v + w * w);
        }

        public override string ToString()
        {
            return "[" + x + "," + y + "," + z + "," + u + "," + v + "," + w + "]";
        }
        
    }
}

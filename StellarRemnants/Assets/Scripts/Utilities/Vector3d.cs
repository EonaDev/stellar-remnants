using System;
using UnityEngine;

public struct Vector3d : IEquatable<Vector3d> {
    public static Vector3d zero = new Vector3d(0.0);
    public static Vector3d one = new Vector3d(1.0);
    public static Vector3d up = new Vector3d(0.0, 1.0, 0.0);
    public static Vector3d down = new Vector3d(0.0, -1.0, 0.0);
    public static Vector3d right = new Vector3d(1.0, 0.0, 0.0);
    public static Vector3d left = new Vector3d(-1.0, 0.0, 0.0);
    public static Vector3d forward = new Vector3d(0.0, 0.0, 1.0);
    public static Vector3d back = new Vector3d(0.0, 0.0, -1.0);
    public static Vector3d negativeInfinity = new Vector3d(double.NegativeInfinity);
    public static Vector3d positiveInfinity = new Vector3d(double.PositiveInfinity);

    public double x;
    public double y;
    public double z;

    public Vector3d(double unit) {
        this.x = unit;
        this.y = unit;
        this.z = unit;
    }

    public Vector3d(double x, double y) {
        this.x = x;
        this.y = y;
        this.z = 0;
    }

    public Vector3d(double x = 0.0, double y = 0.0, double z = 0.0) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double magnitude() {
        return Math.Sqrt(magnitudeSqr());
    }

    public double magnitudeSqr() {
        return x*x + y*y + z*z;
    }

    public Vector3d Scale(double scale) {
        return new Vector3d(x*scale, y*scale, z*scale);
    }

    public override bool Equals(object other) {
        // TODO
        return false;
    }
    public bool Equals(Vector3d other) {
        return x == other.x && y == other.y && z == other.z;
    }
    public override int GetHashCode() {
        return 0; // TODO
    }

    public static Vector3d operator + (Vector3d a, Vector3d b) {
        return new Vector3d(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3d operator - (Vector3d a, Vector3d b) {
        return new Vector3d(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3d operator - (Vector3d a) {
        return new Vector3d(-a.x, -a.y, -a.z);
    }

    public static Vector3d operator - (Vector3d a, Vector3 b) {
        return new Vector3d(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3d operator * (Vector3d a, double d) {
        return a.Scale(d);
    }

    public static Vector3d operator * (double d, Vector3d a) {
        return a.Scale(d);
    }

    public static Vector3d operator / (Vector3d a, double d) {
        return new Vector3d(a.x/d, a.y/d, a.z/d);
    }

    public static bool operator == (Vector3d lhs, Vector3d rhs) {
        return lhs.Equals(rhs);
    }

    public static bool operator != (Vector3d lhs, Vector3d rhs) {
        return !lhs.Equals(rhs);
    }
}
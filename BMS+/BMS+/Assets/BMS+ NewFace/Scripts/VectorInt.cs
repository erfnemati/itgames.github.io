using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public struct VectorInt
{
    public int a;
    public int b;
    public int c;
    public int d;
    public int e;
    public int f; 
    public static VectorInt White = new VectorInt(0, 0, 0, 0, 0, 0);
    public static VectorInt Jammed = new VectorInt(0, 0, 0, 0, 0, 1);
    public static VectorInt Red = new VectorInt(1, 0, 0, 0, 0, 0);
    public static VectorInt Blue = new VectorInt(0, 1, 0, 0, 0, 0);
    public static VectorInt Green = new VectorInt(0, 0, 1, 0, 0, 0);

    
    public VectorInt(int x,int y, int z,int m,int n, int t)
    {
        this.a = x; this.b = y; this.c = z; this.d = m; this.e = n; this.f = t;
    }
    public static VectorInt GetLerpValue(VectorInt x)
    {
        return new VectorInt(Math.Min(x.a , 1), Math.Min(x.b, 1), Math.Min(x.c, 1), Math.Min(x.d, 1)
    , Math.Min(x.e, 1), Math.Min(x.f, 1));
    }

    public static VectorInt operator +(VectorInt x, VectorInt y)
    {
        return new VectorInt(x.a + y.a,x.b + y.b, x.c + y.c, x.d + y.d
            , x.e + y.e, x.f + y.f);
    }
    public static VectorInt operator -(VectorInt x, VectorInt y)
    {
        return new VectorInt(Math.Max(x.a - y.a, 0), Math.Max(x.b - y.b, 0), Math.Max(x.c - y.c, 0), Math.Max(x.d - y.d, 0)
            , Math.Max(x.e - y.e, 0), Math.Max(x.f - y.f, 0));
    }
    public static bool operator ==(VectorInt x, VectorInt y)
    {
        if (x.a == y.a && x.b == y.b && x.c == y.c && x.d == y.d && x.e == y.e && x.f == y.f)
            return true;
        else 
            return false;

    }   
    public static bool operator !=(VectorInt x, VectorInt y)
    {
        if (x.a == y.a && x.b == y.b && x.c == y.c && x.d == y.d && x.e == y.e && x.f == y.f)
            return false;
        else
            return true;

    }
    public override string ToString()
    {
        return $"VectorInt{{a={this.a}, b={this.b}, c={this.c}, d={this.d}, e={this.e}, f={this.f}}}";
    }
}

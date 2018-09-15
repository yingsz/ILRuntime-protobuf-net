//适配文件放到主程序中
using System;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.CLR.Method;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class ProtobufAdaptor : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return null;
        }
    }

    public override Type[] BaseCLRTypes
    {
        get
        {
            return new Type[] { typeof(IEnumerable),typeof(IEnumerable<object>), typeof(IComparer), typeof(IEqualityComparer<System.Object>),
                typeof(IComparable),
                typeof(IEquatable<ILTypeInstance>), typeof(IComparable<ILTypeInstance>), typeof(IEnumerable<System.Byte>) };
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    internal class Adaptor : IEnumerable, IEnumerable<object>, IComparer, IComparable, IEqualityComparer<System.Object>, IEquatable<ILTypeInstance>, IComparable<ILTypeInstance>, IEnumerable<System.Byte>, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public object[] data1 = new object[1];
        public object[] data2 = new object[2];
        public ILTypeInstance ILInstance { get { return instance; } }

        IMethod mEquals = null;
        bool mEqualsGot = false;
        public bool Equals(ILTypeInstance other)
        {
            if (!mEqualsGot)
            {
                mEquals = instance.Type.GetMethod("Equals", 1);
                if (mEquals == null)
                {
                    mEquals = instance.Type.GetMethod("System.IEquatable.Equals", 1);
                }
                mEqualsGot = true;
            }
            if (mEquals != null)
            {
                data1[0] = other;
                return (bool)appdomain.Invoke(mEquals, instance, data1);
            }
            return false;
        }

        IMethod mCompareTo = null;
        bool mCompareToGot = false;
        public int CompareTo(ILTypeInstance other)
        {
            if (!mCompareToGot)
            {
                mCompareTo = instance.Type.GetMethod("CompareTo", 1);
                if (mCompareTo == null)
                {
                    mCompareTo = instance.Type.GetMethod("System.IComparable.CompareTo", 1);
                }
                mCompareToGot = true;
            }
            if (mCompareTo != null)
            {
                data1[0] = other;
                var res = (int)appdomain.Invoke(mCompareTo, instance, data1);
                data1[0] = null;
                return res;
            }
            return 0;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetEnumerator", 0);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.IEnumerable.GetEnumerator", 0);
            }
            if (method != null)
            {
                var res = appdomain.Invoke(method, instance, null);
                return (IEnumerator<byte>)res;
            }
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetEnumerator", 0);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.IEnumerable.GetEnumerator", 0);
            }
            if (method != null)
            {
                var res = appdomain.Invoke(method, instance, null);
                return (IEnumerator)res;
            }
            return null;
        }

        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            IMethod method = null;
            method = instance.Type.GetMethod("Equals", 2);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.Generic.IEqualityComparer.Equals", 2);
            }
            if (method != null)
            {
                data2[0] = x;
                data2[1] = y;
                var res = appdomain.Invoke(method, instance, data2);
                data2[0] = null;
                data2[1] = null;
                return (bool)res;
            }
            return false;
        }

        int IEqualityComparer<object>.GetHashCode(object obj)
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetHashCode", 1);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.Generic.IEqualityComparer.GetHashCode", 1);
            }
            if (method != null)
            {
                data1[0] = obj;
                var res = appdomain.Invoke(method, instance, data1);
                data1[0] = null;
                return (int)res;
            }
            return 0;
        }

        int IComparer.Compare(object x, object y)
        {
            IMethod method = null;
            method = instance.Type.GetMethod("Compare", 2);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.IComparer.Compare", 2);
            }
            if (method != null)
            {
                data2[0] = x;
                data2[1] = y;
                var res = appdomain.Invoke(method, instance, data2);
                data2[0] = null;
                data2[1] = null;
                return (int)res;
            }
            return 0;
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetEnumerator", 0);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.IEnumerable.GetEnumerator", 0);
            }
            if (method != null)
            {
                var res = appdomain.Invoke(method, instance, null);
                return (IEnumerator<object>)res;
            }
            return null;
        }

        int IComparable.CompareTo(object obj)
        {
            IMethod method = null;
            method = instance.Type.GetMethod("CompareTo", 1);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.IComparable.CompareTo", 1);
            }
            if (method != null)
            {
                data1[0] = obj;
                var res = appdomain.Invoke(method, instance, data1);
                return (int)res;
            }
            return 0;
        }
    }
}
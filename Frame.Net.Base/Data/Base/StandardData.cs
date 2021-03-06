using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace EFFC.Frame.Net.Base.Data
{
    

    public abstract class StandardData<T> :ICloneable
    {
        private T _v = default(T);

        public T Value
        {
            get
            {
                if (_v == null)
                {
                    if (typeof(T) == typeof(string))
                    {
                        _v = default(T);
                    }
                    else if (typeof(T) == typeof(Regex))
                    {
                        _v = default(T);
                    }
                    else
                    {
                        _v = System.Activator.CreateInstance<T>();
                    }
                }

                return _v;
                
            }
            set { _v = value; }
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is StandardData<int>)
                {
                    return this.Value.Equals(((StandardData<int>)obj).Value);
                }
                else if (obj is StandardData<float>)
                {
                    return this.Value.Equals(((StandardData<float>)obj).Value);
                }
                else if (obj is StandardData<double>)
                {
                    return this.Value.Equals(((StandardData<double>)obj).Value);
                }
                else if (obj is StandardData<string>)
                {
                    return this.Value.Equals(((StandardData<string>)obj).Value);
                }
                else if (obj is StandardData<DateTime>)
                {
                    return this.Value.Equals(((StandardData<DateTime>)obj).Value);
                }
                else
                {
                    return this.Value.Equals(obj);
                }
            }
            else
            {
                return false;
            }
        }

        public virtual object Clone()
        {
            var rtn = Activator.CreateInstance(this.GetType());
            if (_v is ICloneable)
            {
                typeof(T).GetField("_v").SetValue(rtn, ((ICloneable)_v).Clone());
            }
            else
            {
                typeof(T).GetField("_v").SetValue(rtn, _v);
            }

            return rtn;
        }
    }
}

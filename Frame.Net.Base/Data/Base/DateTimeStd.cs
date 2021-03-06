using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace EFFC.Frame.Net.Base.Data
{
    public class DateTimeStd : StandardData<DateTime>
    {
        /// <summary>
        /// Constuctor
        /// </summary>
        public DateTimeStd()
        {
        }
        /// <summary>
        /// COnstuctor
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public DateTimeStd(int year, int month, int day)
        {
            this.Value = new DateTime(year, month, day);
        }
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public DateTimeStd(int year, int month, int day, int hour, int minute, int second)
        {
            this.Value = new DateTime(year, month, day, hour, minute, second);
        }
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        public DateTimeStd(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            this.Value = new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="o"></param>
        public DateTimeStd(DateTime o)
        {
            this.Value = o;
        }
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="o"></param>
        public DateTimeStd(DateTime? o)
        {
            if (o != null)
                this.Value = o.Value;
        }
        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTimeStd Now
        {
            get { return DateTime.Now; }
        }
        /// <summary>
        /// Equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(DateTimeStd))
            {
                return this.Value.Equals(((DateTimeStd)obj).Value);
            }
            else
            {
                return this.Value.Equals(obj);
            }
        }
        /// <summary>
        /// hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        
        /// <summary>
        /// 當前月份的總天數
        /// </summary>
        public int DayOfMonth
        {
            get
            {
                DateTime dt = this.Value.AddMonths(1).AddMilliseconds(-509);
                return dt.Day;
            }
        }
        /// <summary>
        /// 民国年
        /// </summary>
        public string MingGuoYear
        {
            get
            {
                int mgYear = this.Value.Year - 1911;
                return mgYear.ToString("#000") + this.Value.ToString("yyyy/MM/dd").Substring(4);

            }
        }
        /// <summary>
        /// 转化为民国格式
        /// </summary>
        public string MingGuoYearforChinese
        {
            get
            {
                int mgYear = this.Value.Year - 1911;
                return "民國"+mgYear.ToString("#000") + this.Value.ToString("yyyy年M月d日").Substring(4);

            }
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static implicit operator DateTimeStd(DateTime o)
        {
            return DateTimeStd.ParseStd(o);
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static implicit operator DateTime(DateTimeStd o)
        {
            return o.Value;
        }

        /// <summary>
        /// 獲取指定日期當前月份的總天數
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetDaysOfMonth(DateTime dt)
        {
            DateTimeStd dts = new DateTimeStd(dt);
            return dts.DayOfMonth;
        }
        /// <summary>
        /// 將指定的毫秒數加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddMilliseconds(double o)
        {
            return new DateTimeStd(this.Value.AddMilliseconds(o));
        }
        /// <summary>
        /// 將指定的秒數加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddSeconds(double o)
        {
            return new DateTimeStd(this.Value.AddSeconds(o));
        }

        /// <summary>
        /// 將指定的分钟数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddMinutes(double o)
        {
            return new DateTimeStd(this.Value.AddMinutes(o));
        }

        /// <summary>
        /// 將指定的小时数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddHours(double o)
        {
            return new DateTimeStd(this.Value.AddHours(o));
        }

        /// <summary>
        /// 將指定的天数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddDays(double o)
        {
            return new DateTimeStd(this.Value.AddDays(o));
        }

        /// <summary>
        /// 將指定的周数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddWeeks(double o)
        {
            double days = o * 7;
            return this.AddDays(days);
        }

        /// <summary>
        /// 將指定的月数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddMonths(double o)
        {
            double days = o * (double)365 / 12;
            return this.AddDays(days);
        }

        /// <summary>
        /// 將指定的季数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddQuarter(double o)
        {
            double months = o * 3;
            return this.AddMonths(months);
        }

        /// <summary>
        /// 將指定的年数加入至這個執行個體的值。
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DateTimeStd AddYears(double o)
        {
            double days = o * 365;
            return this.AddDays(days);
        }

        /// <summary>
        /// dt1-dt2的天数
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DoubleStd DayDiff(DateTimeStd dt1, DateTimeStd dt2)
        {
            if (dt1 == null || dt2 == null)
            {
                return null;
            }
            else
            {
                TimeSpan ts = dt1 - dt2;
                return DoubleStd.ParseStd(ts.TotalDays);
            }
        }

        /// <summary>
        /// +運算
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTimeStd operator +(DateTimeStd o1, TimeSpan ts)
        {
            return new DateTimeStd(o1.Value + ts);
        }
        /// <summary>
        /// +運算：天数
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTimeStd operator +(DateTimeStd o1, double days)
        {
            return o1.AddDays(days);
        }


        /// <summary>
        /// -運算
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static TimeSpan operator -(DateTimeStd o1, DateTimeStd o2)
        {
            return o1.Value - o2.Value;
        }
        /// <summary>
        /// -運算：天数
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTimeStd operator -(DateTimeStd o1, double days)
        {
            return o1.AddDays(-days);
        }
        
        /// <summary>
        /// ==判斷
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool operator ==(DateTimeStd o1, DateTimeStd o2)
        {
            object o = o1;
            object oo = o2;
            if (o == null || oo == null)
            {
                return o == oo;
            }
            else
            {
                return o1.Value == o2.Value;
            }
        }

        /// <summary>
        /// !=判斷
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool operator !=(DateTimeStd o1, DateTimeStd o2)
        {
            object o = o1;
            object oo = o2;
            if (o == null || oo == null)
            {
                return o != oo;
            }
            else
            {
                return o1.Value != o2.Value;
            }
        }

        /// <summary>
        /// >=判斷
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool operator >=(DateTimeStd o1, DateTimeStd o2)
        {
            return o1.Value >= o2.Value;
        }

        /// <summary>
        /// 小于等于判斷
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool operator <=(DateTimeStd o1, DateTimeStd o2)
        {
            return o1.Value <= o2.Value;
        }

        /// <summary>
        /// >判斷
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool operator >(DateTimeStd o1, DateTimeStd o2)
        {
            return o1.Value > o2.Value;
        }

        /// <summary>
        /// 小于判斷
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool operator <(DateTimeStd o1, DateTimeStd o2)
        {
            return o1.Value < o2.Value;
        }
        /// <summary>
        /// 转换成DateTimeStd
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTimeStd ParseStd(object o)
        {
            if (IsDateTime(o))
            {
                List<string> formats = new List<string>();
                formats.Add("yyyyMMdd");
                formats.Add("yyyy/MM/dd");
                formats.Add("yyyy-MM-dd");
                formats.Add("yyyyMMddHHmmss");
                formats.Add("yyyy/MM/dd HH:mm");
                formats.Add("yyyy/MM/dd HH:mm:ss");
                formats.Add("yyyy/MM/dd HH:mm:ss fff");
                formats.Add("yyyy-MM-dd HH:mm");
                formats.Add("yyyy-MM-dd HH:mm:ss");
                formats.Add("yyyy-MM-dd HH:mm:ss fff");
                formats.Add("yyyyMd");
                formats.Add("yyyy/M/d");
                formats.Add("yyyy-M-d");
                formats.Add("yyyyMdHHmmss");
                formats.Add("yyyy/M/d HH:mm");
                formats.Add("yyyy/M/d HH:mm:ss");
                formats.Add("yyyy/M/d HH:mm:ss fff");
                formats.Add("yyyy-M-d HH:mm");
                formats.Add("yyyy-M-d HH:mm:ss");
                formats.Add("yyyy-M-d HH:mm:ss fff");
                formats.Add("r");

                if (o.GetType() == typeof(DateTime))
                {
                    DateTimeStd rtn = new DateTimeStd((DateTime)o);
                    return rtn;
                }
                else
                {
                    return ParseStd(o, formats.ToArray());
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 转换成DateTimeStd
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTimeStd ParseStd(object o, string format)
        {
            if (IsDateTime(o, format))
            {
                DateTimeStd rtn = new DateTimeStd();
                if (o is DateTime)
                {
                    rtn.Value = (DateTime)o;
                }
                else
                {
                    rtn.Value = DateTime.ParseExact(o.ToString(), format, CultureInfo.CurrentCulture);
                }
                return rtn;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 转换成DateTimeStd
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTimeStd ParseStd(object o, params string[] format)
        {
            if (IsDateTime(o, format))
            {
                DateTimeStd rtn = new DateTimeStd();
                if (o is DateTime)
                {
                    rtn.Value = (DateTime)o;
                }
                else
                {
                    rtn.Value = DateTime.ParseExact(o.ToString(), format, CultureInfo.CurrentCulture, DateTimeStyles.None);
                }
                return rtn;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// DateTime判斷
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsDateTime(object o)
        {
            List<string> formats = new List<string>();
            formats.Add("yyyyMMdd");
            formats.Add("yyyy/MM/dd");
            formats.Add("yyyy-MM-dd");
            formats.Add("yyyyMMddHHmmss");
            formats.Add("yyyy/MM/dd HH:mm");
            formats.Add("yyyy/MM/dd HH:mm:ss");
            formats.Add("yyyy/MM/dd HH:mm:ss fff");
            formats.Add("yyyy-MM-dd HH:mm");
            formats.Add("yyyy-MM-dd HH:mm:ss");
            formats.Add("yyyy-MM-dd HH:mm:ss fff");
            formats.Add("yyyyMd");
            formats.Add("yyyy/M/d");
            formats.Add("yyyy-M-d");
            formats.Add("yyyyMdHHmmss");
            formats.Add("yyyy/M/d HH:mm");
            formats.Add("yyyy/M/d HH:mm:ss");
            formats.Add("yyyy/M/d HH:mm:ss fff");
            formats.Add("yyyy-M-d HH:mm");
            formats.Add("yyyy-M-d HH:mm:ss");
            formats.Add("yyyy-M-d HH:mm:ss fff");
            formats.Add("r");

            return IsDateTime(o, formats.ToArray());
        }
        /// <summary>
        /// DateTime判斷
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool IsDateTime(object o, string format)
        {
            if (o == null || o == DBNull.Value)
            {
                return false;
            }
            else if (o is DateTime)
            {
                return true;
            }
            else if (o.ToString().Length <= 0)
            {
                return false;
            }
            else
            {
                DateTime t1;
                return DateTime.TryParseExact(o.ToString(), format, CultureInfo.CurrentCulture, DateTimeStyles.None, out t1);
            }
        }
        /// <summary>
        /// DateTime判斷
        /// </summary>
        /// <param name="o"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static bool IsDateTime(object o, params string[] formats)
        {
            if (o == null || o == DBNull.Value)
            {
                return false;
            }
            else if (o is DateTime)
            {
                return true;
            }
            else if (o.ToString().Length <= 0)
            {
                return false;
            }
            else
            {
                DateTime t1;
                return DateTime.TryParseExact(o.ToString(), formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out t1);
            }
        }

        
    }
}


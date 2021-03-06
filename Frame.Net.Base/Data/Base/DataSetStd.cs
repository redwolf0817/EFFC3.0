using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EFFC.Frame.Net.Base.Data
{
    public class DataSetStd:DataSet,ICloneable
    {
        Dictionary<int, DataTableStd> _tables = new Dictionary<int, DataTableStd>();

        public DataTableStd this[int index]
        {
            get
            {
                return DataTableStd.ParseStd(this.Tables[index]);
            }
        }

        public object Clone()
        {
            var rtn = (DataSetStd)base.Clone();
            foreach(var item in this._tables)
            {
                rtn._tables.Add(item.Key, item.Value);
            }
            return rtn;
        }

        /// <summary>
        /// 根据columnName和rowIndex获取指定table中的值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object GetValue(int tableIndex, string columnName, int rowIndex)
        {
            if (this == null)
            {
                return "";
            }
            else if (tableIndex >= this.Tables.Count)
            {
                return "";
            }
            else
            {
                return DataTableStd.ParseStd(this.Tables[tableIndex]).GetValue(columnName, rowIndex);
            }
        }
        /// <summary>
        /// 根据过滤条件获得指定栏位的值，如果过滤条件不够精确导致得到的结果过多，将抛出异常
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public object GetValueByWhere(int tableIndex, string columnName, string filterExpression)
        {
            if (this == null)
            {
                return "";
            }
            else if (tableIndex >= this.Tables.Count)
            {
                return "";
            }
            else
            {
                return DataTableStd.ParseStd(this.Tables[tableIndex]).GetValueByWhere(columnName, filterExpression);
            }
        }
        /// <summary>
        /// 根据columnIndex和rowIndex获取指定table中的值
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object GetValue(int tableIndex, int columnIndex, int rowIndex)
        {
            if (this == null)
            {
                return "";
            }
            else if (tableIndex >= this.Tables.Count)
            {
                return "";
            }
            else
            {
                return DataTableStd.ParseStd(this.Tables[tableIndex]).GetValue(columnIndex, rowIndex);
            }
        }
        /// <summary>
        /// 根据过滤条件获得指定栏位的值，如果过滤条件不够精确导致得到的结果过多，将抛出异常
        /// </summary>
        /// <param name="tableIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public object GetValueByWhere(int tableIndex, int columnIndex, string filterExpression)
        {
            if (this == null)
            {
                return "";
            }
            else if (tableIndex >= this.Tables.Count)
            {
                return "";
            }
            else
            {
                return DataTableStd.ParseStd(this.Tables[tableIndex]).GetValueByWhere(columnIndex, filterExpression);
            }
        }
        /// <summary>
        /// 根据columnName和rowIndex获取第一个table中的值
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object GetValue(string columnName, int rowIndex)
        {

            return GetValue(0, columnName, rowIndex);

        }
        /// <summary>
        /// 根据过滤条件获得指定栏位的值，如果过滤条件不够精确导致得到的结果过多，将抛出异常
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public object GetValueByWhere(string columnName, string filterExpression)
        {
            return GetValueByWhere(0, columnName, filterExpression);
        }
        /// <summary>
        /// 根据columnIndex和rowIndex获取第一个table中的值
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object GetValue(int columnIndex, int rowIndex)
        {
            return GetValue(0, columnIndex, rowIndex);
        }
        /// <summary>
        ///  根据过滤条件获得指定栏位的值，如果过滤条件不够精确导致得到的结果过多，将抛出异常
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public object GetValueByWhere(int columnIndex, string filterExpression)
        {
            return GetValueByWhere(0, columnIndex, filterExpression);
        }
    }
}

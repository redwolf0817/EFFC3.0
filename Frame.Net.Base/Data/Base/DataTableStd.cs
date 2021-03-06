using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using EFFC.Frame.Net.Base.Parameter;
using EFFC.Frame.Net.Base.Common;

namespace EFFC.Frame.Net.Base.Data
{   
    public class DataTableStd :StandardData<DataTable>
    {

        protected List<string> pkContent = new List<string>();

        public DataTableStd()
        {
            pkContent.Clear();
        }

        public override object Clone()
        {
            DataTableStd rtn = (DataTableStd)base.Clone();
            foreach(var s in this.pkContent)
            {
                rtn.pkContent.Add(s);
            }

            return rtn;
        }
        /// <summary>
        /// 獲得Column的結構
        /// </summary>
        /// <returns></returns>
        public static ColumnP GetColumnParamDefine()
        {
            return new ColumnP();
        }
        /// <summary>
        /// 獲取或者設置值
        /// </summary>
        /// <param name="x">行</param>
        /// <param name="y">列</param>
        /// <returns></returns>
        public object this[int x,string y]
        {
            get
            {
                object rtn = GetValue(y, x);
                return rtn;
            }
            set
            {
                SetValue(value, y, x);
            }
        }
        /// <summary>
        /// 獲取或者設置值
        /// </summary>
        /// <param name="x">行</param>
        /// <param name="y">列</param>
        /// <returns></returns>
        public object this[int x, int y]
        {
            get
            {
                object rtn = GetValue(y, x);
                return rtn;
            }
            set
            {
                SetValue(value, y, x);
            }
        }

        public static implicit operator DataTableStd(DataTable dt)
        {
            return ParseStd(dt);
        }
        public static implicit operator DataTable(DataTableStd dt)
        {
            return dt.Value;
        }
        public void ClearData()
        {
            this.Value.Clear();
        }

        /// <summary>
        /// 本表的行數
        /// </summary>
        public int RowLength
        {
            get
            {
                return this.Value.Rows.Count;
            }
        }
        /// <summary>
        /// 返回指定欄位的內容的MaxLength
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int ColumnMaxLength(string columnName)
        {
            return this.Value.Columns[columnName].MaxLength;
        }
        /// <summary>
        /// 返回指定欄位的内容的MaxLength
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public int ColumnMaxLength(int columnIndex)
        {
            return this.Value.Columns[columnIndex].MaxLength;
        }
        /// <summary>
        /// 返回指定欄位的数据类型
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public Type ColumnDateType(string columnName)
        {
            return this.Value.Columns[columnName].DataType;
        }
        /// <summary>
        /// 返回指定欄位的数据类型
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public Type ColumnDateType(int columnIndex)
        {
            return this.Value.Columns[columnIndex].DataType;
        }
        /// <summary>
        /// 添加列，V1.0.0.1
        /// </summary>
        /// <param name="p"></param>
        public void AddColumn(ColumnP p)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = p.ColumnName;
            dc.DataType = Type.GetType(p.DataType);
            if (dc.DataType == typeof(string))
            {
                dc.MaxLength = p.Length;
            }
            dc.AllowDBNull = p.IsAllowNull;
            dc.AutoIncrement = p.IsAutoIncrement;
            dc.AutoIncrementSeed = p.AutoIncrementSeed;
            dc.AutoIncrementStep = p.AutoIncrementStep;
            bool isPK = p.IsPK;

            this.Value.Columns.Add(dc);
            if (isPK && !p.IsAutoIncrement)
            {
                List<DataColumn> l = new List<DataColumn>();
                for (int i = 0; i < this.Value.PrimaryKey.Length; i++)
                {
                    l.Add(this.Value.PrimaryKey[i]);
                }
                l.Add(dc);
                this.Value.PrimaryKey = l.ToArray();
            }
        }
        /// <summary>
        /// 判断欄位是否为自增长
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public bool isAutoIncrement(int columnIndex)
        {
            return this.Value.Columns[columnIndex].AutoIncrement;
        }
        /// <summary>
        /// 判断欄位是否为自增长
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool isAutoIncrement(string columnName)
        {
            return this.Value.Columns[columnName].AutoIncrement;
        }

        /// <summary>
        /// 在指定的位置設置值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        public void SetValue(object value, string columnName, int rowIndex)
        {
            if (rowIndex >= this.Value.Rows.Count)
            {
                return;
            }
            else if (!this.Value.Columns.Contains(columnName))
            {
                return;
            }
            else
            {
                if (value is StandardData<int>)
                {
                    this.Value.Rows[rowIndex][columnName] = ((StandardData<int>)value).Value;
                }
                else if (value is StandardData<float>)
                {
                    this.Value.Rows[rowIndex][columnName] = ((StandardData<float>)value).Value;
                }
                else if (value is StandardData<double>)
                {
                    this.Value.Rows[rowIndex][columnName] = ((StandardData<double>)value).Value;
                }
                else if (value is StandardData<decimal>)
                {
                    this.Value.Rows[rowIndex][columnName] = ((StandardData<decimal>)value).Value;
                }
                else if (value is StandardData<string>)
                {
                    this.Value.Rows[rowIndex][columnName] = ((StandardData<string>)value).Value;
                }
                else if (value is StandardData<DateTime>)
                {
                    this.Value.Rows[rowIndex][columnName] = ((StandardData<DateTime>)value).Value;
                }
                else
                {
                    this.Value.Rows[rowIndex][columnName] = value;
                }
                
            }
        }
        /// <summary>
        /// 在指定的位置設置值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public void SetValue(object value, int columnIndex, int rowIndex)
        {
            if (rowIndex >= this.Value.Rows.Count)
            {
                return;
            }
            else if (columnIndex >= this.Value.Columns.Count)
            {
                return;
            }
            else
            {
                if (value is StandardData<int>)
                {
                    this.Value.Rows[rowIndex][columnIndex] = ((StandardData<int>)value).Value;
                }
                else if (value is StandardData<float>)
                {
                    this.Value.Rows[rowIndex][columnIndex] = ((StandardData<float>)value).Value;
                }
                else if (value is StandardData<double>)
                {
                    this.Value.Rows[rowIndex][columnIndex] = ((StandardData<double>)value).Value;
                }
                else if (value is StandardData<decimal>)
                {
                    this.Value.Rows[rowIndex][columnIndex] = ((StandardData<decimal>)value).Value;
                }
                else if (value is StandardData<string>)
                {
                    this.Value.Rows[rowIndex][columnIndex] = ((StandardData<string>)value).Value;
                }
                else if (value is StandardData<DateTime>)
                {
                    this.Value.Rows[rowIndex][columnIndex] = ((StandardData<DateTime>)value).Value;
                }
                else
                {
                    this.Value.Rows[rowIndex][columnIndex] = value;
                }
            }
        }
        /// <summary>
        /// 将from表中的数据写入到本表中，按照欄位名稱对应，Append方式
        /// </summary>
        /// <param name="from"></param>
        public void SetValueApppend_From(DataTableStd from)
        {
            for (int i = 0; i < from.RowLength; i++)
            {
                foreach (DataColumn dc in from.Value.Columns)
                {
                    if (this.Value.Columns.Contains(dc.ColumnName) 
                        && this.Value.Columns[dc.ColumnName].MaxLength == dc.MaxLength 
                        && this.Value.Columns[dc.ColumnName].DataType == dc.DataType)
                    {
                        this.SetNewRowValue(from[i, dc.ColumnName], dc.ColumnName);
                    }
                }
                this.AddNewRow();
            }

        }
        /// <summary>
        /// 给指定的列赋值-所有行
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnName"></param>
        public void SetValueByColumn(object value, string columnName)
        {
            for (int i = 0; i < this.RowLength; i++)
            {
                this[i, columnName] = value;
            }
        }
        /// <summary>
        /// 给指定的列赋值-所有行
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnIndex"></param>
        public void SetValueByColumn(object value, int columnIndex)
        {
            for (int i = 0; i < this.RowLength; i++)
            {
                this[i, columnIndex] = value;
            }
        }

        /// <summary>
        /// 根據columnName和rowIndex獲取值
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object GetValue(string columnName, int rowIndex)
        {
            if (this == null)
            {
                return null;
            }
            else if (rowIndex >= this.Value.Rows.Count)
            {
                return null;
            }
            else if (!this.Value.Columns.Contains(columnName))
            {
                return null;
            }
            else
            {
                if (this.Value.Rows[rowIndex][columnName] != null && this.Value.Rows[rowIndex][columnName] != DBNull.Value)
                {
                    if (this.Value.Columns[columnName].DataType == typeof(string))
                    {
                        return ComFunc.nvl(this.Value.Rows[rowIndex][columnName]);
                    }
                    else
                    {
                        return this.Value.Rows[rowIndex][columnName];
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根據过滤条件獲得指定欄位的值，如果过滤条件不够精确导致得到的结果过多，将抛出异常
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public object GetValueByWhere(string columnName, string filterExpression)
        {
            if (this == null)
            {
                return null;
            }
            else if (!this.Value.Columns.Contains(columnName))
            {
                return null;
            }
            else
            {
                DataRow[] drs = this.Value.Select(filterExpression);
                if (drs.Length == 1)
                {
                    return ComFunc.nvl(drs[0][columnName]);
                }
                else
                {
                    throw new Exception("There is more Results because of filterExpression is not exactly!");
                }
            }
        }
        
        /// <summary>
        /// 根據columnName和rowIndex獲取值
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object GetValue(int columnIndex, int rowIndex)
        {
            if (this == null)
            {
                return null;
            }
            else if (rowIndex >= this.Value.Rows.Count)
            {
                return null;
            }
            else if (columnIndex >= this.Value.Columns.Count)
            {
                return null;
            }
            else
            {
                if (this.Value.Rows[rowIndex][columnIndex] != null && this.Value.Rows[rowIndex][columnIndex] != DBNull.Value)
                {
                    if (this.Value.Columns[columnIndex].DataType == typeof(string))
                    {
                        return ComFunc.nvl(this.Value.Rows[rowIndex][columnIndex]);
                    }
                    else
                    {
                        return this.Value.Rows[rowIndex][columnIndex];
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根據过滤条件獲得指定欄位的值，如果过滤条件不够精确导致得到的结果过多，将抛出异常
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public object GetValueByWhere(int columnIndex, string filterExpression)
        {
            if (this == null)
            {
                return null;
            }
            else if (columnIndex >= this.Value.Columns.Count)
            {
                return null;
            }
            else
            {
                DataRow[] drs = this.Value.Select(filterExpression);
                if (drs.Length == 1)
                {
                    return ComFunc.nvl(drs[0][columnIndex]);
                }
                else
                {
                    throw new Exception("There is more Results because of filterExpression is not exactly!");
                }
            }
        }

        DataRow _new_row = null;
        /// <summary>
        /// 新增一個臨時行
        /// </summary>
        public void NewRow()
        {
            this._new_row = this.Value.NewRow();
        }
        /// <summary>
        /// 給新增行寫值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnName"></param>
        public void SetNewRowValue(object value, string columnName)
        {
            if (this._new_row == null) NewRow();
            if (columnName == null)
            {
                return;
            }
            else if (!this.Value.Columns.Contains(columnName))
            {
                return;
            }
            else
            {
                if (value is StandardData<int>)
                {
                    _new_row[columnName] = ((StandardData<int>)value).Value;
                }
                else if (value is StandardData<float>)
                {
                    _new_row[columnName] = ((StandardData<float>)value).Value;
                }
                else if (value is StandardData<double>)
                {
                    _new_row[columnName] = ((StandardData<double>)value).Value;
                }
                else if (value is StandardData<decimal>)
                {
                    _new_row[columnName] = ((StandardData<decimal>)value).Value;
                }
                else if (value is StandardData<string>)
                {
                    _new_row[columnName] = ((StandardData<string>)value).Value;
                }
                else if (value is StandardData<DateTime>)
                {
                    _new_row[columnName] = ((StandardData<DateTime>)value).Value;
                }
                else
                {
                    _new_row[columnName] = value;
                }
            }
            
        }
        /// <summary>
        /// 給新增行寫值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnIndex"></param>
        public void SetNewRowValue(object value, int columnIndex)
        {
            if (this._new_row == null) NewRow();

            if (columnIndex >= this.Value.Columns.Count)
            {
                return;
            }
            else
            {
                if (value is StandardData<int>)
                {
                    _new_row[columnIndex] = ((StandardData<int>)value).Value;
                }
                else if (value is StandardData<float>)
                {
                    _new_row[columnIndex] = ((StandardData<float>)value).Value;
                }
                else if (value is StandardData<double>)
                {
                    _new_row[columnIndex] = ((StandardData<double>)value).Value;
                }
                else if (value is StandardData<decimal>)
                {
                    _new_row[columnIndex] = ((StandardData<decimal>)value).Value;
                }
                else if (value is StandardData<string>)
                {
                    _new_row[columnIndex] = ((StandardData<string>)value).Value;
                }
                else if (value is StandardData<DateTime>)
                {
                    _new_row[columnIndex] = ((StandardData<DateTime>)value).Value;
                }
                else
                {
                    _new_row[columnIndex] = value;
                }
            }

        }
        /// <summary>
        /// 獲取临时新增行的数据
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public object GetNewRowValue(string columnName)
        {
            if (this._new_row == null)
            {
                return null;
            }
            else if (!this.Value.Columns.Contains(columnName))
            {
                return null;
            }
            else
            {
                if (this._new_row[columnName] != null && this._new_row[columnName] != DBNull.Value)
                {
                    if (this.Value.Columns[columnName].DataType == typeof(string))
                    {
                        return ComFunc.nvl(this._new_row[columnName]);
                    }
                    else
                    {
                        return this._new_row[columnName];
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 獲取临时新增行的数据
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public object GetNewRowValue(int columnIndex)
        {
            if (this._new_row == null)
            {
                return null;
            }
            else if (columnIndex>= this.Value.Columns.Count)
            {
                return null;
            }
            else
            {
                if (this._new_row[columnIndex] != null && this._new_row[columnIndex] != DBNull.Value)
                {
                    if (this.Value.Columns[columnIndex].DataType == typeof(string))
                    {
                        return ComFunc.nvl(this._new_row[columnIndex]);
                    }
                    else
                    {
                        return this._new_row[columnIndex];
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 將新增的臨時行Add到table中
        /// </summary>
        public void AddNewRow()
        {
            this.Value.Rows.Add(_new_row);
            _new_row = null;
        }

        /// <summary>
        /// 獲得该表的PK，dt必须带schema
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataColumn[] GetPK(DataTable dt)
        {
            if (dt == null) return null;

            return dt.PrimaryKey;
        }

        public DataColumn[] PK
        {
            get { return this.Value.PrimaryKey; }
        }

        /// <summary>
        /// 獲得该表的PK的名稱列表，dt必须带schema
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string[] GetPKName(DataTable dt)
        {
            DataColumn[] dc = GetPK(dt);
            string[] rtn = new string[dc.Length];
            for (int i = 0; i < dc.Length; i++)
            {
                rtn[i] = dc[i].ColumnName;
            }

            return rtn;
        }

        public string[] PKNames
        {
            get { return DataTableStd.GetPKName(this.Value); }
        }
        /// <summary>
        /// 獲得该表的Columns的名稱列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string[] GetColumnName(DataTable dt)
        {
            string[] rtn = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                rtn[i] = dt.Columns[i].ColumnName;
            }

            return rtn;
        }

        public string[] ColumnNames
        {
            get { return DataTableStd.GetColumnName(this.Value); }
        }

        /// <summary>
        /// 找出Dt中RowCount
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int RowNumber(DataTable dt)
        {
            int rtn = 0;

            if (dt != null)
            {
                rtn = dt.Rows.Count;
            }

            return rtn;
        }
        /// <summary>
        /// 复制本表的结构
        /// </summary>
        /// <returns></returns>
        public DataTableStd CloneStd()
        {
            DataTableStd rtn = DataTableStd.ParseStd(this.Value.Copy());
            rtn.Value.Rows.Clear();
            return rtn;
        }

        /// <summary>
        /// 根據columns搜索与values中相同的数据，判断是否存在
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool IsExist(Dictionary<string, object> values)
        {
            bool rtn = true;

            if (RowNumber(this.Value) <= 0)
            {
                rtn = false;
            }
            else
            {
                for (int i = 0; i < RowNumber(this.Value); i++)
                {
                    bool tmp = true;
                    foreach (KeyValuePair<string, object> kvp in values)
                    {
                        tmp = tmp & (ComFunc.nvl(GetValue(kvp.Key, i)) == ComFunc.nvl(kvp.Value));
                    }
                    //如果為true，則表示有完全與values相同的資料存在，否則當前這行資料與values不相同，則瀏覽下一行資料
                    if (tmp)
                    {
                        rtn = true;
                        break;
                    }
                    else
                    {
                        rtn = rtn & tmp;
                    }
                }
            }

            return rtn;
        }

        /// <summary>
        /// clone一個DataRow
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="todt"></param>
        public void CloneDataRow(int rowindex, ref DataTable todt)
        {
            DataRow rtn = todt.NewRow();
            foreach (DataColumn dc in this.Value.Columns)
            {
                object v = GetValue(dc.ColumnName, rowindex);
                rtn[dc.ColumnName] = v == null ? DBNull.Value : v;
            }
            todt.Rows.Add(rtn);
        }
        /// <summary>
        /// clone一個DataRow
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="todt"></param>
        public void CloneDataRow(int rowindex, ref DataTableStd todt)
        {
            DataRow rtn = todt.Value.NewRow();
            foreach (DataColumn dc in this.Value.Columns)
            {
                object v = GetValue(dc.ColumnName, rowindex);
                rtn[dc.ColumnName] = v == null ? DBNull.Value : v;
            }
            todt.Value.Rows.Add(rtn);
        }
        /// <summary>
        /// 根據条件表达式找出需要的数据并返回
        /// </summary>
        /// <param name="filterExpresstion"></param>
        /// <returns></returns>
        public DataTableStd SelectByWhere(string filterExpresstion)
        {
            DataRow[] drs = this.Value.Select(filterExpresstion);
            DataTableStd rtn = this.CloneStd();
            foreach (DataRow dr in drs)
            {
                foreach (string colname in this.ColumnNames)
                {
                    rtn.SetNewRowValue(dr[colname], colname);
                }
                rtn.AddNewRow();
            }
            return rtn;
        }
        /// <summary>
        /// 根據条件表达式找出需要的数据并返回
        /// </summary>
        /// <param name="filterExpresstion"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public DataTableStd SelectByWhere(string filterExpresstion, string sort)
        {
            DataRow[] drs = this.Value.Select(filterExpresstion, sort);
            DataTableStd rtn = this.CloneStd();
            foreach (DataRow dr in drs)
            {
                foreach (string colname in this.ColumnNames)
                {
                    rtn.SetNewRowValue(dr[colname], colname);
                }
                rtn.AddNewRow();
            }
            return rtn;
        }

        /// <summary>
        /// 转化成标准类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataTableStd ParseStd(object o)
        {
            if (o == null)
            {
                return null;
            }
            else if (o.GetType() != Type.GetType("System.Data.DataTable"))
            {
                return null;
            }
            else
            {
                return (DataTableStd)o;
            }
        }
        /// <summary>
        /// 转化成标准类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataTableStd ParseStd(DataTable o)
        {
            if (o == null)
            {
                return null;
            }
            else
            {

                DataTableStd dts = new DataTableStd();
                dts.Value = o;
                return dts;
            }
        }

        /// <summary>
        /// 根據指定column个数转化成标准类型
        /// </summary>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static DataTableStd ParseStd(int columnCount)
        {
            DataTableStd dt = new DataTableStd();

            for (int i = 0; i < columnCount; i++)
            {
                dt.Value.Columns.Add(new DataColumn("F" + i));
            }

            return dt;
        }

        /// <summary>
        /// 根據指定columns转化成标准类型
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataTableStd ParseStd(string[] columns)
        {
            DataTableStd dt = new DataTableStd();

            for (int i = 0; i < columns.Length; i++)
            {
                dt.Value.Columns.Add(new DataColumn(columns[i]));
            }

            return dt;
        }

        /// <summary>
        /// 将table中的指定一行的数据转化成字符串
        /// </summary>
        /// <param name="splitComm"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public string ToString(string splitComm, int rowIndex)
        {
            string str = "";
            foreach(DataColumn dc in this.Value.Columns)
            {
                str = str.Length > 0 ? str + splitComm + GetValue(dc.ColumnName, rowIndex) : str + GetValue(dc.ColumnName, rowIndex);
            }

            return str;
        }
        /// <summary>
        /// 将table中的Header转化成字符串
        /// </summary>
        /// <param name="splitComm"></param>
        /// <returns></returns>
        public string HeaderToString(string splitComm)
        {
            string str = "";
            foreach (DataColumn dc in this.Value.Columns)
            {
                str = str.Length > 0 ? str + splitComm + dc.ColumnName : str + dc.ColumnName;
            }

            return str;
        }

        /// <summary>
        /// 将table中的数据转化成字符串（数据量大的时候不建议使用）
        /// </summary>
        /// <param name="splitComm"></param>
        /// <param name="isIncHeader"></param>
        /// <returns></returns>
        public StringBuilder ToString(string splitComm, bool isIncHeader)
        {
            StringBuilder rtn = new StringBuilder();
            if (isIncHeader)
            {
                rtn.AppendLine(HeaderToString(splitComm));
            }

            for (int i = 0; i < this.RowLength; i++)
            {
                rtn.AppendLine(this.ToString(splitComm, i));
            }

            return rtn;
        }
    }

}

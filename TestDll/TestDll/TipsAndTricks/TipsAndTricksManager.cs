using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Ividatalink.TipsAndTricks
{
    public class TipsAndTricksManager
    {
        private readonly List<TipsAndTricks> _myPrivateListOfInt = new List<TipsAndTricks>();

        public TipsAndTricksManager()
        {
            InitPrivateList();
        }

        private IEnumerable<TipsAndTricks> InitPrivateList()
        {
            _myPrivateListOfInt.Add(new TipsAndTricks() { Count = 1 });
            _myPrivateListOfInt.Add(new TipsAndTricks() { Count = -42 });

            _myPrivateListOfInt.Add(new TipsAndTricks() { Count = 8 });
            _myPrivateListOfInt.Add(new TipsAndTricks() { Count = 16 });
            _myPrivateListOfInt.Add(new TipsAndTricks() { Count = -84 });
            return _myPrivateListOfInt;
        }


        private IEnumerable<TipsAndTricks> _myPrivateList
        {
            get
            {
                yield return new TipsAndTricks() { Count = 1 };
                yield return new TipsAndTricks() { Count = -42 };

                yield return new TipsAndTricks() { Count = 8 };
                yield return new TipsAndTricks() { Count = 16 };
                yield return new TipsAndTricks() { Count = -84 };
               
            }
        }


        public void ManagePrivateList()
        {
            if (_myPrivateListOfInt.Any())
            {
                var res = _myPrivateList.FirstOrDefault(obj => obj.Count > 8);

                if (res.Count == 1)
                {
                }
                //...
            }
        }



        public IEnumerable<Update> GetUpdateslist()
        {
            yield return Update1();
            yield return Update2();
            yield return Update3();
            yield return Update4();

         
        }

        public IEnumerable<Update> GetUpdateslist2()
        {
            var lst = new List<Update>();

            lst.Add(Update1());
            lst.Add(Update2());
            lst.Add(Update3());
            lst.Add(Update4());

            return lst;
        }


        public Update Update1()
        {
            return new Update();
        }

        public Update Update2()
        {
            return new Update();
        }
        public Update Update3()
        {
            return new Update();
        }
        public Update Update4()
        {
            return new Update();
        }
        public IEnumerable<T> Read<T>(string sql, Func<IDataReader, T> makeTipsAndTricksObject, params object[] parms)
        {
            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(CommandType.Text, sql, connection, parms))
                {
                   
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return makeTipsAndTricksObject(reader);
                        }
                    }
                }
            }
        }

        //private T makeTipsAndTricksObject<T>(MyStringReader reader)
        //{
        //    throw new NotImplementedException();
        //}

        private mycom CreateCommand(CommandType text, string sql, IDisposable connection, object[] parms)
        {
           return  new mycom();
        }

        private IDisposable CreateConnection()
        {
            throw new NotImplementedException();
        }

        private class mycom : IDisposable
        {
            public MyStringReader ExecuteReader()
            {
                return new MyStringReader();
            }
            public void Dispose()
            {
            }
        }

        private class MyStringReader : IDisposable, IDataReader
        {
            public void Close()
            {
                throw new NotImplementedException();
            }

            public DataTable GetSchemaTable()
            {
                throw new NotImplementedException();
            }

            public bool NextResult()
            {
                throw new NotImplementedException();
            }

            public bool Read()
            {
                return true;
            }

            public int Depth { get; }
            public bool IsClosed { get; }
            public int RecordsAffected { get; }

            public void Dispose()
            {
            }

            public string GetName(int i)
            {
                throw new NotImplementedException();
            }

            public string GetDataTypeName(int i)
            {
                throw new NotImplementedException();
            }

            public Type GetFieldType(int i)
            {
                throw new NotImplementedException();
            }

            public object GetValue(int i)
            {
                throw new NotImplementedException();
            }

            public int GetValues(object[] values)
            {
                throw new NotImplementedException();
            }

            public int GetOrdinal(string name)
            {
                throw new NotImplementedException();
            }

            public bool GetBoolean(int i)
            {
                throw new NotImplementedException();
            }

            public byte GetByte(int i)
            {
                throw new NotImplementedException();
            }

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public char GetChar(int i)
            {
                throw new NotImplementedException();
            }

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public Guid GetGuid(int i)
            {
                throw new NotImplementedException();
            }

            public short GetInt16(int i)
            {
                throw new NotImplementedException();
            }

            public int GetInt32(int i)
            {
                throw new NotImplementedException();
            }

            public long GetInt64(int i)
            {
                throw new NotImplementedException();
            }

            public float GetFloat(int i)
            {
                throw new NotImplementedException();
            }

            public double GetDouble(int i)
            {
                throw new NotImplementedException();
            }

            public string GetString(int i)
            {
                throw new NotImplementedException();
            }

            public decimal GetDecimal(int i)
            {
                throw new NotImplementedException();
            }

            public DateTime GetDateTime(int i)
            {
                throw new NotImplementedException();
            }

            public IDataReader GetData(int i)
            {
                throw new NotImplementedException();
            }

            public bool IsDBNull(int i)
            {
                throw new NotImplementedException();
            }

            public int FieldCount { get; }

            object IDataRecord.this[int i]
            {
                get { throw new NotImplementedException(); }
            }

            object IDataRecord.this[string name]
            {
                get { throw new NotImplementedException(); }
            }
        }
    }

    public class Update
    {

        public bool Updated { get; set; }

    }
}
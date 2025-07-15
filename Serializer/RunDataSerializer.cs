using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace BODareMode.Serializer
{
    // NOTE: This code is very experimental and is likely subject to change.
    public static class RunDataSerializer
    {
        private const string FieldType = "$TYPE$";
        private const string PrimitiveValue = "$VALUE$";
        private const string IsNull = "$NULL$";
        private const string ObjId = "$OBJID$";
        private const string IListIDictionaryLength = "$LENGTH$";
        private const string IListElement = "$ELEMENT$";
        private const string IDictionaryKey = "$KEY$";
        private const string IDictionaryValue = "$VALUE$";
        private const string ScriptableObjectID = "$SOID$";

        private static bool CanSerializeField(FieldInfo field)
        {
            if (field.GetCustomAttribute<RDSDontSerializeAttribute>() != null)
                return false;

            if(field.IsPublic)
                return true;

            if(field.GetCustomAttribute<RDSForceSerializeAttribute>() != null)
                return true;

            return false;
        }

        public static T DeserializeFromRun<T>(string dataKey, RunInGameData dat)
        {
            TryDeserializeFromRun<T>(dataKey, dat, out var ret);
            return ret;
        }

        public static bool TryDeserializeFromRun<T>(string dataKey, RunInGameData dat, out T output)
        {
            output = default;

            dataKey = $"{MOD_PREFIX}_{dataKey}";
            var objIds = new Dictionary<int, object>();

            if (DeserializeField(dataKey, dataKey, dat, objIds) is not T ret)
                return false;

            output = ret;
            return true;
        }

        private static object DeserializeField(string dataKey, string rootKey, RunInGameData dat, Dictionary<int, object> objIds)
        {
            if (dat.GetBoolData($"{dataKey}_{IsNull}"))
                return null;

            var typeStr = dat.GetStringData($"{dataKey}_{FieldType}");
            var type = Type.GetType(typeStr, false);

            if (type == null)
                return null;

            if (type == typeof(nint) || type == typeof(nuint) || typeof(Delegate).IsAssignableFrom(type))
                return null;

            if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                if(ScriptableObjectSerializer.TryDeserializeScriptable(type, dat.GetStringData($"{dataKey}_{ScriptableObjectID}"), out var so))
                    return so;

                return null;
            }

            if(typeof(UnityEngine.Object).IsAssignableFrom(type))
                return null;

            if (type == typeof(int))
                return dat.GetIntData($"{dataKey}_{PrimitiveValue}");
            else if (type == typeof(bool))
                return dat.GetBoolData($"{dataKey}_{PrimitiveValue}");
            else if (type == typeof(string))
                return dat.GetStringData($"{dataKey}_{PrimitiveValue}");

            if(TryDeserializeNonBasicPrimitive(dataKey, type, dat, out var ret))
                return ret;

            return DeserializeObjectField(dataKey, rootKey, dat, objIds);
        }

        private static object DeserializeObjectField(string dataKey, string rootKey, RunInGameData dat, Dictionary<int, object> objIds)
        {
            var objId = dat.GetIntData($"{dataKey}_{ObjId}");

            if (objIds.TryGetValue(objId, out var o))
                return o;

            return DeserializeObject($"{rootKey}_{objId}", rootKey, objId, dat, objIds);
        }

        private static object DeserializeObject(string dataKey, string rootKey, int id, RunInGameData dat, Dictionary<int, object> objIds)
        {
            var typeStr = dat.GetStringData($"{dataKey}_{FieldType}");
            var type = Type.GetType(typeStr, false);

            if(type == null)
                return null;

            if(TryDeserializeIList(type, dataKey, rootKey, id, dat, objIds, out var r1))
                return r1;

            if(TryDeserializeIDictionary(type, dataKey, rootKey, id, dat, objIds, out var r2))
                return r2;

            return DeserializeCustomObject(type, dataKey, rootKey, id, dat, objIds); 
        }

        private static object DeserializeCustomObject(Type type, string dataKey, string rootKey, int id, RunInGameData dat, Dictionary<int, object> objIds)
        {
            var obj = Activator.CreateInstance(type);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            objIds[id] = obj;

            foreach(var f in fields)
            {
                if(!CanSerializeField(f))
                    continue;

                f.SetValue(obj, DeserializeField($"{dataKey}_{f.Name}", rootKey, dat, objIds));
            }

            return obj;
        }

        private static bool TryDeserializeIDictionary(Type type, string dataKey, string rootKey, int id, RunInGameData dat, Dictionary<int, object> objIds, out object r)
        {
            r = null;

            if(!typeof(IDictionary).IsAssignableFrom(type))
                return false;

            var len = dat.GetIntData($"{dataKey}_{IListIDictionaryLength}");
            r = Activator.CreateInstance(type);
            objIds[id] = r;

            for(var i = 0; i < len; i++)
            {
                var k = DeserializeField($"{dataKey}_{IDictionaryKey}_{i}", rootKey, dat, objIds);
                var v = DeserializeField($"{dataKey}_{IDictionaryValue}_{i}", rootKey, dat, objIds);

                ((IDictionary)r)[k] = v;
            }

            return true;
        }

        private static bool TryDeserializeIList(Type type, string dataKey, string rootKey, int id, RunInGameData dat, Dictionary<int, object> objIds, out object r)
        {
            r = null;

            if(!typeof(IList).IsAssignableFrom(type))
                return false;

            var len = dat.GetIntData($"{dataKey}_{IListIDictionaryLength}");
            var isArray = typeof(Array).IsAssignableFrom(type);

            if (isArray)
                r = Array.CreateInstance(type.GetElementType(), len);
            else
                r = Activator.CreateInstance(type);

            objIds[id] = r;

            for(var i = 0; i < len; i++)
            {
                var v = DeserializeField($"{dataKey}_{IListElement}_{i}", rootKey, dat, objIds);

                if (isArray)
                    ((Array)r).SetValue(v, i);
                else
                    ((IList)r).Add(v);
            }

            return true;
        }

        private static bool TryDeserializeNonBasicPrimitive(string dataKey, Type t, RunInGameData dat, out object r)
        {
            var strval = dat.GetStringData($"{dataKey}_{PrimitiveValue}");

            if(t == typeof(uint))
            {
                int.TryParse(strval, out var i);
                r = i;

                return true;
            }
            else if(t == typeof(short))
            {
                short.TryParse(strval, out var s);
                r = s;

                return true;
            }
            else if(t == typeof(ushort))
            {
                ushort.TryParse(strval, out var us);
                r = us;

                return true;
            }
            else if (t == typeof(long))
            {
                long.TryParse(strval, out var l);
                r = l;

                return true;
            }
            else if (t == typeof(ulong))
            {
                ulong.TryParse(strval, out var ul);
                r = ul;

                return true;
            }
            else if (t == typeof(float))
            {
                float.TryParse(strval, out var f);
                r = f;

                return true;
            }
            else if (t == typeof(double))
            {
                double.TryParse(strval, out var d);
                r = d;
                
                return true;
            }
            else if (t == typeof(byte))
            {
                byte.TryParse(strval, out var b);
                r = b;

                return true;
            }
            else if (t == typeof(sbyte))
            {
                sbyte.TryParse(strval, out var sb);
                r = sb;

                return true;
            }
            else if (t == typeof(decimal))
            {
                decimal.TryParse(strval, out var dc);
                r = dc;

                return true;
            }
            else if (t == typeof(char))
            {
                char.TryParse(strval, out var c);
                r = c;

                return true;
            }

            r = null;
            return false;
        }

        public static void SerializeToRun(object obj, string dataKey, RunInGameData dat)
        {
            dataKey = $"{MOD_PREFIX}_{dataKey}";
            var objIds = new Dictionary<object, int>();
            var lastId = 0;

            SerializeField(obj, dataKey, dataKey, dat, objIds, ref lastId);
        }

        private static void SerializeObject(object obj, string dataKey, string rootKey, RunInGameData dat, Dictionary<object, int> objIds, ref int lastId)
        {
            dat.SetStringData($"{dataKey}_{FieldType}", obj.GetType().AssemblyQualifiedName);

            if (TrySerializeIList(obj, dataKey, rootKey, dat, objIds, ref lastId))
                return;

            if(TrySerializeDictionary(obj, dataKey, rootKey, dat, objIds, ref lastId))
                return;

            SerializeCustomObject(obj, dataKey, rootKey, dat, objIds, ref lastId);
        }

        private static bool TrySerializeIList(object obj, string dataKey, string rootKey, RunInGameData dat, Dictionary<object, int> objIds, ref int lastId)
        {
            if (obj is not IList il)
                return false;

            var count = il.Count;
            dat.SetIntData($"{dataKey}_{IListIDictionaryLength}", count);

            for(var i = 0; i < count; i++)
                SerializeField(il[i], $"{dataKey}_{IListElement}_{i}", rootKey, dat, objIds, ref lastId);

            return true;
        }

        private static bool TrySerializeDictionary(object obj, string dataKey, string rootKey, RunInGameData dat, Dictionary<object, int> objIds, ref int lastId)
        {
            if(obj is not IDictionary dict)
                return false;

            var i = 0;
            foreach(var k in dict.Keys)
            {
                SerializeField(k, $"{dataKey}_{IDictionaryKey}_{i}", rootKey, dat, objIds, ref lastId);
                SerializeField(dict[k], $"{dataKey}_{IDictionaryValue}_{i}", rootKey, dat, objIds, ref lastId);

                i++;
            }

            dat.SetIntData($"{dataKey}_{IListIDictionaryLength}", i);

            return true;
        }

        private static void SerializeCustomObject(object obj, string dataKey, string rootKey, RunInGameData dat, Dictionary<object, int> objIds, ref int lastId)
        {
            var type = obj.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var f in fields)
            {
                if (!CanSerializeField(f))
                    continue;

                SerializeField(f.GetValue(obj), $"{dataKey}_{f.Name}", rootKey, dat, objIds, ref lastId);
            }
        }

        private static void SerializeField(object value, string dataKey, string rootKey, RunInGameData dat, Dictionary<object, int> objIds, ref int lastId)
        {
            var isnull = value == null;
            dat.SetBoolData($"{dataKey}_{IsNull}", isnull);

            if (isnull)
                return;

            var type = value.GetType();
            dat.SetStringData($"{dataKey}_{FieldType}", type.AssemblyQualifiedName);

            if (value is nint or nuint or Delegate)
                return;

            if(value is ScriptableObject so)
            {
                dat.SetStringData($"{dataKey}_{ScriptableObjectID}", so.name);
                return;
            }

            if (value is UnityEngine.Object)
                return;

            if (value is int i)
            {
                dat.SetIntData($"{dataKey}_{PrimitiveValue}", i);
                return;
            }
            else if (value is bool b)
            {
                dat.SetBoolData($"{dataKey}_{PrimitiveValue}", b);
                return;
            }
            else if (value is string s)
            {
                dat.SetStringData($"{dataKey}_{PrimitiveValue}", s);
                return;
            }

            if (TrySerializeNonBasicPrimitive(value, dataKey, dat))
                return;

            SerializeObjectField(value, dataKey, rootKey, dat, objIds, ref lastId);
        }

        private static bool TrySerializeNonBasicPrimitive(object value, string dataKey, RunInGameData dat)
        {
            var strval = value switch
            {
                uint ui => ui.ToString(),

                short s => s.ToString(),
                ushort us => us.ToString(),

                long l => l.ToString(),
                ulong ul => ul.ToString(),

                float f => f.ToString(),
                double d => d.ToString(),

                byte b => b.ToString(),
                sbyte sb => sb.ToString(),

                decimal dc => dc.ToString(),
                char ch => ch.ToString(),

                _ => null
            };

            if (value.GetType().IsEnum)
                strval = value.ToString();

            if (strval == null)
                return false;

            dat.SetStringData($"{dataKey}_{PrimitiveValue}", strval);
            return true;
        }

        private static void SerializeObjectField(object value, string dataKey, string rootKey, RunInGameData dat, Dictionary<object, int> objIds, ref int lastId)
        {
            if (!objIds.TryGetValue(value, out var id))
            {
                objIds[value] = id = lastId++;
                SerializeObject(value, $"{rootKey}_{id}", rootKey, dat, objIds, ref lastId);
            }

            dat.SetIntData($"{dataKey}_{ObjId}", id);
        }
    }
}

namespace KVA.Cinema.Utilities
{
    using System;
    using System.Collections.Generic;

    public static class CheckUtilities
    {
        public static bool ContainsNullOrEmptyValue(params object[] args)
        {
            if (args == null)
            {
                return true;
            }

            Dictionary<Type, object> defaultValueTypes = new Dictionary<Type, object>();

            foreach (var arg in args)
            {
                if (arg == null || (arg is string && string.IsNullOrWhiteSpace((string)arg)))
                {
                    return true;
                }

                var type = arg.GetType();

                if (type.IsValueType)
                {
                    object def;

                    if (defaultValueTypes.ContainsKey(type))
                    {
                        def = defaultValueTypes[type];
                    }
                    else
                    {
                        def = Activator.CreateInstance(type);

                        defaultValueTypes.Add(type, def);
                    }

                    if (def.Equals(arg))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static TResult ParseInput<TResult>(string input, Predicate<TResult> conditionValidation = null)
        {
            bool canBeParsed = true;
            object result = default;

            if (typeof(TResult) == typeof(string))
            {
                result = input;
            }
            else if (typeof(TResult) == typeof(int))
            {
                canBeParsed = int.TryParse(input, out int intResult);
                result = intResult;
            }
            else if (typeof(TResult) == typeof(DateTime))
            {
                canBeParsed = DateTime.TryParse(input, out DateTime dtResult);
                result = dtResult;
            }
            else if (typeof(TResult) == typeof(decimal))
            {
                canBeParsed = decimal.TryParse(input, out decimal dtResult);
                result = dtResult;
            }
            else
            {
                canBeParsed = false;
            }

            if (!canBeParsed)
            {
                return default;
            }

            TResult resultAsRequiredType = (TResult)Convert.ChangeType(result, typeof(TResult));

            bool isMatchCondition = conditionValidation?.Invoke(resultAsRequiredType) ?? true;

            if (!isMatchCondition)
            {
                return default;
            }

            return resultAsRequiredType;
        }
    }
}

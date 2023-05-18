namespace VNet.System
{
    public class TypeMapper<T, K>
    {
        Dictionary<Type, List<Type>> _mapping;

        public TypeMapper()
        {
            _mapping = new Dictionary<Type, List<Type>>();
        }

        public void Add(Type key, Type value)
        {
            if (_mapping.ContainsKey(key))
            {
                _mapping[key].Add(value);
            }
            else
            {
                _mapping.Add(key, new List<Type>());
                _mapping[key].Add(value);
            }
        }

        public void Remove(Type key, Type value)
        {
            if (_mapping.ContainsKey(key))
            {
                _mapping[key].Remove(value);

                if (_mapping[key].Count == 0)
                {
                    _mapping.Remove(key);
                }
            }
        }

        private void ValidateInput(Type key, Type value)
        {
            if(key != typeof(T))
            {
                throw new TypeLoadException("Key must be of type " + key.GetType().Name);
            }

            if (value != typeof(K))
            {
                throw new TypeLoadException("Value must be of type " + value.GetType().Name);
            }
        }
    }
}
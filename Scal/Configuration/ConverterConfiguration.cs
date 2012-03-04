using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace Scal.Configuration
{
    public class ConverterConfiguration : IEnumerable<Tuple<Type,Type,IValueConverter>>
    {

        private List<Tuple<Type, Type, IValueConverter>> _converters = new List<Tuple<Type, Type, IValueConverter>>();

        /// <summary>
        /// Add a converter for Binding
        /// </summary>
        /// <typeparam name="VM">ViewModel Property Type</typeparam>
        /// <typeparam name="V">View property type</typeparam>
        /// <typeparam name="C">Converter</typeparam>
        public ConverterConfiguration Add<VM, V, C>() where C : IValueConverter, new()
        {
            _converters.Add(Tuple.Create(typeof(VM),typeof(V),(IValueConverter)Activator.CreateInstance<C>()));
            return this;
        }

        public IEnumerator<Tuple<Type, Type, IValueConverter>> GetEnumerator()
        {
            return _converters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
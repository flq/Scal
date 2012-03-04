using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using Caliburn.Micro;
using System.Linq;
using DynamicXaml.Extensions;

namespace Scal.Services
{
    public class ValueConverterManagement
    {
        private readonly Dictionary<Tuple<Type, Type>, IValueConverter> _converters;

        public ValueConverterManagement(AppModel model)
        {
            ConventionManager.ApplyValueConverter = ApplyConverter;
            _converters =  model.Converters.ToDictionary(k => Tuple.Create(k.Item1, k.Item2), v => v.Item3);
        }

        private void ApplyConverter(Binding binding, DependencyProperty dProp, PropertyInfo vmProp)
        {
            var t = Tuple.Create(vmProp.PropertyType, dProp.PropertyType);
            _converters.Get(t).Do(v => binding.Converter = v);
        }
    }
}
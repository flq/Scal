using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DynamicXaml.Extensions;
using Scal.Services;
using Scal.ViewLocation;
using Scal;
using System.Linq;
using MemBus.Support;

namespace SampleApp
{
    public class EnumAsGroupBoxViews : IViewLocator
    {
        public Maybe<UIElement> LocateView(LocationContext ctx)
        {
            return ParentAvailableAndPropertyIsEnumWithSetter(ctx) ? 
                CreateOptionGroup(new OptiongroupModel(ctx.ParentDataContext.Value, ctx.UnderlyingProperty.Value)) : 
                Maybe<UIElement>.None;
        }

        private static Maybe<UIElement> CreateOptionGroup(OptiongroupModel model)
        {
            var optionGroup = new OptionGroup();
            optionGroup.InitializeComponent();
            DataContextOverride.SetDataContext(optionGroup, model);
            return optionGroup.ToMaybe<UIElement>();
        }

        private static bool ParentAvailableAndPropertyIsEnumWithSetter(LocationContext ctx)
        {
            return ctx.ParentDataContext && ctx.UnderlyingProperty.Get(pi => pi.CanWrite && pi.PropertyType.IsEnum);
        }
    }

    public class OptiongroupModel : IEnumerable<OptionModel>
    {
        private readonly object _parentDataContext;
        private readonly PropertyInfo _underlyingProperty;
        private readonly List<OptionModel> _models;

        public OptiongroupModel(object parentDataContext, PropertyInfo underlyingProperty)
        {
            _parentDataContext = parentDataContext;
            _underlyingProperty = underlyingProperty;
            _models = Enum
                .GetValues(_underlyingProperty.PropertyType)
                .OfType<Enum>()
                .Select(enumVal => new OptionModel(v => _underlyingProperty.SetValue(_parentDataContext, v, null), enumVal))
                .Pipeline(om => om.PropertyChanged += HandleChange)
                .ToList();
        }

        private void HandleChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChecked") return;
            var optionModel = (OptionModel)sender;
            if (optionModel.IsChecked)
                _models.Except(optionModel.AsEnumerable()).Each(om => om.IsChecked = false);
        }

        public string GroupName
        {
            get { return _underlyingProperty.PropertyType.Name.PascalToWhitespace(); }
        }

        public IEnumerator<OptionModel> GetEnumerator()
        {
            return _models.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class OptionModel : INotifyPropertyChanged
    {
        private readonly Action<Enum> _setValue;
        private bool _isChecked;
        private readonly Enum _value;

        public OptionModel(Action<Enum> setValue, Enum value)
        {
            _setValue = setValue;
            _value = value;
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                if (_isChecked) _setValue(_value);
                PropertyChanged.Raise(this, m => m.IsChecked);
            }
        }

        public object Content { get { return _value.ToString().PascalToWhitespace(); } }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
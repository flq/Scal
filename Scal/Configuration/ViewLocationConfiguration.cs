using System;
using System.Collections.Generic;
using DynamicXaml.Extensions;
using Scal.ViewLocation;

namespace Scal.Configuration
{
    public class ViewLocationConfiguration
    {
        public ViewLocationConfiguration()
        {
            Locators = new List<object>();
        }

        public void AddLocator<T>() where T : IViewLocator
        {
            Locators.Add(typeof(T));
        }

        internal IList<object> Locators { get; set; }

        public ModelMatchConfiguration ModelsMatching(Func<object,bool> match)
        {
            return new ModelMatchConfiguration(match);
        }

        public ModelMatchConfiguration ModelsMatching<T>()
        {
            var modelMatchConfiguration = new ModelMatchConfiguration(o => o.CanBeCastTo<T>());
            Locators.Add(modelMatchConfiguration);
            return modelMatchConfiguration;
        }
    }
}
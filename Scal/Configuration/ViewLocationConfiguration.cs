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

        public ModelMatchConfiguration ModelsMatching(Func<LocationContext,bool> match)
        {
            return new ModelMatchConfiguration(match);
        }

        public ViewLocationConfiguration ModelsMatching<T>(Action<ModelMatchConfiguration> actionOnMatch)
        {
            var modelMatchConfiguration = new ModelMatchConfiguration(o => o.Model.CanBeCastTo<T>());
            Locators.Add(modelMatchConfiguration);
            actionOnMatch(modelMatchConfiguration);
            return this;
        }
    }
}
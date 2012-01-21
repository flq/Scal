using System;
using System.Collections;
using System.Collections.Generic;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Scal.Bootstrapping
{
    public class MessageHubRegistrationConvention : IRegistrationConvention
    {
        private readonly AppModel _model;
        private readonly TypeCollector _messageHubTypes;
        private Action<Registry> _typeCollectorRegistration;

        public MessageHubRegistrationConvention(AppModel model)
        {
            _model = model;
            _messageHubTypes = new TypeCollector();
            _typeCollectorRegistration = r =>
                                             {
                                                 r.For<TypeCollector>().Use(_messageHubTypes);
                                                 _typeCollectorRegistration = r2 => { };
                                             };
        }

        public void Process(Type type, Registry registry)
        {
            _typeCollectorRegistration(registry);
            if (type.IsAbstract)
                return;

            if (_model.RegisterMessageHubPredicate(type))
            {
                _messageHubTypes.Add(type);
            }
        }
    }

    public class TypeCollector : IEnumerable<Type>
    {
        readonly List<Type> _types = new List<Type>();

        public void Add(Type type)
        {
            _types.Add(type);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
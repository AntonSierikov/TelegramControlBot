using System;
using System.Collections.Generic;
using Moq;
using ControlBot.UnitTests.IServiceMocks;

namespace ControlBot.UnitTests.Providers
{
    public class ServiceProviderMock
    {
        private Mock<IServiceProvider> _providerMock;

        private readonly Dictionary<Type, IServiceMock> _serviceMocks;

        //----------------------------------------------------------------//
            
        public ServiceProviderMock()
        {
            _providerMock = new Mock<IServiceProvider>();
            _serviceMocks = new Dictionary<Type, IServiceMock>();
        }

        //----------------------------------------------------------------//

        public Boolean TryAddServiceMock<Service>(IServiceMock _serviceMock) where Service: class
        {
            Type type = typeof(Service);
            return _serviceMocks.TryAdd(type, _serviceMock);
        }

        //----------------------------------------------------------------//

        public IServiceProvider Provider
        {
            get
            {
                foreach(KeyValuePair<Type, IServiceMock> _serviceMock in _serviceMocks)
                {
                    _providerMock.Setup(c => c.GetService(_serviceMock.Key)).Returns(_serviceMock.Value.MockObject);
                }
                return _providerMock.Object;
            }
        }

        //----------------------------------------------------------------//
    }
}

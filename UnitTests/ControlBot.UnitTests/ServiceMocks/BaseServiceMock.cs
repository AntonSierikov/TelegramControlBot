using System;
using Moq;
using ControlBot.UnitTests.IServiceMocks;

namespace ControlBot.UnitTests.ServiceMocks
{
    internal abstract class BaseServiceMock<Service> : IServiceMock where Service: class
    {
        public Mock<Service> _serviceMock { get; protected set; }

        //----------------------------------------------------------------//

        public Object MockObject
        {
            get
            {
                return _serviceMock.Object;
            }
        }

        //----------------------------------------------------------------//

        public BaseServiceMock()
        {
            _serviceMock = new Mock<Service>();
        }

        //----------------------------------------------------------------//
    }
}

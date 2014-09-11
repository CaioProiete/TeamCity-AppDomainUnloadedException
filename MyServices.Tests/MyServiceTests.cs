using System;
using System.ServiceModel;
using NUnit.Framework;

namespace MyServices.Tests
{
    [TestFixture]
    public class MyServiceTests
    {
        private ServiceHost serviceHost;
        private const string basicHttpServiceUrl = "http://localhost:6786/BasicHttpAnswerService.svc";
        private const string wsHttpServiceUrl = "http://localhost:6787/WSHttpService.svc";
        private const string tcpServiceServiceUrl = "net.tcp://localhost:6788/TcpService";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            serviceHost = new ServiceHost(typeof(MyService));
            serviceHost.AddServiceEndpoint(typeof(IMyService), new BasicHttpBinding(), basicHttpServiceUrl);
            serviceHost.AddServiceEndpoint(typeof(IMyService), new WSHttpBinding(), wsHttpServiceUrl);
            serviceHost.AddServiceEndpoint(typeof(IMyService), new NetTcpBinding(), tcpServiceServiceUrl);

            serviceHost.Open();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            serviceHost.Close();
        }

        [Test]
        public void Can_get_answer_via_basic_http()
        {
            var channelFactory = new ChannelFactory<IMyService>(new BasicHttpBinding(), basicHttpServiceUrl);
            var channel = channelFactory.CreateChannel();

            using ((IDisposable)channel)
            {
                Assert.AreEqual(42, channel.GetAnswer());
            }
        }

        [Test]
        public void Can_get_answer_via_ws_http()
        {
            var channelFactory = new ChannelFactory<IMyService>(new WSHttpBinding(), wsHttpServiceUrl);
            var channel = channelFactory.CreateChannel();

            using ((IDisposable)channel)
            {
                Assert.AreEqual(42, channel.GetAnswer());
            }
        }

        [Test]
        public void Can_get_answer_via_tcp()
        {
            var channelFactory = new ChannelFactory<IMyService>(new NetTcpBinding(), tcpServiceServiceUrl);
            var channel = channelFactory.CreateChannel();

            using ((IDisposable)channel)
            {
                Assert.AreEqual(42, channel.GetAnswer());
            }
        }
    }
}

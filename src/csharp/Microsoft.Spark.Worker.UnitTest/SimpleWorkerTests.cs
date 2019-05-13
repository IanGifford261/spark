using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Spark.Network;
using Microsoft.Spark.Services;
using Xunit;

namespace Microsoft.Spark.Worker.UnitTest
{
    public class SimpleWorkerTests
    {
        private static readonly ILoggerService s_logger =
            LoggerServiceFactory.GetLogger(typeof(SimpleWorker));
        private Version _version;

        //Attempt to create a test for the internal Run() method in Simple Worker
        [Fact]
        public void TestForRunSimpleWorker()
        {
            int port = Utils.SettingUtils.GetWorkerFactoryPort(_version);
            string secret = Utils.SettingUtils.GetWorkerFactorySecret(_version);

            s_logger.LogInfo($"RunSimpleWorker() is starting with port = {port}.");

            ISocketWrapper socket = SocketFactory.CreateSocket();
            socket.Connect(IPAddress.Loopback, port, secret);

            new TaskRunner(0, socket, false, _version).Run();


        }
    }
}

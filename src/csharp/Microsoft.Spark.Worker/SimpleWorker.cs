// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using Microsoft.Spark.Network;
using Microsoft.Spark.Services;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Microsoft.Spark.UnitTest, PublicKey=002400000480000094000000060200000024000052534131000400000100010015c01ae1f50e8cc09ba9eac9147cf8fd9fce2cfe9f8dce4f7301c4132ca9fb50ce8cbf1df4dc18dd4d210e4345c744ecb3365ed327efdbc52603faa5e21daa11234c8c4a73e51f03bf192544581ebe107adee3a34928e39d04e524a9ce729d5090bfd7dad9d10c722c0def9ccc08ff0a03790e48bcd1f9b6c476063e1966a1c4")]

namespace Microsoft.Spark.Worker
{
    internal sealed class SimpleWorker
    {
        private static readonly ILoggerService s_logger =
            LoggerServiceFactory.GetLogger(typeof(SimpleWorker));

        private readonly Version _version;

        internal SimpleWorker(Version version)
        {
            _version = version;
        }

        internal void Run()
        {
            try
            {
                int port = Utils.SettingUtils.GetWorkerFactoryPort(_version);
                string secret = Utils.SettingUtils.GetWorkerFactorySecret(_version);

                s_logger.LogInfo($"RunSimpleWorker() is starting with port = {port}.");

                ISocketWrapper socket = SocketFactory.CreateSocket();
                socket.Connect(IPAddress.Loopback, port, secret);

                new TaskRunner(0, socket, false, _version).Run();
            }
            catch (Exception e)
            {
                s_logger.LogError("RunSimpleWorker() failed with exception:");
                s_logger.LogException(e);
                Environment.Exit(-1);
            }

            s_logger.LogInfo("RunSimpleWorker() finished successfully");
        }
    }
}

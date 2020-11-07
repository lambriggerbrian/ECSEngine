using System;
using System.Threading;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Simulation;

namespace Engine
{
    internal class App
    {
        private readonly World _world;
        private readonly InputHandler _input;
        private readonly ILogger<App> _logger;
        private readonly CancellationToken _token;

        public App(ILoggerFactory logger, World world, InputHandler input, CancellationTokenSource cancellationTokenSource)
        {
            _logger = logger.CreateLogger<App>();
            _world = world;
            _input = input;
            _token = cancellationTokenSource.Token;
        }

        public void Run()
        {
            UInt64 loopsFromStart = 0;
            _logger.LogInformation("Engine starting up...");
            _world.StartManagers();
            bool shutdownRequested = false;
            while (!_token.IsCancellationRequested && !shutdownRequested)
            {
                _logger.LogTrace($"Loop {loopsFromStart} starting");
                _ = _input.ReadInput();
                _world.Simulate(1 / 60);
                loopsFromStart++;
            }
            _logger.LogInformation("Engine shutting down...");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Simulation;
using Simulation.CommandSystem;

namespace Engine
{
    internal class InputHandler
    {
        private readonly CommandHandler _handler;
        private readonly CancellationTokenSource _tokenSource;
        private readonly StreamReader stream;

        public InputHandler(CommandHandler handler, CancellationTokenSource tokenSource, Stream stream)
        {
            _handler = handler;
            _tokenSource = tokenSource;
            this.stream = new StreamReader(stream);
        }

        public async Task ReadInput()
        {
            var line = await stream.ReadLineAsync();
            if (String.IsNullOrEmpty(line)) return;
            if (line.Equals("q")) _tokenSource.Cancel();
            else await Task.Run(() => _handler.HandleCommand(line));
        }

        //public async void ReadInput()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    var result = stream.ReadByte();
        //    bool terminated = false;
        //    while (result > 0 && !terminated)
        //    {
        //        char value = (char)result;
        //        if (value == '\n') terminated = true;
        //        builder.Append(value);
        //        result = stream.ReadByte();
        //    }
        //    if ()
        //    await _world.SendCommand(builder.ToString());
        //}
    }
}

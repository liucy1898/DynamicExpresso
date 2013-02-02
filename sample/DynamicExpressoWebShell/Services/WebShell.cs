﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace DynamicExpressoWebShell.Services
{
    public class WebShell
    {
        public static WebShell Current { get; private set;}

        public static void Init()
        {
            Current = new WebShell();
        }

        readonly DynamicExpresso.Interpreter _interpreter;
        readonly CommandsHistory _commandsHistory;

        public WebShell()
        {
            _interpreter = new DynamicExpresso.Interpreter();
            _commandsHistory = new CommandsHistory();

            _interpreter.SetVariable("Commands", _commandsHistory);
        }

        public object Eval(string expression)
        {
            if (expression != null && expression.Length > 200)
                throw new Exception("Live demo doesn't support expression with more than 200 characters.");

            _commandsHistory.HandleCommandExecuted(new CommandEvent(expression));

            var result = _interpreter.Eval(expression);

            return result;
        }

        public CommandsHistory CommandsHistory
        {
            get;
            private set;
        }
    }
}
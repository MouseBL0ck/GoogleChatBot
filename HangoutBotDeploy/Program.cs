using System;

namespace HangoutBotDeploy {

  class Program {

    static void Main(string[] args) {

      Request request = new Request();

      if (args.Length > 0)
        request.SendMessage((string)args[0]);


    }

  }
}

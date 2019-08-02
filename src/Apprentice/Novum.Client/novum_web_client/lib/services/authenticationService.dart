
import 'package:example_flutter/services/protobuf/novum.pb.dart';

import 'grpc.dart';

class AuthenticationService {
  static Future<bool> initialize() async {
    var request = new InitializeRequest();
    try {
      request.clientType = ClientType.ORDERMAN;
      request.clientVersion = "1.1.1.1";
      request.id = "125-123456";

      var reply = await Grpc.authenticationClient
          .initialize(request)
          .timeout(Duration(seconds: 2), onTimeout: () {
        print("timeouted");
      });

      print("rep: " + reply.toString());
      print("timestamp: " + reply.unixTimestamp.toString());
      return true;
    } catch (e) {
      return false;
    }
  }

  static Future login(String pin) async {
    var request = new LoginRequest();
    try {
      request.input = pin;
      request.inputType = LoginInputType.MANUALLY;
      var reply = await Grpc.authenticationClient.login(request);

      print("login success");
    } catch (e) {
      print(e);
      throw e;
    }
  }
}

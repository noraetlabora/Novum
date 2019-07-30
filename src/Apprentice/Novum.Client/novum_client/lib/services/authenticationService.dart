import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';

class AuthenticationService {
  static Future initialize() async {
    var request = new InitializeRequest();
    try {
      request.clientType = ClientType.ORDERMAN;
      request.clientVersion = "1.1.1.1";
      request.id = "125-123456";
      print("extecuted before");

      var reply = await Grpc.authenticationClient
          .initialize(request)
          .timeout(Duration(seconds: 4));

      print("extecuted after");

      print("rep: " + reply.toString());
      print(reply.unixTimestamp);
    } catch (e) {
      throw new Exception();
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

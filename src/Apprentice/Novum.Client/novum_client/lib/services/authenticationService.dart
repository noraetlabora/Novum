import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';

class AuthenticationService {

  static Future login(String pin) async {
    var request = new InitializeRequest();
    try {
      request.clientType = ClientType.ORDERMAN;
      request.clientVersion = "1.1.1.1";
      request.id = "125-123456";
      var reply = await Grpc.authenticationClient.initialize(request);
      print(reply.unixTimestamp);
    }
    catch(e) { 
      print(e); 
   } 
  }
}
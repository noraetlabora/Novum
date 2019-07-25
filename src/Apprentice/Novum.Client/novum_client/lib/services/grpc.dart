import 'package:grpc/grpc.dart';
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';

class Grpc {

  static ClientChannel clientChannel;

  Grpc() {

  }

  void set(String ip, int port) {
    clientChannel = new ClientChannel(ip,
        port: port,
        options: const ChannelOptions(
            credentials: const ChannelCredentials.insecure(),
            idleTimeout: Duration(seconds: 5)));
  }

    final grpcClient = new AuthenticationClient(channel);
    final request = new InitializeRequest();
    request.clientType = ClientType.ORDERMAN;
    request.clientVersion = "1.1.1";
    request.id = "125-123456789";
    request.test = 5;
    try {
      final reply = await grpcClient.initialize(request);
      print(reply.toString());
    } catch (exception) {
      print(exception);
    }
  }
}
import 'package:example_flutter/services/protobuf/novum.pbgrpc.dart';
import 'package:grpc/grpc.dart';

class Grpc {

  static ClientChannel clientChannel;
  static SystemClient systemClient;
  static AuthenticationClient authenticationClient;
  static StaticDataClient staticDataClient;
  static RuntimeDataClient runtimeDataClient;

  static void set(String ip, int port) {
    clientChannel = new ClientChannel(ip,
         port: port,
         options: const ChannelOptions(
             credentials: const ChannelCredentials.insecure(),
             idleTimeout: Duration(seconds: 5)));

    systemClient = new SystemClient(clientChannel);
    authenticationClient = new AuthenticationClient(clientChannel);
    staticDataClient = new StaticDataClient(clientChannel);
    runtimeDataClient = new RuntimeDataClient(clientChannel);
  }
}
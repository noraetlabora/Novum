import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:grpc/grpc.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart' as prefix0;
import 'package:novum_client/widgets/pinpadbutton.dart';
import 'services/protobuf/novum.pb.dart' as grpc;
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';
import 'package:protobuf/protobuf.dart';
import 'package:http2/transport.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';
import 'package:novum_client/widgets/statusbar.dart';

void main() => runApp(LoginApp());

class LoginApp extends StatelessWidget {
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(
        primarySwatch: Colors.yellow,
      ),
      home: Login(),
    );
  }
}

class Login extends StatelessWidget {
  static String pin = "";
  bool tof = true;
  @override
  Widget build(BuildContext context) {
    //TextEditingController tfController = TextEditingController();
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;

    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisAlignment: MainAxisAlignment.end,
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          new StatusBar(),
          new Container(
              width: width,
              height: heigth * 0.2995,
              decoration: new BoxDecoration(
                image: new DecorationImage(
                  image: ExactAssetImage('assets/NovaTouch-logo.jpg'),
                  fit: BoxFit.cover,
                ),
              )
            ),
          new PinPad(),
          new BottomButtonBar(),
        ],
      ),
    );
  }

  Future initialize() async {
    final channel = new ClientChannel("10.0.2.2",
        port: 50051,
        options: const ChannelOptions(
            credentials: const ChannelCredentials.insecure(),
            idleTimeout: Duration(seconds: 5)));

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

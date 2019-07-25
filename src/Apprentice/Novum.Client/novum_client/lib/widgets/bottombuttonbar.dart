import 'package:flutter/material.dart';
import 'package:novum_client/screens/funktions.dart';

import 'package:grpc/grpc.dart';
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';

  Future initialize() async {
    final channel = new ClientChannel("192.168.1.113",
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

class BottomButtonBar extends StatelessWidget {
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return Row(
      mainAxisSize: MainAxisSize.max, // this will take space as minimum as posible(to center)
      children: <Widget>[
        ButtonTheme(
          buttonColor: Colors.yellow,
          minWidth: width / 2,
          height: heigth * 0.1015,
          child: RaisedButton(
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => FClient()),
              );
            },
            child: Text("Funktionen"),
          ),
        ),
        ButtonTheme(
          buttonColor: Colors.yellow,
          minWidth: width / 2,
          height: heigth * 0.1015,
          child: RaisedButton(
            shape: new ContinuousRectangleBorder(),
            onPressed: () {
              initialize();
            },
            child: Text("OK"),
          ),
        ),
      ],
    );
  }
}

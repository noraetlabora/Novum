import 'package:flutter/material.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:grpc/grpc.dart';
import 'package:novum_client/screens/functions.dart';
import 'package:novum_client/screens/tablescreen.dart';
import 'package:novum_client/widgets/deviceinformationlist.dart';
import 'package:novum_client/widgets/pinpad.dart';

class BottomButton extends StatelessWidget {
  final int amount;
  final String text;
  BottomButton({@required this.amount, this.text});

  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      buttonColor: Colors.yellow,
      minWidth: width / amount,
      height: heigth * 0.1015,
      child: RaisedButton(
        onPressed: () {
          switch (text) {
            case "OK":
              if (PinPad.pin == "1234") {
                // initializ
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => TableScreen()),
                );
              }
              break;
            case "Funktionen":
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => Functions()),
              );
              break;
          }
        },
        child: AutoSizeText(text),
      ),
    );
  }
  // Future initialize() async {
  //   final channel = new ClientChannel("0.0.0.0",
  //       port: 50051,
  //       options: const ChannelOptions(
  //           credentials: const ChannelCredentials.insecure(),
  //           idleTimeout: Duration(seconds: 5)));

  //   final grpcClient = new AuthenticationClient(channel);
  //   final request = new InitializeRequest();
  //   request.clientType = ClientType.ORDERMAN;
  //   request.clientVersion = "1.1.1";
  //   request.id = "125-123456789";
  //   request.test = 5;
  //   try {
  //     final reply = await grpcClient.initialize(request);
  //     print(reply.toString());
  //   } catch (exception) {
  //     print(exception);
  //   }
  // }
}

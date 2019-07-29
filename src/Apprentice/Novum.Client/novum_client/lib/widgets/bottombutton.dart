import 'package:flutter/material.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:grpc/grpc.dart';
import 'package:novum_client/screens/functions.dart';
import 'package:novum_client/screens/tablescreen.dart';
import 'package:novum_client/widgets/deviceinformationlist.dart';
import 'package:novum_client/widgets/pinpad.dart';
import 'package:novum_client/widgets/bottombuttonbar.dart';

class BottomButton extends StatelessWidget {
  final int amount;
  final String text;
  final BottomButtonBar bar;
  BottomButton(
      {@required this.amount, @required this.text, @required this.bar});

  Widget build(BuildContext context) {
    var id = bar.getId();
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return ButtonTheme(
      buttonColor: Colors.yellow,
      minWidth: width / amount,
      height: heigth * 0.1015,
      child: RaisedButton(
        onPressed: () {
          if (id == "login") {
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
          } else if (id == "table") {
            switch (text) {
              case "OK":
                break;
              case "Funktionen":
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => Functions()),
                );
                break;
            }
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

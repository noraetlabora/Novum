import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:grpc/grpc.dart';
import 'services/protobuf/novum.pb.dart' as grpc;
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';
import 'package:protobuf/protobuf.dart';
import 'package:http2/transport.dart';

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
  String pin = "";
  bool tof = true;
  @override
  Widget build(BuildContext context) {
    TextEditingController tfController = TextEditingController();
    SystemChrome.setEnabledSystemUIOverlays([]);
    double width = MediaQuery.of(context).size.width;
    double heigth = MediaQuery.of(context).size.height;
    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisAlignment: MainAxisAlignment.end,
        mainAxisSize: MainAxisSize.max,
        children: <Widget>[
          Container(
            height: heigth * 0.0469,
            color: Colors.black,
            child: Column(
              children: <Widget>[
                Row(
                  mainAxisSize: MainAxisSize.max,
                  children: <Widget>[
                    Icon(
                      Icons.person,
                      color: Colors.white,
                    ),
                    Center(
                      child: Text(
                        "Kassa 1",
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                  ],
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  mainAxisSize: MainAxisSize.max,
                  children: <Widget>[],
                )
              ],
            ),
          ),
          new Container(
              width: width,
              height: heigth * 0.2995,
              decoration: new BoxDecoration(
                image: new DecorationImage(
                  image: ExactAssetImage('assets/NovaTouch-logo.jpg'),
                  fit: BoxFit.cover,
                ),
              )),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            mainAxisSize: MainAxisSize.max,
            children: <Widget>[
              Container(
                color: Color.fromRGBO(200, 200, 200, 1.0),
                width: width,
                height: heigth * 0.0832,
                child: Center(
                    child: TextField(
                        controller: tfController,
                        decoration: InputDecoration(
                            hintText: "PIN eingeben", border: InputBorder.none),
                        textAlign: TextAlign.center,
                        enabled: false,
                        style: TextStyle(fontSize: 25),
                        obscureText: true)),
              ),
            ],
          ),
          Column(
            children: <Widget>[
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(1);
                        tfController.text = pin;
                      },
                      child: Text(
                        "1",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(2);
                        tfController.text = pin;
                      },
                      child: Text(
                        "2",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(3);
                        tfController.text = pin;
                      },
                      child: Text(
                        "3",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(4);
                        tfController.text = pin;
                      },
                      child: Text(
                        "4",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(5);
                        tfController.text = pin;
                      },
                      child: Text(
                        "5",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(6);
                        tfController.text = pin;
                      },
                      child: Text(
                        "6",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(7);
                        tfController.text = pin;
                      },
                      child: Text(
                        "7",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(8);
                        tfController.text = pin;
                      },
                      child: Text(
                        "8",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(9);
                        tfController.text = pin;
                      },
                      child: Text(
                        "9",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: (){},
                      child: Text(
                        "",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: RaisedButton(
                      shape: new ContinuousRectangleBorder(),
                      textColor: Colors.white,
                      onPressed: () {
                        pinText(0);
                        tfController.text = pin;
                      },
                      child: Text(
                        "0",
                        style: TextStyle(fontSize: 17),
                      ),
                      color: Color.fromRGBO(100, 100, 100, 1.0),
                    ),
                  ),
                  ButtonTheme(
                    minWidth: width / 3,
                    height: heigth * 0.1172,
                    child: GestureDetector(
                      onLongPress: (){
                        tof = true;
                        while(tof == true){
                          pinText(-1);
                          tfController.text = pin;
                          if(pin.length == 0){
                            tof = false;
                          }
                        }
                      },
                    child: RaisedButton(
                      onPressed: (){
                        pinText(-1);
                        tfController.text = pin;
                      },
                        color: Color.fromRGBO(100, 100, 100, 1.0),
                        shape: new ContinuousRectangleBorder(),
                        textColor: Colors.white,
                        child: Column(
                          children: <Widget>[Icon(Icons.backspace)],
                        )),
                  ),
                  ),
                ],
              ),
            ],
          ),
          Row(
            mainAxisSize: MainAxisSize
                .min, // this will take space as minimum as posible(to center)
            children: <Widget>[
              ButtonTheme(
                buttonColor: Colors.yellow,
                minWidth: width / 2,
                height: heigth * 0.1015,
                child: RaisedButton(
                  onPressed: () {},
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
          ),
        ],
      ),
    );
  }

Future initialize() async {
    final channel = new ClientChannel("192.168.0.160",
      port: 50051,
    
      options: const ChannelOptions(
        credentials: const ChannelCredentials.insecure(),
        idleTimeout: Duration(seconds: 30)));

  final grpcClient = new AuthenticationClient(channel);
  final request = new InitializeRequest();
  request.clientType = ClientType.ORDERMAN;
  request.clientVersion = "1.1.1";
  request.id = "125-123456789";
  request.test = 5;
  final reply =  await grpcClient.initialize(request);
  print(reply.toString());
}

  void pinText(int c) {
    if (c != -1 && c != null) {
      pin += c.toString();
    } else if (c == -1) {
      pin = removeLastCharacter(pin);
    }
    print(pin);
  }

  String removeLastCharacter(String s) {
    if (s.length > 0) {
      if (s.length == 1) {
        return "";
      } else {
        return s.substring(0, s.length - 1);
      }
    }
    return "";
  }
}

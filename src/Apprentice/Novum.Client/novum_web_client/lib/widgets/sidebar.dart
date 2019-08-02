import 'package:example_flutter/popups/dialogs.dart';
import 'package:example_flutter/screens/login.dart';
import 'package:example_flutter/services/grpc.dart';
import 'package:example_flutter/services/systemService.dart';
import 'package:flutter/material.dart';

import '../main.dart';


class SideBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double height = MediaQuery.of(context).size.height;

    return Container(
        width: width * 0.8,
        child: Drawer(
          child: ListView(
            padding: EdgeInsets.zero,
            children: <Widget>[
              Container(
                height: height * 0.15,
                child: DrawerHeader(
                  child: Text(
                    "Funktionen",
                    style: TextStyle(
                        color: Colors.black,
                        fontSize: 35,
                        fontWeight: FontWeight.w300),
                  ),
                  decoration: BoxDecoration(color: Colors.yellow),
                ),
              ),
              Divider(),
              ListTile(
                title: listText("Abmelden"),
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => Login()),
                  );
                },
              ),
              Divider(),
              ListTile(
                title: listText("Ping"),
                onTap: () async {
                  Grpc.set(Initialize.ip, Initialize.port);
                  String ping = await SystemService.ping();
                  DialogSelection.informationDialog("Ping", ping, context);
                },
              ),
              Divider(),
            ],
          ),
        ));
  }

  Text listText(String text) {
    return Text(
      text,
      style: TextStyle(fontSize: 18, fontWeight: FontWeight.w400),
    );
  }
}

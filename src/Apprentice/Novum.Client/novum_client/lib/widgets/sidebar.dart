import 'package:flutter/material.dart';
import 'package:novum_client/popups/dialogs.dart';
import 'package:novum_client/screens/login.dart';
import 'package:novum_client/main.dart';
import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/systemService.dart';
import 'package:novum_client/utils/utils.dart';
import 'package:novum_client/widgets/deviceinformationlist.dart';

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
              informationListTile(context),
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

  Container informationListTile(BuildContext context) {
    if (!Utils.isWindows) {
      return Container(
        child: ListTile(
          title: listText("Informationen"),
          onTap: () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => DeviceInfo()),
            );
          },
        ),
        decoration: BoxDecoration(
            border: Border(bottom: BorderSide(color: Colors.grey[200]))),
      );
    } else {
      
    }
  }

  Text listText(String text) {
    return Text(
      text,
      style: TextStyle(fontSize: 18, fontWeight: FontWeight.w400),
    );
  }
}

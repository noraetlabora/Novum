import 'dart:async';

import 'package:flutter/widgets.dart';
import 'package:novum_client/services/protobuf/novum.pb.dart';
import 'package:novum_client/services/runtimeDataService.dart';
import 'package:novum_client/widgets/bottombutton.dart';
import 'package:novum_client/widgets/tablebutton.dart';

class Table extends StatefulWidget {
  @override
  TableState createState() => TableState();
}

class TableState extends State<Table> {
  static List<TableButton> tables = <TableButton>[];
  bool killswitch = false;

  @override
  Widget build(BuildContext context) {
    if (!killswitch) {
      getTables();
      killswitch = true;
    }
    Timer.periodic(Duration(seconds: 20), (timer) async {
      getTables();
    });

    return GridView.builder(
        itemCount: tables.length,
        gridDelegate:
            new SliverGridDelegateWithFixedCrossAxisCount(crossAxisCount: 4),
        itemBuilder: (BuildContext context, int _index) {
          return new GestureDetector(
            child: new Container(
              alignment: Alignment.center,
              child: tables[_index],
            ),
          );
        });
  }

  Future<void> getTables() async {
    Tables tableList = await RuntimeDataService.GetTables();
    var list = tableList.tables;
    List<TableButton> tableButtonList = <TableButton>[];
    for (int i = 0; i < list.length; i++) {
      tableButtonList.add(TableButton(
        height: BottomButton.heigth,
        price: list[i].amount,
        name: list[i].name,
      ));
    }
    setState(() {
      tables = tableButtonList;
    });
  }

  static void add(TableButton bt) {
    tables.add(bt);
  }
}

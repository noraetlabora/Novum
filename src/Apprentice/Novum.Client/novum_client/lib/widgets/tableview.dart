import 'dart:async';

import 'package:flutter/widgets.dart';
import 'package:novum_client/services/protobuf/novum.pb.dart';
import 'package:novum_client/services/runtimeDataService.dart';

import 'buttons/bottombutton.dart';
import 'buttons/tablebutton.dart';

class Table extends StatefulWidget {
  @override
  TableState createState() => TableState();
}

class TableState extends State<Table> {
  static List<TableButton> tables = <TableButton>[];
  bool killswitch = false;

  @override
  Widget build(BuildContext context) {
    print("table view build");
    print(killswitch);
    if (!killswitch && mounted) {
        getTables();
        killswitch = true;
    }
    if (mounted) {
      Timer(Duration(seconds: 20), () async {
        getTables();
      });
    }

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
        amount: list[i].amount,
        name: list[i].name,
        state: list[i].state,
        guests: list[i].guests,
        id: list[i].id,
        waiterId: list[i].waiterId,
      ));
    }
    if (mounted) {
      setState(() {
        tables = tableButtonList;
      });
    }
  }

  static void add(TableButton bt) {
    tables.add(bt);
  }
}

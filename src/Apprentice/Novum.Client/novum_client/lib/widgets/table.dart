import 'dart:async';

import 'package:flutter/widgets.dart';
import 'package:novum_client/widgets/tablebutton.dart';

class Table extends StatelessWidget {
  static List<TableButton> tables = <TableButton>[];
  Table({@required this.height});
  final double height;

  @override
  Widget build(BuildContext context) {
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

  static void add(TableButton bt) {
    tables.add(bt);
  }
}

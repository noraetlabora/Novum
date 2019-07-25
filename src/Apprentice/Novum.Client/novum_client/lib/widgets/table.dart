import 'package:flutter/widgets.dart';
import 'package:novum_client/widgets/tablebutton.dart';

class Table extends StatelessWidget {
  Table({@required this.amount, @required this.id, @required this.height});
  final int amount;
  final String id;
  final double height;

  Widget build(BuildContext context) {

    return GridView.builder(
        itemCount: amount,
        gridDelegate:
            new SliverGridDelegateWithFixedCrossAxisCount(crossAxisCount: 4),
        itemBuilder: (BuildContext context, int _index) {
          return new GestureDetector(
            child: new Container(
              alignment: Alignment.center,
              child: TableButton(
                height: height,
                id: id,
                price: 3,
              ),
            ),
          );
        });
  }
}

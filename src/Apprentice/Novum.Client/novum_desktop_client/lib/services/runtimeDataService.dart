import 'package:example_flutter/services/protobuf/google/protobuf/empty.pb.dart';
import 'package:example_flutter/services/protobuf/novum.pb.dart';

import 'grpc.dart';

class RuntimeDataService {
  static Future<Tables> GetTables() async {
    try {
      print(DateTime.now().toIso8601String() + ": start getTables()");
      var tables = await Grpc.runtimeDataClient.getTables(new Empty());
      print(DateTime.now().toIso8601String() + ": getTables() done");
      return tables;
    } on Exception {
      print("XX");
    } catch (e) {
      print(e);
      //throw e;
    }
  }
}

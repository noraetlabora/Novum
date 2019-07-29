import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/protobuf/google/protobuf/empty.pb.dart';
import 'package:novum_client/services/protobuf/novum.pb.dart';

class RuntimeDataService {

  static Future<Tables> GetTables() async {
    try {
      var tables = await Grpc.runtimeDataClient.getTables(new Empty());
      return tables;
    }
    on Exception {
      print("XX");
    }
    catch(e) { 
      print(e); 
      //throw e;
   } 
  }
}

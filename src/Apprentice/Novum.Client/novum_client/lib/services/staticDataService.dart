import 'package:novum_client/services/grpc.dart';
import 'package:novum_client/services/protobuf/google/protobuf/empty.pb.dart';
import 'package:novum_client/services/protobuf/novum.pbgrpc.dart';

class StaticDataService{
  static Future<Theme> theme() async {
    try{
      var theme = await Grpc.staticDataClient.getTheme(Empty.create());
      return theme;
    }catch(ex){
      print(ex);
    }

  }
}
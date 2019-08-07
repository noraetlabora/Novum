///
//  Generated code. Do not modify.
//  source: Desktop/novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

// ignore_for_file: UNDEFINED_SHOWN_NAME,UNUSED_SHOWN_NAME
import 'dart:core' as $core show int, dynamic, String, List, Map;
import 'package:protobuf/protobuf.dart' as $pb;

class ClientType extends $pb.ProtobufEnum {
  static const ClientType ORDERMAN = ClientType._(0, 'ORDERMAN');
  static const ClientType WINDOWS = ClientType._(1, 'WINDOWS');
  static const ClientType WEB = ClientType._(2, 'WEB');

  static const $core.List<ClientType> values = <ClientType> [
    ORDERMAN,
    WINDOWS,
    WEB,
  ];

  static final $core.Map<$core.int, ClientType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static ClientType valueOf($core.int value) => _byValue[value];

  const ClientType._($core.int v, $core.String n) : super(v, n);
}

class LoginInputType extends $pb.ProtobufEnum {
  static const LoginInputType MANUALLY = LoginInputType._(0, 'MANUALLY');
  static const LoginInputType KEY = LoginInputType._(1, 'KEY');

  static const $core.List<LoginInputType> values = <LoginInputType> [
    MANUALLY,
    KEY,
  ];

  static final $core.Map<$core.int, LoginInputType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static LoginInputType valueOf($core.int value) => _byValue[value];

  const LoginInputType._($core.int v, $core.String n) : super(v, n);
}

class TableState extends $pb.ProtobufEnum {
  static const TableState ORDERED = TableState._(0, 'ORDERED');
  static const TableState WAITING = TableState._(1, 'WAITING');
  static const TableState IMPATIENT = TableState._(2, 'IMPATIENT');

  static const $core.List<TableState> values = <TableState> [
    ORDERED,
    WAITING,
    IMPATIENT,
  ];

  static final $core.Map<$core.int, TableState> _byValue = $pb.ProtobufEnum.initByValue(values);
  static TableState valueOf($core.int value) => _byValue[value];

  const TableState._($core.int v, $core.String n) : super(v, n);
}


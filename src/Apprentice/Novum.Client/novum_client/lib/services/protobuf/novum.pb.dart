///
//  Generated code. Do not modify.
//  source: novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:core' as $core show bool, Deprecated, double, int, List, Map, override, pragma, String;

import 'package:protobuf/protobuf.dart' as $pb;

import 'novum.pbenum.dart';

export 'novum.pbenum.dart';

class InitializeRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('InitializeRequest', package: const $pb.PackageName('Novum.Server'))
    ..aOS(1, 'id')
    ..e<ClientType>(2, 'clientType', $pb.PbFieldType.OE, ClientType.ORDERMAN, ClientType.valueOf, ClientType.values)
    ..aOS(3, 'clientVersion')
    ..a<$core.int>(4, 'test', $pb.PbFieldType.OU3)
    ..hasRequiredFields = false
  ;

  InitializeRequest._() : super();
  factory InitializeRequest() => create();
  factory InitializeRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory InitializeRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  InitializeRequest clone() => InitializeRequest()..mergeFromMessage(this);
  InitializeRequest copyWith(void Function(InitializeRequest) updates) => super.copyWith((message) => updates(message as InitializeRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static InitializeRequest create() => InitializeRequest._();
  InitializeRequest createEmptyInstance() => create();
  static $pb.PbList<InitializeRequest> createRepeated() => $pb.PbList<InitializeRequest>();
  static InitializeRequest getDefault() => _defaultInstance ??= create()..freeze();
  static InitializeRequest _defaultInstance;

  $core.String get id => $_getS(0, '');
  set id($core.String v) { $_setString(0, v); }
  $core.bool hasId() => $_has(0);
  void clearId() => clearField(1);

  ClientType get clientType => $_getN(1);
  set clientType(ClientType v) { setField(2, v); }
  $core.bool hasClientType() => $_has(1);
  void clearClientType() => clearField(2);

  $core.String get clientVersion => $_getS(2, '');
  set clientVersion($core.String v) { $_setString(2, v); }
  $core.bool hasClientVersion() => $_has(2);
  void clearClientVersion() => clearField(3);

  $core.int get test => $_get(3, 0);
  set test($core.int v) { $_setUnsignedInt32(3, v); }
  $core.bool hasTest() => $_has(3);
  void clearTest() => clearField(4);
}

class InitializeReply extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('InitializeReply', package: const $pb.PackageName('Novum.Server'))
    ..a<$core.int>(1, 'unixTimestamp', $pb.PbFieldType.OU3)
    ..hasRequiredFields = false
  ;

  InitializeReply._() : super();
  factory InitializeReply() => create();
  factory InitializeReply.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory InitializeReply.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  InitializeReply clone() => InitializeReply()..mergeFromMessage(this);
  InitializeReply copyWith(void Function(InitializeReply) updates) => super.copyWith((message) => updates(message as InitializeReply));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static InitializeReply create() => InitializeReply._();
  InitializeReply createEmptyInstance() => create();
  static $pb.PbList<InitializeReply> createRepeated() => $pb.PbList<InitializeReply>();
  static InitializeReply getDefault() => _defaultInstance ??= create()..freeze();
  static InitializeReply _defaultInstance;

  $core.int get unixTimestamp => $_get(0, 0);
  set unixTimestamp($core.int v) { $_setUnsignedInt32(0, v); }
  $core.bool hasUnixTimestamp() => $_has(0);
  void clearUnixTimestamp() => clearField(1);
}

class LoginRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('LoginRequest', package: const $pb.PackageName('Novum.Server'))
    ..aOS(1, 'input')
    ..e<LoginInputType>(2, 'inputType', $pb.PbFieldType.OE, LoginInputType.MANUALLY, LoginInputType.valueOf, LoginInputType.values)
    ..hasRequiredFields = false
  ;

  LoginRequest._() : super();
  factory LoginRequest() => create();
  factory LoginRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LoginRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  LoginRequest clone() => LoginRequest()..mergeFromMessage(this);
  LoginRequest copyWith(void Function(LoginRequest) updates) => super.copyWith((message) => updates(message as LoginRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LoginRequest create() => LoginRequest._();
  LoginRequest createEmptyInstance() => create();
  static $pb.PbList<LoginRequest> createRepeated() => $pb.PbList<LoginRequest>();
  static LoginRequest getDefault() => _defaultInstance ??= create()..freeze();
  static LoginRequest _defaultInstance;

  $core.String get input => $_getS(0, '');
  set input($core.String v) { $_setString(0, v); }
  $core.bool hasInput() => $_has(0);
  void clearInput() => clearField(1);

  LoginInputType get inputType => $_getN(1);
  set inputType(LoginInputType v) { setField(2, v); }
  $core.bool hasInputType() => $_has(1);
  void clearInputType() => clearField(2);
}

class LoginReply extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('LoginReply', package: const $pb.PackageName('Novum.Server'))
    ..hasRequiredFields = false
  ;

  LoginReply._() : super();
  factory LoginReply() => create();
  factory LoginReply.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LoginReply.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  LoginReply clone() => LoginReply()..mergeFromMessage(this);
  LoginReply copyWith(void Function(LoginReply) updates) => super.copyWith((message) => updates(message as LoginReply));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LoginReply create() => LoginReply._();
  LoginReply createEmptyInstance() => create();
  static $pb.PbList<LoginReply> createRepeated() => $pb.PbList<LoginReply>();
  static LoginReply getDefault() => _defaultInstance ??= create()..freeze();
  static LoginReply _defaultInstance;
}

class GetTablesRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('GetTablesRequest', package: const $pb.PackageName('Novum.Server'))
    ..hasRequiredFields = false
  ;

  GetTablesRequest._() : super();
  factory GetTablesRequest() => create();
  factory GetTablesRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory GetTablesRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  GetTablesRequest clone() => GetTablesRequest()..mergeFromMessage(this);
  GetTablesRequest copyWith(void Function(GetTablesRequest) updates) => super.copyWith((message) => updates(message as GetTablesRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static GetTablesRequest create() => GetTablesRequest._();
  GetTablesRequest createEmptyInstance() => create();
  static $pb.PbList<GetTablesRequest> createRepeated() => $pb.PbList<GetTablesRequest>();
  static GetTablesRequest getDefault() => _defaultInstance ??= create()..freeze();
  static GetTablesRequest _defaultInstance;
}

class GetTablesReply extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('GetTablesReply', package: const $pb.PackageName('Novum.Server'))
    ..pc<Table>(1, 'tables', $pb.PbFieldType.PM,Table.create)
    ..hasRequiredFields = false
  ;

  GetTablesReply._() : super();
  factory GetTablesReply() => create();
  factory GetTablesReply.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory GetTablesReply.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  GetTablesReply clone() => GetTablesReply()..mergeFromMessage(this);
  GetTablesReply copyWith(void Function(GetTablesReply) updates) => super.copyWith((message) => updates(message as GetTablesReply));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static GetTablesReply create() => GetTablesReply._();
  GetTablesReply createEmptyInstance() => create();
  static $pb.PbList<GetTablesReply> createRepeated() => $pb.PbList<GetTablesReply>();
  static GetTablesReply getDefault() => _defaultInstance ??= create()..freeze();
  static GetTablesReply _defaultInstance;

  $core.List<Table> get tables => $_getList(0);
}

class Tables extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Tables', package: const $pb.PackageName('Novum.Server'))
    ..pc<Table>(1, 'tables', $pb.PbFieldType.PM,Table.create)
    ..hasRequiredFields = false
  ;

  Tables._() : super();
  factory Tables() => create();
  factory Tables.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Tables.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Tables clone() => Tables()..mergeFromMessage(this);
  Tables copyWith(void Function(Tables) updates) => super.copyWith((message) => updates(message as Tables));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Tables create() => Tables._();
  Tables createEmptyInstance() => create();
  static $pb.PbList<Tables> createRepeated() => $pb.PbList<Tables>();
  static Tables getDefault() => _defaultInstance ??= create()..freeze();
  static Tables _defaultInstance;

  $core.List<Table> get tables => $_getList(0);
}

class Table extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Table', package: const $pb.PackageName('Novum.Server'))
    ..aOS(1, 'id')
    ..aOS(2, 'name')
    ..a<$core.double>(3, 'amount', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  Table._() : super();
  factory Table() => create();
  factory Table.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Table.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Table clone() => Table()..mergeFromMessage(this);
  Table copyWith(void Function(Table) updates) => super.copyWith((message) => updates(message as Table));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Table create() => Table._();
  Table createEmptyInstance() => create();
  static $pb.PbList<Table> createRepeated() => $pb.PbList<Table>();
  static Table getDefault() => _defaultInstance ??= create()..freeze();
  static Table _defaultInstance;

  $core.String get id => $_getS(0, '');
  set id($core.String v) { $_setString(0, v); }
  $core.bool hasId() => $_has(0);
  void clearId() => clearField(1);

  $core.String get name => $_getS(1, '');
  set name($core.String v) { $_setString(1, v); }
  $core.bool hasName() => $_has(1);
  void clearName() => clearField(2);

  $core.double get amount => $_getN(2);
  set amount($core.double v) { $_setDouble(2, v); }
  $core.bool hasAmount() => $_has(2);
  void clearAmount() => clearField(3);
}

class CancellationReasons extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('CancellationReasons', package: const $pb.PackageName('Novum.Server'))
    ..pc<CancellationReason>(1, 'cancellationReasons', $pb.PbFieldType.PM,CancellationReason.create)
    ..hasRequiredFields = false
  ;

  CancellationReasons._() : super();
  factory CancellationReasons() => create();
  factory CancellationReasons.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CancellationReasons.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  CancellationReasons clone() => CancellationReasons()..mergeFromMessage(this);
  CancellationReasons copyWith(void Function(CancellationReasons) updates) => super.copyWith((message) => updates(message as CancellationReasons));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CancellationReasons create() => CancellationReasons._();
  CancellationReasons createEmptyInstance() => create();
  static $pb.PbList<CancellationReasons> createRepeated() => $pb.PbList<CancellationReasons>();
  static CancellationReasons getDefault() => _defaultInstance ??= create()..freeze();
  static CancellationReasons _defaultInstance;

  $core.List<CancellationReason> get cancellationReasons => $_getList(0);
}

class CancellationReason extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('CancellationReason', package: const $pb.PackageName('Novum.Server'))
    ..aOS(1, 'id')
    ..aOS(2, 'name')
    ..hasRequiredFields = false
  ;

  CancellationReason._() : super();
  factory CancellationReason() => create();
  factory CancellationReason.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CancellationReason.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  CancellationReason clone() => CancellationReason()..mergeFromMessage(this);
  CancellationReason copyWith(void Function(CancellationReason) updates) => super.copyWith((message) => updates(message as CancellationReason));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CancellationReason create() => CancellationReason._();
  CancellationReason createEmptyInstance() => create();
  static $pb.PbList<CancellationReason> createRepeated() => $pb.PbList<CancellationReason>();
  static CancellationReason getDefault() => _defaultInstance ??= create()..freeze();
  static CancellationReason _defaultInstance;

  $core.String get id => $_getS(0, '');
  set id($core.String v) { $_setString(0, v); }
  $core.bool hasId() => $_has(0);
  void clearId() => clearField(1);

  $core.String get name => $_getS(1, '');
  set name($core.String v) { $_setString(1, v); }
  $core.bool hasName() => $_has(1);
  void clearName() => clearField(2);
}


///
//  Generated code. Do not modify.
//  source: novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:async' as $async;
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

class LogoutRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('LogoutRequest', package: const $pb.PackageName('Novum.Server'))
    ..hasRequiredFields = false
  ;

  LogoutRequest._() : super();
  factory LogoutRequest() => create();
  factory LogoutRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LogoutRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  LogoutRequest clone() => LogoutRequest()..mergeFromMessage(this);
  LogoutRequest copyWith(void Function(LogoutRequest) updates) => super.copyWith((message) => updates(message as LogoutRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LogoutRequest create() => LogoutRequest._();
  LogoutRequest createEmptyInstance() => create();
  static $pb.PbList<LogoutRequest> createRepeated() => $pb.PbList<LogoutRequest>();
  static LogoutRequest getDefault() => _defaultInstance ??= create()..freeze();
  static LogoutRequest _defaultInstance;
}

class LogoutReply extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('LogoutReply', package: const $pb.PackageName('Novum.Server'))
    ..hasRequiredFields = false
  ;

  LogoutReply._() : super();
  factory LogoutReply() => create();
  factory LogoutReply.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LogoutReply.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  LogoutReply clone() => LogoutReply()..mergeFromMessage(this);
  LogoutReply copyWith(void Function(LogoutReply) updates) => super.copyWith((message) => updates(message as LogoutReply));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LogoutReply create() => LogoutReply._();
  LogoutReply createEmptyInstance() => create();
  static $pb.PbList<LogoutReply> createRepeated() => $pb.PbList<LogoutReply>();
  static LogoutReply getDefault() => _defaultInstance ??= create()..freeze();
  static LogoutReply _defaultInstance;
}

class AuthenticationApi {
  $pb.RpcClient _client;
  AuthenticationApi(this._client);

  $async.Future<InitializeReply> initialize($pb.ClientContext ctx, InitializeRequest request) {
    var emptyResponse = InitializeReply();
    return _client.invoke<InitializeReply>(ctx, 'Authentication', 'Initialize', request, emptyResponse);
  }
  $async.Future<LoginReply> login($pb.ClientContext ctx, LoginRequest request) {
    var emptyResponse = LoginReply();
    return _client.invoke<LoginReply>(ctx, 'Authentication', 'Login', request, emptyResponse);
  }
  $async.Future<LogoutReply> logou($pb.ClientContext ctx, LogoutRequest request) {
    var emptyResponse = LogoutReply();
    return _client.invoke<LogoutReply>(ctx, 'Authentication', 'Logou', request, emptyResponse);
  }
}


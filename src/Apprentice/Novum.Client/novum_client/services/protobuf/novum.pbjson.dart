///
//  Generated code. Do not modify.
//  source: novum.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

const ClientType$json = const {
  '1': 'ClientType',
  '2': const [
    const {'1': 'ORDERMAN', '2': 0},
    const {'1': 'WINDOWS', '2': 1},
    const {'1': 'WEB', '2': 2},
  ],
};

const LoginInputType$json = const {
  '1': 'LoginInputType',
  '2': const [
    const {'1': 'MANUALLY', '2': 0},
    const {'1': 'KEY', '2': 1},
  ],
};

const InitializeRequest$json = const {
  '1': 'InitializeRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'clientType', '3': 2, '4': 1, '5': 14, '6': '.Novum.Server.ClientType', '10': 'clientType'},
    const {'1': 'clientVersion', '3': 3, '4': 1, '5': 9, '10': 'clientVersion'},
    const {'1': 'test', '3': 4, '4': 1, '5': 13, '10': 'test'},
  ],
};

const InitializeReply$json = const {
  '1': 'InitializeReply',
  '2': const [
    const {'1': 'unixTimestamp', '3': 1, '4': 1, '5': 13, '10': 'unixTimestamp'},
  ],
};

const LoginRequest$json = const {
  '1': 'LoginRequest',
  '2': const [
    const {'1': 'input', '3': 1, '4': 1, '5': 9, '10': 'input'},
    const {'1': 'inputType', '3': 2, '4': 1, '5': 14, '6': '.Novum.Server.LoginInputType', '10': 'inputType'},
  ],
};

const LoginReply$json = const {
  '1': 'LoginReply',
};

const LogoutRequest$json = const {
  '1': 'LogoutRequest',
};

const LogoutReply$json = const {
  '1': 'LogoutReply',
};

const AuthenticationServiceBase$json = const {
  '1': 'Authentication',
  '2': const [
    const {'1': 'Initialize', '2': '.Novum.Server.InitializeRequest', '3': '.Novum.Server.InitializeReply'},
    const {'1': 'Login', '2': '.Novum.Server.LoginRequest', '3': '.Novum.Server.LoginReply'},
    const {'1': 'Logou', '2': '.Novum.Server.LogoutRequest', '3': '.Novum.Server.LogoutReply'},
  ],
};

const AuthenticationServiceBase$messageJson = const {
  '.Novum.Server.InitializeRequest': InitializeRequest$json,
  '.Novum.Server.InitializeReply': InitializeReply$json,
  '.Novum.Server.LoginRequest': LoginRequest$json,
  '.Novum.Server.LoginReply': LoginReply$json,
  '.Novum.Server.LogoutRequest': LogoutRequest$json,
  '.Novum.Server.LogoutReply': LogoutReply$json,
};


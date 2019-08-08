@echo off
protoc --dart_out=grpc:./  --proto_path=./ .\novum.proto
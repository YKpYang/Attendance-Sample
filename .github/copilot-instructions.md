# Copilot Global Instructions for Attendance Sample

このリポジトリは C# (.NET 8) を使用した勤怠管理サンプルである。  
Copilot は以下のルールに従ってコード生成・レビューを行うこと。

## 技術方針
- C# は .NET 8 を標準とする
- 非同期処理は async/await を優先する
- DB は実際には使用せず、インメモリ（List<T>）を疑似DBとする
- 勤怠モデル（Employee, Company, Attendance）を前提にコード生成する
- SOLID 原則を守る
- ログは Console.WriteLine を使用（簡易サンプルのため）

## コード生成ルール
- DTO は record を使用してもよい
- null チェックは guard clause を使う
- 勤怠更新ロジックは既存データの有無を判定して新規作成または更新する

## 禁止事項
- 実DB（SQL Server, PostgreSQL 等）を前提としたコード生成は禁止
- ORM（EF Core 等）の使用は禁止

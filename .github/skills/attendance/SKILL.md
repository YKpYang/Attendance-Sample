# Attendance Skill

## 目的
勤怠管理ロジック（新規作成・更新・一覧表示）を自動生成・改善する。

## 入力
- Employee, Company, Attendance のモデル構造
- 勤怠更新要求（Year, Month）

## 出力
- 勤怠更新ロジックのコード
- 勤怠一覧表示ロジック
- 勤怠データの整合性チェック

## 制約
- インメモリDB（List<T>）を使用する
- 実DBは使用しない
- LINQ を使用する

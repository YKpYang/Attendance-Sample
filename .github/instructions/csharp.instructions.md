---
applyTo: ["src/**/*.cs", "Program.cs"]
priority: 10
---

# C# Attendance Sample Instructions

このディレクトリの C# コードに対して、Copilot は以下を遵守する。

## モデルルール
- Employee, Company, Attendance の 3 モデルを前提とする
- 勤怠更新ロジックは AttendanceService に実装する

## コーディングルール
- interface は I 接頭辞を付ける
- 非同期メソッドは Task を返す
- guard clause を使って null チェックを行う
- LINQ を積極的に使用する

## 勤怠ロジック
- Attendance は (EmployeeId, Year, Month) の組で一意
- 新規作成時は WorkDays=20, PaidHolidays=1 を初期値とする
- 更新時は WorkDays を +1 する（簡易ロジック）

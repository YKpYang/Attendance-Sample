---
name: "generate-attendance"
description: "勤怠更新ロジックのコード生成"
agent: "attendance-expert"
model: "gpt-4o"
---

以下の仕様に基づいて、C# (.NET 8) の勤怠更新ロジックを生成せよ。

- Employee, Company, Attendance モデルを使用する
- インメモリDB（List<T>）を使う
- (EmployeeId, Year, Month) で一意
- 新規作成時は WorkDays=20, PaidHolidays=1
- 更新時は WorkDays を +1
- LINQ を使用する
- null チェックは guard clause

---
name: "service-mode"
description: "Attendance-Sample の Services 層を具体的に生成・修正する"
agent: "attendance-expert"
model: "gpt-4o"
---

以下は [Attendance-Sample](/d:/個人/生成AI/DP_Azure%20DevOps/SAMPLE/Attendance-Sample) の `src/Services` に対する具体的な生成指示である。
Service 層のコードを生成・修正する場合は、この仕様を優先して従うこと。

## 対象範囲

- `src/Services/IAttendanceService.cs`
- `src/Services/AttendanceService.cs`

## 前提

- モデルは `Attendance_Sample.Models` を使う
- `Attendance`, `Employee`, `Company` は既存定義に合わせる
- DB は使わず、インメモリの `List<T>` のみで管理する
- ORM（EF Core など）は使用しない

## IAttendanceService の仕様

- namespace は `Attendance_Sample.Services`
- 以下の 3 メソッドを持つ
  - `Task UpdateAsync(int employeeId, int year, int month)`
  - `Task<List<Attendance>> GetAttendancesAsync()`
  - `Task<Attendance?> GetAttendanceAsync(int employeeId, int year, int month)`
- 追加メソッドを作る場合は、既存の責務を壊さないこと

## AttendanceService の仕様

- namespace は `Attendance_Sample.Services`
- `IAttendanceService` を実装する
- `private readonly List<Attendance> _attendances = new();` を保持する
- 保存対象は `_attendances` のみとする

## 更新ロジック

### 検索条件
- `EmployeeId`, `Year`, `Month` の組み合わせで 1 件を判定する
- 検索には LINQ の `FirstOrDefault` を使う

### 新規作成時
- 該当データがない場合は新規 `Attendance` を作成する
- 初期値は以下
  - `WorkDays = 20`
  - `PaidHolidays = 1`
- 作成後は `_attendances.Add(attendance)` する

### 更新時
- 該当データがある場合は `WorkDays += 1` する
- `PaidHolidays` は変更しない

### 出力
- 更新結果は `Console.WriteLine` で出力する
- 出力内容は `EmployeeId`, `Year`, `Month`, `WorkDays`, `PaidHolidays` を含める

## 取得ロジック

### GetAttendancesAsync
- 全件を返す前に以下の順で整列する
  - `EmployeeId`
  - `Year`
  - `Month`
- LINQ の `OrderBy` / `ThenBy` を使用する
- 戻り値は `Task.FromResult(...)` で返してよい

### GetAttendanceAsync
- 指定された 1 件を `FirstOrDefault` で返す
- 見つからない場合は `null`

## バリデーション

- `employeeId <= 0` は不正値として例外を投げる
- `year < 2000 || year > 2100` は不正値として例外を投げる
- `month < 1 || month > 12` は不正値として例外を投げる
- guard clause で先に弾く

## 実装方針

- 非同期 API は `Task` を返す
- `async/await` を使う場合も、無駄に state machine を増やさない
- LINQ を積極的に使う
- 既存の `Program.cs` と整合すること
- 既存の Model の責務は変えない

## 禁止事項

- 実 DB を前提にしない
- repository / unit of work を持ち込まない
- 仕様にないプロパティやメソッドを安易に追加しない
- 既存のモデル構造を勝手に変更しない
